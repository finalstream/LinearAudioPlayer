using System;
using System.Windows.Forms;
using FINALSTREAM.Commons.Controls;
using FINALSTREAM.Commons.Grid;


namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    partial class ListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.playlistContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathcopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathpasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionNormaltoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionFavoriteAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionExclusionAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionSpeciallistDelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileDeleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.SortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ディレクトリ直下ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.階層を保持ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picVolumeBar = new System.Windows.Forms.PictureBox();
            this.picVolumeSwitch = new System.Windows.Forms.PictureBox();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.gridInfo = new System.Windows.Forms.Label();
            this.picDatabase = new System.Windows.Forms.PictureBox();
            this.picPlaylistMode = new System.Windows.Forms.PictureBox();
            this.lblPlaylistMode = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.databaseContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createNewDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.renameDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.vacuumDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportWithoutArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cleardatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.picSpeaker = new System.Windows.Forms.PictureBox();
            this.areaSizeChangeHeight = new System.Windows.Forms.Label();
            this.picTag = new System.Windows.Forms.PictureBox();
            this.tagContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dragContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exclusionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileUpdateDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRadio = new System.Windows.Forms.Timer(this.components);
            this.lblArtist = new System.Windows.Forms.Label();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.picLimit = new System.Windows.Forms.PictureBox();
            this.picShuffle = new System.Windows.Forms.PictureBox();
            this.lblShuffle = new System.Windows.Forms.Label();
            this.lblLinkLibrary = new System.Windows.Forms.Label();
            this.filteringBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.registUserDicttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.filtercutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.コピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.貼り付けToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterclearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.並び替えToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countDescToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alphabetAscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picArtwork = new System.Windows.Forms.PictureBox();
            this.txtAlbumDescription = new System.Windows.Forms.TextBox();
            this.albumDescriptionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.adAllselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adCopyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.adPasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.adAmazonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ltArtist = new System.Windows.Forms.Label();
            this.labelArtist = new System.Windows.Forms.Label();
            this.ltTitle = new System.Windows.Forms.Label();
            this.labelAlbum = new System.Windows.Forms.Label();
            this.ltAlbum = new System.Windows.Forms.Label();
            this.ltLastfm = new System.Windows.Forms.Label();
            this.labelLastfm = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picLinkLibrary = new System.Windows.Forms.PictureBox();
            this.picMedley = new System.Windows.Forms.PictureBox();
            this.lblMedley = new System.Windows.Forms.Label();
            this.medleyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.startPositionDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPositionOpeningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPositionMiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPositionEndingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.playtimeDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playtimeShortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playtimeLongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playtimeHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridLink = new FINALSTREAM.Commons.Grid.SourceGridEx();
            this.gridGroup = new FINALSTREAM.Commons.Grid.SourceGridEx();
            this.gridFiltering = new FINALSTREAM.Commons.Grid.SourceGridEx();
            this.progressSeekBar = new FINALSTREAM.Commons.Controls.VistaProgressBar();
            this.grid = new FINALSTREAM.Commons.Grid.SourceGridEx();
            this.databaseList = new FINALSTREAM.Commons.Controls.ComboBoxEx();
            this.filteringBox = new FINALSTREAM.Commons.Controls.TextBoxEx();
            this.picMovie = new System.Windows.Forms.PictureBox();
            this.playlistContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVolumeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVolumeSwitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlaylistMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.databaseContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpeaker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTag)).BeginInit();
            this.dragContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShuffle)).BeginInit();
            this.filteringBoxContextMenuStrip.SuspendLayout();
            this.groupGridContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picArtwork)).BeginInit();
            this.albumDescriptionContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLinkLibrary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMedley)).BeginInit();
            this.medleyContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMovie)).BeginInit();
            this.SuspendLayout();
            // 
            // playlistContextMenuStrip
            // 
            this.playlistContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripSeparator4,
            this.addToolStripMenuItem,
            this.pathcopyToolStripMenuItem,
            this.pathpasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.selectionToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.editTagToolStripMenuItem,
            this.getTagToolStripMenuItem,
            this.toolStripSeparator7,
            this.SortToolStripMenuItem,
            this.toolStripMenuItem1,
            this.folderOpenToolStripMenuItem});
            this.playlistContextMenuStrip.Name = "contextMenuStrip";
            this.playlistContextMenuStrip.Size = new System.Drawing.Size(209, 314);
            this.playlistContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.playlistContextMenuStrip_Opening);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.playToolStripMenuItem.Text = "いますぐ再生する";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem2.Text = "次に再生する";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem3.Text = "次はここから再生する";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(205, 6);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addURLToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addToolStripMenuItem.Text = "追加";
            // 
            // addURLToolStripMenuItem
            // 
            this.addURLToolStripMenuItem.Name = "addURLToolStripMenuItem";
            this.addURLToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.addURLToolStripMenuItem.Text = "URL";
            this.addURLToolStripMenuItem.Click += new System.EventHandler(this.addURLToolStripMenuItem_Click);
            // 
            // pathcopyToolStripMenuItem
            // 
            this.pathcopyToolStripMenuItem.Name = "pathcopyToolStripMenuItem";
            this.pathcopyToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.pathcopyToolStripMenuItem.Text = "コピー";
            this.pathcopyToolStripMenuItem.Click += new System.EventHandler(this.pathcopyToolStripMenuItem_Click);
            // 
            // pathpasteToolStripMenuItem
            // 
            this.pathpasteToolStripMenuItem.Name = "pathpasteToolStripMenuItem";
            this.pathpasteToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.pathpasteToolStripMenuItem.Text = "貼り付け";
            this.pathpasteToolStripMenuItem.Click += new System.EventHandler(this.pathpasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectionNormaltoolStripMenuItem,
            this.selectionFavoriteAddToolStripMenuItem,
            this.selectionExclusionAddToolStripMenuItem,
            this.selectionSpeciallistDelToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.selectionToolStripMenuItem.Text = "セレクション";
            // 
            // selectionNormaltoolStripMenuItem
            // 
            this.selectionNormaltoolStripMenuItem.Name = "selectionNormaltoolStripMenuItem";
            this.selectionNormaltoolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.selectionNormaltoolStripMenuItem.Text = "通常に戻す";
            this.selectionNormaltoolStripMenuItem.Click += new System.EventHandler(this.selectionNormaltoolStripMenuItem_Click);
            // 
            // selectionFavoriteAddToolStripMenuItem
            // 
            this.selectionFavoriteAddToolStripMenuItem.Name = "selectionFavoriteAddToolStripMenuItem";
            this.selectionFavoriteAddToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.selectionFavoriteAddToolStripMenuItem.Text = "お気に入りに追加";
            this.selectionFavoriteAddToolStripMenuItem.Click += new System.EventHandler(this.selectionFavoriteAddToolStripMenuItem_Click);
            // 
            // selectionExclusionAddToolStripMenuItem
            // 
            this.selectionExclusionAddToolStripMenuItem.Name = "selectionExclusionAddToolStripMenuItem";
            this.selectionExclusionAddToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.selectionExclusionAddToolStripMenuItem.Text = "除外に追加";
            this.selectionExclusionAddToolStripMenuItem.Click += new System.EventHandler(this.selectionExclusionAddToolStripMenuItem_Click);
            // 
            // selectionSpeciallistDelToolStripMenuItem
            // 
            this.selectionSpeciallistDelToolStripMenuItem.Name = "selectionSpeciallistDelToolStripMenuItem";
            this.selectionSpeciallistDelToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.selectionSpeciallistDelToolStripMenuItem.Text = "XXXから削除";
            this.selectionSpeciallistDelToolStripMenuItem.Click += new System.EventHandler(this.selectionSpeciallistDelToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDeleteToolStripMenuItem,
            this.fileDeleteToolStripMenuItem1});
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.deleteToolStripMenuItem.Text = "削除";
            // 
            // selectDeleteToolStripMenuItem
            // 
            this.selectDeleteToolStripMenuItem.Name = "selectDeleteToolStripMenuItem";
            this.selectDeleteToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.selectDeleteToolStripMenuItem.Text = "リストから削除...";
            this.selectDeleteToolStripMenuItem.Click += new System.EventHandler(this.selectDeleteToolStripMenuItem_Click);
            // 
            // fileDeleteToolStripMenuItem1
            // 
            this.fileDeleteToolStripMenuItem1.Name = "fileDeleteToolStripMenuItem1";
            this.fileDeleteToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.fileDeleteToolStripMenuItem1.Text = "ファイルごと削除...";
            this.fileDeleteToolStripMenuItem1.Click += new System.EventHandler(this.fileDeleteToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // editTagToolStripMenuItem
            // 
            this.editTagToolStripMenuItem.Name = "editTagToolStripMenuItem";
            this.editTagToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.editTagToolStripMenuItem.Text = "オーディオタグ編集...";
            this.editTagToolStripMenuItem.Click += new System.EventHandler(this.editTagToolStripMenuItem_Click);
            // 
            // getTagToolStripMenuItem
            // 
            this.getTagToolStripMenuItem.Name = "getTagToolStripMenuItem";
            this.getTagToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.getTagToolStripMenuItem.Text = "オーディオ情報スキャン";
            this.getTagToolStripMenuItem.Click += new System.EventHandler(this.getTagToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // SortToolStripMenuItem
            // 
            this.SortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortDefaultToolStripMenuItem,
            this.sortManualToolStripMenuItem});
            this.SortToolStripMenuItem.Name = "SortToolStripMenuItem";
            this.SortToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.SortToolStripMenuItem.Text = "並び替え";
            // 
            // sortDefaultToolStripMenuItem
            // 
            this.sortDefaultToolStripMenuItem.Name = "sortDefaultToolStripMenuItem";
            this.sortDefaultToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.sortDefaultToolStripMenuItem.Text = "デフォルト";
            this.sortDefaultToolStripMenuItem.Click += new System.EventHandler(this.sortDefaultToolStripMenuItem_Click);
            // 
            // sortManualToolStripMenuItem
            // 
            this.sortManualToolStripMenuItem.Name = "sortManualToolStripMenuItem";
            this.sortManualToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.sortManualToolStripMenuItem.Text = "オリジナルソート";
            this.sortManualToolStripMenuItem.Click += new System.EventHandler(this.sortManualToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ディレクトリ直下ToolStripMenuItem,
            this.階層を保持ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem1.Text = "ファイルエクスポート";
            // 
            // ディレクトリ直下ToolStripMenuItem
            // 
            this.ディレクトリ直下ToolStripMenuItem.Name = "ディレクトリ直下ToolStripMenuItem";
            this.ディレクトリ直下ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.ディレクトリ直下ToolStripMenuItem.Text = "ディレクトリ直下に出力...";
            this.ディレクトリ直下ToolStripMenuItem.Click += new System.EventHandler(this.ディレクトリ直下ToolStripMenuItem_Click);
            // 
            // 階層を保持ToolStripMenuItem
            // 
            this.階層を保持ToolStripMenuItem.Name = "階層を保持ToolStripMenuItem";
            this.階層を保持ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.階層を保持ToolStripMenuItem.Text = "階層構造を保持して出力...";
            this.階層を保持ToolStripMenuItem.Click += new System.EventHandler(this.階層を保持ToolStripMenuItem_Click);
            // 
            // folderOpenToolStripMenuItem
            // 
            this.folderOpenToolStripMenuItem.Name = "folderOpenToolStripMenuItem";
            this.folderOpenToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.folderOpenToolStripMenuItem.Text = "格納フォルダを開く";
            this.folderOpenToolStripMenuItem.Click += new System.EventHandler(this.folderOpenToolStripMenuItem_Click);
            // 
            // picVolumeBar
            // 
            this.picVolumeBar.BackColor = System.Drawing.Color.Transparent;
            this.picVolumeBar.Location = new System.Drawing.Point(461, 289);
            this.picVolumeBar.Name = "picVolumeBar";
            this.picVolumeBar.Size = new System.Drawing.Size(114, 8);
            this.picVolumeBar.TabIndex = 2;
            this.picVolumeBar.TabStop = false;
            this.picVolumeBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picVolumeBar_MouseDown);
            // 
            // picVolumeSwitch
            // 
            this.picVolumeSwitch.BackColor = System.Drawing.Color.Transparent;
            this.picVolumeSwitch.Location = new System.Drawing.Point(422, 290);
            this.picVolumeSwitch.Name = "picVolumeSwitch";
            this.picVolumeSwitch.Size = new System.Drawing.Size(14, 8);
            this.picVolumeSwitch.TabIndex = 3;
            this.picVolumeSwitch.TabStop = false;
            this.picVolumeSwitch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picVolumeSwitch_MouseMove);
            this.picVolumeSwitch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picVolumeSwitch_MouseUp);
            // 
            // picSearch
            // 
            this.picSearch.Location = new System.Drawing.Point(466, 16);
            this.picSearch.Name = "picSearch";
            this.picSearch.Size = new System.Drawing.Size(16, 16);
            this.picSearch.TabIndex = 6;
            this.picSearch.TabStop = false;
            this.picSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSearch_MouseDown);
            // 
            // gridInfo
            // 
            this.gridInfo.BackColor = System.Drawing.Color.Transparent;
            this.gridInfo.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.gridInfo.Location = new System.Drawing.Point(12, 290);
            this.gridInfo.Name = "gridInfo";
            this.gridInfo.Size = new System.Drawing.Size(0, 13);
            this.gridInfo.TabIndex = 9;
            this.gridInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gridInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridInfo_MouseDoubleClick);
            // 
            // picDatabase
            // 
            this.picDatabase.Location = new System.Drawing.Point(12, 15);
            this.picDatabase.Name = "picDatabase";
            this.picDatabase.Size = new System.Drawing.Size(16, 16);
            this.picDatabase.TabIndex = 10;
            this.picDatabase.TabStop = false;
            this.picDatabase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDatabase_MouseDown);
            // 
            // picPlaylistMode
            // 
            this.picPlaylistMode.Location = new System.Drawing.Point(117, 15);
            this.picPlaylistMode.Name = "picPlaylistMode";
            this.picPlaylistMode.Size = new System.Drawing.Size(16, 16);
            this.picPlaylistMode.TabIndex = 12;
            this.picPlaylistMode.TabStop = false;
            this.picPlaylistMode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPlaylistMode_MouseDown);
            // 
            // lblPlaylistMode
            // 
            this.lblPlaylistMode.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPlaylistMode.Location = new System.Drawing.Point(139, 18);
            this.lblPlaylistMode.Name = "lblPlaylistMode";
            this.lblPlaylistMode.Size = new System.Drawing.Size(61, 13);
            this.lblPlaylistMode.TabIndex = 13;
            this.lblPlaylistMode.Text = "Normal";
            this.lblPlaylistMode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPlaylistMode_MouseDown);
            // 
            // picClose
            // 
            this.picClose.Location = new System.Drawing.Point(562, 15);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(16, 16);
            this.picClose.TabIndex = 14;
            this.picClose.TabStop = false;
            this.picClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseDown);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseMove);
            // 
            // databaseContextMenuStrip
            // 
            this.databaseContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewDatabase,
            this.copyDatabase,
            this.renameDatabase,
            this.vacuumDatabase,
            this.toolStripSeparator6,
            this.toolStripMenuItem4,
            this.toolStripSeparator3,
            this.cleardatabaseToolStripMenuItem,
            this.deleteDatabase});
            this.databaseContextMenuStrip.Name = "databaseContextMenuStrip";
            this.databaseContextMenuStrip.Size = new System.Drawing.Size(209, 170);
            // 
            // createNewDatabase
            // 
            this.createNewDatabase.Name = "createNewDatabase";
            this.createNewDatabase.Size = new System.Drawing.Size(208, 22);
            this.createNewDatabase.Text = "データベース新規作成...";
            this.createNewDatabase.Click += new System.EventHandler(this.createNewDatabase_Click);
            // 
            // copyDatabase
            // 
            this.copyDatabase.Name = "copyDatabase";
            this.copyDatabase.Size = new System.Drawing.Size(208, 22);
            this.copyDatabase.Text = "データベース複製...";
            this.copyDatabase.Click += new System.EventHandler(this.copyDatabase_Click);
            // 
            // renameDatabase
            // 
            this.renameDatabase.Name = "renameDatabase";
            this.renameDatabase.Size = new System.Drawing.Size(208, 22);
            this.renameDatabase.Text = "データベース名称変更...";
            this.renameDatabase.Click += new System.EventHandler(this.renameDatabase_Click);
            // 
            // vacuumDatabase
            // 
            this.vacuumDatabase.Name = "vacuumDatabase";
            this.vacuumDatabase.Size = new System.Drawing.Size(208, 22);
            this.vacuumDatabase.Text = "データベース最適化";
            this.vacuumDatabase.Click += new System.EventHandler(this.vacuumDatabase_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportWithoutArchiveToolStripMenuItem,
            this.exportPlaylistToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem4.Text = "ライブラリ";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.importToolStripMenuItem.Text = "データベースにインポート...";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportWithoutArchiveToolStripMenuItem
            // 
            this.exportWithoutArchiveToolStripMenuItem.Name = "exportWithoutArchiveToolStripMenuItem";
            this.exportWithoutArchiveToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.exportWithoutArchiveToolStripMenuItem.Text = "データベース内すべてエクスポート(アーカイブ以外)...";
            this.exportWithoutArchiveToolStripMenuItem.Click += new System.EventHandler(this.exportWithoutArchiveToolStripMenuItem_Click);
            // 
            // exportPlaylistToolStripMenuItem
            // 
            this.exportPlaylistToolStripMenuItem.Name = "exportPlaylistToolStripMenuItem";
            this.exportPlaylistToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.exportPlaylistToolStripMenuItem.Text = "リストに表示中のみエクスポート(アーカイブ以外)...";
            this.exportPlaylistToolStripMenuItem.Click += new System.EventHandler(this.exportPlaylistToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
            // 
            // cleardatabaseToolStripMenuItem
            // 
            this.cleardatabaseToolStripMenuItem.Name = "cleardatabaseToolStripMenuItem";
            this.cleardatabaseToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.cleardatabaseToolStripMenuItem.Text = "データベースクリア...";
            this.cleardatabaseToolStripMenuItem.Click += new System.EventHandler(this.cleardatabaseToolStripMenuItem_Click);
            // 
            // deleteDatabase
            // 
            this.deleteDatabase.Name = "deleteDatabase";
            this.deleteDatabase.Size = new System.Drawing.Size(208, 22);
            this.deleteDatabase.Text = "データベース削除...";
            this.deleteDatabase.Click += new System.EventHandler(this.deleteDatabase_Click);
            // 
            // picSpeaker
            // 
            this.picSpeaker.Location = new System.Drawing.Point(400, 286);
            this.picSpeaker.Name = "picSpeaker";
            this.picSpeaker.Size = new System.Drawing.Size(16, 16);
            this.picSpeaker.TabIndex = 15;
            this.picSpeaker.TabStop = false;
            this.picSpeaker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSpeaker_MouseDown);
            // 
            // areaSizeChangeHeight
            // 
            this.areaSizeChangeHeight.BackColor = System.Drawing.Color.Transparent;
            this.areaSizeChangeHeight.Location = new System.Drawing.Point(326, 286);
            this.areaSizeChangeHeight.Name = "areaSizeChangeHeight";
            this.areaSizeChangeHeight.Size = new System.Drawing.Size(8, 21);
            this.areaSizeChangeHeight.TabIndex = 19;
            this.areaSizeChangeHeight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.areaSizeChangeHeight_MouseDown);
            this.areaSizeChangeHeight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.areaSizeChangeHeight_MouseMove);
            // 
            // picTag
            // 
            this.picTag.Location = new System.Drawing.Point(444, 16);
            this.picTag.Name = "picTag";
            this.picTag.Size = new System.Drawing.Size(16, 16);
            this.picTag.TabIndex = 20;
            this.picTag.TabStop = false;
            this.picTag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTag_MouseDown);
            // 
            // tagContextMenuStrip
            // 
            this.tagContextMenuStrip.Name = "tagContextMenuStrip";
            this.tagContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // dragContextMenuStrip
            // 
            this.dragContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.exclusionToolStripMenuItem,
            this.fileUpdateDateToolStripMenuItem});
            this.dragContextMenuStrip.Name = "dragContextMenuStrip";
            this.dragContextMenuStrip.Size = new System.Drawing.Size(197, 70);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.normalToolStripMenuItem.Text = "すべて登録";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // exclusionToolStripMenuItem
            // 
            this.exclusionToolStripMenuItem.Name = "exclusionToolStripMenuItem";
            this.exclusionToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exclusionToolStripMenuItem.Text = "除外を除いて登録";
            this.exclusionToolStripMenuItem.Click += new System.EventHandler(this.exclusionToolStripMenuItem_Click);
            // 
            // fileUpdateDateToolStripMenuItem
            // 
            this.fileUpdateDateToolStripMenuItem.Name = "fileUpdateDateToolStripMenuItem";
            this.fileUpdateDateToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.fileUpdateDateToolStripMenuItem.Text = "ファイル更新日で登録";
            this.fileUpdateDateToolStripMenuItem.Click += new System.EventHandler(this.fileUpdateDateToolStripMenuItem_Click);
            // 
            // timerRadio
            // 
            this.timerRadio.Interval = 30000;
            this.timerRadio.Tick += new System.EventHandler(this.timerRadio_Tick);
            // 
            // lblArtist
            // 
            this.lblArtist.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblArtist.Location = new System.Drawing.Point(12, 164);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(47, 15);
            this.lblArtist.TabIndex = 25;
            this.lblArtist.Text = "Artist";
            this.lblArtist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblArtist_MouseDown);
            // 
            // lblAlbum
            // 
            this.lblAlbum.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblAlbum.Location = new System.Drawing.Point(64, 164);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(47, 15);
            this.lblAlbum.TabIndex = 26;
            this.lblAlbum.Text = "Album";
            this.lblAlbum.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblAlbum_MouseDown);
            // 
            // lblGenre
            // 
            this.lblGenre.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGenre.Location = new System.Drawing.Point(114, 164);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(47, 15);
            this.lblGenre.TabIndex = 27;
            this.lblGenre.Text = "Genre";
            this.lblGenre.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblGenre_MouseDown);
            // 
            // lblYear
            // 
            this.lblYear.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblYear.Location = new System.Drawing.Point(167, 164);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(37, 15);
            this.lblYear.TabIndex = 28;
            this.lblYear.Text = "Year";
            this.lblYear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblYear_MouseDown);
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTag.Location = new System.Drawing.Point(126, 148);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(33, 15);
            this.lblTag.TabIndex = 30;
            this.lblTag.Text = "Tag";
            this.lblTag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTag_MouseDown);
            // 
            // lblFolder
            // 
            this.lblFolder.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblFolder.Location = new System.Drawing.Point(165, 148);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(47, 15);
            this.lblFolder.TabIndex = 31;
            this.lblFolder.Text = "Folder";
            this.lblFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblFolder_MouseDown);
            // 
            // picLimit
            // 
            this.picLimit.Location = new System.Drawing.Point(188, 15);
            this.picLimit.Name = "picLimit";
            this.picLimit.Size = new System.Drawing.Size(16, 16);
            this.picLimit.TabIndex = 33;
            this.picLimit.TabStop = false;
            this.picLimit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSetting_MouseDown);
            // 
            // picShuffle
            // 
            this.picShuffle.Location = new System.Drawing.Point(206, 16);
            this.picShuffle.Name = "picShuffle";
            this.picShuffle.Size = new System.Drawing.Size(16, 16);
            this.picShuffle.TabIndex = 34;
            this.picShuffle.TabStop = false;
            // 
            // lblShuffle
            // 
            this.lblShuffle.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblShuffle.Location = new System.Drawing.Point(228, 18);
            this.lblShuffle.Name = "lblShuffle";
            this.lblShuffle.Size = new System.Drawing.Size(59, 15);
            this.lblShuffle.TabIndex = 35;
            this.lblShuffle.Text = "Shuffle";
            this.lblShuffle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblShuffle_MouseDown);
            // 
            // lblLinkLibrary
            // 
            this.lblLinkLibrary.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLinkLibrary.Location = new System.Drawing.Point(356, 15);
            this.lblLinkLibrary.Name = "lblLinkLibrary";
            this.lblLinkLibrary.Size = new System.Drawing.Size(82, 16);
            this.lblLinkLibrary.TabIndex = 36;
            this.lblLinkLibrary.Text = "LinkLibrary";
            this.lblLinkLibrary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblLinkLibrary_MouseDown);
            // 
            // filteringBoxContextMenuStrip
            // 
            this.filteringBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registUserDicttoolStripMenuItem,
            this.toolStripSeparator5,
            this.filtercutToolStripMenuItem,
            this.コピーToolStripMenuItem,
            this.貼り付けToolStripMenuItem,
            this.filterclearToolStripMenuItem});
            this.filteringBoxContextMenuStrip.Name = "filteringBoxContextMenuStrip";
            this.filteringBoxContextMenuStrip.Size = new System.Drawing.Size(173, 120);
            // 
            // registUserDicttoolStripMenuItem
            // 
            this.registUserDicttoolStripMenuItem.Name = "registUserDicttoolStripMenuItem";
            this.registUserDicttoolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.registUserDicttoolStripMenuItem.Text = "ユーザ辞書登録...";
            this.registUserDicttoolStripMenuItem.Click += new System.EventHandler(this.registUserDicttoolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
            // 
            // filtercutToolStripMenuItem
            // 
            this.filtercutToolStripMenuItem.Name = "filtercutToolStripMenuItem";
            this.filtercutToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.filtercutToolStripMenuItem.Text = "切り取り";
            this.filtercutToolStripMenuItem.Click += new System.EventHandler(this.filtercutToolStripMenuItem_Click);
            // 
            // コピーToolStripMenuItem
            // 
            this.コピーToolStripMenuItem.Name = "コピーToolStripMenuItem";
            this.コピーToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.コピーToolStripMenuItem.Text = "コピー";
            this.コピーToolStripMenuItem.Click += new System.EventHandler(this.filtercopyToolStripMenuItem_Click);
            // 
            // 貼り付けToolStripMenuItem
            // 
            this.貼り付けToolStripMenuItem.Name = "貼り付けToolStripMenuItem";
            this.貼り付けToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.貼り付けToolStripMenuItem.Text = "貼り付け";
            this.貼り付けToolStripMenuItem.Click += new System.EventHandler(this.filterpasteToolStripMenuItem_Click);
            // 
            // filterclearToolStripMenuItem
            // 
            this.filterclearToolStripMenuItem.Name = "filterclearToolStripMenuItem";
            this.filterclearToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.filterclearToolStripMenuItem.Text = "クリア";
            this.filterclearToolStripMenuItem.Click += new System.EventHandler(this.filterclearToolStripMenuItem_Click);
            // 
            // groupGridContextMenuStrip
            // 
            this.groupGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.並び替えToolStripMenuItem});
            this.groupGridContextMenuStrip.Name = "groupGridContextMenuStrip";
            this.groupGridContextMenuStrip.Size = new System.Drawing.Size(125, 26);
            this.groupGridContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.groupGridContextMenuStrip_Opening);
            // 
            // 並び替えToolStripMenuItem
            // 
            this.並び替えToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.countDescToolStripMenuItem,
            this.alphabetAscToolStripMenuItem});
            this.並び替えToolStripMenuItem.Name = "並び替えToolStripMenuItem";
            this.並び替えToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.並び替えToolStripMenuItem.Text = "並び替え";
            // 
            // countDescToolStripMenuItem
            // 
            this.countDescToolStripMenuItem.Name = "countDescToolStripMenuItem";
            this.countDescToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.countDescToolStripMenuItem.Text = "オリジナル";
            this.countDescToolStripMenuItem.Click += new System.EventHandler(this.countDescToolStripMenuItem_Click);
            // 
            // alphabetAscToolStripMenuItem
            // 
            this.alphabetAscToolStripMenuItem.Name = "alphabetAscToolStripMenuItem";
            this.alphabetAscToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.alphabetAscToolStripMenuItem.Text = "名前の昇順";
            this.alphabetAscToolStripMenuItem.Click += new System.EventHandler(this.alphabetAscToolStripMenuItem_Click);
            // 
            // picArtwork
            // 
            this.picArtwork.BackColor = System.Drawing.Color.Transparent;
            this.picArtwork.Location = new System.Drawing.Point(199, 47);
            this.picArtwork.Name = "picArtwork";
            this.picArtwork.Size = new System.Drawing.Size(150, 150);
            this.picArtwork.TabIndex = 39;
            this.picArtwork.TabStop = false;
            this.picArtwork.Visible = false;
            this.picArtwork.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picArtwork_MouseDown);
            // 
            // txtAlbumDescription
            // 
            this.txtAlbumDescription.BackColor = System.Drawing.Color.White;
            this.txtAlbumDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAlbumDescription.ContextMenuStrip = this.albumDescriptionContextMenuStrip;
            this.txtAlbumDescription.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtAlbumDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAlbumDescription.Location = new System.Drawing.Point(321, 132);
            this.txtAlbumDescription.Multiline = true;
            this.txtAlbumDescription.Name = "txtAlbumDescription";
            this.txtAlbumDescription.Size = new System.Drawing.Size(167, 31);
            this.txtAlbumDescription.TabIndex = 40;
            this.txtAlbumDescription.Visible = false;
            this.txtAlbumDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAlbumDescription_KeyDown);
            this.txtAlbumDescription.Leave += new System.EventHandler(this.txtAlbumDescription_Leave);
            this.txtAlbumDescription.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.txtAlbumDescription_MouseWheel);
            // 
            // albumDescriptionContextMenuStrip
            // 
            this.albumDescriptionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adAllselectToolStripMenuItem,
            this.adCopyToolStripMenuItem1,
            this.adPasteToolStripMenuItem1,
            this.toolStripSeparator8,
            this.adAmazonToolStripMenuItem});
            this.albumDescriptionContextMenuStrip.Name = "albumDescriptionContextMenuStrip";
            this.albumDescriptionContextMenuStrip.Size = new System.Drawing.Size(148, 98);
            // 
            // adAllselectToolStripMenuItem
            // 
            this.adAllselectToolStripMenuItem.Name = "adAllselectToolStripMenuItem";
            this.adAllselectToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.adAllselectToolStripMenuItem.Text = "すべて選択";
            this.adAllselectToolStripMenuItem.Click += new System.EventHandler(this.adAllselectToolStripMenuItem_Click);
            // 
            // adCopyToolStripMenuItem1
            // 
            this.adCopyToolStripMenuItem1.Name = "adCopyToolStripMenuItem1";
            this.adCopyToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.adCopyToolStripMenuItem1.Text = "コピー";
            this.adCopyToolStripMenuItem1.Click += new System.EventHandler(this.adCopyToolStripMenuItem1_Click);
            // 
            // adPasteToolStripMenuItem1
            // 
            this.adPasteToolStripMenuItem1.Name = "adPasteToolStripMenuItem1";
            this.adPasteToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.adPasteToolStripMenuItem1.Text = "貼り付け";
            this.adPasteToolStripMenuItem1.Click += new System.EventHandler(this.adPasteToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(144, 6);
            // 
            // adAmazonToolStripMenuItem
            // 
            this.adAmazonToolStripMenuItem.Name = "adAmazonToolStripMenuItem";
            this.adAmazonToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.adAmazonToolStripMenuItem.Text = "Amazon検索";
            this.adAmazonToolStripMenuItem.Click += new System.EventHandler(this.adAmazonToolStripMenuItem_Click);
            // 
            // ltArtist
            // 
            this.ltArtist.AutoSize = true;
            this.ltArtist.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ltArtist.Location = new System.Drawing.Point(324, 67);
            this.ltArtist.Name = "ltArtist";
            this.ltArtist.Size = new System.Drawing.Size(64, 20);
            this.ltArtist.TabIndex = 41;
            this.ltArtist.Text = " Artist   ";
            this.ltArtist.Visible = false;
            // 
            // labelArtist
            // 
            this.labelArtist.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelArtist.Location = new System.Drawing.Point(385, 67);
            this.labelArtist.Name = "labelArtist";
            this.labelArtist.Size = new System.Drawing.Size(13, 20);
            this.labelArtist.TabIndex = 42;
            this.labelArtist.Text = " ";
            this.labelArtist.Visible = false;
            this.labelArtist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelArtist_MouseDown);
            // 
            // ltTitle
            // 
            this.ltTitle.AutoSize = true;
            this.ltTitle.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ltTitle.Location = new System.Drawing.Point(324, 47);
            this.ltTitle.Name = "ltTitle";
            this.ltTitle.Size = new System.Drawing.Size(64, 20);
            this.ltTitle.TabIndex = 43;
            this.ltTitle.Text = "   Title   ";
            this.ltTitle.Visible = false;
            // 
            // labelAlbum
            // 
            this.labelAlbum.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelAlbum.Location = new System.Drawing.Point(385, 87);
            this.labelAlbum.Name = "labelAlbum";
            this.labelAlbum.Size = new System.Drawing.Size(13, 20);
            this.labelAlbum.TabIndex = 46;
            this.labelAlbum.Text = " ";
            this.labelAlbum.Visible = false;
            this.labelAlbum.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelAlbum_MouseDown);
            // 
            // ltAlbum
            // 
            this.ltAlbum.AutoSize = true;
            this.ltAlbum.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ltAlbum.Location = new System.Drawing.Point(322, 87);
            this.ltAlbum.Name = "ltAlbum";
            this.ltAlbum.Size = new System.Drawing.Size(66, 20);
            this.ltAlbum.TabIndex = 45;
            this.ltAlbum.Text = "Album   ";
            this.ltAlbum.Visible = false;
            // 
            // ltLastfm
            // 
            this.ltLastfm.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ltLastfm.Location = new System.Drawing.Point(315, 107);
            this.ltLastfm.Name = "ltLastfm";
            this.ltLastfm.Size = new System.Drawing.Size(74, 20);
            this.ltLastfm.TabIndex = 47;
            this.ltLastfm.Text = "   ";
            this.ltLastfm.Visible = false;
            this.ltLastfm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ltLastfm_MouseDown);
            // 
            // labelLastfm
            // 
            this.labelLastfm.AutoSize = true;
            this.labelLastfm.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelLastfm.Location = new System.Drawing.Point(385, 109);
            this.labelLastfm.Name = "labelLastfm";
            this.labelLastfm.Size = new System.Drawing.Size(13, 20);
            this.labelLastfm.TabIndex = 48;
            this.labelLastfm.Text = " ";
            this.labelLastfm.Visible = false;
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTitle.Location = new System.Drawing.Point(385, 47);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(13, 20);
            this.labelTitle.TabIndex = 50;
            this.labelTitle.Text = " ";
            this.labelTitle.Visible = false;
            // 
            // picLinkLibrary
            // 
            this.picLinkLibrary.Location = new System.Drawing.Point(344, 14);
            this.picLinkLibrary.Name = "picLinkLibrary";
            this.picLinkLibrary.Size = new System.Drawing.Size(16, 16);
            this.picLinkLibrary.TabIndex = 51;
            this.picLinkLibrary.TabStop = false;
            // 
            // picMedley
            // 
            this.picMedley.Location = new System.Drawing.Point(281, 18);
            this.picMedley.Name = "picMedley";
            this.picMedley.Size = new System.Drawing.Size(16, 16);
            this.picMedley.TabIndex = 52;
            this.picMedley.TabStop = false;
            // 
            // lblMedley
            // 
            this.lblMedley.ContextMenuStrip = this.medleyContextMenuStrip;
            this.lblMedley.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblMedley.Location = new System.Drawing.Point(293, 18);
            this.lblMedley.Name = "lblMedley";
            this.lblMedley.Size = new System.Drawing.Size(59, 15);
            this.lblMedley.TabIndex = 53;
            this.lblMedley.Text = "Medley";
            this.lblMedley.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblMedley_MouseDown);
            // 
            // medleyContextMenuStrip
            // 
            this.medleyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem10});
            this.medleyContextMenuStrip.Name = "medleyContextMenuStrip";
            this.medleyContextMenuStrip.Size = new System.Drawing.Size(149, 48);
            this.medleyContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.medleyContextMenuStrip_Opening);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startPositionDefaultToolStripMenuItem,
            this.startPositionOpeningToolStripMenuItem,
            this.startPositionMiddleToolStripMenuItem,
            this.startPositionEndingToolStripMenuItem});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem5.Text = "スタート位置";
            // 
            // startPositionDefaultToolStripMenuItem
            // 
            this.startPositionDefaultToolStripMenuItem.Name = "startPositionDefaultToolStripMenuItem";
            this.startPositionDefaultToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.startPositionDefaultToolStripMenuItem.Text = "デフォルト(15-50%)";
            this.startPositionDefaultToolStripMenuItem.Click += new System.EventHandler(this.startPositionDefaultToolStripMenuItem_Click);
            // 
            // startPositionOpeningToolStripMenuItem
            // 
            this.startPositionOpeningToolStripMenuItem.Name = "startPositionOpeningToolStripMenuItem";
            this.startPositionOpeningToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.startPositionOpeningToolStripMenuItem.Text = "序盤(10-30%)";
            this.startPositionOpeningToolStripMenuItem.Click += new System.EventHandler(this.startPositionOpeningToolStripMenuItem_Click);
            // 
            // startPositionMiddleToolStripMenuItem
            // 
            this.startPositionMiddleToolStripMenuItem.Name = "startPositionMiddleToolStripMenuItem";
            this.startPositionMiddleToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.startPositionMiddleToolStripMenuItem.Text = "中盤(40-60%)";
            this.startPositionMiddleToolStripMenuItem.Click += new System.EventHandler(this.startPositionMiddleToolStripMenuItem_Click);
            // 
            // startPositionEndingToolStripMenuItem
            // 
            this.startPositionEndingToolStripMenuItem.Name = "startPositionEndingToolStripMenuItem";
            this.startPositionEndingToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.startPositionEndingToolStripMenuItem.Text = "終盤(60-80%)";
            this.startPositionEndingToolStripMenuItem.Click += new System.EventHandler(this.startPositionEndingToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playtimeDefaultToolStripMenuItem,
            this.playtimeShortToolStripMenuItem,
            this.playtimeLongToolStripMenuItem,
            this.playtimeHalfToolStripMenuItem});
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem10.Text = "再生時間";
            // 
            // playtimeDefaultToolStripMenuItem
            // 
            this.playtimeDefaultToolStripMenuItem.Name = "playtimeDefaultToolStripMenuItem";
            this.playtimeDefaultToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.playtimeDefaultToolStripMenuItem.Text = "デフォルト(15-30%)";
            this.playtimeDefaultToolStripMenuItem.Click += new System.EventHandler(this.playtimeDefaultToolStripMenuItem_Click);
            // 
            // playtimeShortToolStripMenuItem
            // 
            this.playtimeShortToolStripMenuItem.Name = "playtimeShortToolStripMenuItem";
            this.playtimeShortToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.playtimeShortToolStripMenuItem.Text = "ショート(5-10%)";
            this.playtimeShortToolStripMenuItem.Click += new System.EventHandler(this.playtimeShortToolStripMenuItem_Click);
            // 
            // playtimeLongToolStripMenuItem
            // 
            this.playtimeLongToolStripMenuItem.Name = "playtimeLongToolStripMenuItem";
            this.playtimeLongToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.playtimeLongToolStripMenuItem.Text = "ロング(30-50%)";
            this.playtimeLongToolStripMenuItem.Click += new System.EventHandler(this.playtimeLongToolStripMenuItem_Click);
            // 
            // playtimeHalfToolStripMenuItem
            // 
            this.playtimeHalfToolStripMenuItem.Name = "playtimeHalfToolStripMenuItem";
            this.playtimeHalfToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.playtimeHalfToolStripMenuItem.Text = "ハーフ(50-70%)";
            this.playtimeHalfToolStripMenuItem.Click += new System.EventHandler(this.playtimeHalfToolStripMenuItem_Click);
            // 
            // gridLink
            // 
            this.gridLink.AllowDrop = true;
            this.gridLink.BackColor = System.Drawing.SystemColors.Window;
            this.gridLink.BorderColor = System.Drawing.Color.Black;
            this.gridLink.EnableSort = true;
            this.gridLink.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gridLink.IsInnerDrag = true;
            this.gridLink.Location = new System.Drawing.Point(494, 47);
            this.gridLink.Name = "gridLink";
            this.gridLink.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridLink.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridLink.Size = new System.Drawing.Size(80, 125);
            this.gridLink.TabIndex = 49;
            this.gridLink.TabStop = true;
            this.gridLink.ToolTipText = "";
            this.gridLink.Visible = false;
            // 
            // gridGroup
            // 
            this.gridGroup.AllowDrop = true;
            this.gridGroup.BackColor = System.Drawing.SystemColors.Window;
            this.gridGroup.BorderColor = System.Drawing.Color.Black;
            this.gridGroup.ContextMenuStrip = this.groupGridContextMenuStrip;
            this.gridGroup.EnableSort = true;
            this.gridGroup.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gridGroup.IsInnerDrag = false;
            this.gridGroup.Location = new System.Drawing.Point(12, 182);
            this.gridGroup.Name = "gridGroup";
            this.gridGroup.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridGroup.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridGroup.Size = new System.Drawing.Size(181, 83);
            this.gridGroup.TabIndex = 24;
            this.gridGroup.TabStop = true;
            this.gridGroup.ToolTipText = "";
            // 
            // gridFiltering
            // 
            this.gridFiltering.AllowDrop = true;
            this.gridFiltering.BackColor = System.Drawing.SystemColors.Window;
            this.gridFiltering.BorderColor = System.Drawing.Color.Black;
            this.gridFiltering.EnableSort = true;
            this.gridFiltering.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gridFiltering.IsInnerDrag = false;
            this.gridFiltering.Location = new System.Drawing.Point(12, 47);
            this.gridFiltering.Name = "gridFiltering";
            this.gridFiltering.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridFiltering.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridFiltering.Size = new System.Drawing.Size(181, 98);
            this.gridFiltering.TabIndex = 23;
            this.gridFiltering.TabStop = true;
            this.gridFiltering.ToolTipText = "";
            // 
            // progressSeekBar
            // 
            this.progressSeekBar.BackColor = System.Drawing.Color.Transparent;
            this.progressSeekBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.progressSeekBar.Location = new System.Drawing.Point(15, 271);
            this.progressSeekBar.Maximum = 0F;
            this.progressSeekBar.Name = "progressSeekBar";
            this.progressSeekBar.ProgressBarMainBottomActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(82)))), ((int)(((byte)(0)))));
            this.progressSeekBar.ProgressBarMainBottomBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.progressSeekBar.ProgressBarMainUnderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.progressSeekBar.ProgressBarMainUnderBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.progressSeekBar.ProgressBarUpBottomActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(129)))), ((int)(((byte)(38)))));
            this.progressSeekBar.ProgressBarUpBottomBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.progressSeekBar.ProgressBarUpUnderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(155)))), ((int)(((byte)(84)))));
            this.progressSeekBar.ProgressBarUpUnderBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.progressSeekBar.Size = new System.Drawing.Size(559, 10);
            this.progressSeekBar.TabIndex = 21;
            this.progressSeekBar.Theme = FINALSTREAM.Commons.Controls.VistaProgressBarTheme.Orange;
            this.progressSeekBar.Value = 0F;
            this.progressSeekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressSeekBar_MouseDown);
            // 
            // grid
            // 
            this.grid.AllowDrop = true;
            this.grid.BackColor = System.Drawing.SystemColors.Window;
            this.grid.BorderColor = System.Drawing.Color.Black;
            this.grid.ContextMenuStrip = this.playlistContextMenuStrip;
            this.grid.EnableSort = true;
            this.grid.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grid.IsInnerDrag = true;
            this.grid.Location = new System.Drawing.Point(199, 182);
            this.grid.Name = "grid";
            this.grid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.grid.Size = new System.Drawing.Size(376, 83);
            this.grid.TabIndex = 0;
            this.grid.TabStop = true;
            this.grid.ToolTipText = "";
            this.grid.DragDrop += new System.Windows.Forms.DragEventHandler(this.grid_DragDrop);
            this.grid.DragEnter += new System.Windows.Forms.DragEventHandler(this.grid_DragEnter);
            this.grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grid_MouseDown);
            // 
            // databaseList
            // 
            this.databaseList.BackColor = System.Drawing.Color.White;
            this.databaseList.Color1 = System.Drawing.Color.White;
            this.databaseList.Color2 = System.Drawing.Color.Gainsboro;
            this.databaseList.Color3 = System.Drawing.Color.White;
            this.databaseList.Color4 = System.Drawing.Color.PaleGoldenrod;
            this.databaseList.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.databaseList.DropDownHeight = 200;
            this.databaseList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseList.DropDownWidth = 77;
            this.databaseList.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.databaseList.IsDroppedDown = false;
            this.databaseList.Location = new System.Drawing.Point(34, 11);
            this.databaseList.MaxDropDownItems = 8;
            this.databaseList.Name = "databaseList";
            this.databaseList.SelectedIndex = -1;
            this.databaseList.SelectedItem = null;
            this.databaseList.Size = new System.Drawing.Size(77, 26);
            this.databaseList.Soreted = false;
            this.databaseList.TabIndex = 4;
            this.databaseList.SelectedIndexChanged += new System.EventHandler(this.databaseList_SelectedIndexChanged);
            // 
            // filteringBox
            // 
            this.filteringBox.BorderColor = System.Drawing.Color.Black;
            this.filteringBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filteringBox.ContextMenuStrip = this.filteringBoxContextMenuStrip;
            this.filteringBox.Font = new System.Drawing.Font("メイリオ", 9F);
            this.filteringBox.ForeColor = System.Drawing.Color.LightGray;
            this.filteringBox.Location = new System.Drawing.Point(488, 11);
            this.filteringBox.Name = "filteringBox";
            this.filteringBox.Size = new System.Drawing.Size(68, 18);
            this.filteringBox.TabIndex = 4;
            this.filteringBox.TextChanged += new System.EventHandler(this.filteringBox_TextChanged);
            this.filteringBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.filteringBox_MouseDoubleClick);
            // 
            // picMovie
            // 
            this.picMovie.Location = new System.Drawing.Point(488, 21);
            this.picMovie.Name = "picMovie";
            this.picMovie.Size = new System.Drawing.Size(16, 16);
            this.picMovie.TabIndex = 54;
            this.picMovie.TabStop = false;
            this.picMovie.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMovie_MouseDown);
            // 
            // ListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(587, 306);
            this.Controls.Add(this.picMovie);
            this.Controls.Add(this.lblMedley);
            this.Controls.Add(this.picMedley);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.gridLink);
            this.Controls.Add(this.labelLastfm);
            this.Controls.Add(this.ltLastfm);
            this.Controls.Add(this.labelAlbum);
            this.Controls.Add(this.ltAlbum);
            this.Controls.Add(this.picLinkLibrary);
            this.Controls.Add(this.ltTitle);
            this.Controls.Add(this.labelArtist);
            this.Controls.Add(this.ltArtist);
            this.Controls.Add(this.picArtwork);
            this.Controls.Add(this.txtAlbumDescription);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.lblAlbum);
            this.Controls.Add(this.picShuffle);
            this.Controls.Add(this.lblArtist);
            this.Controls.Add(this.gridGroup);
            this.Controls.Add(this.gridFiltering);
            this.Controls.Add(this.progressSeekBar);
            this.Controls.Add(this.areaSizeChangeHeight);
            this.Controls.Add(this.lblShuffle);
            this.Controls.Add(this.lblLinkLibrary);
            this.Controls.Add(this.picSpeaker);
            this.Controls.Add(this.picPlaylistMode);
            this.Controls.Add(this.picLimit);
            this.Controls.Add(this.picDatabase);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.lblPlaylistMode);
            this.Controls.Add(this.gridInfo);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.picVolumeSwitch);
            this.Controls.Add(this.databaseList);
            this.Controls.Add(this.picVolumeBar);
            this.Controls.Add(this.filteringBox);
            this.Controls.Add(this.picTag);
            this.Controls.Add(this.picSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ListForm";
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.Resize += new System.EventHandler(this.ListForm_Resize);
            this.playlistContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVolumeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVolumeSwitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlaylistMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.databaseContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSpeaker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTag)).EndInit();
            this.dragContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShuffle)).EndInit();
            this.filteringBoxContextMenuStrip.ResumeLayout(false);
            this.groupGridContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picArtwork)).EndInit();
            this.albumDescriptionContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLinkLibrary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMedley)).EndInit();
            this.medleyContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMovie)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SourceGridEx grid;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private PictureBox picVolumeBar;
        private PictureBox picVolumeSwitch;
        private ToolStripMenuItem selectDeleteToolStripMenuItem;
        private TextBoxEx filteringBox;
        private PictureBox picSearch;
        private Label gridInfo;
        private PictureBox picDatabase;
        private PictureBox picPlaylistMode;
        private ToolStripMenuItem selectionToolStripMenuItem;
        private ToolStripMenuItem selectionExclusionAddToolStripMenuItem;
        private Label lblPlaylistMode;
        private PictureBox picClose;
        public ToolStripMenuItem selectionFavoriteAddToolStripMenuItem;
        private ToolStripMenuItem selectionSpeciallistDelToolStripMenuItem;
        private ToolStripMenuItem getTagToolStripMenuItem;
        private ContextMenuStrip databaseContextMenuStrip;
        private ToolStripMenuItem createNewDatabase;
        private ToolStripMenuItem renameDatabase;
        private ToolStripMenuItem deleteDatabase;
        private ToolStripMenuItem copyDatabase;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem editTagToolStripMenuItem;
        private PictureBox picSpeaker;
        public ContextMenuStrip playlistContextMenuStrip;
        public Label areaSizeChangeHeight;
        private PictureBox picTag;
        private ContextMenuStrip tagContextMenuStrip;
        private ToolStripMenuItem vacuumDatabase;
        public Commons.Controls.VistaProgressBar progressSeekBar;
        private ContextMenuStrip dragContextMenuStrip;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripMenuItem exclusionToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem addURLToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private Timer timerRadio;
        private ToolStripMenuItem pathcopyToolStripMenuItem;
        private ToolStripMenuItem pathpasteToolStripMenuItem;
        private ToolStripMenuItem fileDeleteToolStripMenuItem1;
        private ToolStripMenuItem fileUpdateDateToolStripMenuItem;
        private Commons.Controls.ComboBoxEx databaseList;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cleardatabaseToolStripMenuItem;
        private ToolStripMenuItem selectionNormaltoolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem ディレクトリ直下ToolStripMenuItem;
        private ToolStripMenuItem 階層を保持ToolStripMenuItem;
        private SourceGridEx gridFiltering;
        private SourceGridEx gridGroup;
        private Label lblArtist;
        private Label lblAlbum;
        private Label lblGenre;
        private Label lblYear;
        private Label lblTag;
        private Label lblFolder;
        private PictureBox picLimit;
        private PictureBox picShuffle;
        private Label lblShuffle;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator4;
        private Label lblLinkLibrary;
        private ContextMenuStrip filteringBoxContextMenuStrip;
        private ToolStripMenuItem registUserDicttoolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem filtercutToolStripMenuItem;
        private ToolStripMenuItem コピーToolStripMenuItem;
        private ToolStripMenuItem 貼り付けToolStripMenuItem;
        private ToolStripMenuItem filterclearToolStripMenuItem;
        private ContextMenuStrip groupGridContextMenuStrip;
        private ToolStripMenuItem 並び替えToolStripMenuItem;
        private ToolStripMenuItem countDescToolStripMenuItem;
        private ToolStripMenuItem alphabetAscToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportWithoutArchiveToolStripMenuItem;
        private ToolStripMenuItem SortToolStripMenuItem;
        private ToolStripMenuItem sortDefaultToolStripMenuItem;
        private ToolStripMenuItem sortManualToolStripMenuItem;
        private PictureBox picArtwork;
        private Label ltArtist;
        private Label labelArtist;
        private Label ltTitle;
        private Label labelAlbum;
        private Label ltAlbum;
        private Label ltLastfm;
        private Label labelLastfm;
        private ToolStripSeparator toolStripSeparator7;
        private SourceGridEx gridLink;
        private Label labelTitle;
        private PictureBox picLinkLibrary;
        private PictureBox picMedley;
        private Label lblMedley;
        private ContextMenuStrip medleyContextMenuStrip;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem startPositionDefaultToolStripMenuItem;
        private ToolStripMenuItem startPositionOpeningToolStripMenuItem;
        private ToolStripMenuItem startPositionMiddleToolStripMenuItem;
        private ToolStripMenuItem startPositionEndingToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem playtimeDefaultToolStripMenuItem;
        private ToolStripMenuItem playtimeShortToolStripMenuItem;
        private ToolStripMenuItem playtimeLongToolStripMenuItem;
        private ToolStripMenuItem playtimeHalfToolStripMenuItem;
        private ToolStripMenuItem exportPlaylistToolStripMenuItem;
        private TextBox txtAlbumDescription;
        private ContextMenuStrip albumDescriptionContextMenuStrip;
        private ToolStripMenuItem adAllselectToolStripMenuItem;
        private ToolStripMenuItem adCopyToolStripMenuItem1;
        private ToolStripMenuItem adPasteToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem adAmazonToolStripMenuItem;
        private ToolStripMenuItem playToolStripMenuItem;
        private ToolStripMenuItem folderOpenToolStripMenuItem;
        private PictureBox picMovie;
        private ToolStripMenuItem toolStripMenuItem3;

    }
}