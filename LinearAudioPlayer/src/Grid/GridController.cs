using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevAge.ComponentModel.Converter;
using DevAge.Drawing;
using DevAge.Drawing.VisualElements;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Grid;
using FINALSTREAM.Commons.Network;
using FINALSTREAM.LinearAudioPlayer.Setting;
using SourceGrid;
using SourceGrid.Cells;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.GUI;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Models;
using SourceGrid.Cells.Virtual;
using BorderStyle = System.Windows.Forms.BorderStyle;
using ContentAlignment = DevAge.Drawing.ContentAlignment;
using TextRenderer = System.Windows.Forms.TextRenderer;


namespace FINALSTREAM.LinearAudioPlayer.Grid
{
    class GridController : SourceGridController
    {
        /*
         * パブリックメンバ
         */
        #region PublicMember

        public static readonly int[] COLUMN_HEADER_WIDTHS = new int[] { 0, 0, 20, 150, 150, 150, 25, 50, 70, 80, 40, 20, 60, 0, 40, 30, 0, 0 };

        public GridController(): base()
        {
        }

        public GridController(SourceGrid.Grid grid) : base(grid)
        {
        }

        protected override int[] getColumnHeaderWidths()
        {
            return COLUMN_HEADER_WIDTHS;
        }

        #endregion

        /*
            プライベートメンバ
        */
        #region PrivateMember

        private static Bitmap starFavoriteImage;
        private static Bitmap starNormalImage;
        private static Bitmap playingImage;
        private static Bitmap notfoundImage;
        private static Bitmap disableImage;
        private static CellBackColorAlternate cellView;
        private static CellBackColorAlternate cellViewPlaying;
        private static CellBackColorAlternate cellViewTextAlignmentRight;
        private static CellBackColorAlternate cellViewPlayingTextAlignmentRight;
        CellClickEvent doubleclickController;
        SelectionClickEvent selectionClickController;
        private IconClickEvent iconClickController;
        SourceGrid.Cells.Controllers.ToolTipText toolTipController;

        #endregion

        /*
            列挙型
         */
        #region Enum

        /// <summary>
        /// グリッド
        /// </summary>
        public enum EnuGrid : int
        {
            /// <summary>
            /// ID
            /// </summary>
            ID = 0,
            /// <summary>
            /// ファイルパス
            /// </summary>
            FILEPATH = 1,
            /// <summary>
            /// アイコン
            /// </summary>
            ICON = 2,
            /// <summary>
            /// タイトル
            /// </summary>
            TITLE = 3,
            /// <summary>
            /// アーティスト
            /// </summary>
            ARTIST = 4,
            /// <summary>
            /// アルバム
            /// </summary>
            ALBUM = 5,
            /// <summary>
            /// トラック
            /// </summary>
            TRACK = 6,
            /// <summary>
            /// 時間
            /// </summary>
            TIME = 7,
            /// <summary>
            /// ビットレート
            /// </summary>
            BITRATE = 8,
            /// <summary>
            /// ジャンル
            /// </summary>
            GENRE = 9,
            /// <summary>
            /// 年
            /// </summary>
            YEAR = 10,
            /// <summary>
            /// セレクション
            /// </summary>
            SELECTION = 11,
            /// <summary>
            /// 日付(v0.7.0から最終再生日時を表示するように変更)
            /// </summary>
            DATE = 12,
            /// <summary>
            /// レーティング
            /// </summary>
            RATING = 13,
            /// <summary>
            /// タグ
            /// </summary>
            TAG = 14,
            /// <summary>
            /// 再生回数
            /// </summary>
            PLAYCOUNT = 15,
            /// <summary>
            /// NOTFOUND
            /// </summary>
            NOTFOUND = 16,
            /// <summary>
            /// オプション
            /// </summary>
            OPTION = 17
        }

        public enum EnuPlayType :int
        {
            NOPLAY,
            PLAYING
        }

        public enum EnuGridIcon : int
        {
            NONE,
            PLAYING,
            NOTFOUND
        }



        #endregion

        /*
            プロパティ
        */
        #region Property


        #endregion


        /*
            パブリックメソッド 
        */
        #region PublicMethod

        public void setColorProfile(ColorConfig colorConfig)
        {
            ((SourceGrid.Selection.RowSelection)Grid.Selection).Border = DevAge.Drawing.RectangleBorder.NoBorder;
            ((SourceGrid.Selection.RowSelection)Grid.Selection).BackColor = colorConfig.SelectRowColor;
            ((SourceGrid.Selection.RowSelection)Grid.Selection).FocusBackColor = ((SourceGrid.Selection.RowSelection)Grid.Selection).BackColor;
            // セルビュー作成
            createCellView(colorConfig);
            setHeader(colorConfig);
            Grid.BackColor = colorConfig.FirstRowBackgroundColor;

            // スターアイコンロード
            using (FileStream fs = File.OpenRead(LinearGlobal.StyleDirectory + LinearConst.STAR_FAVORITE_FILE))
            using (System.Drawing.Image fsimg = System.Drawing.Image.FromStream(fs, false, false))
            {
                Bitmap bm = new Bitmap(fsimg);
                starFavoriteImage = bm;
            }
            using (FileStream fs = File.OpenRead(LinearGlobal.StyleDirectory + LinearConst.STAR_NORMAL_FILE))
            using (System.Drawing.Image fsimg = System.Drawing.Image.FromStream(fs, false, false))
            {
                Bitmap bm = new Bitmap(fsimg);
                starNormalImage = bm;
            }
            using (FileStream fs = File.OpenRead(LinearGlobal.StyleDirectory + "audio.png"))
            using (System.Drawing.Image fsimg = System.Drawing.Image.FromStream(fs, false, false))
            {
                Bitmap bm = new Bitmap(fsimg);
                playingImage = bm;
            }
            using (FileStream fs = File.OpenRead(LinearGlobal.StyleDirectory + "notfound.png"))
            using (System.Drawing.Image fsimg = System.Drawing.Image.FromStream(fs, false, false))
            {
                Bitmap bm = new Bitmap(fsimg);
                notfoundImage = bm;
            }

            string disableIconPath = LinearGlobal.StyleDirectory + "disable.png";
            if (!File.Exists(disableIconPath))
            {
                disableIconPath = LinearGlobal.DefaultStyleDirectory + "disable.png";
            }

            using (FileStream fs = File.OpenRead(disableIconPath))
            using (System.Drawing.Image fsimg = System.Drawing.Image.FromStream(fs, false, false))
            {
                Bitmap bm = new Bitmap(fsimg);
                disableImage = bm;
            }

            //starFavoriteImage = new Bitmap(LinearGlobal.StyleDirectory + LinearConst.STAR_FAVORITE_FILE);
            //starNormalImage = new Bitmap(LinearGlobal.StyleDirectory + LinearConst.STAR_NORMAL_FILE);
            //playingImage = new Bitmap(LinearGlobal.StyleDirectory + "audio.png");
            //notfoundImage = new Bitmap(LinearGlobal.StyleDirectory + "notfound.png");
        }

