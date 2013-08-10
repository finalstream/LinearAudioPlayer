using System;
using System.Drawing;
using FINALSTREAM.Commons.Controls;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    /// <summary>
    /// XML保存用カラーコンフィグクラス
    /// </summary>
    public class ColorConfigXml
    {

        /// <summary>
        /// Form背景色
        /// </summary>
        public int FormBackgroundColor { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public int DisplayBackgroundColor { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public int DisplayBorderColor { get; set; }

        /// <summary>
        /// First行背景色
        /// </summary>
        public int FirstRowBackgroundColor { get; set; }

        /// <summary>
        /// Second行背景色
        /// </summary>
        public int SecondRowBackgroundColor { get; set; }

        /// <summary>
        /// 前景色
        /// </summary>
        public int FontColor { get; set; }

        /// <summary>
        /// 選択行色
        /// </summary>
        public int SelectRowColor { get; set; }

        /// <summary>
        /// 再生中色
        /// </summary>
        public int PlayingColor { get; set;  }

        /// <summary>
        /// 再生していない色
        /// </summary>
        public int NoPlayColor { get; set; }

        /// <summary>
        /// ビットレート色
        /// </summary>
        public int BitRateColor { get; set; }

        /// <summary>
        /// 再生時間色
        /// </summary>
        public int PlayTimeColor { get; set; }

        /// <summary>
        /// 再生モード色
        /// </summary>
        public int PlayModeColor { get; set; }

        /// <summary>
        /// ヘッダ背景色
        /// </summary>
        public int HeaderBackgroundColor { get; set; }

        /// <summary>
        /// ヘッダ前景色
        /// </summary>
        public int HeaderFontColor { get; set; }

        /// <summary>
        /// プレイリスト情報文字色
        /// </summary>
        public int PlaylistInfoColor { get; set; }

        public int ProgressSeekBarMainBottomBackgroundColor { get; set; }
        public int ProgressSeekBarMainUnderBackgroundColor { get; set; }
        public int ProgressSeekBarUpBottomBackgroundColor { get; set; }
        public int ProgressSeekBarUpUnderBackgroundColor { get; set; }
        public int ProgressSeekBarBorderColor { get; set; }

        public int ProgressSeekBarMainBottomActiveColor { get; set; }
        public int ProgressSeekBarMainUnderActiveColor { get; set; }
        public int ProgressSeekBarUpBottomActiveColor { get; set; }
        public int ProgressSeekBarUpUnderActiveColor { get; set; }

        /// <summary>
        /// プログレスバーテーマ
        /// </summary>
        public VistaProgressBarTheme ProgressSeekBarTheme { get; set; }

        public int MiniProgressSeekBarMainBottomBackgroundColor { get; set; }
        public int MiniProgressSeekBarMainUnderBackgroundColor { get; set; }
        public int MiniProgressSeekBarUpBottomBackgroundColor { get; set; }
        public int MiniProgressSeekBarUpUnderBackgroundColor { get; set; }

        public int MiniProgressSeekBarMainBottomActiveColor { get; set; }
        public int MiniProgressSeekBarMainUnderActiveColor { get; set; }
        public int MiniProgressSeekBarUpBottomActiveColor { get; set; }
        public int MiniProgressSeekBarUpUnderActiveColor { get; set; }

        public int MiniProgressSeekBarBorderColor { get; set; }
        /// <summary>
        /// プログレスバーテーマ
        /// </summary>
        public VistaProgressBarTheme MiniProgressSeekBarTheme { get; set; }

        public int SpectrumLevelHightLevelColor { get; set; }
        public int SpectrumLevelLowLevelColor { get; set; }

        public int NotificationHeaderColor { get; set; }
        public int NotificationFontColor { get; set; }
        public int NotificationBodyFirstColor { get; set; }
        public int NotificationBodySecondColor { get; set; }

        public ColorConfigXml()
        {
            FormBackgroundColor = SystemColors.Control.ToArgb();
            DisplayBackgroundColor = Color.Black.ToArgb();
            DisplayBorderColor = Color.LightGray.ToArgb();
            FirstRowBackgroundColor = Color.Black.ToArgb();
            SecondRowBackgroundColor = Color.Black.ToArgb();
            FontColor = Color.White.ToArgb();
            SelectRowColor = Color.FromArgb(50, Color.DarkOrange).ToArgb();
            PlayingColor = Color.Orange.ToArgb();
            NoPlayColor = Color.Gray.ToArgb();
            BitRateColor = Color.Tomato.ToArgb();
            PlayTimeColor = Color.DarkOrange.ToArgb();
            PlayModeColor = Color.Silver.ToArgb();
            HeaderBackgroundColor = Color.Black.ToArgb();
            HeaderFontColor = Color.White.ToArgb();

            ProgressSeekBarMainBottomBackgroundColor = Color.FromArgb(202, 202, 202).ToArgb();
	        ProgressSeekBarMainUnderBackgroundColor = Color.FromArgb(234, 234, 234).ToArgb();
	        ProgressSeekBarUpBottomBackgroundColor = Color.FromArgb(219, 219, 219).ToArgb();
            ProgressSeekBarUpUnderBackgroundColor = Color.FromArgb(243, 243, 243).ToArgb();
            ProgressSeekBarTheme = VistaProgressBarTheme.Orange;
            ProgressSeekBarBorderColor = Color.FromArgb(178, 178, 178).ToArgb();

            MiniProgressSeekBarMainBottomBackgroundColor = Color.FromArgb(202, 202, 202).ToArgb();
            MiniProgressSeekBarMainUnderBackgroundColor = Color.FromArgb(234, 234, 234).ToArgb();
            MiniProgressSeekBarUpBottomBackgroundColor = Color.FromArgb(219, 219, 219).ToArgb();
            MiniProgressSeekBarUpUnderBackgroundColor = Color.FromArgb(243, 243, 243).ToArgb();
            MiniProgressSeekBarBorderColor = Color.FromArgb(178, 178, 178).ToArgb();
            MiniProgressSeekBarTheme = VistaProgressBarTheme.Orange;
            
            PlaylistInfoColor = Color.Black.ToArgb();

            SpectrumLevelHightLevelColor = Color.FromArgb(255, 50, 50, 50).ToArgb();
            SpectrumLevelLowLevelColor = Color.WhiteSmoke.ToArgb();

            NotificationHeaderColor = Color.FromArgb(255, 64, 64, 64).ToArgb();
            NotificationFontColor = Color.Black.ToArgb();
            NotificationBodyFirstColor = Color.Gainsboro.ToArgb();
            NotificationBodySecondColor = Color.Transparent.ToArgb();
        }

    }
}
