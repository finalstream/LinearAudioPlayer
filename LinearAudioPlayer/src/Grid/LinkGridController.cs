using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using DevAge.Drawing;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Grid;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Setting;
using SourceGrid;
using SourceGrid.Cells;
using System.Windows.Forms;
using System.Drawing;
using FINALSTREAM.LinearAudioPlayer.Info;
using BorderStyle = System.Windows.Forms.BorderStyle;


namespace FINALSTREAM.LinearAudioPlayer.Grid
{
    class LinkGridController : SourceGridController
    {
        /*
         * パブリックメンバ
         */
        #region PublicMember

        public static readonly int[] COLUMN_HEADER_WIDTHS = new int[] { 170};

        public int LastSelectRowNo = -1;

        public List<string> SimilarArtistList { get; set; }

        public object[][] SameArtistTrackList { get; set; } 


        public LinkGridController(SourceGrid.Grid grid) : base(grid)
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

        private static CellBackColorAlternate cellView;
        private static CellBackColorAlternate cellViewPlaying;
        CellClickEvent clickController;
        private string[] headerTitle = new string[] { "Now Playing","Same artist music", "Similar artists" };
        public EnuMode mode = EnuMode.NOWPLAYING;
        private int listcount = 6;  // 表示件数

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
            LINKTITILE = 0
        }

        public enum EnuPlayType : int
        {
            NOPLAY,
            PLAYING
        }

        public enum EnuMode : int
        {
            NOWPLAYING,
            SAMEARTIST,
            SIMILARARTIST,
            OVER
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
            //((SourceGrid.Selection.RowSelection)Grid.Selection).BackColor = colorConfig.SelectRowColor;
            //((SourceGrid.Selection.RowSelection)Grid.Selection).FocusBackColor = ((SourceGrid.Selection.RowSelection)Grid.Selection).BackColor;
            // セルビュー作成
            createCellView(colorConfig);
            setHeader(colorConfig);
            //Grid.BackColor = colorConfig.FirstRowBackgroundColor;
            Grid.BackColor = colorConfig.FormBackgroundColor;

        }

        public void loadStyle()
        {
            
        }

        protected override void setSelectionConfig()
        {
            
        }

       

        private void createCellView(ColorConfig colorConfig)
        {
            base.createCellView();

            // セルビュー作成
            //cellView = new CellBackColorAlternate(
            //    colorConfig.FirstRowBackgroundColor,
            //    colorConfig.SecondRowBackgroundColor);
            //cellView.ForeColor = colorConfig.NoPlayColor;
            cellView = new CellBackColorAlternate(
                colorConfig.FormBackgroundColor,
                colorConfig.FormBackgroundColor);
            cellView.ForeColor = colorConfig.PlaylistInfoColor;
            cellView.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            cellViewPlaying = new CellBackColorAlternate(
                colorConfig.FirstRowBackgroundColor,
                colorConfig.SecondRowBackgroundColor);
            cellViewPlaying.ForeColor = colorConfig.PlayingColor;
            cellViewPlaying.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            

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
            SourceGrid.Cells.Views.ColumnHeader headerView = new SourceGrid.Cells.Views.ColumnHeader();


            headerView.Background = headerBackground;
            headerView.ForeColor = colorConfig.HeaderFontColor;

            // SourceGrid用イベント初期化
            ColumnHeaderClickEvent columnHeaderClickEvent = new ColumnHeaderClickEvent();

            Grid[0, (int)EnuGrid.LINKTITILE] = new SourceGrid.Cells.ColumnHeader(headerTitle[0]);
            Grid[0, (int) EnuGrid.LINKTITILE].Column.Width = 278;
            Grid[0, (int)EnuGrid.LINKTITILE].AddController(columnHeaderClickEvent);
            Grid.EnableSort = false;
            
            //SourceGrid.Cells.Controllers.SortableHeader sortableHeader = new SourceGrid.Cells.Controllers.SortableHeader();
            // ヘッダビューをセット
            if (LinearGlobal.StyleConfig.GridHeaderStyle != StyleConfig.EnumGridHeaderStyle.Windows)
            {
                this.setHeaderView(0, headerView);
            }
        }