        public void loadStyle()
        {
            
        }

        protected override void setSelectionConfig()
        {
            //base.setSelectionConfig();
            
            //((SourceGrid.Selection.RowSelection)Grid.Selection).Border = DevAge.Drawing.RectangleBorder.NoBorder;
            //((SourceGrid.Selection.RowSelection)Grid.Selection).BackColor = LinearGlobal.ColorConfig.SelectRowColor;
            //((SourceGrid.Selection.RowSelection)Grid.Selection).FocusBackColor = ((SourceGrid.Selection.RowSelection)Grid.Selection).BackColor;
            
        }

       

        private void createCellView(ColorConfig colorConfig)
        {
            base.createCellView();

            // セルビュー作成
            cellView = new CellBackColorAlternate(
                colorConfig.FirstRowBackgroundColor,
                colorConfig.SecondRowBackgroundColor);
            cellView.ForeColor = colorConfig.NoPlayColor;
            cellView.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            //cellView.TextAlignment = ContentAlignment.MiddleLeft;
            cellViewPlaying = new CellBackColorAlternate(
                colorConfig.FirstRowBackgroundColor,
                colorConfig.SecondRowBackgroundColor);
            cellViewPlaying.ForeColor = colorConfig.PlayingColor;
            cellViewPlaying.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            cellViewTextAlignmentRight = new CellBackColorAlternate(
                colorConfig.FirstRowBackgroundColor,
                colorConfig.SecondRowBackgroundColor);
            cellViewTextAlignmentRight.ForeColor = colorConfig.NoPlayColor;
            cellViewTextAlignmentRight.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            cellViewTextAlignmentRight.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            cellViewPlayingTextAlignmentRight = new CellBackColorAlternate(
                colorConfig.FirstRowBackgroundColor,
                colorConfig.SecondRowBackgroundColor);
            cellViewPlayingTextAlignmentRight.ForeColor = colorConfig.PlayingColor;
            cellViewPlayingTextAlignmentRight.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            cellViewPlayingTextAlignmentRight.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;

        }

        private void setHeader(ColorConfig colorConfig)
        {
            // ヘッダービュー作成
            DevAge.Drawing.VisualElements.ColumnHeader headerBackground = new DevAge.Drawing.VisualElements.ColumnHeader();
            headerBackground.BackColor = colorConfig.HeaderBackgroundColor;
            headerBackground.Border = DevAge.Drawing.RectangleBorder.NoBorder;

            if (LinearGlobal.StyleConfig.GridHeaderStyle == StyleConfig.EnumGridHeaderStyle.Flat)
            {
                headerBackground.BackgroundColorStyle = BackgroundColorStyle.Solid;
            }
            //headerBackground.Style = ControlDrawStyle.Pressed;

            //headerBackground.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            SourceGrid.Cells.Views.ColumnHeader headerView = new SourceGrid.Cells.Views.ColumnHeader();

            headerView.Background = headerBackground;
            headerView.ForeColor = colorConfig.HeaderFontColor;

            // SourceGrid用イベント初期化
            ColumnHeaderClickEvent columnHeaderClickEvent = new ColumnHeaderClickEvent();
            SourceGrid.Cells.Controllers.SortableHeader sortableHeader = new SourceGrid.Cells.Controllers.SortableHeader();

            // ID [Hidden]
            Grid[0, (int)EnuGrid.ID] = new SourceGrid.Cells.ColumnHeader(String.Empty);
            Grid[0, (int)EnuGrid.ID].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.ID];
            Grid[0, (int) EnuGrid.ID].Row.Height = 20;

