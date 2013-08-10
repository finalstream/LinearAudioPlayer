using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Core;
using FINALSTREAM.LinearAudioPlayer.Exceptions;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;
using Un4seen.BassWasapi;

namespace FINALSTREAM.LinearAudioPlayer.Engine
{
    public class BASSEngine : IPlayEngine
    {
        /*
         * 静的パブリックメソッド
         */
        #region Static Public Method

        private static int[] sound = new int[2];
        private Dictionary<int, String> _loadPlugins = null;
        private TAG_INFO tagInfo;
        private bool enablewasapi = false;
        private DSP_Gain _gain;
        private string _filePath;

        /// <summary>
        /// BASSを初期化する
        /// </summary>
        public void init()
        {

            // Bass.Net のスプラッシュ スクリーンを抑止
            BassNet.Registration("info@finalstream.net", "2X29142420162918");

            // Bass.Net
            if (!Bass.LoadMe(Application.StartupPath + "\\lib\\bass"))
            {
                throw new LinearAudioPlayerException("Bass.Net の初期化に失敗しました。");
            }

            // デバイス初期化
            int i = 0;
            for (i = 0; i < 10; i++)
            {
                if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                {
                    break;
                }
            }
            if (i == 10)
            {
                var error = Bass.BASS_ErrorGetCode();
                throw new LinearAudioPlayerException("デバイスの初期化に失敗しました。\nError : " + error.ToString());
            }

            // プラグイン
            _loadPlugins = Bass.BASS_PluginLoadDirectory(Application.StartupPath + LinearConst.BASS_PLUGIN_DIRECTORY_NAME);

            if (BassWasapi.LoadMe(Application.StartupPath + LinearConst.BASS_PLUGIN_DIRECTORY_NAME))
            {
                BassWasapi.BASS_WASAPI_Init(-1, 0, 0, BASSWASAPIInit.BASS_WASAPI_SHARED, 0.5F, 0, null,
                                            IntPtr.Zero);
                if (BassWasapi.BASS_WASAPI_Start())
                {
                    enablewasapi = true;
                }
                else
                {
                    enablewasapi = false;
                }
            }
            else
            {
                enablewasapi = false;
            }

            
        }

        /// <summary>
        /// BASSを破棄する
        /// </summary>
        public void Dispose()
        {

            /*
                Shut down
            */
            if (enablewasapi)
            {
                BassWasapi.BASS_WASAPI_Stop(true);
            }
            Bass.BASS_Stop();
            Bass.BASS_Free();
            Bass.BASS_PluginFree(0);
            Bass.FreeMe();
        }


        #endregion

        /*
            パブリックメソッド 
        */
        #region PublicMethod

        /// <summary>
        /// ファイルをオープンする
        /// </summary>
        /// <param name="filePath">再生するファイル</param>
        public void open(string filePath)
        {

            // ストリーム生成
            if (StringUtils.isURL(filePath))
            {
                openStream(filePath);
            }
            else
            {
                sound[PlayerController.MainChannel] = Bass.BASS_StreamCreateFile(filePath, 0, 0, BASSFlag.BASS_DEFAULT);
                _filePath = filePath;

                if (sound[PlayerController.MainChannel] != 0)
                {
                    {
                        long length = Bass.BASS_ChannelGetLength(sound[PlayerController.MainChannel]);
                        double seconds = Bass.BASS_ChannelBytes2Seconds(sound[PlayerController.MainChannel], length);

                        // normalize
                        _gain = new DSP_Gain(sound[PlayerController.MainChannel], 0);
                        
                    }
                }
            }
            
        }

        private void openStream(string url)
        {
            sound[PlayerController.MainChannel]  = Bass.BASS_StreamCreateURL(url, 0, BASSFlag.BASS_STREAM_STATUS, null, IntPtr.Zero);

            tagInfo = new TAG_INFO(url);
            BASS_CHANNELINFO info = Bass.BASS_ChannelGetInfo(sound[PlayerController.MainChannel]);

            // display buffering for MP3, OGG...
            while (true)
            {
                long len = Bass.BASS_StreamGetFilePosition(sound[PlayerController.MainChannel], BASSStreamFilePosition.BASS_FILEPOS_END);
                if (len == -1)
                    break; // typical for WMA streams
                // percentage of buffer filled
                float progress = (
                    Bass.BASS_StreamGetFilePosition(sound[PlayerController.MainChannel], BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD) -
                    Bass.BASS_StreamGetFilePosition(sound[PlayerController.MainChannel], BASSStreamFilePosition.BASS_FILEPOS_CURRENT)
                    ) * 100f / len;

                if (progress > 75f)
                {
                    break; // over 75% full, enough
                }

                //Application.DoEvents();
                System.Threading.Thread.Sleep(500);
            }


        }

        /// <summary>
        /// 再生する
        /// </summary>
        /// <param name="isPause">一時停止かどうか</param>
        public bool play(bool isPause)
        {

            if (sound[PlayerController.MainChannel] != 0)
            {
                
                Bass.BASS_ChannelPlay(sound[PlayerController.MainChannel], isPause);

                return true;
            }
                
            return false;

        }

        /// <summary>
        /// 一時停止する
        /// </summary>
        public bool pause()
        {

            bool paused = false;

            if (sound[PlayerController.MainChannel] != 0)
            {
                if (Bass.BASS_ChannelIsActive(sound[PlayerController.MainChannel]) == BASSActive.BASS_ACTIVE_PLAYING)
                {
                    Bass.BASS_ChannelPause(sound[PlayerController.MainChannel]);
                    paused = true;
                }
                else
                {
                    Bass.BASS_ChannelPlay(sound[PlayerController.MainChannel], false);
                    paused = false;
                }
                
            }

            return paused;


        }

