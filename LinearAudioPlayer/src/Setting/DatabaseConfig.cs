using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    /// <summary>
    /// データベース設定クラス
    /// </summary>
    public class DatabaseConfig
    {

        public decimal LimitCount { get; set; }

        public DatabaseConfig()
        {
            LimitCount = 100;
        }

    }
}