            // FilePath [Hidden] 
            Grid[0, (int)EnuGrid.FILEPATH] = new SourceGrid.Cells.ColumnHeader(String.Empty);
            Grid[0, (int)EnuGrid.FILEPATH].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.FILEPATH];
            // Icon
            SourceGrid.Cells.ColumnHeader iconColumHeader = new SourceGrid.Cells.ColumnHeader(String.Empty);
            iconColumHeader.AutomaticSortEnabled = false;
            Grid[0, (int)EnuGrid.ICON] = iconColumHeader;
            Grid[0, (int)EnuGrid.ICON].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.ICON];
            Grid[0, (int)EnuGrid.ICON].AddController(columnHeaderClickEvent);

            // Title
            Grid[0, (int)EnuGrid.TITLE] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Title);
            Grid[0, (int)EnuGrid.TITLE].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.TITLE];
            // Artist
            Grid[0, (int)EnuGrid.ARTIST] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Artist);
            Grid[0, (int)EnuGrid.ARTIST].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.ARTIST];

            // Album
            Grid[0, (int)EnuGrid.ALBUM] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Album);
            Grid[0, (int)EnuGrid.ALBUM].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.ALBUM];

            // Track
            Grid[0, (int)EnuGrid.TRACK] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Track);
            Grid[0, (int)EnuGrid.TRACK].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.TRACK];

            // Time
            Grid[0, (int)EnuGrid.TIME] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Time);
            var ch = Grid[0, (int)EnuGrid.TIME] as SourceGrid.Cells.ColumnHeader;
            ch.AutomaticSortEnabled = false;
            Grid[0, (int)EnuGrid.TIME].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.TIME];
            Grid[0, (int) EnuGrid.TIME].AddController(sortableHeader);
            Grid[0, (int)EnuGrid.TIME].AddController(columnHeaderClickEvent);

            // Bitrate
            Grid[0, (int)EnuGrid.BITRATE] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_BitRate);
            Grid[0, (int)EnuGrid.BITRATE].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.BITRATE];

            // Genre
            Grid[0, (int)EnuGrid.GENRE] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Genre);
            Grid[0, (int)EnuGrid.GENRE].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.GENRE];

            // Year
            Grid[0, (int)EnuGrid.YEAR] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Year);
            Grid[0, (int)EnuGrid.YEAR].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.YEAR];

            // Selection
            Grid[0, (int)EnuGrid.SELECTION] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Selection);
            Grid[0, (int)EnuGrid.SELECTION].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.SELECTION];


            // Date
            Grid[0, (int)EnuGrid.DATE] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Date);
            var ch2 = Grid[0, (int)EnuGrid.DATE] as SourceGrid.Cells.ColumnHeader;
            ch2.AutomaticSortEnabled = false;
            Grid[0, (int)EnuGrid.DATE].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.DATE];
            //Grid[0, (int) EnuGrid.DATE].Column.Width = 0;
            Grid[0, (int)EnuGrid.DATE].AddController(sortableHeader);
            Grid[0, (int)EnuGrid.DATE].AddController(columnHeaderClickEvent);

            // Rating
            Grid[0, (int)EnuGrid.RATING] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Rating);
            Grid[0, (int)EnuGrid.RATING].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.RATING];

            // Tag
            Grid[0, (int)EnuGrid.TAG] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_Tag);
            Grid[0, (int)EnuGrid.TAG].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.TAG];

            // PlayCount
            Grid[0, (int)EnuGrid.PLAYCOUNT] = new SourceGrid.Cells.ColumnHeader(Properties.LabelResource.grid_PlayCount);
            Grid[0, (int)EnuGrid.PLAYCOUNT].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.PLAYCOUNT];

            // NotFound
            Grid[0, (int)EnuGrid.NOTFOUND] = new SourceGrid.Cells.ColumnHeader(String.Empty);
            Grid[0, (int)EnuGrid.NOTFOUND].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.NOTFOUND];

            // Option
            Grid[0, (int)EnuGrid.OPTION] = new SourceGrid.Cells.ColumnHeader(String.Empty);
            Grid[0, (int)EnuGrid.OPTION].Column.Width =
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth[(int)EnuGrid.OPTION];
            // End

            // ヘッダビューをセット
            if (LinearGlobal.StyleConfig.GridHeaderStyle != StyleConfig.EnumGridHeaderStyle.Windows)
            {
                this.setHeaderView(0, headerView);
            }
        }

        protected override void setHeader()
        {
            setHeader(LinearGlobal.ColorConfig);
        }

        /// <summary>
        /// グリッドを初期化する
        /// </summary>
        public override void initialGrid()
        {
            loadStyle();

            doubleclickController = new CellClickEvent();
            selectionClickController = new SelectionClickEvent();
            iconClickController = new IconClickEvent();
            toolTipController = new SourceGrid.Cells.Controllers.ToolTipText();
            toolTipController.IsBalloon = false;

            // カラムが増えてないか確認);
            if (getColumnHeaderWidths().Length != LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth.Length)
            {
                LinearGlobal.LinearConfig.ViewConfig.ColumnHeaderWidth = COLUMN_HEADER_WIDTHS;
            }

            // グリッドスタイル
            Grid.BorderStyle = BorderStyle.Fixed3D;
            Grid.Selection.EnableMultiSelection = true;
            Grid.DefaultHeight = (int) (TextRenderer.MeasureText("X", Grid.Font).Height *1.5);

            setSelectionConfig();

            // グリッドの列数
            Grid.ColumnsCount = getColumnHeaderWidths().Length;
            Grid.FixedRows = 1;

            // ヘッダ生成
            Grid.Rows.Insert(0);

            setColorProfile(LinearGlobal.ColorConfig);
            

        }

        /// <summary>
        /// グリッドにアイテムを追加する
        /// </summary>
        /// <param name="filePath"></param>
        public override void addItem(ISourceGridItem sgi)
        {
            // 新規行番号を取得
            int i = Grid.RowsCount;
            // 新規行を追加
            Grid.Rows.Insert(i);

            // SourceGrid用イベント初期化
            GridItemInfo gi = (GridItemInfo) sgi;
            
            
            // ToolTip Controller
            
            //toolTipController.ToolTipTitle = gi.Title;

            // ID用
            Grid[i, (int)EnuGrid.ID] = new Cell(gi.Id);
            Grid[i, (int)EnuGrid.ID].AddController(doubleclickController);
            
            // ファイルパス用
            Grid[i, (int)EnuGrid.FILEPATH] = new Cell(gi.FilePath);
            Grid[i, (int)EnuGrid.FILEPATH].AddController(doubleclickController);

            // Icon
            Grid[i, (int)EnuGrid.ICON] = new Cell();
            Grid[i, (int)EnuGrid.ICON].AddController(doubleclickController);
            Grid[i, (int)EnuGrid.ICON].AddController(toolTipController);
            Grid[i, (int)EnuGrid.ICON].AddController(iconClickController);
            if (!LinearGlobal.invalidIdTable.Contains(gi.Id))
            {
                Grid[i, (int) EnuGrid.ICON].Tag = true; // 一時無効フラグ(trueが有効、falseが無効)
            }
            else
            {
                Grid[i, (int)EnuGrid.ICON].Tag = false; // 一時無効フラグ
                Grid[i, (int) EnuGrid.ICON].Image = disableImage;
            }
            Grid[i, (int)EnuGrid.ICON].ToolTipText = gi.Title + " - " +gi.Artist;

            // Title
            Grid[i, (int)EnuGrid.TITLE] = new Cell(gi.Title);
            //Grid[i, (int)EnuGrid.TITLE].ToolTipText = gi.Title;
            Grid[i, (int)EnuGrid.TITLE].AddController(doubleclickController);
            //Grid[i, (int)EnuGrid.TITLE].AddController(toolTipController);

            // Artist
            Grid[i,(int)EnuGrid.ARTIST] = new Cell(gi.Artist);
            //Grid[i, (int)EnuGrid.TITLE].ToolTipText = gi.Artist;
            Grid[i,(int)EnuGrid.ARTIST].AddController(doubleclickController);
            //Grid[i, (int)EnuGrid.ARTIST].AddController(toolTipController);

            // Album
            Grid[i, (int)EnuGrid.ALBUM] = new Cell(gi.Album);
            //Grid[i, (int)EnuGrid.TITLE].ToolTipText = gi.Album;
            Grid[i, (int)EnuGrid.ALBUM].AddController(doubleclickController);
            //Grid[i, (int)EnuGrid.ALBUM].AddController(toolTipController);

            // Track
            Grid[i, (int)EnuGrid.TRACK] = new Cell(gi.Track);
            Grid[i, (int)EnuGrid.TRACK].AddController(doubleclickController);

            // Time
            var len = new ViewLength(gi.Time);
            Grid[i, (int)EnuGrid.TIME] = new Cell(len.LengthString);
            Grid[i, (int) EnuGrid.TIME].Tag = len.Length;
            //var mc = new ModelContainer();
            //mc.ValueModel = new ValueModel(len.Length);
            //Grid[i, (int) EnuGrid.TIME].Model = mc;
            //Grid[i, (int)EnuGrid.TIME].Value = len.Length;
            //var edit = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            //var cnv = new DateTimeTypeConverter("H m ss");
            //edit.TypeConverter = cnv;
            //var context = new CellContext(Grid, new Position(i, (int)EnuGrid.TIME));
            //edit.SetCellValue(context, len.Length);
            //Grid[i, (int)EnuGrid.TIME].Editor = edit;
            //Grid[i, (int) EnuGrid.TIME].Editor.EnableEdit = false;
            Grid[i, (int)EnuGrid.TIME].AddController(doubleclickController);

            // Bitrate
            if (String.IsNullOrEmpty(gi.Bitrate))
            {
                Grid[i, (int)EnuGrid.BITRATE] = new Cell(gi.Bitrate);
            }
            else
            {
                Grid[i, (int)EnuGrid.BITRATE] = new Cell(gi.Bitrate + " kbps");
            }
            Grid[i, (int) EnuGrid.BITRATE].Tag = gi.Bitrate;
            Grid[i, (int)EnuGrid.BITRATE].AddController(doubleclickController);

            // Genre
            Grid[i, (int)EnuGrid.GENRE] = new Cell(gi.Genre);
            Grid[i, (int)EnuGrid.GENRE].AddController(doubleclickController);

            // Year
            if (String.IsNullOrEmpty(gi.Year) && !String.IsNullOrEmpty(gi.Date))
            {
                gi.Year = gi.Date.Substring(0, 4);
            }
            Grid[i, (int)EnuGrid.YEAR] = new Cell(gi.Year);
            Grid[i, (int)EnuGrid.YEAR].Tag = gi.Date;
            Grid[i, (int)EnuGrid.YEAR].AddController(doubleclickController);

            // Selection
            Grid[i, (int)EnuGrid.SELECTION] = new Cell(gi.Rating);
            switch(gi.Rating) {
                case (int)LinearEnum.RatingValue.NORMAL:
                case (int)LinearEnum.RatingValue.NOTRATING:
                    Grid[i, (int)EnuGrid.SELECTION].Image = starNormalImage;
                    Grid[i, (int)EnuGrid.SELECTION].Tag = LinearEnum.RatingValue.NORMAL;
                    break;
                case (int)LinearEnum.RatingValue.FAVORITE:
                    Grid[i, (int)EnuGrid.SELECTION].Image = starFavoriteImage;
                    Grid[i, (int)EnuGrid.SELECTION].Tag = LinearEnum.RatingValue.FAVORITE;
                    break;
                case (int)LinearEnum.RatingValue.EXCLUSION:
                    Grid[i, (int)EnuGrid.SELECTION].Tag = LinearEnum.RatingValue.EXCLUSION;
                    Grid[i, (int) EnuGrid.SELECTION].Value = "";
                    break;

            }
            //Grid[i, (int)EnuGrid.SELECTION].Tag = LinearEnum.PlaylistMode.NORMAL;
            //Grid[i, (int)EnuGrid.SELECTION] = new Cell("");
            Grid[i, (int)EnuGrid.SELECTION].AddController(selectionClickController);
            Grid[i, (int)EnuGrid.SELECTION].Editor = null;

            // Date
            Grid[i, (int)EnuGrid.DATE] = new Cell(DateTimeUtils.getRelativeTimeJapaneseString(gi.Lastplaydate));
            Grid[i, (int) EnuGrid.DATE].Tag = gi.Lastplaydate;
            Grid[i, (int)EnuGrid.DATE].AddController(doubleclickController);

            // Rating
            Grid[i, (int)EnuGrid.RATING] = new Cell(gi.Rating);
            Grid[i, (int)EnuGrid.RATING].AddController(doubleclickController);

            // PlayCount
            Grid[i, (int)EnuGrid.PLAYCOUNT] = new Cell(gi.PlayCount);
            Grid[i, (int)EnuGrid.PLAYCOUNT].AddController(doubleclickController);

            // NotFound
            Grid[i, (int)EnuGrid.NOTFOUND] = new Cell(gi.NotFound);
            Grid[i, (int)EnuGrid.NOTFOUND].AddController(doubleclickController);

            // Tag
            Grid[i, (int)EnuGrid.TAG] = new Cell(gi.Tag);
            Grid[i, (int)EnuGrid.TAG].AddController(doubleclickController);

            // Option
            Grid[i, (int)EnuGrid.OPTION] = new Cell(gi.Option);
            Grid[i, (int)EnuGrid.OPTION].AddController(doubleclickController);


            // NOTFOUNDアイコンをつける
            if (gi.NotFound == LinearConst.FLG_ON)
            {
                this.setPlayIcon(i, EnuGridIcon.NOTFOUND, true);
            }

            // セルビューをセット
            this.setCellView(i, EnuPlayType.NOPLAY);

           
            
        }

        
        private class ViewLength
        {

            public ViewLength(string time)
            {
                LengthString = time;
                if (!String.IsNullOrEmpty(time))
                {
                    if (time.Length < 6)
                    {
                        time = "0:" + time;
                    }
                    Length = TimeSpan.Parse(time).TotalSeconds;
                }
            }
            public string LengthString { get; set; }

            public double Length { get; set; }

        }

        /// <summary>
        /// アイテムを更新する
        /// </summary>
        /// <param name="gi"></param>
        /// <param name="rowNo"></param>
        public override void updateItem(ISourceGridItem sgi,int rowNo){

            if (rowNo != -1)
            {
                GridItemInfo gi = (GridItemInfo) sgi;
                Grid[rowNo, (int)EnuGrid.TITLE].Value = gi.Title;
                //Grid[rowNo, (int)EnuGrid.PLAYCOUNT].View.TextAlignment = DevAge.Drawing.ContentAlignment.BottomLeft;
                ///Grid[rowNo, (int)EnuGrid.TITLE].ToolTipText = gi.Title;
                Grid[rowNo, (int)EnuGrid.ARTIST].Value = gi.Artist;
                //Grid[rowNo, (int)EnuGrid.ARTIST].ToolTipText = gi.Artist;
                Grid[rowNo, (int)EnuGrid.ALBUM].Value = gi.Album;
                //Grid[rowNo, (int)EnuGrid.ALBUM].ToolTipText = gi.Album;
                Grid[rowNo, (int)EnuGrid.TRACK].Value = gi.Track;
                Grid[rowNo, (int)EnuGrid.TIME].Value = gi.Time;
                Grid[rowNo, (int)EnuGrid.BITRATE].Value = gi.Bitrate + " kbps";
                Grid[rowNo, (int)EnuGrid.GENRE].Value = gi.Genre;
                Grid[rowNo, (int)EnuGrid.YEAR].Value = gi.Year;
                Grid[rowNo, (int)EnuGrid.RATING].Value = gi.Rating;
                Grid[rowNo, (int)EnuGrid.PLAYCOUNT].Value = gi.PlayCount;
                Grid[rowNo, (int)EnuGrid.NOTFOUND].Value = gi.NotFound;
                Grid[rowNo, (int)EnuGrid.DATE].Value = DateTimeUtils.getRelativeTimeJapaneseString(gi.Lastplaydate);
                Grid[rowNo, (int) EnuGrid.DATE].Tag = gi.Lastplaydate;
                //Grid[rowNo, (int)EnuGrid.PLAYCOUNT].View.TextAlignment = DevAge.Drawing.ContentAlignment.BottomRight;

            }
            
        }


        /// <summary>
        /// グリッドアイテムを新しく作成する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public GridItemInfo createNewGridItem(string path,string option)
        {
            GridItemInfo gi = new GridItemInfo();
            gi.Id = 0;
            gi.FilePath = path;

            if (StringUtils.isURL(path))
            {
                // Shoutcast
                ShoutcastManager sm = new ShoutcastManager(path);
                gi.Title = sm.getShoutcastName();
                gi.Option = String.Empty;
            } 
            else if (String.IsNullOrEmpty(option))
            {
                // オーディオファイル
                //gi.Title = Path.GetFileNameWithoutExtension(path);
                gi.Option = String.Empty;
            }
            else
            {
                // アーカイブファイル
                //gi.Title = option;
                gi.Option = option;
            }

            string archiveTempFilePath = "";
            if (!String.IsNullOrEmpty(option))
            {
                string arcfilePath = Path.Combine(LinearGlobal.TempDirectory, option);
                if (!File.Exists(arcfilePath))
                {
                    SevenZipManager.Instance.extract(path, option, LinearGlobal.TempDirectory);
                    gi.FilePath = arcfilePath;
                    archiveTempFilePath = arcfilePath;
                }
                else
                {
                    gi.FilePath = arcfilePath;
                }

            }

            LinearAudioPlayer.PlayController.getTag(gi);

            gi.FilePath = path;
            gi.Date = FileUtils.getCreationDateTime(path);
            //gi.Adddate = DateTime.Now.ToString(FormatUtils.FORMAT_SQLITE_DATETIME);
            //gi.AddDateTime = DateTime.Now.ToString(LinearConst.DATEFORMAT_ADDDATE);
            gi.Rating = (int)LinearEnum.RatingValue.NOTRATING;
            gi.PlayCount = 0;
            gi.NotFound = LinearConst.FLG_OFF;

            // セレクション
            //gi.Selection = (long) LinearGlobal.PlaylistMode;

            if (!String.IsNullOrEmpty(archiveTempFilePath))
            {
                // 使用後のファイルを削除
                FileUtils.delete(archiveTempFilePath);
            }

            return gi;
        }


        public GridItemInfo createNewGridItem(string id)
        {
            GridItemInfo gi = new GridItemInfo();
            gi.Id = int.Parse(id);
            return gi;
        }



        /// <summary>
        /// データベースからロードした情報でグリッドアイテムを作成する
        /// </summary>
        /// <param name="sqlr"></param>
        /// <returns></returns>
        public GridItemInfo createLoadGridItem(IList<object> recordList)
        {
            // 0:id
            // 1:fillpath
            // 2:title
            // 3:airtist
            // 4:length
            // 5:bitrate
            // 6:date
            // 7:album
            // 8:track
            // 9:genre
            // 10:year
            // 11:rating
            // 12:playcount

            GridItemInfo gi = new GridItemInfo();
            gi.Id = int.Parse(recordList[(int)EnuGrid.ID].ToString());
            gi.FilePath = recordList[(int)EnuGrid.FILEPATH].ToString();
            gi.Title = recordList[(int)EnuGrid.TITLE].ToString();
            gi.Artist = recordList[(int)EnuGrid.ARTIST].ToString();
            gi.Time = recordList[(int)EnuGrid.TIME].ToString();
            gi.Bitrate = recordList[(int)EnuGrid.BITRATE].ToString();
            gi.Date = recordList[(int)EnuGrid.DATE].ToString();
            gi.Album = recordList[(int)EnuGrid.ALBUM].ToString();
            int trackNo;
            if (int.TryParse(recordList[(int)EnuGrid.TRACK].ToString(), out trackNo))
            {
                gi.Track = trackNo;
            }
            gi.Genre = recordList[(int)EnuGrid.GENRE].ToString();
            gi.Year = recordList[(int)EnuGrid.YEAR].ToString();
            //if (!recordList[(int)EnuGrid.SELECTION].Equals(System.DBNull.Value))
            //{
            //    gi.Selection = (long) recordList[(int)EnuGrid.SELECTION];
            //}
            gi.Lastplaydate = recordList[(int) EnuGrid.SELECTION].ToString();
            int rate;
            if (int.TryParse(recordList[(int)EnuGrid.RATING].ToString(), out rate))
            {
                gi.Rating = rate;
            }
            gi.Tag = recordList[(int)EnuGrid.TAG].ToString();
            int playcount;
            if (int.TryParse(recordList[(int)EnuGrid.PLAYCOUNT].ToString(), out playcount)) {
                gi.PlayCount = playcount;
            }
            int notfound;
            if (int.TryParse(recordList[(int)EnuGrid.NOTFOUND].ToString(), out notfound))
            {
                gi.NotFound = notfound;
            }
            gi.Option = recordList[(int)EnuGrid.OPTION].ToString();

            return gi;
        }


        /// <summary>
        /// RowNoのデータをGridItemInfoで返す。
        /// </summary>
        /// <returns></returns>
        public override ISourceGridItem getRowGridItem(int rowNo) {
            GridItemInfo gi = new GridItemInfo();

            gi.Album = this.getValue(rowNo, (int)EnuGrid.ALBUM);
            gi.Artist = this.getValue(rowNo, (int)EnuGrid.ARTIST);
            gi.Bitrate = this.getTagValue(rowNo, (int)EnuGrid.BITRATE).ToString();
            gi.Date = this.getTagValue(rowNo, (int)EnuGrid.YEAR).ToString();
            gi.Lastplaydate = this.getTagValue(rowNo, (int)EnuGrid.DATE).ToString();
            gi.FilePath = this.getValue(rowNo, (int)EnuGrid.FILEPATH);
            gi.Genre = this.getValue(rowNo, (int)EnuGrid.GENRE);
            
            gi.Id = int.Parse(this.getValue(rowNo, (int)EnuGrid.ID));
            gi.PlayCount = int.Parse(this.getValue(rowNo, (int)EnuGrid.PLAYCOUNT));
            int rate;
            if (int.TryParse(this.getValue(rowNo, (int)EnuGrid.RATING), out rate))
            {
                gi.Rating = rate;
            }
            gi.Time = this.getValue(rowNo, (int)EnuGrid.TIME);
            gi.Title = this.getValue(rowNo, (int)EnuGrid.TITLE);
            int trackNo;
            if (int.TryParse(this.getValue(rowNo, (int)EnuGrid.TRACK), out trackNo))
            {
                gi.Track = trackNo;
            }
            gi.Year = this.getValue(rowNo, (int)EnuGrid.YEAR);
            int notfound;
            if (int.TryParse(this.getValue(rowNo, (int)EnuGrid.NOTFOUND), out notfound))
            {
                gi.NotFound = notfound;
            }
            gi.Tag = this.getValue(rowNo, (int)EnuGrid.TAG);
            gi.Option = this.getValue(rowNo, (int)EnuGrid.OPTION);

            return gi;
        }


        /// <summary>
        /// RowNoの行にカラーを設定
        /// </summary>
        /// <param name="color"></param>
        public void setRowColor(int RowNo,EnuPlayType playtype)
        {
            if (RowNo != -1)
            {
                // セルビュー作成
                //CellBackColorAlternate cellView = new CellBackColorAlternate(Color.Khaki, Color.DarkKhaki);
                //cellView.BackColor = Color.Transparent;
                switch(playtype)
                {
                    case EnuPlayType.NOPLAY:
                        this.setCellView(RowNo, EnuPlayType.NOPLAY);
                        break;
                    case EnuPlayType.PLAYING:
                        this.setCellView(RowNo, EnuPlayType.PLAYING);
                        break;
                }
                
                // 右揃え
                //Grid[RowNo, (int)EnuGrid.TRACK].View.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                //Grid[RowNo, (int)EnuGrid.PLAYCOUNT].View.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            
                Grid.Refresh();
            }
        }

        ///// <summary>
        ///// セレクションアイコンを設定する。
        ///// </summary>
        //public void setSelectionIcon(int rowNo, LinearEnum.PlaylistMode selection)
        //{

        //    switch (selection)
        //    {
        //        case LinearEnum.PlaylistMode.NORMAL:
        //            Grid[rowNo, (int)EnuGrid.SELECTION].Image = starNormalImage;
        //            Grid[rowNo, (int)EnuGrid.SELECTION].Tag = LinearEnum.PlaylistMode.NORMAL;
        //            break;
        //        case LinearEnum.PlaylistMode.FAVORITE:
        //            Grid[rowNo, (int)EnuGrid.SELECTION].Image = starFavoriteImage;
        //            Grid[rowNo, (int)EnuGrid.SELECTION].Tag = LinearEnum.PlaylistMode.FAVORITE;
        //            break;
        //        case LinearEnum.PlaylistMode.EXCLUSION:
        //            Grid[rowNo, (int)EnuGrid.SELECTION].Tag = LinearEnum.PlaylistMode.EXCLUSION;
        //            Grid[rowNo, (int) EnuGrid.SELECTION].Image = null;
        //            Grid[rowNo, (int)EnuGrid.SELECTION].Value = "";
        //            break;

        //    }
        //    //this.Grid.Refresh();
        //}

        /// <summary>
        /// セレクションアイコンを設定する。
        /// </summary>
        public void setRatingIcon(int rowNo, LinearEnum.RatingValue rating)
        {

            switch (rating)
            {
                case LinearEnum.RatingValue.NORMAL:
                case LinearEnum.RatingValue.NOTRATING:
                    Grid[rowNo, (int)EnuGrid.SELECTION].Image = starNormalImage;
                    break;
                case LinearEnum.RatingValue.FAVORITE:
                    Grid[rowNo, (int)EnuGrid.SELECTION].Image = starFavoriteImage;
                    break;
                case LinearEnum.RatingValue.EXCLUSION:
                    Grid[rowNo, (int)EnuGrid.SELECTION].Image = null;
                    Grid[rowNo, (int)EnuGrid.SELECTION].Value = "";
                    break;
            }
            Grid[rowNo, (int)EnuGrid.SELECTION].Tag = rating;
            this.Grid.Refresh();
        }

        /// <summary>
        /// アイコンを設定する
        /// </summary>
        /// <param name="RowNo"></param>
        /// <param name="isSet"></param>
        public void setPlayIcon(int RowNo, EnuGridIcon gridicon,bool isSet)
        {
            Bitmap icon = null;
            switch (gridicon)
            {
                    case EnuGridIcon.PLAYING:
                    icon = playingImage;
                    break;
                    case EnuGridIcon.NOTFOUND:
                    icon = notfoundImage;
                    break;
            }
            if (RowNo != -1)
            {
                if (isSet)
                {
                    this.Grid[RowNo, (int) EnuGrid.ICON].Image = icon;
                }
                else
                {
                    if (LinearConst.FLG_OFF.ToString().Equals(this.getValue(RowNo, (int)EnuGrid.NOTFOUND)))
                    {
                        this.Grid[RowNo, (int) EnuGrid.ICON].Image = null;
                        //cellView = new CellBackColorAlternate(Color.Khaki, Color.DarkKhaki);
                        //cellView.BackColor = Color.Transparent;
                        //cellView.Border = DevAge.Drawing.RectangleBorder.NoBorder;
                        this.Grid[RowNo, (int)EnuGrid.ICON].View = cellView;
                    }   
                }

                // tooltip
               // SourceGrid.Cells.Controllers.ToolTipText tooltipController = new SourceGrid.Cells.Controllers.ToolTipText();
                //tooltipController.ToolTipTitle = this.getValue(RowNo, (int)EnuGrid.TITLE);
                //this.Grid[RowNo, (int)EnuGrid.ICON].AddController(tooltipController);
                //this.Grid[RowNo, (int)EnuGrid.ICON].ToolTipText = this.getValue(RowNo, (int)EnuGrid.ARTIST);
                this.Grid.Refresh();
            }
        }

        

        public void setRadioStatus(int rowNo)
        {
            if (!"OFF AIR".Equals(Grid[rowNo, (int)GridController.EnuGrid.BITRATE].Value))
            {
                ShoutcastManager sm = new ShoutcastManager(getValue(rowNo, (int) EnuGrid.FILEPATH));
                string[] status = sm.getPlayStatus();

                if (status != null)
                {
                    Grid[rowNo, (int)GridController.EnuGrid.ARTIST].Value = status[6];
                    Grid[rowNo, (int)GridController.EnuGrid.ALBUM].Value = status[0] + " / " + status[3];
                    Grid[rowNo, (int)GridController.EnuGrid.BITRATE].Value = status[5] + "kbps";
                }
                else
                {
                    Grid[rowNo, (int) GridController.EnuGrid.ARTIST].Value = "";
                    Grid[rowNo, (int)GridController.EnuGrid.ALBUM].Value =  "";
                    Grid[rowNo, (int)GridController.EnuGrid.BITRATE].Value = "OFF AIR";
                }
            }
            
        }

        #endregion


        /*
            プライベートメソッド
        */
        #region Private Method

        /// <summary>
        /// ヘッダビューを設定する
        /// </summary>
        /// <param name="RowNo">設定するRowNo</param>
        /// <param name="cellView">設定するCellView</param>
        private void setHeaderView(int RowNo, SourceGrid.Cells.Views.ColumnHeader headerView)
        {
            int i = 0;

            while (i < Grid.ColumnsCount)
            {
                Grid[RowNo, i].View = headerView;
                i++;
            }
        }

        /// <summary>
        /// セルビューを設定する
        /// </summary>
        /// <param name="RowNo">設定するRowNo</param>
        /// <param name="cellView">設定するCellView</param>
        private void setCellView(int RowNo, EnuPlayType playtype)
        {
            int i = 0;

            while (i < Grid.ColumnsCount)
            {
                if (EnuPlayType.NOPLAY == playtype)
                {
                    switch (i)
                    {
                        case (int)EnuGrid.TRACK:
                        case (int)EnuGrid.PLAYCOUNT:
                        case (int)EnuGrid.BITRATE:
                        case (int)EnuGrid.TIME:
                        case (int)EnuGrid.DATE:
                            Grid[RowNo, i].View = cellViewTextAlignmentRight;
                            break;
                        default:
                            Grid[RowNo, i].View = cellView;
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case (int)EnuGrid.TRACK:
                        case (int)EnuGrid.PLAYCOUNT:
                        case (int)EnuGrid.BITRATE:
                        case (int)EnuGrid.TIME:
                        case (int)EnuGrid.DATE:
                            Grid[RowNo, i].View = cellViewPlayingTextAlignmentRight;
                            break;
                        default:
                            Grid[RowNo, i].View = cellViewPlaying;
                            break;
                    }
                }
                
                i++;
            }

        }

        
        

        #endregion


        /*
            SourceGrid用イベントクラス 
        */
        #region SGEventClass


        /// <summary>
        /// ヘッダクリックイベントクラス
        /// </summary>
        private class ColumnHeaderClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            public override void OnMouseDown(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseDown(sender, e);

                if (e.Button == MouseButtons.Left)
                {
                    if (sender.Position.Column == (int)EnuGrid.ICON)
                    {
                        SourceGrid.Grid grid = (SourceGrid.Grid)sender.Grid;
                        SourceGrid.Cells.ColumnHeader header;
                        
                        header =
                                (SourceGrid.Cells.ColumnHeader) grid[sender.Position.Row, (int) EnuGrid.ID];


                        if (header.SortStyle == DevAge.Drawing.HeaderSortStyle.Ascending)
                        {
                            header.Sort(false);
                        }
                        else
                        {
                            header.Sort(true);
                        }

                    }
                    else if (sender.Position.Column == (int)EnuGrid.TIME || sender.Position.Column == (int)EnuGrid.DATE)
                    {
                         SourceGrid.Grid grid = (SourceGrid.Grid)sender.Grid;
                        SourceGrid.Cells.ColumnHeader header;

                        header = (SourceGrid.Cells.ColumnHeader)grid[sender.Position.Row, sender.Position.Column];
                        header.SortComparer = new TagCellComparer();
                    }
                    
                }
               
            }
        }

        /// <summary>
        /// A comparer for the Cell class. (Not for CellVirtual). Using the value of the cell.
        /// </summary>
        public class TagCellComparer : IComparer
        {
            public virtual System.Int32 Compare(System.Object x, System.Object y)
            {
                //Cell object
                if (x == null && y == null)
                    return 0;
                if (x == null)
                    return -1;
                if (y == null)
                    return 1;

                if (x is IComparable)
                {
                    if (x.GetType().Equals(y.GetType()) == false)
                        return -1;
                    return ((IComparable)x).CompareTo(y);
                }
                if (y is IComparable)
                {
                    if (x.GetType().Equals(y.GetType()) == false)
                        return -1;
                    return (-1 * ((IComparable)y).CompareTo(x));
                }

                //Cell.Value object
                object vx = ((ICell)x).Tag;
                object vy = ((ICell)y).Tag;
                if (vx == null && vy == null)
                    return 0;
                if (vx == null)
                    return -1;
                if (vy == null)
                    return 1;

                if (vx is IComparable)
                {
                    if (vx.GetType().Equals(vy.GetType()) == false)
                        return -1;
                    return ((IComparable)vx).CompareTo(vy);
                }
                if (vy is IComparable)
                {
                    if (vx.GetType().Equals(vy.GetType()) == false)
                        return -1;
                    return (-1 * ((IComparable)vy).CompareTo(vx));
                }

                throw new ArgumentException("Invalid cell object, no IComparable interface found");
            }
        }

        /// <summary>
        /// セルクリックイベントクラス
        /// </summary>
        private class CellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {

            /// <summary>
            /// 右クリックで選択状態にする。
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnMouseDown(CellContext sender, MouseEventArgs e)
            {
                base.OnMouseDown(sender, e);

                sender.Grid.ContextMenuStrip = LinearGlobal.MainForm.ListForm.playlistContextMenuStrip;

                if (e.Button == MouseButtons.Right)
                {
                    if (!sender.Grid.Selection.IsSelectedRow(sender.Position.Row))
                    {
                        sender.Grid.Selection.ResetSelection(false);
                    }
                    sender.Grid.Selection.SelectRow(sender.Position.Row, true);
                }
            }

            /// <summary>
            /// ダブルクリック時
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnDoubleClick(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnDoubleClick(sender, e);

                sender.Grid.Selection.FocusRow(sender.Position.Row);

                //pc.stop();

                /*
                // 割り込みモード時は割り込みに追加
                if (LinearGlobal.MainForm.ListForm.InterruptForm.Visible)
                {
                    LinearAudioPlayer.PlayController.addInterruptItem(sender.Position.Row);

                    return;
                }*/

                SourceGrid.Grid grid = (SourceGrid.Grid) sender.Grid;

                LinearAudioPlayer.PlayController.playForGridNo(
                    sender.Position.Row, false);
            }
        }

        /// <summary>
        /// キーコントロールクラス
        /// </summary>
        /* SourceGrid 4.30から不要
        private class KeyController : SourceGrid.Cells.Controllers.ControllerBase
        {
            /// <summary>
            /// キーが押されたとき
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnKeyDown(SourceGrid.CellContext sender, KeyEventArgs e)
            {
                base.OnKeyDown(sender, e);

                
                if (e.KeyCode == Keys.ControlKey)
                {
                    SourceGrid.Grid grid = (SourceGrid.Grid)sender.Grid;
                    //grid.Selection.EnableMultiSelection = true;
                }
            }

            /// <summary>
            /// キーが離されたとき
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnKeyUp(SourceGrid.CellContext sender, KeyEventArgs e)
            {
                base.OnKeyUp(sender, e);
                SourceGrid.Grid grid = (SourceGrid.Grid)sender.Grid;
                //grid.Selection.EnableMultiSelection = false;
            }
        }*/

        /// <summary>
        /// セレクションクリックイベント
        /// </summary>
        private class SelectionClickEvent : SourceGrid.Cells.Controllers.ControllerBase 
        {


            /// <summary>
            /// マウスボタンが押下されたとき
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnMouseDown(CellContext sender, MouseEventArgs e)
            {
                base.OnMouseDown(sender, e);

                sender.Grid.ContextMenuStrip = null;

                ListFunction lf = new ListFunction();
                SourceGrid.Grid grid = (SourceGrid.Grid)sender.Grid;

                if (e.Button == MouseButtons.Left)
                {
                    // 左クリック
                    if (LinearGlobal.PlaylistMode == LinearEnum.PlaylistMode.NORMAL)
                    {
                        
                        // 通常モード
                        if (((SourceGrid.Cells.Cell)sender.Cell).Tag.Equals(
                            LinearEnum.RatingValue.NORMAL) || ((SourceGrid.Cells.Cell)sender.Cell).Tag.Equals(
                            LinearEnum.RatingValue.NOTRATING))
                        {
                            // お気に入りにする
                            grid[sender.Position.Row, sender.Position.Column].Value = (int)LinearEnum.RatingValue.FAVORITE;
                            grid[sender.Position.Row, sender.Position.Column].Image = starFavoriteImage;
                            grid[sender.Position.Row, sender.Position.Column].Tag = LinearEnum.RatingValue.FAVORITE;
                            lf.operateSelection(LinearEnum.PlaylistMode.FAVORITE);
                        }
                        else
                        {
                            // 通常にする
                            grid[sender.Position.Row, sender.Position.Column].Value = (int)LinearEnum.RatingValue.NORMAL;
                            grid[sender.Position.Row, sender.Position.Column].Image = starNormalImage;
                            grid[sender.Position.Row, sender.Position.Column].Tag = LinearEnum.RatingValue.NORMAL;
                            // FAVORITEから削除するため一時的にFAVORTITEモードにする
                            lf.operateSelection(LinearGlobal.PlaylistMode);
                        }
                        sender.Grid.Refresh();

                        //  再生中のものの場合
                        long id = long.Parse(LinearAudioPlayer.GridController.getValue(sender.Position.Row, (int)EnuGrid.ID));
                        if (id == LinearGlobal.CurrentPlayItemInfo.Id)
                        {
                            LinearGlobal.MainForm.setRating(
                                long.Parse(grid[sender.Position.Row, sender.Position.Column].Value.ToString()));
                        }
                    }
                    else
                    {

                        // 通常モード以外なら通常に戻す
                        lf.operateSelection(LinearGlobal.PlaylistMode);

                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // 右クリックで選択
                    // TODO:同じとこがあるのであとでメソッド化する。
                    if (!sender.Grid.Selection.IsSelectedRow(sender.Position.Row))
                    {
                        sender.Grid.Selection.ResetSelection(false);
                    }
                    sender.Grid.Selection.SelectRow(sender.Position.Row, true);

                    if (LinearGlobal.PlaylistMode == LinearEnum.PlaylistMode.NORMAL)
                    {
                        // 除外に追加
                        lf.operateSelection(LinearEnum.PlaylistMode.EXCLUSION);
                    }
                }
                
            }

        }

        /// <summary>
        /// アイコンクリックイベント
        /// </summary>
        private class IconClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {


            /// <summary>
            /// マウスボタンが押下されたとき
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnMouseDown(CellContext sender, MouseEventArgs e)
            {
                base.OnMouseDown(sender, e);

                sender.Grid.ContextMenuStrip = null;

                SourceGrid.Grid grid = (SourceGrid.Grid) sender.Grid;

                if (e.Button == MouseButtons.Left)
                {
                    bool nowEnable = (bool) ((SourceGrid.Cells.Cell) sender.Cell).Tag;
                    nowEnable = !nowEnable;
                    grid[sender.Position.Row, (int) EnuGrid.ICON].Tag = nowEnable;
                    if (nowEnable)
                    {
                        grid[sender.Position.Row, (int) EnuGrid.ICON].Image = null;
                        LinearGlobal.invalidIdTable.Remove((long)grid[sender.Position.Row, (int)EnuGrid.ID].Value);
                    }
                    else
                    {
                        grid[sender.Position.Row, (int) EnuGrid.ICON].Image = disableImage;
                        LinearGlobal.invalidIdTable.Add((long) grid[sender.Position.Row, (int) EnuGrid.ID].Value);
                    }
                    //grid.Refresh();

                }

            }
        }

        private class CellBackColorAlternate : SourceGrid.Cells.Views.Cell
        {
            public CellBackColorAlternate(Color firstColor, Color secondColor)
            {
                FirstBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(firstColor);
                SecondBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(secondColor);
            }

            private DevAge.Drawing.VisualElements.IVisualElement mFirstBackground;
            public DevAge.Drawing.VisualElements.IVisualElement FirstBackground
            {
                get { return mFirstBackground; }
                set { mFirstBackground = value; }
            }

            private DevAge.Drawing.VisualElements.IVisualElement mSecondBackground;
            public DevAge.Drawing.VisualElements.IVisualElement SecondBackground
            {
                get { return mSecondBackground; }
                set { mSecondBackground = value; }
            }

            protected override void PrepareView(SourceGrid.CellContext context)
            {
                base.PrepareView(context);

                if (Math.IEEERemainder(context.Position.Row, 2) == 0)
                    Background = SecondBackground;
                else
                    Background = FirstBackground;
            }
        }
        #endregion



        
    }
}
