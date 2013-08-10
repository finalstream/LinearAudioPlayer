using System;
using System.ComponentModel;
using System.Drawing;
using FINALSTREAM.Commons.Controls;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    /// <summary>
    /// カラーコンフィグクラス
    /// </summary>
    [Serializable()]
    public class ColorConfig
    {
        /// <summary>
        /// 背景色
        /// </summary>
        [Category("All")]
        [Description("ウインドウの背景色を指定します。")]
        public Color FormBackgroundColor { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        [Category("All")]
        [Description("メインディスプレイの背景色、テキストボックスの背景色などを指定します。")]
        public Color DisplayBackgroundColor { get; set; }

        /// <summary>
        /// 枠線色
        /// </summary>
        [Category("All")]
        [Description("メインディスプレイの枠線色、テキストボックスの枠線色などを指定します。")]
        public Color DisplayBorderColor { get; set; }

        /// <summary>
        /// 前景色
        /// </summary>
        [Category("All")]
        [Description("メインディスプレイの文字色、テキストボックスの文字色などを指定します。")]
        public Color FontColor { get; set; }

        /*
        /// <summary>
        /// ビットレート色
        /// </summary>
        public Color BitRateColor { get; set; }
        */

        /// <summary>
        /// 再生時間色
        /// </summary>
        [Category("MainFace")]
        [Description("メインフェイスに表示する再生時間の文字色を指定します。")]
        public Color PlayTimeColor { get; set; }

        /// <summary>
        /// 再生モード色
        /// </summary>
        /// <summary>
        /// ヘッダ背景色
        /// </summary>
        [Category("MainFace")]
        [Description("メインフェイスに表示するプレイリストモードの文字色を指定します。")]
        public Color PlayModeColor { get; set; }

        /// <summary>
        /// ヘッダ背景色
        /// </summary>
        [Category("Playlist")]
        [Description("プレイリストのヘッダ背景色を指定します。")]
        public Color HeaderBackgroundColor { get; set; }

        /// <summary>
        /// ヘッダ前景色
        /// </summary>
        [Category("Playlist")]
        [Description("プレイリストのヘッダ文字色を指定します。")]
        public Color HeaderFontColor { get; set; }

        /// <summary>
        /// 選択行色
        /// </summary>
        [Category("Playlist")]
        [Description("プレイリストで選択行の色を指定します。アルファ値(透明度)の指定も可能です。")]
        public Color SelectRowColor { get; set; }


        /// <summary>
        /// 再生していない色
        /// </summary>
        [Category("Playlist")]
        [Description("プレイリストで再生していないアイテムの文字色を指定します。")]
        public Color NoPlayColor { get; set; }

        
        /// <summary>
        /// 再生中色
        /// </summary>
        [Category("Playlist")]
        [Description("プレイリストで再生中のアイテムの文字色を指定します。")]
        public Color PlayingColor { get; set; }


        /// <summary>
        /// First行背景色
        /// </summary>
        [Category("Playlist")]
        [Description("プレイリストの背景色(奇数行)を指定します。")]
        public Color FirstRowBackgroundColor { get; set; }

        /// <summary>
        /// Second行背景色
        /// </summary>
        [Category("Playlist")]
        [Description("プレイリストの背景色(偶数行)を指定します。")]
        public Color SecondRowBackgroundColor { get; set; }

        [Category("Playlist")]
        [Description("プレイリストウインドウに表示するプレイリスト情報の文字色を指定します。")]
        public Color PlaylistInfoColor { get; set; }

        /// <summary>
        /// ミニプログレスシークバー
        /// </summary>
        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarの背景色(メイン上部)を指定します。")]
        public Color ProgressSeekBarMainBottomBackgroundColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarの背景色(メイン下部)を指定します。")]
        public Color ProgressSeekBarMainUnderBackgroundColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarの背景色(上段上部)を指定します。")]
        public Color ProgressSeekBarUpBottomBackgroundColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarの背景色(上段下部)を指定します。")]
        public Color ProgressSeekBarUpUnderBackgroundColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarのシークバーの色(メイン上部)を指定します。")]
        public Color ProgressSeekBarMainBottomActiveColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarのシークバーの色(メイン下部)を指定します。")]
        public Color ProgressSeekBarMainUnderActiveColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarのシークバーの色(上段上部)を指定します。")]
        public Color ProgressSeekBarUpBottomActiveColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarのシークバーの色(上段下部)を指定します。")]
        public Color ProgressSeekBarUpUnderActiveColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarの枠線色を指定します。")]
        public Color ProgressSeekBarBorderColor { get; set; }

        [Category("ProgressSeekBar")]
        [Description("ProgressSeekBarのテーマを指定します。自由な色を設定したい場合はCustomを選択してください。")]
        public VistaProgressBarTheme ProgressSeekBarTheme { get; set; }

        /// <summary>
        /// ミニプログレスシークバー
        /// </summary>
        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarのシークバーの色(メイン上部)を指定します。")]
        public Color MiniProgressSeekBarMainBottomActiveColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarのシークバーの色(メイン下部)を指定します。")]
        public Color MiniProgressSeekBarMainUnderActiveColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarのシークバーの色(上段上部)を指定します。")]
        public Color MiniProgressSeekBarUpBottomActiveColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarのシークバーの色(上段下部)を指定します。")]
        public Color MiniProgressSeekBarUpUnderActiveColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarの背景色(メイン上部)を指定します。")]
        public Color MiniProgressSeekBarMainBottomBackgroundColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarの背景色(メイン下部)を指定します。")]
        public Color MiniProgressSeekBarMainUnderBackgroundColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarの背景色(上段上部)を指定します。")]
        public Color MiniProgressSeekBarUpBottomBackgroundColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarの背景色(上段下部)を指定します。")]
        public Color MiniProgressSeekBarUpUnderBackgroundColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarの枠線色を指定します。")]
        public Color MiniProgressSeekBarBorderColor { get; set; }

        [Category("MiniProgressSeekBar")]
        [Description("MiniProgressSeekBarのテーマを指定します。自由な色を設定したい場合はCustomを選択してください。")]
        public VistaProgressBarTheme MiniProgressSeekBarTheme { get; set; }

        [Category("MiniVisualization")]
        [Description("MiniVisualizationのハイレベルカラーを指定します。")]
        public Color SpectrumLevelHightLevelColor { get; set; }

        [Category("MiniVisualization")]
        [Description("MiniVisualizationのローレベルカラーを指定します。")]
        public Color SpectrumLevelLowLevelColor { get; set; }


        /// <summary>
        /// 再生通知ウインドウ
        /// </summary>
        [Category("NotificationWindow(Play Info Notification)")]
        [Description("再生情報通知のヘッダ色を指定します。")]
        public Color NotificationHeaderColor { get; set; }
        [Category("NotificationWindow(Play Info Notification)")]
        [Description("再生情報通知のフォント色を指定します。")]
        public Color NotficationFontColor { get; set; }
        [Category("NotificationWindow(Play Info Notification)")]
        [Description("再生情報通知の背景色(開始)を指定します。")]
        public Color NotficationBodyFirstColor { get; set; }
        [Category("NotificationWindow(Play Info Notification)")]
        [Description("再生情報通知の背景色(終了)を指定します。")]
        public Color NotficationBodySecondColor { get; set; }
    }
}