        protected override void setHeader()
        {
            //setHeader(LinearGlobal.ColorConfig);
        }

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
        /// グリッドを初期化する
        /// </summary>
        public override void initialGrid()
        {
            loadStyle();

            clickController = new CellClickEvent();
            //selectionClickController = new SelectionClickEvent();
            //toolTipController = new SourceGrid.Cells.Controllers.ToolTipText();
            //toolTipController.IsBalloon = false;

            // グリッドスタイル
            //Grid.BorderStyle = BorderStyle.Fixed3D;
            Grid.BorderStyle = BorderStyle.FixedSingle;
            
            Grid.Selection.EnableMultiSelection = false;
            //Grid.DefaultHeight = (int)(TextRenderer.MeasureText("X", Grid.Font).Height * 1.5);

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
            AnyGridItemInfo gi = (AnyGridItemInfo)sgi;
            
            

            // ToolTip Controller
            
            //toolTipController.ToolTipTitle = gi.Title;

            // ID用
            Grid[i, (int)EnuGrid.LINKTITILE] = new Cell(gi.DisplayValue);
            Grid[i, (int) EnuGrid.LINKTITILE].Tag = gi;
            Grid[i, (int)EnuGrid.LINKTITILE].AddController(clickController);

            /*
            if (i == 0)
            {
                Grid[i, (int)EnuGrid.LINKTITILE].Column.Width = 170;
            }*/

            // セルビューをセット
            this.setCellView(i, EnuPlayType.NOPLAY);

            
        }

        /// <summary>
        /// アイテムを更新する
        /// </summary>
        /// <param name="gi"></param>
        /// <param name="rowNo"></param>
        public override void updateItem(ISourceGridItem sgi,int rowNo){

           
            
        }



        /// <summary>
        /// RowNoのデータをGridItemInfoで返す。
        /// </summary>
        /// <returns></returns>
        public override ISourceGridItem getRowGridItem(int rowNo) {
            ConditionGridItemInfo cii = new ConditionGridItemInfo();

            cii.DisplayValue = this.getValue(rowNo, (int)EnuGrid.LINKTITILE);
            cii.Value = Grid[rowNo, (int) EnuGrid.LINKTITILE].Tag.ToString();

            return cii;
        }

        /// <summary>
        /// RowNoにフォーカスを設定する
        /// </summary>
        /// <param name="RowNo"></param>
        public override void setFocusRowNo(int RowNo)
        {
            base.setFocusRowNo(RowNo);
            setRowColor(RowNo, EnuPlayType.PLAYING);
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
                        LastSelectRowNo = RowNo;
                        break;
                }
                
                // 右揃え
                //Grid[RowNo, (int)EnuGrid.TRACK].View.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                //Grid[RowNo, (int)EnuGrid.PLAYCOUNT].View.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                

