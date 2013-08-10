using System;
namespace FINALSTREAM.LinearAudioPlayer.Engine
{
    /// <summary>
    /// 再生エンジンのインタフェース。
    /// </summary>
    interface IPlayEngine : IDisposable
    {

        /// <summary>
        /// ファイル長さをミリ秒で取得する。
        /// </summary>
        /// <returns></returns>
        uint getLength();

        /// <summary>
        /// 再生位置をミリ秒で取得する。
        /// </summary>
        /// <returns></returns>
        uint getPosition();
        
        /// <summary>
        /// バージョンを取得する。
        /// </summary>
        /// <returns></returns>
        int getVersion();

        /// <summary>
        /// 初期化する。
        /// </summary>
        void init();

        /// <summary>
        /// 一時停止中であるかどうか
        /// </summary>
        /// <returns></returns>
        bool isPasued();

        /// <summary>
        /// 再生中であるかどうか
        /// </summary>
        /// <param name="channelNo"></param>
        /// <returns></returns>
        bool isPlaying(int channelNo);

        /// <summary>
        /// ファイルをオープンする。
        /// </summary>
        /// <param name="filePath"></param>
        void open(string filePath);

        /// <summary>
        /// 一時停止する。
        /// </summary>
        /// <returns></returns>
        bool pause();

        /// <summary>
        /// 再生する。
        /// </summary>
        /// <param name="isPause"></param>
        /// <returns></returns>
        bool play(bool isPause);

        /// <summary>
        /// 再生位置を設定する。
        /// </summary>
        /// <param name="ms"></param>
        void setPosition(uint ms);

        /// <summary>
        /// ボリュームを設定する。
        /// </summary>
        /// <param name="channelNo"></param>
        /// <param name="vol"></param>
        void setVolume(int channelNo, float vol);

        /// <summary>
        /// 停止する。
        /// </summary>
        void stop();

        /// <summary>
        /// チャネルを指定して停止する。
        /// </summary>
        /// <param name="channelNo"></param>
        void stop(int channelNo);

        bool isUpdateMetadata();

        float[] getSpectrum();

        bool isEnableWasapi();


        void applyNormalize(bool isApply);
    }
}
