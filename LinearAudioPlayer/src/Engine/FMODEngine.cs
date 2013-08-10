using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FINALSTREAM.Commons.Exceptions;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Core;
using FINALSTREAM.LinearAudioPlayer.Exceptions;
using FMOD;

namespace FINALSTREAM.LinearAudioPlayer.Engine
{
    /// <summary>
    /// FMOD再生エンジンクラス。
    /// </summary>
    public class FMODEngine : IPlayEngine
    {

        private byte[] audiodata;
        private FMOD.DSP dspNormalize = null;
        private FMOD.DSPConnection dspconnectiontemp = null;

        /*
         * 静的プライベートメンバ
         */
        #region Static Private Method

        private static FMOD.System system = null;
        private static FMOD.Sound[] sound = new FMOD.Sound[2];
        private static FMOD.Channel[] channel = new FMOD.Channel[2];
        private static FMOD.ChannelGroup channelGroup = null;
        private static FMOD.SoundGroup soundGroup = null;
        private static string openFilePath;

        #endregion

        
        /*
            プロパティ
        */
        #region Property






        #endregion

        /*
         * 静的パブリックメソッド
         */
        #region Static Public Method

        /// <summary>
        /// FMODを初期化する
        /// </summary>
        public void init()
        {

            FMOD.RESULT result;

            result = FMOD.Factory.System_Create(ref system);
            FMODEngine.checkFMODError(result);

            result = system.init(2, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
            FMODEngine.checkFMODError(result);

            // defaultでoutputtypeはwasapiになる。

            result = system.getMasterChannelGroup(ref channelGroup);
            FMODEngine.checkFMODError(result);

            result = system.getMasterSoundGroup(ref soundGroup);
            FMODEngine.checkFMODError(result);


            result = system.createDSPByType(FMOD.DSP_TYPE.NORMALIZE, ref dspNormalize);
            FMODEngine.checkFMODError(result);

        }


        /// <summary>
        /// FMODを破棄する
        /// </summary>
        public void Dispose()
        {
            FMOD.RESULT result;

            /*
                Shut down
            */
            if (channelGroup != null)
            {
                result = channelGroup.release();
                //FMODEngine.checkFMODError(result);
            }

            if (soundGroup != null)
            {
                result = soundGroup.release();
                //FMODEngine.checkFMODError(result);
            }

            if (system != null)
            {
                result = system.close();
                //FMODEngine.checkFMODError(result);
                result = system.release();
                //FMODEngine.checkFMODError(result);
            }

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
            FMOD.RESULT result;
            
            openFilePath = filePath;

            if (StringUtils.isURL(filePath))
            {
                openStream(filePath);

            } else {

                /* メモリロード方式　ver.0.4.6で廃止
                int length;
                FMOD.CREATESOUNDEXINFO exinfo = new FMOD.CREATESOUNDEXINFO();
                length = LoadFileIntoMemory(filePath);

                exinfo.cbsize = Marshal.SizeOf(exinfo);
                //exinfo.cbsize = 132;
                exinfo.length = (uint)length;
                result = system.createSound(audiodata, (FMOD.MODE.SOFTWARE | MODE.OPENMEMORY | FMOD.MODE.CREATECOMPRESSEDSAMPLE), ref exinfo, ref sound[PlayerController.MainChannel]);
                checkFMODError(result);
                */

                result = system.createStream(filePath, (FMOD.MODE.DEFAULT | FMOD.MODE.CREATESTREAM), ref sound[PlayerController.MainChannel]);
                checkFMODError(result);
            }
              

            if (sound[PlayerController.MainChannel] != null) {
                sound[PlayerController.MainChannel].setSoundGroup(soundGroup);
            }

            // ファイル直接リード
            // result = system.createStream(filePath, (FMOD.MODE.DEFAULT | FMOD.MODE.CREATESTREAM), ref sound);

        }

        private void openStream(string url)
        {
            FMOD.RESULT result;
            result = system.setStreamBufferSize(64 * 1024, FMOD.TIMEUNIT.RAWBYTES);
            checkFMODError(result);

            result = system.createSound(url, (FMOD.MODE.HARDWARE | FMOD.MODE._2D | FMOD.MODE.CREATESTREAM | FMOD.MODE.NONBLOCKING), ref sound[PlayerController.MainChannel]);
            checkFMODError(result);

            FMOD.OPENSTATE openstate = FMOD.OPENSTATE.CONNECTING;
            uint percentbuffered = 0;
            bool starving = false;
            bool diskbusy = false;
            while (openstate != FMOD.OPENSTATE.READY && openstate != FMOD.OPENSTATE.ERROR)
            {
                sound[PlayerController.MainChannel].getOpenState(ref openstate, ref percentbuffered, ref starving, ref diskbusy);
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
            FMOD.RESULT result;

            if (sound[PlayerController.MainChannel] != null)
            {
                result = system.playSound(FMOD.CHANNELINDEX.FREE, sound[PlayerController.MainChannel], isPause, ref channel[PlayerController.MainChannel]);
                checkFMODError(result);
                channel[PlayerController.MainChannel].setChannelGroup(channelGroup);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 一時停止する
        /// </summary>
        public bool pause()
        {
            FMOD.RESULT result;

            bool paused = false;

            if (channel[PlayerController.MainChannel] != null)
            {
                result = channel[PlayerController.MainChannel].getPaused(ref paused);
                checkFMODError(result);
                result = channel[PlayerController.MainChannel].setPaused(!paused);
                checkFMODError(result);
            }

            return !paused;


        }

        /// <summary>
        /// 全チャネル停止する
        /// </summary>
        public void stop()
        {
            if (soundGroup != null)
            {
                if (channelGroup != null)
                {
                    channelGroup.stop();
                    channelGroup.release();
                    //channelGroup = null;
                    channel[PlayerController.MainChannel] = null;
                    channel[PlayerController.SubChannel] = null;
                }
                soundGroup.stop();
                soundGroup.release();
                //soundGroup = null;
            }
        }

        /// <summary>
        /// 停止する
        /// </summary>
        public void stop(int channelNo)
        {
            if (sound[channelNo] != null)
            {
                if (channel[channelNo] != null)
                {
                    channel[channelNo].stop();
                    channel[channelNo] = null;
                }
                sound[channelNo].release();
                sound[channelNo] = null;
            }
        }

        public bool isUpdateMetadata()
        {
            FMOD.TAG tagdata = new FMOD.TAG();
            sound[PlayerController.MainChannel].getTag("TITLE", -1, ref tagdata);

            return tagdata.updated;
        }

        public float[] getSpectrum()
        {
            FMOD.RESULT result;
            float[] spectrum = new float[PlayerController.SPECTRUMSIZE];

            if (channel[PlayerController.MainChannel] != null)
            {
                result = channel[PlayerController.MainChannel].getSpectrum(spectrum, PlayerController.SPECTRUMSIZE, 0, FMOD.DSP_FFT_WINDOW.TRIANGLE);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    checkFMODError(result);
                }
            }


            return spectrum;
        }

        public bool isEnableWasapi()
        {
            bool result = false;
            OUTPUTTYPE outputtype = OUTPUTTYPE.NOSOUND;
            system.getOutput(ref outputtype);

            if (outputtype == OUTPUTTYPE.WASAPI)
            {
                result = true;
            }

            return result;
        }

        public void applyNormalize(bool isApply)
        {
            if (isApply)
            {
                //dspNormalize.setParameter((int) FMOD.DSP_NORMALIZE.FADETIME, 0);
                //dspNormalize.setParameter((int)FMOD.DSP_NORMALIZE.THRESHHOLD, 0.3f);
                if (dspconnectiontemp == null)
                {
                    //float gain = 0;
                    //channel[PlayerController.MainChannel].getLowPassGain(ref gain);
                    //Debug.WriteLine(gain);
                    //Debug.WriteLine("normalize");
                    dspNormalize.setParameter((int) FMOD.DSP_NORMALIZE.FADETIME, 0);
                    dspNormalize.setParameter((int)FMOD.DSP_NORMALIZE.MAXAMP, 1.25f);
                    //dspNormalize.setParameter((int)FMOD.DSP_NORMALIZE.THRESHHOLD, 0.1f);
                    RESULT result = system.addDSP(dspNormalize, ref dspconnectiontemp);
                    checkFMODError(result);
                }
                //RESULT result = system.addDSP(dspNormalize, ref dspconnectiontemp);
                //checkFMODError(result);
            }
            else
            {
                RESULT result = dspNormalize.remove();
                dspconnectiontemp = null;
                checkFMODError(result);
            }
        }


        /// <summary>
        /// 再生位置を取得する
        /// </summary>
        /// <returns>再生位置(ミリ秒)</returns>
        public uint getPosition()
        {
            FMOD.RESULT result;
            uint ms = 0;

            if (channel[PlayerController.MainChannel] != null)
            {
                result = channel[PlayerController.MainChannel].getPosition(ref ms, FMOD.TIMEUNIT.MS);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    checkFMODError(result);
                }
            }
            

            return ms;

        }

        /// <summary>
        /// 再生位置を変更する
        /// </summary>
        /// <param name="ms"></param>
        public void setPosition(uint ms)
        {
            FMOD.RESULT result;

            if (channel[PlayerController.MainChannel] != null)
            {
                result = channel[PlayerController.MainChannel].setPosition(ms, FMOD.TIMEUNIT.MS);
                checkFMODError(result);
            }
        }


        /// <summary>
        /// ファイルの長さを取得する
        /// </summary>
        /// <returns>ファイルの長さ(ミリ秒)</returns>
        public uint getLength()
        {
            FMOD.RESULT result;
            uint ms = 0;

            if (sound[PlayerController.MainChannel] != null)
            {
                result = sound[PlayerController.MainChannel].getLength(ref ms, FMOD.TIMEUNIT.MS);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    checkFMODError(result);
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
            FMOD.RESULT result;
            bool playing = false;

            if (channel[channelNo] != null)
            {
                result = channel[channelNo].isPlaying(ref playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    checkFMODError(result);
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
            FMOD.RESULT result;
            bool paused = false;

            if (channel[PlayerController.MainChannel] != null)
            {
                result = channel[PlayerController.MainChannel].getPaused(ref paused);
            }

            return paused;

        }

        public int getVersion(){
            
            return FMOD.VERSION.number;
        }

        /// <summary>
        /// ボリュームを設定する。
        /// </summary>
        /// <param name="channelNo"></param>
        /// <param name="vol"></param>
        public void setVolume(int channelNo,
            float vol)
        {
            if (channel[channelNo] != null)
            {
                channel[channelNo].setVolume(vol);
            }
        }

        /* 2010/01/24 del taglib#に変更
        public void getTag(GridItemInfo gi)
        {
            Dictionary<string, string> tagDic = new Dictionary<string, string>();
            FMOD.TAG tag = new FMOD.TAG();

            for (; ; )
            {
                //
                //    An index of -1 means "get the first tag that's new or updated".
                //    If no tags are new or updated then getTag will return FMOD_ERR_TAGNOTFOUND.
                //    This is the first time we've read any tags so they'll all be new but after we've read them, 
                //    they won't be new any more.
                //
                if (sound == null)
                {
                    break;
                }
                if (sound.getTag(null, -1, ref tag) != FMOD.RESULT.OK)
                {
                    break;
                }
                if (tag.datatype == FMOD.TAGDATATYPE.STRING)
                {
                    tagDic[tag.name] = Marshal.PtrToStringAnsi(tag.data);
                }
                else if (tag.datatype == TAGDATATYPE.STRING_UTF8)
                {
                    MarshalPtrToUtf8 mutf8 = new MarshalPtrToUtf8();
                    tagDic[tag.name] = (string)mutf8.MarshalNativeToManaged(tag.data);
                }
                else if (tag.datatype == TAGDATATYPE.STRING_UTF16)
                {
                    tagDic[tag.name] = Marshal.PtrToStringUni(tag.data);
                }
            }
            // タイトル
            if(tagDic.ContainsKey("TITLE")) {
                gi.Title = tagDic["TITLE"];
            }
            if (tagDic.ContainsKey("TIT2"))
            {
                gi.Title = tagDic["TIT2"];
            }
            // アーティスト
            if (tagDic.ContainsKey("ARTIST"))
            {
                gi.Artist = tagDic["ARTIST"];
            }
            if (tagDic.ContainsKey("TPE1"))
            {
                gi.Artist = tagDic["TPE1"];
            }
            // アルバム
            if (tagDic.ContainsKey("ALBUM"))
            {
                gi.Album = tagDic["ALBUM"];
            }
            if (tagDic.ContainsKey("TALB"))
            {
                gi.Album = tagDic["TALB"];
            }
            // トラック
            if (tagDic.ContainsKey("TRACK"))
            {
                gi.Track = tagDic["TRACK"];
            }
            if (tagDic.ContainsKey("TRCK")) 
            {
                gi.Track = tagDic["TRCK"];
            }
            // ジャンル
            if (tagDic.ContainsKey("TCON")) 
            {
                gi.Genre = tagDic["TCON"];
            }
            // 年
            if (tagDic.ContainsKey("YEAR"))
            {
                gi.Year = tagDic["YEAR"];
            }
            if (tagDic.ContainsKey("TDRC"))
            {
                gi.Year = tagDic["TDRC"];
            }
        } 
        */

        /* 2010/02/11 del BitrateはTagLib#で取得することにした
        public uint getBitRate()
        {
            uint rawlen = 0;
            uint mslen = 0;
            if (sound != null)
            {
                sound.getLength(ref rawlen, FMOD.TIMEUNIT.RAWBYTES);
                sound.getLength(ref mslen, FMOD.TIMEUNIT.MS);

                //double aa = (Math.Round((double)rawlen / (double)mslen,0) * 8000);
                //Debug.WriteLine(aa);
                return (uint)(Math.Round((double)rawlen / (double)mslen, 0) * 8000) / 1000;
            }
            else
            {
                return 0;
            }
        }
        */
        
        /*
        /// <summary>
        /// soundに設定する(メモリリード)
        /// </summary>
        /// <param name="filePath">設定するファイル</param>
        public void setSoundFile(string filePath)
        {
            FMOD.RESULT result;
            int length;
            FMOD.CREATESOUNDEXINFO exinfo = new FMOD.CREATESOUNDEXINFO();

            // 現在再生している場合は停止してリリースする。
            if (sound[PlayerController.MainChannel] != null)
            {
                stop(PlayerController.MainChannel);
            }

            length = LoadFileIntoMemory(filePath);

            exinfo.cbsize = Marshal.SizeOf(exinfo);
            exinfo.length = (uint)length;

            result = system.createStream(audiodata, (FMOD.MODE.DEFAULT | MODE.OPENMEMORY | FMOD.MODE.CREATESTREAM), ref exinfo, ref sound[PlayerController.MainChannel]);
            checkFMODError(result);

        }
        */

        /*
        /// <summary>
        /// soundに設定する(ファイル直接リード)
        /// </summary>
        /// <param name="filePath">設定するファイル</param>
        public void setSoundFile(string filePath)
        {
            FMOD.RESULT result;

            // 現在再生している場合は停止してリリースする。
            if (sound != null)
            {
                stop();
            }

            result = system.createStream(filePath, (FMOD.MODE.DEFAULT | FMOD.MODE.CREATESTREAM), ref sound);
            checkFMODError(result);

        }
        */

        #endregion


        /*
            プライベートメソッド
        */
        #region PrivateMethod

        private static void checkFMODError(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {

                throw new LinearAudioPlayerException(FinalstreamException.ERROR_LEVEL.Warn,
                    "FMOD error! " + result + " - " + FMOD.Error.String(result) + " openFilePath: " + openFilePath);

                //Debug.WriteLine();
            }
        }

        private int LoadFileIntoMemory(string filename)
        {
            int length = 0;

            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {

                    audiodata = new byte[fs.Length];

                    length = (int)fs.Length;

                    fs.Read(audiodata, 0, length);
                }
            }

            return length;
        }


        #endregion
    }
}
