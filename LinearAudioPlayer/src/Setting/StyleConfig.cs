using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FINALSTREAM.Commons.Controls;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    /// <summary>
    /// スタイルコンフィグクラス
    /// </summary>
    public class StyleConfig
    {
        /// <summary>
        /// スタイル名
        /// </summary>
        [Category("StyleInfo")]
        [Description("スタイルの名前を指定します。")]
        public string Name { get; set; }

        /// <summary>
        /// デザイナー
        /// </summary>
        [Category("StyleInfo")]
        [Description("スタイルの製作者名を指定します。")]
        public string Designer { get; set; }

        /// <summary>
        /// バージョン
        /// </summary>
        [Category("StyleInfo")]
        [Description("スタイルのバージョンを指定します。")]
        public string Version { get; set; }

        /// <summary>
        /// コメント
        /// </summary>
        [Category("StyleInfo")]
        [Description("スタイルのコメント（使用者へ向けてのメッセージ）を指定します。")]
        public string Comment { get; set; }

        /// <summary>
        /// 外側右下ラインカラー
        /// </summary>
        [Browsable(false)]
        public int OutSideUnderRightLineColor { get; set; }

        private Color _FormOutSideUnderRightLineColor;
        [Category("Border")]
        [Description("ウインドウ枠線色(外側右下)を指定します。立体的に表示したいときは内側右下より暗めにします。")]
        public Color FormOutSideUnderRightLineColor { 
            get
            {
                _FormOutSideUnderRightLineColor = Color.FromArgb(OutSideUnderRightLineColor);
                return _FormOutSideUnderRightLineColor;
            }
            set {
                this._FormOutSideUnderRightLineColor = value;
                if (this._FormOutSideUnderRightLineColor.Name != "0")
                {
                    OutSideUnderRightLineColor = this._FormOutSideUnderRightLineColor.ToArgb();
                }
            } 
        }

        /// <summary>
        /// 内側右下ラインカラー
        /// </summary>
        [Browsable(false)]
        public int InSideUnderRightLineColor { get; set; }

        private Color _FormInSideUnderRightLineColor;
        [Category("Border")]
        [Description("ウインドウ枠線色(内側右下)を指定します。立体的に表示したいときは外側左上より暗めにします。")]
        public Color FormInSideUnderRightLineColor
        {
            get
            {
                _FormInSideUnderRightLineColor = Color.FromArgb(InSideUnderRightLineColor);
                return _FormInSideUnderRightLineColor;
            }
            set
            {
                this._FormInSideUnderRightLineColor = value;
                if (this._FormInSideUnderRightLineColor.Name != "0")
                {
                    InSideUnderRightLineColor = this._FormInSideUnderRightLineColor.ToArgb();
                }
            }
        }

        /// <summary>
        /// 外側左上ラインカラー
        /// </summary>
        [Browsable(false)]
        public int OutSideBottomLeftLineColor { get; set; }

        private Color _FormOutSideBottomLeftLineColor;
        [Category("Border")]
        [Description("ウインドウ枠線色(外側左上)を指定します。立体的に表示したいときは内側左上より暗めにします。")]
        public Color FormOutSideBottomLeftLineColor
        {
            get
            {
                _FormOutSideBottomLeftLineColor = Color.FromArgb(OutSideBottomLeftLineColor);
                return _FormOutSideBottomLeftLineColor;
            }
            set
            {
                this._FormOutSideBottomLeftLineColor = value;
                if (this._FormOutSideBottomLeftLineColor.Name != "0")
                {
                    OutSideBottomLeftLineColor = this._FormOutSideBottomLeftLineColor.ToArgb();
                }
            }
        }

        /// <summary>
        /// 内側左上ラインカラー
        /// </summary>
        [Browsable(false)]
        public int InSideBottomLeftLineColor { get; set; }

        private Color _FormInSideBottomLeftLineColor;
        [Category("Border")]
        [Description("ウインドウ枠線色(内側左上)を指定します。立体的に表示したいときは外側左上より明るめにします。")]
        public Color FormInSideBottomLeftLineColor
        {
            get
            {
                _FormInSideBottomLeftLineColor = Color.FromArgb(InSideBottomLeftLineColor);
                return _FormInSideBottomLeftLineColor;
            }
            set
            {
                this._FormInSideBottomLeftLineColor = value;
                if (this._FormInSideBottomLeftLineColor.Name != "0")
                {
                    InSideBottomLeftLineColor = this._FormInSideBottomLeftLineColor.ToArgb();
                }
            }
        }


        public override string ToString()
        {
            return Name;
        }

        [Category("Font")]
        [Description("メインディスプレイ(タイトル）のフォント指定します。")]
        public FontConfig MainTitleFontConfig { get; set; }

        [Category("Font")]
        [Description("メインディスプレイ(タイトル以外）のフォント指定します。")]
        public FontConfig MainDisplayFontConfig { get; set; }

        [Category("Font")]
        [Description("プレイリストのフォント指定します。")]
        public FontConfig PlaylistFontConfig { get; set; }

        [Category("Style")]
        [Description("枠線のスタイルを指定します。")]
        public BorderStyle DisplayBorderStyle { get; set; }

        [Browsable(false)]
        public ComboBoxStyle ComboBoxStyle { get; set; }

        [Category("Style")]
        [Description("スタイルのデフォルトカラープロファイルを指定します。")]
        public string DefaultColorProfile { get; set; }

        [Category("Style")]
        [Description("グリッドのヘッダのスタイルを指定します。")]
        public EnumGridHeaderStyle GridHeaderStyle { get; set; }

        public enum EnumGridHeaderStyle
        {
            Windows = 0,
            Gradient = 1,
            Flat  = 2 
        }

        public StyleConfig()
        {
            Name = LinearConst.DEFAULT_STYLE;
            GridHeaderStyle = EnumGridHeaderStyle.Gradient;
        }
    }
}
