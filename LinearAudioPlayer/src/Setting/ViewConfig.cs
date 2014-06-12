using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FINALSTREAM.LinearAudioPlayer.Grid;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    /// <summary>
    /// 表示設定クラス
    /// </summary>
    public class ViewConfig
    {

        Point _mainLocation;
        Size _mainSize;
        Point _listLocation;
        Size _listSize;
        Point _configLocation;
        Size _configSize;
        int[] _columnHeaderWidth;
        bool _topMost;
        bool _titleScroll;
        public string TitleTemplete { get; set; }
        public bool isTitleCentering { get; set; }
        public LinearEnum.TitleScrollMode TitleScrollMode { get; set; }
        public int MiniVisualizationLevel { get; set; }
        public int MiniVisualizationLineCount { get; set; }
        public bool isNotificationWindow { get; set; }
        public bool isSlidePIP { get; set; }
        public Point TagEditDialogLocation { get; set; }
        public string StyleName { get; set; }
        public bool FontBold { get; set; }
        public Point ColorProfileEditDialogLocation { get; set; }
        public Point StyleEditDialogLocation { get; set; }
        public bool isGetNetworkArtwork { get; set; }
        public int GroupListOrder { get; set; }
        public int AlbumJudgeCount { get; set; }
        public bool UseWebInterface { get; set; }
        public int WebInterfaceListenPort { get; set; }
        public string WebInterfaceTheme { get; set; }


        /// <summary>
        /// メイン画面のロケーション
        /// </summary>
        public Point MainLocation
        {
            get { return _mainLocation; }
            set { _mainLocation = value; }
        }

        
        /// <summary>
        /// メイン画面のサイズ
        /// </summary>
        public Size MainSize
        {
            get { return _mainSize; }
            set { _mainSize = value; }
        }

        /// <summary>
        /// リスト画面のロケーション
        /// </summary>
        public Point ListLocation
        {
            get { return _listLocation; }
            set { _listLocation = value; }
        }

        /// <summary>
        /// リスト画面のサイズ
        /// </summary>
        public Size ListSize
        {
            get { return _listSize; }
            set { _listSize = value; }
        }

        /// <summary>
        /// 設定画面のロケーション
        /// </summary>
        public Point ConfigLocation
        {
            get { return _configLocation; }
            set { _configLocation = value; }
        }

        /// <summary>
        /// 設定画面のサイズ
        /// </summary>
        public Size ConfigSize
        {
            get { return _configSize; }
            set { _configSize = value; }
        }

        /// <summary>
        /// カラムヘッダ幅
        /// </summary>
        public int[] ColumnHeaderWidth
        {
            get { return _columnHeaderWidth; }
            set { _columnHeaderWidth = value; }
        }

        /// <summary>
        /// タイトルスクロール
        /// </summary>
        public bool TitleScroll
        {
            get { return _titleScroll; }
            set { _titleScroll = value; }
        }

        /// <summary>
        /// 常に手前に表示
        /// </summary>
        public bool TopMost
        {
            get { return _topMost; }
            set { _topMost = value; }
        }

        /// <summary>
        /// カラープロファイル
        /// </summary>
        public string ColorProfile { get; set; }


        public double Opacity { get; set; }

        public float PIPViewDuration { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewConfig()
        {

            this._mainLocation = new Point(300, 300);
            this._mainSize = new Size(1200, 28);
            this._listSize = new Size(1200, 522);
            this._configSize = new Size(400, 375);
            this._columnHeaderWidth = GridController.COLUMN_HEADER_WIDTHS;
            this._titleScroll = true;
            this._topMost = false;
            this.ColorProfile = LinearConst.DEFAULT_STYLE + ".xml";
            this.Opacity = 1.00;
            this.TitleTemplete = "[%A] %T - %R (%L) %N";
            this.isTitleCentering = false;
            this.TitleScrollMode = LinearEnum.TitleScrollMode.ROLL;
            this.MiniVisualizationLevel = 5;
            this.isNotificationWindow = true;
            this.PIPViewDuration = 3.0f;
            this.isSlidePIP = true;
            this.StyleName = LinearConst.DEFAULT_STYLE;
            this.FontBold = false;
            this.isGetNetworkArtwork = true;
            this.GroupListOrder = (int) LinearEnum.EnumGroupListOrder.COUNT_DESC;
            this.AlbumJudgeCount = 10;
            this.WebInterfaceListenPort = 8888;
            this.WebInterfaceTheme = "light";
            this.MiniVisualizationLineCount = 24;
        }

    }
}
