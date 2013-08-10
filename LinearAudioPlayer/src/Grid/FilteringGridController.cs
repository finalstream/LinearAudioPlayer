using System;
using System.Collections.Generic;
using FINALSTREAM.Commons.Grid;
using FINALSTREAM.LinearAudioPlayer.Setting;
using SourceGrid;
using SourceGrid.Cells;
using System.Windows.Forms;
using System.Drawing;
using FINALSTREAM.LinearAudioPlayer.Info;
using BorderStyle = System.Windows.Forms.BorderStyle;


namespace FINALSTREAM.LinearAudioPlayer.Grid
{
    class FilteringGridController : SourceGridController
    {
        /*
         * パブリックメンバ
         */
        #region PublicMember

        public static readonly int[] COLUMN_HEADER_WIDTHS = new int[] { 230};

        public int LastSelectRowNo = -1;

        public FilteringGridController(SourceGrid.Grid grid) : base(grid)
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
            FILTERING = 0
        }

        public enum EnuPlayType : int
        {
            NOPLAY,
            PLAYING
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
            cellView = new CellBackColorAlternate(
                colorConfig.FirstRowBackgroundColor,
                colorConfig.SecondRowBackgroundColor);
            cellView.ForeColor = colorConfig.NoPlayColor;
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
            //DevAge.Drawing.VisualElements.ColumnHeader headerBackground = new DevAge.Drawing.VisualElements.ColumnHeader();
            //headerBackground.BackColor = colorConfig.HeaderBackgroundColor;
            //headerBackground.Border = DevAge.Drawing.RectangleBorder.NoBorder;
           
            //headerBackground.BackgroundColorStyle = BackgroundColorStyle.Solid;
            //headerBackground.Style = ControlDrawStyle.Hot;

            //headerBackground.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            //SourceGrid.Cells.Views.ColumnHeader headerView = new SourceGrid.Cells.Views.ColumnHeader();

            //headerView.Background = headerBackground;
            //headerView.ForeColor = colorConfig.HeaderFontColor;


            // SourceGrid用イベント初期化
            //ColumnHeaderClickEvent columnHeaderClickEvent = new ColumnHeaderClickEvent();
            //SourceGrid.Cells.Controllers.SortableHeader sortableHeader = new SourceGrid.Cells.Controllers.SortableHeader();
            // ヘッダビューをセット
            //this.setHeaderView(0, headerView);
        }

        protected override void setHeader()
        {
            //setHeader(LinearGlobal.ColorConfig);
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
            Grid.BorderStyle = BorderStyle.Fixed3D;
            Grid.Selection.EnableMultiSelection = false;
            Grid.DefaultHeight = (int)(TextRenderer.MeasureText("X", Grid.Font).Height * 1.5);

            setSelectionConfig();

            // グリッドの列数
            Grid.ColumnsCount = getColumnHeaderWidths().Length;
            //Grid.FixedRows = 1;

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
            ConditionGridItemInfo gi = (ConditionGridItemInfo)sgi;
            
            

            // ToolTip Controller
            
            //toolTipController.ToolTipTitle = gi.Title;

            // ID用
            Grid[i, (int)EnuGrid.FILTERING] = new Cell(gi.DisplayValue);
            Grid[i, (int) EnuGrid.FILTERING].Tag = gi;
            Grid[i, (int)EnuGrid.FILTERING].AddController(clickController);

            if (i == 0)
            {
                Grid[i, (int)EnuGrid.FILTERING].Column.Width = 255;
            }

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

            cii.DisplayValue = this.getValue(rowNo, (int)EnuGrid.FILTERING);
            cii.Value = Grid[rowNo, (int) EnuGrid.FILTERING].Tag.ToString();

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

        
        public bool isShowPlayingList()
        {
            bool result = false;

            IList<int> rows = getSelectRowNoList();

            if (rows.Count > 0)
            {
                ConditionGridItemInfo cgi = (ConditionGridItemInfo) Grid[rows[0], (int)EnuGrid.FILTERING].Tag;

                if (cgi.Value.ToLower().IndexOf("playinglist") >= 0)
                {
                    result = true;
                }
            }

            return result;
        }

        #endregion


        /*
            SourceGrid用イベントクラス 
        */
        #region SGEventClass


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
                    LinearGlobal.MainForm.ListForm.setFilteringText("");
                    SourceGrid.Grid grid = (SourceGrid.Grid) sender.Grid;

                    LinearAudioPlayer.FilteringGridController.setRowColor(
                        LinearAudioPlayer.FilteringGridController.LastSelectRowNo, EnuPlayType.NOPLAY);


                    LinearAudioPlayer.FilteringGridController.setRowColor(sender.Position.Row, EnuPlayType.PLAYING);
                    LinearAudioPlayer.GroupGridController.setRowColor(LinearAudioPlayer.GroupGridController.LastSelectRowNo, GroupGridController.EnuPlayType.NOPLAY);

                    LinearGlobal.LinearConfig.PlayerConfig.SelectFilter = grid[sender.Position.Row, 0].Value.ToString();

                    LinearGlobal.FilteringMode = LinearEnum.FilteringMode.DEFAULT;
                    //LinearGlobal.MainForm.ListForm.setGroupMode();
                    LinearGlobal.MainForm.ListForm.reloadDatabase(true);
                    LinearAudioPlayer.FilteringGridController.Grid.Focus();
                }
            }

            public override void OnDoubleClick(CellContext sender, EventArgs e)
            {
                base.OnDoubleClick(sender, e);
                LinearAudioPlayer.PlayController.playForGridNo(
                    1, false);
                LinearAudioPlayer.GridController.Grid.Selection.FocusFirstCell(true);
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
