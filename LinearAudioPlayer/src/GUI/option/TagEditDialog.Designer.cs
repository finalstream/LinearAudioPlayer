namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    partial class TagEditDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagEditDialog));
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblArtist = new System.Windows.Forms.Label();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.txtAlbum = new System.Windows.Forms.TextBox();
            this.lblTrackNo = new System.Windows.Forms.Label();
            this.txtTrackNo = new System.Windows.Forms.TextBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.buttonConvertZip = new System.Windows.Forms.Button();
            this.picArtwork = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openArtworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveArtworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearArtworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtArchivePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblArtworkType = new System.Windows.Forms.Label();
            this.checkArtworkSave = new System.Windows.Forms.CheckBox();
            this.albumView = new System.Windows.Forms.DataGridView();
            this.columArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columRelease = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnURL = new System.Windows.Forms.DataGridViewLinkColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.allAlbumTrackUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allAlbumTrackUpdateTrackNoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allAlbumTrackUpdateFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTagSearch = new System.Windows.Forms.Label();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.buttonAmazonSearch = new System.Windows.Forms.Button();
            this.trackView = new System.Windows.Forms.DataGridView();
            this.columnDiscNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTrackNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTrackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picAmazonImage = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.checkTagSearchShow = new System.Windows.Forms.CheckBox();
            this.btnRename = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picArtwork)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.albumView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAmazonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(63, 58);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 19);
            this.txtTitle.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(213, 166);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(300, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(29, 61);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(28, 12);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Title";
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Location = new System.Drawing.Point(20, 89);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(37, 12);
            this.lblArtist.TabIndex = 4;
            this.lblArtist.Text = "Airtist";
            // 
            // txtArtist
            // 
            this.txtArtist.Location = new System.Drawing.Point(63, 86);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(200, 19);
            this.txtArtist.TabIndex = 5;
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Location = new System.Drawing.Point(20, 117);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(37, 12);
            this.lblAlbum.TabIndex = 6;
            this.lblAlbum.Text = "Album";
            // 
            // txtAlbum
            // 
            this.txtAlbum.Location = new System.Drawing.Point(63, 114);
            this.txtAlbum.Name = "txtAlbum";
            this.txtAlbum.Size = new System.Drawing.Size(200, 19);
            this.txtAlbum.TabIndex = 7;
            // 
            // lblTrackNo
            // 
            this.lblTrackNo.AutoSize = true;
            this.lblTrackNo.Location = new System.Drawing.Point(269, 61);
            this.lblTrackNo.Name = "lblTrackNo";
            this.lblTrackNo.Size = new System.Drawing.Size(48, 12);
            this.lblTrackNo.TabIndex = 8;
            this.lblTrackNo.Text = "TrackNo";
            // 
            // txtTrackNo
            // 
            this.txtTrackNo.Location = new System.Drawing.Point(323, 58);
            this.txtTrackNo.Name = "txtTrackNo";
            this.txtTrackNo.Size = new System.Drawing.Size(56, 19);
            this.txtTrackNo.TabIndex = 9;
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Location = new System.Drawing.Point(282, 89);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(35, 12);
            this.lblGenre.TabIndex = 10;
            this.lblGenre.Text = "Genre";
            // 
            // txtGenre
            // 
            this.txtGenre.Location = new System.Drawing.Point(323, 86);
            this.txtGenre.Name = "txtGenre";
            this.txtGenre.Size = new System.Drawing.Size(56, 19);
            this.txtGenre.TabIndex = 11;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(289, 117);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(28, 12);
            this.lblYear.TabIndex = 12;
            this.lblYear.Text = "Year";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(323, 114);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(56, 19);
            this.txtYear.TabIndex = 13;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(12, 143);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(166, 12);
            this.lblMessage.TabIndex = 14;
            this.lblMessage.Text = "複数のファイルを一括変更します。";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(10, 9);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(47, 12);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "FilePath";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(63, 6);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(316, 19);
            this.txtFilePath.TabIndex = 1;
            // 
            // buttonConvertZip
            // 
            this.buttonConvertZip.Location = new System.Drawing.Point(318, 29);
            this.buttonConvertZip.Name = "buttonConvertZip";
            this.buttonConvertZip.Size = new System.Drawing.Size(61, 23);
            this.buttonConvertZip.TabIndex = 17;
            this.buttonConvertZip.Text = "ZIP変換";
            this.buttonConvertZip.UseVisualStyleBackColor = true;
            this.buttonConvertZip.Click += new System.EventHandler(this.buttonConvertZip_Click);
            // 
            // picArtwork
            // 
            this.picArtwork.ContextMenuStrip = this.contextMenuStrip2;
            this.picArtwork.Location = new System.Drawing.Point(386, 9);
            this.picArtwork.Name = "picArtwork";
            this.picArtwork.Size = new System.Drawing.Size(178, 178);
            this.picArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picArtwork.TabIndex = 18;
            this.picArtwork.TabStop = false;
            this.picArtwork.Paint += new System.Windows.Forms.PaintEventHandler(this.picArtwork_Paint);
            this.picArtwork.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picArtwork_MouseDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openArtworkToolStripMenuItem,
            this.saveArtworkToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearArtworkToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(197, 76);
            // 
            // openArtworkToolStripMenuItem
            // 
            this.openArtworkToolStripMenuItem.Name = "openArtworkToolStripMenuItem";
            this.openArtworkToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openArtworkToolStripMenuItem.Text = "アートワークを開く...";
            this.openArtworkToolStripMenuItem.Click += new System.EventHandler(this.openArtworkToolStripMenuItem_Click);
            // 
            // saveArtworkToolStripMenuItem
            // 
            this.saveArtworkToolStripMenuItem.Name = "saveArtworkToolStripMenuItem";
            this.saveArtworkToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.saveArtworkToolStripMenuItem.Text = "アートワークを保存...";
            this.saveArtworkToolStripMenuItem.Click += new System.EventHandler(this.saveArtworkToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // clearArtworkToolStripMenuItem
            // 
            this.clearArtworkToolStripMenuItem.Name = "clearArtworkToolStripMenuItem";
            this.clearArtworkToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.clearArtworkToolStripMenuItem.Text = "アートワークをクリア";
            this.clearArtworkToolStripMenuItem.Click += new System.EventHandler(this.clearArtworkToolStripMenuItem_Click);
            // 
            // txtArchivePath
            // 
            this.txtArchivePath.Location = new System.Drawing.Point(63, 31);
            this.txtArchivePath.Name = "txtArchivePath";
            this.txtArchivePath.Size = new System.Drawing.Size(184, 19);
            this.txtArchivePath.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "Archive";
            // 
            // lblArtworkType
            // 
            this.lblArtworkType.AutoSize = true;
            this.lblArtworkType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtworkType.ForeColor = System.Drawing.Color.Black;
            this.lblArtworkType.Location = new System.Drawing.Point(335, 142);
            this.lblArtworkType.Name = "lblArtworkType";
            this.lblArtworkType.Size = new System.Drawing.Size(39, 14);
            this.lblArtworkType.TabIndex = 21;
            this.lblArtworkType.Text = "NONE";
            this.lblArtworkType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkArtworkSave
            // 
            this.checkArtworkSave.AutoSize = true;
            this.checkArtworkSave.Location = new System.Drawing.Point(271, 142);
            this.checkArtworkSave.Name = "checkArtworkSave";
            this.checkArtworkSave.Size = new System.Drawing.Size(64, 16);
            this.checkArtworkSave.TabIndex = 23;
            this.checkArtworkSave.Text = "Artwork";
            this.checkArtworkSave.UseVisualStyleBackColor = true;
            // 
            // albumView
            // 
            this.albumView.AllowUserToAddRows = false;
            this.albumView.AllowUserToDeleteRows = false;
            this.albumView.AllowUserToResizeRows = false;
            this.albumView.BackgroundColor = System.Drawing.Color.White;
            this.albumView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.albumView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.albumView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.albumView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columArtist,
            this.columAlbum,
            this.columRelease,
            this.columLabel,
            this.columnURL});
            this.albumView.ContextMenuStrip = this.contextMenuStrip1;
            this.albumView.Location = new System.Drawing.Point(14, 232);
            this.albumView.MultiSelect = false;
            this.albumView.Name = "albumView";
            this.albumView.RowHeadersVisible = false;
            this.albumView.RowTemplate.Height = 21;
            this.albumView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.albumView.ShowCellErrors = false;
            this.albumView.ShowCellToolTips = false;
            this.albumView.ShowEditingIcon = false;
            this.albumView.ShowRowErrors = false;
            this.albumView.Size = new System.Drawing.Size(550, 78);
            this.albumView.TabIndex = 24;
            this.albumView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.albumView_CellContentClick);
            this.albumView.SelectionChanged += new System.EventHandler(this.albumView_SelectionChanged);
            // 
            // columArtist
            // 
            this.columArtist.DataPropertyName = "Artist";
            this.columArtist.HeaderText = "Artist";
            this.columArtist.Name = "columArtist";
            // 
            // columAlbum
            // 
            this.columAlbum.DataPropertyName = "Title";
            this.columAlbum.HeaderText = "Album";
            this.columAlbum.Name = "columAlbum";
            this.columAlbum.Width = 220;
            // 
            // columRelease
            // 
            this.columRelease.DataPropertyName = "ReleaseDate";
            this.columRelease.HeaderText = "Release";
            this.columRelease.Name = "columRelease";
            this.columRelease.Width = 70;
            // 
            // columLabel
            // 
            this.columLabel.DataPropertyName = "Label";
            this.columLabel.HeaderText = "Label";
            this.columLabel.Name = "columLabel";
            this.columLabel.Width = 110;
            // 
            // columnURL
            // 
            this.columnURL.ActiveLinkColor = System.Drawing.Color.Silver;
            this.columnURL.HeaderText = "Link";
            this.columnURL.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.columnURL.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.columnURL.Name = "columnURL";
            this.columnURL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnURL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnURL.Text = "■";
            this.columnURL.TrackVisitedState = false;
            this.columnURL.UseColumnTextForLinkValue = true;
            this.columnURL.VisitedLinkColor = System.Drawing.Color.Silver;
            this.columnURL.Width = 30;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allAlbumTrackUpdateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(209, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // allAlbumTrackUpdateToolStripMenuItem
            // 
            this.allAlbumTrackUpdateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allAlbumTrackUpdateTrackNoToolStripMenuItem,
            this.allAlbumTrackUpdateFilePathToolStripMenuItem});
            this.allAlbumTrackUpdateToolStripMenuItem.Name = "allAlbumTrackUpdateToolStripMenuItem";
            this.allAlbumTrackUpdateToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.allAlbumTrackUpdateToolStripMenuItem.Text = "アルバム内タグ一括更新";
            // 
            // allAlbumTrackUpdateTrackNoToolStripMenuItem
            // 
            this.allAlbumTrackUpdateTrackNoToolStripMenuItem.Name = "allAlbumTrackUpdateTrackNoToolStripMenuItem";
            this.allAlbumTrackUpdateTrackNoToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.allAlbumTrackUpdateTrackNoToolStripMenuItem.Text = "トラック順";
            this.allAlbumTrackUpdateTrackNoToolStripMenuItem.Click += new System.EventHandler(this.allAlbumTrackUpdateTrackNoToolStripMenuItem_Click);
            // 
            // allAlbumTrackUpdateFilePathToolStripMenuItem
            // 
            this.allAlbumTrackUpdateFilePathToolStripMenuItem.Name = "allAlbumTrackUpdateFilePathToolStripMenuItem";
            this.allAlbumTrackUpdateFilePathToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.allAlbumTrackUpdateFilePathToolStripMenuItem.Text = "ファイルパス順";
            this.allAlbumTrackUpdateFilePathToolStripMenuItem.Click += new System.EventHandler(this.allAlbumTrackUpdateFilePathToolStripMenuItem_Click);
            // 
            // lblTagSearch
            // 
            this.lblTagSearch.AutoSize = true;
            this.lblTagSearch.Location = new System.Drawing.Point(12, 208);
            this.lblTagSearch.Name = "lblTagSearch";
            this.lblTagSearch.Size = new System.Drawing.Size(63, 12);
            this.lblTagSearch.TabIndex = 25;
            this.lblTagSearch.Text = "Tag Search";
            this.lblTagSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTagSearch_MouseDown);
            // 
            // textSearch
            // 
            this.textSearch.Location = new System.Drawing.Point(96, 205);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(405, 19);
            this.textSearch.TabIndex = 26;
            this.textSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSearch_KeyDown);
            // 
            // buttonAmazonSearch
            // 
            this.buttonAmazonSearch.Location = new System.Drawing.Point(507, 203);
            this.buttonAmazonSearch.Name = "buttonAmazonSearch";
            this.buttonAmazonSearch.Size = new System.Drawing.Size(57, 23);
            this.buttonAmazonSearch.TabIndex = 27;
            this.buttonAmazonSearch.Text = "検索";
            this.buttonAmazonSearch.UseVisualStyleBackColor = true;
            this.buttonAmazonSearch.Click += new System.EventHandler(this.buttonAmazonSearch_Click);
            // 
            // trackView
            // 
            this.trackView.AllowUserToAddRows = false;
            this.trackView.AllowUserToDeleteRows = false;
            this.trackView.AllowUserToResizeRows = false;
            this.trackView.BackgroundColor = System.Drawing.Color.White;
            this.trackView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.trackView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.trackView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trackView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDiscNumber,
            this.columnTrackNumber,
            this.columnTrackName});
            this.trackView.Location = new System.Drawing.Point(121, 316);
            this.trackView.MultiSelect = false;
            this.trackView.Name = "trackView";
            this.trackView.RowHeadersVisible = false;
            this.trackView.RowTemplate.Height = 21;
            this.trackView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.trackView.ShowCellErrors = false;
            this.trackView.ShowCellToolTips = false;
            this.trackView.ShowEditingIcon = false;
            this.trackView.ShowRowErrors = false;
            this.trackView.Size = new System.Drawing.Size(443, 98);
            this.trackView.TabIndex = 28;
            this.trackView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.trackView_CellDoubleClick);
            // 
            // columnDiscNumber
            // 
            this.columnDiscNumber.DataPropertyName = "DiscNumber";
            this.columnDiscNumber.HeaderText = "DiscNo";
            this.columnDiscNumber.Name = "columnDiscNumber";
            this.columnDiscNumber.Width = 70;
            // 
            // columnTrackNumber
            // 
            this.columnTrackNumber.DataPropertyName = "TrackNumber";
            this.columnTrackNumber.HeaderText = "TrackNo";
            this.columnTrackNumber.Name = "columnTrackNumber";
            this.columnTrackNumber.Width = 70;
            // 
            // columnTrackName
            // 
            this.columnTrackName.DataPropertyName = "TrackName";
            this.columnTrackName.HeaderText = "TrackName";
            this.columnTrackName.Name = "columnTrackName";
            this.columnTrackName.Width = 250;
            // 
            // picAmazonImage
            // 
            this.picAmazonImage.Location = new System.Drawing.Point(12, 316);
            this.picAmazonImage.Name = "picAmazonImage";
            this.picAmazonImage.Size = new System.Drawing.Size(98, 98);
            this.picAmazonImage.TabIndex = 29;
            this.picAmazonImage.TabStop = false;
            this.picAmazonImage.Paint += new System.Windows.Forms.PaintEventHandler(this.picAmazonImage_Paint);
            this.picAmazonImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picAmazonImage_MouseDown);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(157, 166);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(50, 23);
            this.buttonReset.TabIndex = 30;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // checkTagSearchShow
            // 
            this.checkTagSearchShow.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkTagSearchShow.AutoSize = true;
            this.checkTagSearchShow.Location = new System.Drawing.Point(12, 166);
            this.checkTagSearchShow.Name = "checkTagSearchShow";
            this.checkTagSearchShow.Size = new System.Drawing.Size(56, 22);
            this.checkTagSearchShow.TabIndex = 31;
            this.checkTagSearchShow.Text = "タグ検索";
            this.checkTagSearchShow.UseVisualStyleBackColor = true;
            this.checkTagSearchShow.CheckedChanged += new System.EventHandler(this.checkTagSearchShow_CheckedChanged);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(253, 29);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(61, 23);
            this.btnRename.TabIndex = 32;
            this.btnRename.Text = "RENAME";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // TagEditDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(571, 423);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.checkTagSearchShow);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.picAmazonImage);
            this.Controls.Add(this.trackView);
            this.Controls.Add(this.buttonAmazonSearch);
            this.Controls.Add(this.textSearch);
            this.Controls.Add(this.lblTagSearch);
            this.Controls.Add(this.albumView);
            this.Controls.Add(this.checkArtworkSave);
            this.Controls.Add(this.lblArtworkType);
            this.Controls.Add(this.txtArchivePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picArtwork);
            this.Controls.Add(this.buttonConvertZip);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtGenre);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.txtTrackNo);
            this.Controls.Add(this.lblTrackNo);
            this.Controls.Add(this.txtAlbum);
            this.Controls.Add(this.lblAlbum);
            this.Controls.Add(this.txtArtist);
            this.Controls.Add(this.lblArtist);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TagEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Linear Tag Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TagEditDialog_FormClosing);
            this.Load += new System.EventHandler(this.TagEditDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picArtwork)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.albumView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAmazonImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.Label lblAlbum;
        private System.Windows.Forms.Label lblTrackNo;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.TextBox txtArtist;
        public System.Windows.Forms.TextBox txtAlbum;
        public System.Windows.Forms.TextBox txtTrackNo;
        public System.Windows.Forms.TextBox txtGenre;
        public System.Windows.Forms.TextBox txtYear;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblFilePath;
        public System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button buttonConvertZip;
        private System.Windows.Forms.PictureBox picArtwork;
        public System.Windows.Forms.TextBox txtArchivePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblArtworkType;
        private System.Windows.Forms.CheckBox checkArtworkSave;
        private System.Windows.Forms.DataGridView albumView;
        private System.Windows.Forms.Label lblTagSearch;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Button buttonAmazonSearch;
        private System.Windows.Forms.DataGridView trackView;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDiscNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTrackNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTrackName;
        private System.Windows.Forms.PictureBox picAmazonImage;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.CheckBox checkTagSearchShow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem allAlbumTrackUpdateToolStripMenuItem;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem openArtworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveArtworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearArtworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allAlbumTrackUpdateTrackNoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allAlbumTrackUpdateFilePathToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columArtist;
        private System.Windows.Forms.DataGridViewTextBoxColumn columAlbum;
        private System.Windows.Forms.DataGridViewTextBoxColumn columRelease;
        private System.Windows.Forms.DataGridViewTextBoxColumn columLabel;
        private System.Windows.Forms.DataGridViewLinkColumn columnURL;
    }
}