        /// <summary>
        /// 全チャネル停止する
        /// </summary>
        public void stop()
        {
            stop(PlayerController.MainChannel);
            stop(PlayerController.SubChannel);
            
        }

        /// <summary>
        /// 停止する
        /// </summary>
        public void stop(int channelNo)
        {
            if (sound[channelNo] != 0)
            {
                Bass.BASS_ChannelStop(sound[channelNo]);
                Bass.BASS_ChannelSetPosition(sound[channelNo], 0.0);
                Bass.BASS_StreamFree(sound[channelNo]);
            }
        }

        private string[] bkmeta = null;
        public bool isUpdateMetadata()
        {
            bool result = false;
            string[] meta = Bass.BASS_ChannelGetTagsMETA(sound[PlayerController.MainChannel]);
            
            if ((meta !=null && bkmeta == null)
                || (meta !=null && bkmeta != null && !meta[0].Equals(bkmeta[0])))
            {
                result = true;
            }

            bkmeta = meta;

            return result;
        }

        public float[] getSpectrum()
        {
            float[] spectrum = new float[PlayerController.SPECTRUMSIZE];

            if (sound[PlayerController.MainChannel] != 0)
            {
                Bass.BASS_ChannelGetData(sound[PlayerController.MainChannel], spectrum, (int) BASSData.BASS_DATA_FFT256);
            }
            return spectrum;
        }

        public bool isEnableWasapi()
        {
            return enablewasapi;
        }

        public void applyNormalize(bool isApply)
        {
            if (_gain != null)
            {
                if (isApply)
                {
                    _gain.SetBypass(false);
                    // getgain
                    float gain = 0;
                    Un4seen.Bass.Utils.GetNormalizationGain(_filePath, 30f, -1, -1, ref gain);
                    //Debug.WriteLine("normalize gain:" +  gain);
                    //Debug.WriteLine("music gain " + _gain.Gain);
                    //Debug.WriteLine("music gaindb " + _gain.Gain_dBV);
                    //Debug.WriteLine("diff gaindb " + (Un4seen.Bass.Utils.LevelToDB(gain, 1) * -1));
                    //if (gain < 1.0f)
                    //{
                        _gain.Gain_dBV = (Un4seen.Bass.Utils.LevelToDB(gain, 1)*-1);
                    //}
                }
                else
                {
                    _gain.SetBypass(true);
                }
            }
        }


        /// <summary>
        /// 再生位置を取得する
        /// </summary>
        /// <returns>再生位置(ミリ秒)</returns>
        public uint getPosition()
        {
            {
                long position = Bass.BASS_ChannelGetPosition(sound[PlayerController.MainChannel]);
                double seconds = Bass.BASS_ChannelBytes2Seconds(sound[PlayerController.MainChannel], position);
                return (uint) TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            }

        }

        /// <summary>
        /// 再生位置を変更する
        /// </summary>
        /// <param name="ms"></param>
        public void setPosition(uint ms)
        {
            if (sound[PlayerController.MainChannel] != 0)
            {
                long position = Bass.BASS_ChannelSeconds2Bytes(sound[PlayerController.MainChannel], (double)((double)ms / 1000));
                Bass.BASS_ChannelSetPosition(sound[PlayerController.MainChannel], position);
            }
            
        }


        /// <summary>
        /// ファイルの長さを取得する
        /// </summary>
        /// <returns>ファイルの長さ(ミリ秒)</returns>
        public uint getLength()
        {
            uint ms = 0;

            if (sound[PlayerController.MainChannel] != 0)
            {
                {
                    long length = Bass.BASS_ChannelGetLength(sound[PlayerController.MainChannel]);
                    double seconds = Bass.BASS_ChannelBytes2Seconds(sound[PlayerController.MainChannel], length);
                    return (uint) TimeSpan.FromSeconds(seconds).TotalMilliseconds;
                }
            }

            return ms;

        }

        /// <summary>
        /// 再生中であるか確認する
        /// </summary>
        /// <returns></returns>
        public bool isPlaying(int channelNo)
        {
            bool playing = false;

            if (sound[channelNo] != 0)
            {
                BASSActive status = Bass.BASS_ChannelIsActive(sound[channelNo]);

                if (status == BASSActive.BASS_ACTIVE_PLAYING)
                {
                    playing = true;
                }
            }

            return playing;
        }

        /// <summary>
        /// 一時停止状態かどうか確認する
        /// </summary>
        /// <returns></returns>
        public bool isPasued()
        {

            bool paused = false;

            if (sound[PlayerController.MainChannel] != 0)
            {
                BASSActive status = Bass.BASS_ChannelIsActive(sound[PlayerController.MainChannel]);

                if (status == BASSActive.BASS_ACTIVE_PAUSED)
                {
                    paused = true;
                }
            }

            return paused;

        }

        public int getVersion(){
            
            return Bass.BASS_GetVersion();
        }

        /// <summary>
        /// ボリュームを設定する。
        /// </summary>
        /// <param name="channelNo"></param>
        /// <param name="vol"></param>
        public void setVolume(int channelNo,
            float vol)
        {
            if (sound[channelNo] != 0)
            {
                Bass.BASS_ChannelSetAttribute(
                    sound[channelNo], 
                    BASSAttribute.BASS_ATTRIB_VOL, 
                    vol);
            }
        }

        #endregion


        /*
            プライベートメソッド
        */
        #region PrivateMethod

      
        #endregion

    }
}