                Grid.Refresh();
            }
        }

        #endregion


        /*
            プライベートメソッド
        */
        #region Private Method

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
                if (Grid[RowNo, i] != null)
                {
                    if (EnuPlayType.NOPLAY == playtype)
                    {
                        switch (i)
                        {
                            default:
                                Grid[RowNo, i].View = cellView;
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            default:
                                Grid[RowNo, i].View = cellViewPlaying;
                                break;
                        }
                    }
                }
                
                i++;
            }

        }

        
        public void getSameArtistTrackList()
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SQLiteParameter("Id", LinearGlobal.CurrentPlayItemInfo.Id));
            parameters.Add(new SQLiteParameter("Artist", LinearGlobal.CurrentPlayItemInfo.Artist));
            SameArtistTrackList = SQLiteManager.Instance.executeQueryNormal(
                SQLBuilder.selectSameArtistTrackList(listcount), parameters);
        }

        public void reloadGrid()
        {
            Grid[0, (int)EnuGrid.LINKTITILE].Value = headerTitle[(int)mode];
            clearGrid();
            switch (mode)
            {
                case EnuMode.NOWPLAYING:
                    // 再生中の曲
                    GridItemInfo[] nowPlayings = LinearAudioPlayer.PlayController.getNowPlayingList(listcount);

                    foreach (var gridItemInfo in nowPlayings)
                    {
                        AnyGridItemInfo anyGridItem = new AnyGridItemInfo();
                        anyGridItem.DisplayValue = gridItemInfo.Title;
                        anyGridItem.Value = gridItemInfo.Id;
                        addItem(anyGridItem);
                    }

                    break;

                case EnuMode.SAMEARTIST:
                    // 同じアーティストの曲を選択
                    if (SameArtistTrackList != null)
                    {
                        foreach (var o in SameArtistTrackList)
                        {
                            AnyGridItemInfo anyGridItem = new AnyGridItemInfo();
                            anyGridItem.DisplayValue = o[1].ToString();
                            anyGridItem.Value = o[0];
                            addItem(anyGridItem);
                        }
                    }
                    break;

                    case EnuMode.SIMILARARTIST:

                    if (SimilarArtistList != null)
                    {
                        foreach (var artist in SimilarArtistList)
                        {
                            AnyGridItemInfo cgi = new AnyGridItemInfo();
                            cgi.DisplayValue = artist;
                            cgi.Value = artist;
                            addItem(cgi);
                        }
                    }
                    break;
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
                    LinearAudioPlayer.LinkGridController.mode++;
                    if (LinearAudioPlayer.LinkGridController.mode == EnuMode.OVER)
                    {
                        LinearAudioPlayer.LinkGridController.mode = EnuMode.NOWPLAYING;
                    }
                    LinearAudioPlayer.LinkGridController.reloadGrid();
                }

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
                
                if (e.Button == MouseButtons.Left)
                {

                    

                    SourceGrid.Grid grid = (SourceGrid.Grid) sender.Grid;

                    //LinearAudioPlayer.FilteringGridController.setRowColor(
                    //    LinearAudioPlayer.FilteringGridController.LastSelectRowNo, EnuPlayType.NOPLAY);


                    //LinearAudioPlayer.LinkGridController.setRowColor(sender.Position.Row, EnuPlayType.PLAYING);
                    //LinearAudioPlayer.GroupGridController.setRowColor(LinearAudioPlayer.GroupGridController.LastSelectRowNo, GroupGridController.EnuPlayType.NOPLAY);

                    //LinearGlobal.LinearConfig.PlayerConfig.SelectFilter = grid[sender.Position.Row, 0].Value.ToString();

                    switch (LinearAudioPlayer.LinkGridController.mode)
                    {
                            case EnuMode.NOWPLAYING:
                            case EnuMode.SAMEARTIST:
                            AnyGridItemInfo anyGridItem = (AnyGridItemInfo) grid[sender.Position.Row, 0].Tag;
                            LinearAudioPlayer.PlayController.skipPlayingList((long)anyGridItem.Value);
                            LinearAudioPlayer.PlayController.play((long) anyGridItem.Value, false, true);
                            break;

                            case EnuMode.SIMILARARTIST:
                            LinearGlobal.MainForm.ListForm.setFilteringText(grid[sender.Position.Row, 0].Value.ToString());
                            break;
                    }
                    

                    
                    //LinearGlobal.MainForm.ListForm.setGroupMode();

                    //LinearAudioPlayer.FilteringGridController.Grid.Focus();
                }
            }

            public override void OnDoubleClick(CellContext sender, EventArgs e)
            {
                base.OnDoubleClick(sender, e);
                LinearAudioPlayer.PlayController.playForGridNo(
                    1, false);
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
