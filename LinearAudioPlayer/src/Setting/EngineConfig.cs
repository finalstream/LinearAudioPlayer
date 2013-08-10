using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    public class EngineConfig
    {

        LinearEnum.PlayEngine _playEngine;

        /// <summary>
        /// 再生エンジン
        /// </summary>
        public LinearEnum.PlayEngine PlayEngine
        {
            get { return _playEngine; }
            set { _playEngine = value; }
        }

        public EngineConfig() {

            this._playEngine = LinearEnum.PlayEngine.FMOD;

        }

    }
}
