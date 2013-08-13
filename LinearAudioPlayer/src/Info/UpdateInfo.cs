using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FINALSTREAM.LinearAudioPlayer.Info
{
    public class UpdateInfo
    {
        /// <summary>
        /// 最新バージョン有無
        /// </summary>
        public bool IsReleaseNewVersion { get; set; }

        /// <summary>
        /// 新しいバージョン
        /// </summary>
        public string NewFileVersion { get; set; }

        /// <summary>
        /// チェック結果メッセージ
        /// </summary>
        public string CheckResultMessage { get; set; }

        public Color CheckResultMessageColor { get; set; }

        public UpdateInfo()
        {
            IsReleaseNewVersion = false;
            CheckResultMessage = "";
            CheckResultMessageColor = Color.Black;
        }

    }
}
