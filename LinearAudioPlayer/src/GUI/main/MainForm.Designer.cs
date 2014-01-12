using System;
using System.Windows.Forms;
using FINALSTREAM.Commons.Controls;


namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.picPlaylist = new System.Windows.Forms.PictureBox();
            this.picFwd = new System.Windows.Forms.PictureBox();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picPause = new System.Windows.Forms.PictureBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PoweredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.TaskTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.topMosttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fadeEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoFittoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPlayMode = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.areaSizeChangeRight = new System.Windows.Forms.Label();
            this.lblBitRate = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TaskTrayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TTTaskTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TTExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picSpectrum = new System.Windows.Forms.PictureBox();
            this.picRating = new System.Windows.Forms.PictureBox();
            this.miniProgressSeekBar = new FINALSTREAM.Commons.Controls.VistaProgressBar();
            this.picDisplay = new FINALSTREAM.Commons.Controls.PictureBoxEx();
            this.popupNotifier = new FINALSTREAM.Commons.Controls.PopupNotifier();
            this.audioAutoRegisttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMonitoringDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRegistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPlaylist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.TaskTrayContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(96, 17);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "XLAYER alpha - FINALSTREAM";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseUp);
            // 
            // picPlaylist
            // 
            this.picPlaylist.Location = new System.Drawing.Point(386, 8);
            this.picPlaylist.Name = "picPlaylist";
            this.picPlaylist.Size = new System.Drawing.Size(16, 16);
            this.picPlaylist.TabIndex = 2;
            this.picPlaylist.TabStop = false;
            this.picPlaylist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPlaylist_MouseDown);
            this.picPlaylist.MouseLeave += new System.EventHandler(this.picPlaylist_MouseLeave);
            this.picPlaylist.MouseHover += new System.EventHandler(this.picPlaylist_MouseHover);
            this.picPlaylist.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPlaylist_MouseMove);
            // 
            // picFwd
            // 
            this.picFwd.Location = new System.Drawing.Point(364, 8);
            this.picFwd.Name = "picFwd";
            this.picFwd.Size = new System.Drawing.Size(16, 16);
            this.picFwd.TabIndex = 3;
            this.picFwd.TabStop = false;
            this.picFwd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picFwd_MouseDown);
            this.picFwd.MouseLeave += new System.EventHandler(this.picFwd_MouseLeave);
            this.picFwd.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picFwd_MouseMove);
            // 
            // picStop
            // 
            this.picStop.Location = new System.Drawing.Point(342, 9);
            this.picStop.Name = "picStop";
            this.picStop.Size = new System.Drawing.Size(16, 16);
            this.picStop.TabIndex = 4;
            this.picStop.TabStop = false;
            this.picStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picStop_MouseDown);
            this.picStop.MouseLeave += new System.EventHandler(this.picStop_MouseLeave);
            this.picStop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picStop_MouseMove);
            // 
            // picPause
            // 
            this.picPause.Location = new System.Drawing.Point(320, 9);
            this.picPause.Name = "picPause";
            this.picPause.Size = new System.Drawing.Size(16, 16);
            this.picPause.TabIndex = 5;
            this.picPause.TabStop = false;
            this.picPause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPause_MouseDown);
            this.picPause.MouseLeave += new System.EventHandler(this.picPause_MouseLeave);
            this.picPause.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPause_MouseMove);
            // 
            // picPlay
            // 
            this.picPlay.Location = new System.Drawing.Point(298, 8);
            this.picPlay.Name = "picPlay";
            this.picPlay.Size = new System.Drawing.Size(16, 16);
            this.picPlay.TabIndex = 6;
            this.picPlay.TabStop = false;
            this.picPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPlay_MouseDown);
            this.picPlay.MouseLeave += new System.EventHandler(this.picPlay_MouseLeave);
            this.picPlay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPlay_MouseMove);
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTime.Location = new System.Drawing.Point(202, 7);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(31, 17);
            this.lblTime.TabIndex = 7;
            this.lblTime.Text = "00:00";
            this.lblTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTime_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.PoweredToolStripMenuItem,
            this.toolStripSeparator7,
            this.audioAutoRegisttoolStripMenuItem,
            this.toolStripSeparator3,
            this.configToolStripMenuItem,
            this.toolStripSeparator5,
            this.topMosttoolStripMenuItem,
            this.fadeEffectToolStripMenuItem,
            this.autoFittoolStripMenuItem,
            this.TaskTrayToolStripMenuItem,
            this.versionToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(209, 254);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // PoweredToolStripMenuItem
            // 
            this.PoweredToolStripMenuItem.Enabled = false;
            this.PoweredToolStripMenuItem.Name = "PoweredToolStripMenuItem";
            this.PoweredToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // TaskTrayToolStripMenuItem
            // 
            this.TaskTrayToolStripMenuItem.Name = "TaskTrayToolStripMenuItem";
            this.TaskTrayToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.TaskTrayToolStripMenuItem.Text = "タスクトレイ格納";
            this.TaskTrayToolStripMenuItem.Click += new System.EventHandler(this.TaskTrayToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.configToolStripMenuItem.Text = "設定...";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(205, 6);
            // 
            // topMosttoolStripMenuItem
            // 
            this.topMosttoolStripMenuItem.CheckOnClick = true;
            this.topMosttoolStripMenuItem.Name = "topMosttoolStripMenuItem";
            this.topMosttoolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.topMosttoolStripMenuItem.Text = "常に手前に表示";
            this.topMosttoolStripMenuItem.Click += new System.EventHandler(this.topMosttoolStripMenuItem_Click);
            // 
            // fadeEffectToolStripMenuItem
            // 
            this.fadeEffectToolStripMenuItem.CheckOnClick = true;
            this.fadeEffectToolStripMenuItem.Name = "fadeEffectToolStripMenuItem";
            this.fadeEffectToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.fadeEffectToolStripMenuItem.Text = "フェードエフェクト";
            this.fadeEffectToolStripMenuItem.Click += new System.EventHandler(this.fadeEffectToolStripMenuItem_Click);
            // 
            // autoFittoolStripMenuItem
            // 
            this.autoFittoolStripMenuItem.Name = "autoFittoolStripMenuItem";
            this.autoFittoolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.autoFittoolStripMenuItem.Text = "オートフィット";
            this.autoFittoolStripMenuItem.Click += new System.EventHandler(this.autoFittoolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.versionToolStripMenuItem.Text = "Version...";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lblPlayMode
            // 
            this.lblPlayMode.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayMode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPlayMode.ForeColor = System.Drawing.Color.Silver;
            this.lblPlayMode.Location = new System.Drawing.Point(239, 6);
            this.lblPlayMode.Name = "lblPlayMode";
            this.lblPlayMode.Size = new System.Drawing.Size(53, 18);
            this.lblPlayMode.TabIndex = 8;
            this.lblPlayMode.Text = "Normal";
            this.lblPlayMode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPlayMode_MouseDown);
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // areaSizeChangeRight
            // 
            this.areaSizeChangeRight.BackColor = System.Drawing.Color.Transparent;
            this.areaSizeChangeRight.Location = new System.Drawing.Point(408, 4);
            this.areaSizeChangeRight.Name = "areaSizeChangeRight";
            this.areaSizeChangeRight.Size = new System.Drawing.Size(8, 21);
            this.areaSizeChangeRight.TabIndex = 9;
            this.areaSizeChangeRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.areaSizeChangeRight_MouseDown);
            this.areaSizeChangeRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.areaSizeChangeRight_MouseMove);
            // 
            // lblBitRate
            // 
            this.lblBitRate.BackColor = System.Drawing.Color.Transparent;
            this.lblBitRate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblBitRate.ForeColor = System.Drawing.Color.Tomato;
            this.lblBitRate.Location = new System.Drawing.Point(165, 7);
            this.lblBitRate.Name = "lblBitRate";
            this.lblBitRate.Size = new System.Drawing.Size(31, 17);
            this.lblBitRate.TabIndex = 10;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.TaskTrayContextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Linear Audio Player";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            this.notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDown);
            // 
            // TaskTrayContextMenuStrip
            // 
            this.TaskTrayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.TTTaskTrayToolStripMenuItem,
            this.toolStripSeparator6,
            this.TTExitToolStripMenuItem});
            this.TaskTrayContextMenuStrip.Name = "contextMenuStrip";
            this.TaskTrayContextMenuStrip.Size = new System.Drawing.Size(197, 60);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(193, 6);
            // 
            // TTTaskTrayToolStripMenuItem
            // 
            this.TTTaskTrayToolStripMenuItem.Name = "TTTaskTrayToolStripMenuItem";
            this.TTTaskTrayToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.TTTaskTrayToolStripMenuItem.Text = "タスクトレイから出す";
            this.TTTaskTrayToolStripMenuItem.Click += new System.EventHandler(this.TTTaskTrayToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(193, 6);
            // 
            // TTExitToolStripMenuItem
            // 
            this.TTExitToolStripMenuItem.Name = "TTExitToolStripMenuItem";
            this.TTExitToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.TTExitToolStripMenuItem.Text = "Exit";
            this.TTExitToolStripMenuItem.Click += new System.EventHandler(this.TTExitToolStripMenuItem_Click);
            // 
            // picSpectrum
            // 
            this.picSpectrum.BackColor = System.Drawing.Color.Transparent;
            this.picSpectrum.Location = new System.Drawing.Point(422, 12);
            this.picSpectrum.Name = "picSpectrum";
            this.picSpectrum.Size = new System.Drawing.Size(16, 10);
            this.picSpectrum.TabIndex = 32;
            this.picSpectrum.TabStop = false;
            this.picSpectrum.Paint += new System.Windows.Forms.PaintEventHandler(this.picSpectrum_Paint);
            // 
            // picRating
            // 
            this.picRating.BackColor = System.Drawing.Color.Transparent;
            this.picRating.Location = new System.Drawing.Point(141, 4);
            this.picRating.Name = "picRating";
            this.picRating.Size = new System.Drawing.Size(16, 16);
            this.picRating.TabIndex = 34;
            this.picRating.TabStop = false;
            this.picRating.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picRating_MouseDown);
            // 
            // miniProgressSeekBar
            // 
            this.miniProgressSeekBar.BackColor = System.Drawing.Color.Transparent;
            this.miniProgressSeekBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.miniProgressSeekBar.Location = new System.Drawing.Point(163, 12);
            this.miniProgressSeekBar.Maximum = 0F;
            this.miniProgressSeekBar.Name = "miniProgressSeekBar";
            this.miniProgressSeekBar.ProgressBarMainBottomActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(82)))), ((int)(((byte)(0)))));
            this.miniProgressSeekBar.ProgressBarMainBottomBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.miniProgressSeekBar.ProgressBarMainUnderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.miniProgressSeekBar.ProgressBarMainUnderBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.miniProgressSeekBar.ProgressBarUpBottomActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(129)))), ((int)(((byte)(38)))));
            this.miniProgressSeekBar.ProgressBarUpBottomBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.miniProgressSeekBar.ProgressBarUpUnderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(155)))), ((int)(((byte)(84)))));
            this.miniProgressSeekBar.ProgressBarUpUnderBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.miniProgressSeekBar.Size = new System.Drawing.Size(33, 10);
            this.miniProgressSeekBar.TabIndex = 33;
            this.miniProgressSeekBar.Theme = FINALSTREAM.Commons.Controls.VistaProgressBarTheme.Orange;
            this.miniProgressSeekBar.Value = 0F;
            this.miniProgressSeekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.miniProgressSeekBar_MouseDown);
            // 
            // picDisplay
            // 
            this.picDisplay.BackColor = System.Drawing.Color.Transparent;
            this.picDisplay.BorderColor = System.Drawing.Color.Black;
            this.picDisplay.Location = new System.Drawing.Point(6, 3);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(275, 22);
            this.picDisplay.TabIndex = 0;
            this.picDisplay.TabStop = false;
            this.picDisplay.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseDoubleClick);
            // 
            // popupNotifier
            // 
            this.popupNotifier.BodyColor = System.Drawing.Color.Gainsboro;
            this.popupNotifier.BodyColor2 = System.Drawing.Color.Transparent;
            this.popupNotifier.ButtonHoverColor = System.Drawing.Color.Crimson;
            this.popupNotifier.ContentFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.popupNotifier.ContentHoverColor = System.Drawing.SystemColors.ControlText;
            this.popupNotifier.ContentPadding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.popupNotifier.ContentText = null;
            this.popupNotifier.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.popupNotifier.Image = null;
            this.popupNotifier.ImagePadding = new System.Windows.Forms.Padding(10);
            this.popupNotifier.ImageSize = new System.Drawing.Size(72, 72);
            this.popupNotifier.isChangeColor = false;
            this.popupNotifier.OptionsMenu = null;
            this.popupNotifier.OwnerForm = null;
            this.popupNotifier.Position = new System.Drawing.Point(0, 0);
            this.popupNotifier.Scroll = false;
            this.popupNotifier.ShowGrip = false;
            this.popupNotifier.Size = new System.Drawing.Size(400, 100);
            this.popupNotifier.TitleColor = System.Drawing.Color.Black;
            this.popupNotifier.TitleFont = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.popupNotifier.TitlePadding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.popupNotifier.TitleText = null;
            this.popupNotifier.Click += new System.EventHandler(this.popupNotifier_Click);
            // 
            // audioAutoRegisttoolStripMenuItem
            // 
            this.audioAutoRegisttoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoRegistToolStripMenuItem,
            this.openMonitoringDirToolStripMenuItem});
            this.audioAutoRegisttoolStripMenuItem.Name = "audioAutoRegisttoolStripMenuItem";
            this.audioAutoRegisttoolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.audioAutoRegisttoolStripMenuItem.Text = "オーディオファイル登録";
            // 
            // openMonitoringDirToolStripMenuItem
            // 
            this.openMonitoringDirToolStripMenuItem.Name = "openMonitoringDirToolStripMenuItem";
            this.openMonitoringDirToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openMonitoringDirToolStripMenuItem.Text = "監視フォルダを開く";
            this.openMonitoringDirToolStripMenuItem.Click += new System.EventHandler(this.openMonitoringDirToolStripMenuItem_Click);
            // 
            // autoRegistToolStripMenuItem
            // 
            this.autoRegistToolStripMenuItem.Name = "autoRegistToolStripMenuItem";
            this.autoRegistToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.autoRegistToolStripMenuItem.Text = "自動登録実行";
            this.autoRegistToolStripMenuItem.Click += new System.EventHandler(this.autoRegistToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 30);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.picRating);
            this.Controls.Add(this.miniProgressSeekBar);
            this.Controls.Add(this.picSpectrum);
            this.Controls.Add(this.lblBitRate);
            this.Controls.Add(this.areaSizeChangeRight);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.picPlay);
            this.Controls.Add(this.lblPlayMode);
            this.Controls.Add(this.picDisplay);
            this.Controls.Add(this.picPause);
            this.Controls.Add(this.picStop);
            this.Controls.Add(this.picFwd);
            this.Controls.Add(this.picPlaylist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Linear Audio Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseWheel);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picPlaylist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.TaskTrayContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSpectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBoxEx picDisplay;
        private Label lblTitle;
        private PictureBox picPlaylist;
        private PictureBox picFwd;
        private PictureBox picStop;
        private PictureBox picPause;
        private PictureBox picPlay;
        private Label lblTime;
        private ContextMenuStrip contextMenuStrip;
        private Label lblPlayMode;
        private Timer timerMain;
        private Label areaSizeChangeRight;
        private Label lblBitRate;
        private ToolStripMenuItem topMosttoolStripMenuItem;
        private ToolStripMenuItem versionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem autoFittoolStripMenuItem;
        private ToolStripMenuItem fadeEffectToolStripMenuItem;
        private ToolStripMenuItem TaskTrayToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ContextMenuStrip TaskTrayContextMenuStrip;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem TTTaskTrayToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem TTExitToolStripMenuItem;
        public NotifyIcon notifyIcon;
        private ToolStripMenuItem configToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem PoweredToolStripMenuItem;
        private PictureBox picSpectrum;
        public Commons.Controls.PopupNotifier popupNotifier;
        private Commons.Controls.VistaProgressBar miniProgressSeekBar;
        private PictureBox picRating;
        private ToolStripMenuItem audioAutoRegisttoolStripMenuItem;
        private ToolStripMenuItem autoRegistToolStripMenuItem;
        private ToolStripMenuItem openMonitoringDirToolStripMenuItem;
    }
}

