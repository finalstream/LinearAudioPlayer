using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    /// <summary>
    /// サウンド設定クラス
    /// </summary>
    public class SoundConfig
    {

        int _volume;
        int _silentVolume;
        bool _fadeEffect;
        float _fadeDuration;
        public bool IsVolumeNormalize { get; set; }

        /// <summary>
        /// ボリューム
        /// </summary>
        public int Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        /// <summary>
        /// サイレントボリューム
        /// </summary>
        public int SilentVolume
        {
            get { return _silentVolume; }
            set { _silentVolume = value; }
        }

        /// <summary>
        /// フェードエフェクト
        /// </summary>
        public bool FadeEffect
        {
            get { return _fadeEffect; }
            set { _fadeEffect = value; }
        }

        /// <summary>
        /// フェード持続時間
        /// </summary>
        public float FadeDuration
        {
            get { return _fadeDuration; }
            set { _fadeDuration = value; }
        }

        public SoundConfig()
        {

            this._volume = 100;
            this._silentVolume = 10;
            this._fadeEffect = true;
            this._fadeDuration = (float) 0.5;

        }

    }
}
