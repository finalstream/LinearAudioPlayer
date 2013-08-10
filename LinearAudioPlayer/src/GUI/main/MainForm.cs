using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FINALSTREAM.Commons.Controls;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.LinearAudioPlayer.Core;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.Commons.Utils;
using System.IO;
using FINALSTREAM.LinearAudioPlayer.GUI.option;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Resources;
using FINALSTREAM.LinearAudioPlayer.Setting;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    /// <summary>
    /// MainForm Class
    /// </summary>
    public partial class MainForm : Form
    {

        /*
            プライベートメンバ
        */

        #region PrivateMember

        private bool _isOpenListForm = false;
        private ListForm _listForm = null;
        private bool _isTaskTrayBeforeOpen = false;
        private ConfigForm _configForm = null;
        private LinearGradientBrush gb;
        private bool isInputTaskTray = false;

        private static Bitmap ratingFavoriteImage;
        private static Bitmap ratingNormalImage;

        /// <summary>
        /// タイトルクリック中
        /// </summary>
        private bool isTitleClicking = false;

        #endregion



        /*
            パブリックプロパティ
        */

        #region PublicProperty

        /// <summary>
        /// ListForm
        /// </summary>
        public ListForm ListForm
        {
            get { return _listForm; }
            set { _listForm = value; }
        }

        /// <summary>
        /// 設定フォーム
        /// </summary>
        public ConfigForm ConfigForm
        {
            get { return _configForm; }
            set { _configForm = value; }
        }

        /// <summary>
        /// ListFormを開いているか
        /// </summary>
        public bool IsOpenListForm
        {
            get { return _isOpenListForm; }
            set { _isOpenListForm = value; }
        }

        /// <summary>
        /// タスクトレイ前開いているか
        /// </summary>
        public bool IsTaskTrayBeforeOpen
        {
            get { return _isTaskTrayBeforeOpen; }
            set { _isTaskTrayBeforeOpen = value; }
        }


        #endregion




        /*
            イベント 
        */

        #region Event

        /// <summary>
        /// MainFormを動かしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Move(object sender, EventArgs e)
        {
            syncFormPosSize();
        }

        /// <summary>
        /// リストボタンをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPlaylist_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switchViewPlaylist();
            }
        }


        private void picPlaylist_MouseLeave(object sender, EventArgs e)
        {
            if (this.IsOpenListForm == false)
            {
                this.picPlaylist.Load(LinearGlobal.StyleDirectory + "control_eject.png");
            }
        }

        private void picPlaylist_MouseMove(object sender, MouseEventArgs e)
        {
            this.picPlaylist.Load(LinearGlobal.StyleDirectory + "control_eject_on.png");
        }

        private void picFwd_MouseMove(object sender, MouseEventArgs e)
        {
            this.picFwd.Load(LinearGlobal.StyleDirectory + "control_fwd_on.png");
        }

        private void picFwd_MouseLeave(object sender, EventArgs e)
        {
            this.picFwd.Load(LinearGlobal.StyleDirectory + "control_fwd.png");
        }

        private void picStop_MouseMove(object sender, MouseEventArgs e)
        {
            this.picStop.Load(LinearGlobal.StyleDirectory + "control_stop_on.png");
        }

        private void picStop_MouseLeave(object sender, EventArgs e)
        {
            this.picStop.Load(LinearGlobal.StyleDirectory + "control_stop.png");
        }

        private void picPause_MouseMove(object sender, MouseEventArgs e)
        {
            this.picPause.Load(LinearGlobal.StyleDirectory + "control_pause_on.png");
        }

        private void picPause_MouseLeave(object sender, EventArgs e)
        {
            this.picPause.Load(LinearGlobal.StyleDirectory + "control_pause.png");
        }

        private void picPlay_MouseMove(object sender, MouseEventArgs e)
        {
            this.picPlay.Load(LinearGlobal.StyleDirectory + "control_play_on.png");
        }

        private void picPlay_MouseLeave(object sender, EventArgs e)
        {
            this.picPlay.Load(LinearGlobal.StyleDirectory + "control_play.png");
        }



        /// <summary>
        /// タイトルをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTitleClicking = true;
                // 位置、サイズをMainFormにあわせる

                if (Cursor.Position.Y + popupNotifier.Size.Height > Screen.GetWorkingArea(this).Height)
                {
                    // ワークエリアを越えたら上に表示
                    popupNotifier.Position = new Point(Cursor.Position.X, Cursor.Position.Y - popupNotifier.Size.Height);
                }
                else
                {
                    // ワークエリアを越えなかったら下に表示
                    popupNotifier.Position = Cursor.Position;
                }

                popupNotifier.Popup();
            }
        }

        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTitleClicking = false;
            }
        }



        /// <summary>
        /// 時間をクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTime_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 表示モード切替
                if (LinearGlobal.TimeDisplayMode == LinearConst.DISPLAYTIMEMODE_NORMAL)
                {
                    LinearGlobal.TimeDisplayMode = LinearConst.DISPLAYTIMEMODE_REVERSE;
                }
                else
                {
                    LinearGlobal.TimeDisplayMode = LinearConst.DISPLAYTIMEMODE_NORMAL;
                }
            }
        }

        /// <summary>
        /// プレイモードをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayMode_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.PlayMode++;
                setPlayMode(LinearGlobal.PlayMode);
                setTitle(LinearAudioPlayer.PlayController.createTitle());
                //LinearAudioPlayer.PlayController.updateNextPlaylist(LinearConst.MAX_NEXTPLAYLIST_NUM);
            }
        }

        /// <summary>
        /// マウスホイールアクション
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                // 半透明度を変更
                if (e.Delta/120 == 1 && this.Opacity != 1)
                {
                    this.Opacity = this.Opacity + 0.0125;
                }
                else if (e.Delta/120 == -1 && this.Opacity > 0.2)
                {
                    this.Opacity = this.Opacity - 0.0125;
                }
                this.ListForm.Opacity = this.Opacity;
                //this.ListForm.InterruptForm.Opacity = this.Opacity;
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                // TODO:同じところがある(上下キーと同じ)
                int vol = LinearGlobal.Volume;

                if (e.Delta / 120 == 1)
                {
                    vol++;
                    if (vol > 100)
                    {
                        vol = 100;
                    }
                    LinearGlobal.Volume = vol;
                    LinearGlobal.MainForm.ListForm.setVolume();
                    this.lblTitle.Text = LinearAudioPlayer.PlayController.createTitle() + " (volume: " + LinearGlobal.Volume + "%)";

                }
                else if (e.Delta / 120 == -1)
                {
                    vol--;
                    if (vol < 0)
                    {
                        vol = 0;
                    }
                    LinearGlobal.Volume = vol;
                    LinearGlobal.MainForm.ListForm.setVolume();
                    this.lblTitle.Text = LinearAudioPlayer.PlayController.createTitle() + " (volume: " + LinearGlobal.Volume + "%)";
                }
            

            }
        }

        /// <summary>
        /// タイマーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerMain_Tick(object sender, EventArgs e)
        {
            picSpectrum.Invalidate();

            // Shoutcast);
            if (StringUtils.isURL(LinearGlobal.CurrentPlayItemInfo.FilePath) &&
                LinearAudioPlayer.PlayController.isUpdateMetadata())
            {

                // グリッド更新
                int curRowNo = LinearAudioPlayer.GridController.Find((int) GridController.EnuGrid.ID,
                                                                     LinearGlobal.CurrentPlayItemInfo.Id.ToString());

                LinearAudioPlayer.GridController.setRadioStatus(curRowNo);

                LinearGlobal.MainForm.lblTitle.Text = LinearAudioPlayer.GridController.getValue(curRowNo,
                                                                                                (int)
                                                                                                GridController.EnuGrid.
                                                                                                    ARTIST);
                LinearGlobal.MainForm.lblBitRate.Text = LinearAudioPlayer.GridController.getValue(curRowNo,
                                                                                                  (int)
                                                                                                  GridController.EnuGrid
                                                                                                      .BITRATE);
            }

            // 時間表示取得
            LinearGlobal.MainForm.lblTime.Text = LinearAudioPlayer.PlayController.getDisplayTime();
            // プログレスバー更新
            LinearGlobal.MainForm.ListForm.setProgressBarValue((int) LinearAudioPlayer.PlayController.getPosition());
            LinearGlobal.MainForm.setMiniProgressSeekBarValue((int)LinearAudioPlayer.PlayController.getPosition());
            // Windows7 TaskBar
            if (WindowsUtils.IsWindows7Later())
            {
                TaskbarManager.Instance.SetProgressValue((int) ListForm.progressSeekBar.Value, 100);
            }

            if (LinearGlobal.TitleDisplayScroll && !isTitleClicking)
            {
                MainFunction mf = new MainFunction();
                mf.scrollTitle(this.lblTitle, this.picRating.Left);
                mf = null;
            }

            // 再生が終わったか確認
            if (!LinearAudioPlayer.PlayController.isPlaying())
            {
                LinearAudioPlayer.PlayController.endOfStream();
            }

            // プレイカウントインクリメント
            if (LinearGlobal.PlayCountUpStopwatch.ElapsedMilliseconds > 0 &&
                (LinearGlobal.PlayCountUpStopwatch.ElapsedMilliseconds/1000) >
                LinearGlobal.CurrentPlayItemInfo.PlayCountUpSecond)
            {
                MainFunction.incrementPlayCountUp();
                // プラグイン処理(再生後処理)
                afterPlayCountUpPluginDelegate pd = new afterPlayCountUpPluginDelegate(afterPlayCountUpPlugin);
                pd.BeginInvoke(null, null);
                LinearGlobal.PlayCountUpStopwatch.Reset();
            }

            // メドレー再生終了判定
            if (LinearAudioPlayer.PlayController.MedleyInfo.Enable)
            {
                if (LinearGlobal.CurrentPlayItemInfo.Id == LinearAudioPlayer.PlayController.MedleyInfo.MedleyId &&
                    LinearAudioPlayer.PlayController.getPosition() >= LinearAudioPlayer.PlayController.MedleyInfo.EndPoint)
                {
                    LinearAudioPlayer.PlayController.endOfStream();
                }
            } 

        }

        delegate void afterPlayCountUpPluginDelegate();

        private void afterPlayCountUpPlugin()
        {
            foreach (var plugin in LinearGlobal.Plugins)
            {
                if (plugin.Enable)
                {
                    try
                    {
                        plugin.RunAfterPlayCountUp(LinearGlobal.CurrentPlayItemInfo.Rating);
                    }
                    catch (Exception ex)
                    {
                        LinearAudioPlayer.writeErrorMessage(ex);
                    }
                }
            }
        }

        /// <summary>
        ///  再生ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (LinearGlobal.CurrentPlayItemInfo.Id != -1)
                {
                    // CurrentIdを再生
                    int currentRowNo = LinearAudioPlayer.GridController.Find((int) GridController.EnuGrid.ID,
                                                                             LinearGlobal.CurrentPlayItemInfo.Id.
                                                                                 ToString());
                    if (currentRowNo != -1)
                    {
                        LinearAudioPlayer.PlayController.play(LinearGlobal.CurrentPlayItemInfo.Id, false, false);
                    }
                    else
                    {
                        if (LinearAudioPlayer.GridController.getRowCount() > 0)
                        {
                            LinearAudioPlayer.PlayController.playForGridNo(1, false);
                        }
                    }
                }
                else
                {
                    int activeRowNo = LinearAudioPlayer.GridController.getActiveRowNo();

                    if (activeRowNo > 0)
                    {
                        // フォーカスがあたっているのを再生
                        LinearAudioPlayer.PlayController.playForGridNo(
                            activeRowNo, false);
                    }
                }
            }
        }


        /// <summary>
        /// ポーズボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPause_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                // ポーズ処理
                bool paused = LinearAudioPlayer.PlayController.pause();

            }
        }

        /// <summary>
        /// ストップボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                // 停止処理
                LinearAudioPlayer.PlayController.stop();

            }
        }

        /// <summary>
        /// フォワードボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picFwd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (LinearAudioPlayer.PlayController.isPlaying())
                {
                    LinearAudioPlayer.PlayController.endOfStream();
                }
            }
        }

        /// <summary>
        /// フォームが閉じられたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && isInputTaskTray)
            {
                e.Cancel = true;
                this.Visible = false; // フォーム非表示
                if (this.ListForm.Visible)
                {
                    IsTaskTrayBeforeOpen = true;
                    this.ListForm.Visible = false;
                }
                else
                {
                    IsTaskTrayBeforeOpen = false;
                }
                this.notifyIcon.Visible = true; // Notifyアイコン表示
                isInputTaskTray = false;

            }
            else
            {
                this.notifyIcon.Visible = false;
                MainFunction mf = new MainFunction();
                mf.endPlayer();
                mf = null;
            }
        }

        private void areaSizeChangeRight_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int sizeChangeValue = e.X - sizeChangeStartPos;
                this.Width = this.Width + sizeChangeValue;
            }
        }

        private int sizeChangeStartPos;

        private void areaSizeChangeRight_MouseDown(object sender, MouseEventArgs e)
        {
            sizeChangeStartPos = e.X;
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            MainFunction mf = new MainFunction();

            
            

            // フォームのどこつかんでも移動できるようにする
            picDisplay.MouseDown +=
                new MouseEventHandler(MainForm_MouseDown);
            picDisplay.MouseMove +=
                new MouseEventHandler(MainForm_MouseMove);
            lblTitle.MouseDown +=
                new MouseEventHandler(MainForm_MouseDown);
            lblTitle.MouseMove +=
                new MouseEventHandler(MainForm_MouseMove);

            // レジューム情報があれば再生
            if (LinearGlobal.LinearConfig.PlayerConfig.ResumeId != -1
                && mf.isIdRegistDatabase(LinearGlobal.LinearConfig.PlayerConfig.ResumeId))
            {

                LinearGlobal.CurrentPlayItemInfo.Id =
                    LinearGlobal.LinearConfig.PlayerConfig.ResumeId;

                if (LinearGlobal.LinearConfig.PlayerConfig.ResumePlay
                    && LinearGlobal.LinearConfig.PlayerConfig.ResumePosition != -1
                    && LinearEnum.DatabaseMode.MUSIC.Equals(LinearGlobal.DatabaseMode))
                {

                    // レジューム再生
                    
                    // todo:ボリュームを操作する必要ある？
                    int backupVolume = LinearGlobal.Volume;
                    LinearGlobal.Volume = 0;
                    LinearAudioPlayer.PlayController.play(LinearGlobal.LinearConfig.PlayerConfig.ResumeId,
                        true, false);
                    LinearGlobal.Volume = backupVolume;
                    LinearAudioPlayer.PlayController.setPosition(
                        (uint) LinearGlobal.LinearConfig.PlayerConfig.ResumePosition);
                }
            }

            if (LinearGlobal.LinearConfig.PlayerConfig.IsOpenPlaylist)
            {
                switchViewPlaylist();
            }

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // スタイルロード
            //loadStyle();

            // とりあえずサイズとか調整
            int yDisplayPosition = 4;
            int yDisplayTextPosition = -1;
            int yDisplayTitlePosition = 0;
            // ディスプレイのY軸とコントロールのY軸の差
            int diffYDisplayPositionYControlPosition = 1;
            // ボタンの間隔
            int intervalButton = 19;
            // フォーム高さとディスプレイの高さの差
            int diffFormHeightDisplayHeight = 7;
            // ディスプレイの高さとディスプレイアイテムの高さの差
            int diffDisplayHeightDisplayItemHeight = 2;
            // ディスプレイの高さとスペクトリウムの高さの差
            int diffDisplayHeightSpectrumHeight = 4;
            int diffyPositionSpectrum = 0;
            // スペクトリウムとプレイモードの間隔
            int intervalSpectrumPlayMode = 0;
            int diffMiniprogressbarWidth = 4;
            int diffSpectrum = 0;
            int ypositionMiniprogressbar = 6;
            int ypositionRating = 1;

            // メイリオじゃないとずれるので
            if (!this.lblTitle.Font.Name.Equals("メイリオ"))
            {
                yDisplayTitlePosition = 2;
            }

            // ３D以外のとき調整
            if (this.picDisplay.BorderStyle == BorderStyle.None)
            {
                diffFormHeightDisplayHeight += 2;
                yDisplayTextPosition += 3;
                yDisplayTitlePosition = yDisplayTextPosition;
                diffSpectrum = 4;
                ypositionMiniprogressbar = 5;
                ypositionRating = 0;
                diffyPositionSpectrum = 1;
            }
            else if (this.picDisplay.BorderStyle == BorderStyle.FixedSingle)
            {
                diffFormHeightDisplayHeight += 2;
                yDisplayTextPosition += 2;
                yDisplayTitlePosition = yDisplayTextPosition;
                diffSpectrum = 2;
                ypositionMiniprogressbar = 5;
                diffYDisplayPositionYControlPosition += 0;
                ypositionRating = 0;
            }
            MainFunction.rolltop = yDisplayTitlePosition;

            this.Height = LinearGlobal.LinearConfig.ViewConfig.MainSize.Height;

            this.picDisplay.Size = new Size(this.Width - (intervalButton*4) - ((int) (picPlay.Size.Width*2.2)),
                                            this.Height - diffFormHeightDisplayHeight);

            int heightDisplayItem = this.picDisplay.Size.Height - diffDisplayHeightDisplayItemHeight;
            int widthTime = TextRenderer.MeasureText("99:99", lblTime.Font).Width;
            this.lblTime.Size = new Size(widthTime, heightDisplayItem);
            this.picSpectrum.Size = new Size(28 - diffSpectrum, heightDisplayItem - diffDisplayHeightSpectrumHeight  +1);
            int widthPlayMode = TextRenderer.MeasureText("Random", lblPlayMode.Font).Width;
            this.lblPlayMode.Size = new Size(widthPlayMode, heightDisplayItem);
            int widthBitRate = TextRenderer.MeasureText("99999", lblBitRate.Font).Width;
            this.lblBitRate.Size = new Size(widthBitRate, heightDisplayItem);
            this.miniProgressSeekBar.Size = new Size(widthBitRate  - (diffMiniprogressbarWidth*2), 6);
            //this.miniProgressSeekBar.Size = new Size(33, 6);
            picRating.Size = new Size(16,16);
            picRating.SizeMode = PictureBoxSizeMode.CenterImage;
            

            this.picDisplay.Location = new Point(5, yDisplayPosition);


            this.lblTitle.Location = new Point(0, yDisplayTitlePosition);
            

            this.picSpectrum.Location = new Point(this.picDisplay.Width - this.picSpectrum.Width, yDisplayTextPosition + diffyPositionSpectrum + 1);
            this.lblPlayMode.Location = new Point(this.picSpectrum.Location.X - this.lblPlayMode.Width - intervalSpectrumPlayMode, yDisplayTextPosition);
           
            this.lblTime.Location = new Point(this.lblPlayMode.Location.X - this.lblTime.Width, yDisplayTextPosition);
            this.lblBitRate.Location = new Point(this.lblTime.Location.X - this.lblBitRate.Width, yDisplayTextPosition);
            this.picRating.Location = new Point(this.lblBitRate.Location.X - this.picRating.Width, yDisplayTextPosition + ypositionRating);
            
            this.miniProgressSeekBar.Location = new Point(this.lblBitRate.Location.X + diffMiniprogressbarWidth, yDisplayTextPosition + ypositionMiniprogressbar);

            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTime.TextAlign = ContentAlignment.MiddleRight;
            this.lblPlayMode.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBitRate.TextAlign = ContentAlignment.MiddleRight;
            this.lblTitle.AutoSize = true;

            // サイズ変更グリップ
            this.areaSizeChangeRight.Size = new Size(3, this.Height);
            this.areaSizeChangeRight.Location = new Point(this.Width - 8, 1);

            setTitleCentering();

            // 画像の表示
            int diffControlPosition = 24;
            this.picPlaylist.Location = new Point(this.Size.Width - diffControlPosition,
                                                  yDisplayPosition + diffYDisplayPositionYControlPosition);

            this.picFwd.Location = new Point(this.picPlaylist.Location.X - intervalButton,
                                             yDisplayPosition + diffYDisplayPositionYControlPosition);

            this.picStop.Location = new Point(this.picFwd.Location.X - intervalButton,
                                              yDisplayPosition + diffYDisplayPositionYControlPosition);

            this.picPause.Location = new Point(this.picStop.Location.X - intervalButton,
                                               yDisplayPosition + diffYDisplayPositionYControlPosition);

            this.picPlay.Location = new Point(this.picPause.Location.X - intervalButton,
                                              yDisplayPosition + diffYDisplayPositionYControlPosition);

            //this.picRating.Load(LinearGlobal.StyleDirectory + "rating_favorite.png");


        }

        /// <summary>
        /// スタイルロード
        /// </summary>
        public void loadStyle(StyleConfig styleConfig)
        {

            if (IsOpenListForm)
            {
                this.picPlaylist.Load(LinearGlobal.StyleDirectory + "control_eject_on.png");
            }
            else
            {
                this.picPlaylist.Load(LinearGlobal.StyleDirectory + "control_eject.png");
            }

            this.picFwd.Load(LinearGlobal.StyleDirectory + "control_fwd.png");

            this.picStop.Load(LinearGlobal.StyleDirectory + "control_stop.png");

            this.picPause.Load(LinearGlobal.StyleDirectory + "control_pause.png");

            this.picPlay.Load(LinearGlobal.StyleDirectory + "control_play.png");

            if (File.Exists(LinearGlobal.StyleDirectory + "background_miniface.png"))
            {
                this.picDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
                using (FileStream fs = File.OpenRead(LinearGlobal.StyleDirectory + "background_miniface.png"))
                using (Image fsimg = Image.FromStream(fs, false, false))
                {
                    Bitmap bm = new Bitmap(fsimg); 
                    this.picDisplay.BackgroundImage = bm;
                }
                this.picDisplay.BackgroundImageLayout = ImageLayout.Tile;

                //this.picDisplay.Load(LinearGlobal.StyleDirectory + "background_miniface.png");
            }
            else
            {
                this.picDisplay.BackgroundImage = null;
            }

            Image img = new Bitmap(LinearGlobal.StyleDirectory + "rating_favorite.png");
            ratingFavoriteImage = new Bitmap(12, 12);
            Graphics g = Graphics.FromImage(ratingFavoriteImage);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, 12, 12);

            img = new Bitmap(LinearGlobal.StyleDirectory + "rating_normal.png");
            ratingNormalImage = new Bitmap(12, 12);
            g = Graphics.FromImage(ratingNormalImage);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, 12, 12);

            if (LinearGlobal.LinearConfig.ViewConfig.FontBold)
            {
                styleConfig.MainTitleFontConfig.FontStyle = FontStyle.Bold;
                styleConfig.MainDisplayFontConfig.FontStyle = FontStyle.Bold;
            }

            lblTitle.Font = new Font(styleConfig.MainTitleFontConfig.FontName,
                                     styleConfig.MainTitleFontConfig.FontSize,
                                     styleConfig.MainTitleFontConfig.FontStyle);

            Font displayFont = new Font(styleConfig.MainDisplayFontConfig.FontName,
                                        styleConfig.MainDisplayFontConfig.FontSize,
                                        styleConfig.MainDisplayFontConfig.FontStyle);
            lblTime.Font = displayFont;
            lblPlayMode.Font = displayFont;

            this.picDisplay.BorderStyle = styleConfig.DisplayBorderStyle;

            if (LinearEnum.RatingValue.FAVORITE.Equals(picRating.Tag))
            {
                picRating.Image = ratingFavoriteImage;
            }
            else
            {
                picRating.Image = ratingNormalImage;
            }

            this.MainForm_Resize(null, null);
            this.Refresh();
            this.picDisplay.Refresh();

        }

        
        public void setTitleCentering()
        {
            if (LinearGlobal.LinearConfig.ViewConfig.isTitleCentering)
            {
                this.lblTitle.Left = ((picDisplay.Width - picRating.Width - miniProgressSeekBar.Width - lblTime.Width - lblPlayMode.Width - picSpectrum.Width)/2) - (lblTitle.Width/2);
            }
        }

        private delegate void ShowNotificationWindowDelegate(GridItemInfo gi);

        public void showNotificationWindow(GridItemInfo gi)
        {
            LinearAudioPlayer.PlayController.getPicture(gi);
            this.BeginInvoke(new ShowNotificationWindowDelegate(showNotificationWindowAsync), gi);

        }

        private void showNotificationWindowAsync(GridItemInfo gi)
        {
            // 通知ウインドウに設定
            
            LinearGlobal.MainForm.setNotificationWindow(gi);
            popupNotifier.Position = Point.Empty;
            popupNotifier.Scroll = LinearGlobal.LinearConfig.ViewConfig.isSlidePIP;

            if (LinearGlobal.LinearConfig.ViewConfig.isNotificationWindow)
            {
                popupNotifier.OwnerForm = this.ListForm;
                popupNotifier.PopupAsync();
            }
        }

        /// <summary>
        /// ドラッグ&ドロップされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                // ドラッグ中のファイルやディレクトリの取得
                string[] drags = (string[]) e.Data.GetData(DataFormats.FileDrop);

                e.Effect = DragDropEffects.Link;
            }
        }

        /// <summary>
        /// ドラッグ&ドロップされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            this.ListForm.grid_DragDrop(sender, e);
        }

        /// <summary>
        /// プレイリストボタン上でマウスが静止したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPlaylist_MouseHover(object sender, EventArgs e)
        {
            //switchViewPlaylist();
        }


        #endregion


        /*
            パブリックメソッド 
        */

        #region PublicMethod

        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,
                          true);


            // Aplication Title
            this.lblTitle.Text = LinearConst.APPLICATION_TITLE.ToUpper() + " " + LinearGlobal.ApplicationVersion +
                                 " - FINALSTREAM";

            // lblTitleの親コントロールをpicDisplayとする
            picDisplay.Controls.Add(picRating);
            picDisplay.Controls.Add(picSpectrum);
            picDisplay.Controls.Add(lblTime);
            picDisplay.Controls.Add(miniProgressSeekBar);
            picDisplay.Controls.Add(lblBitRate);
            picDisplay.Controls.Add(lblPlayMode);
            picDisplay.Controls.Add(lblTitle);
            
            


            // ListFormをNew
            _listForm = new ListForm();
            _listForm.Owner = this;

            restoreSetting();

            ConfigForm = new ConfigForm();

            

            // ツールチップ作成
            //ToolTip viewListTip = new ToolTip();
            //viewListTip.ShowAlways = true;
            //viewListTip.SetToolTip(this.picPlaylist, "test");

            /*
            this.picAni.Image = Image.FromFile(LinearGlobal.StyleDirectory + "HighSpin2 Work Red.gif");
            this.picAni.Size = new Size(18, 18);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            //丸を描く
            path.AddEllipse(new Rectangle(0, 0, 15, 15));
            //真ん中を丸くくりぬく
            path.AddEllipse(new Rectangle(9, 9, 3, 3));
            this.picAni.Region = new Region(path);
            */

            /*
            Bitmap bmp = new Bitmap(LinearGlobal.StyleDirectory + "aaa.bmp");
            bmp.MakeTransparent(Color.Fuchsia);
            this.picAni.Image = bmp;
            this.picAni.Size = bmp.Size;
            */

            


        }

        
        

        /// <summary>
        /// タイマーを設定する
        /// </summary>
        /// <param name="enabled">有効</param>
        public void setTimer(bool enabled)
        {
            this.timerMain.Enabled = enabled;
            //this.timerSpectrum.Enabled = enabled;
        }

        /// <summary>
        /// タイトルを設定する
        /// </summary>
        /// <param name="title"></param>
        public void setTitle(string title)
        {
            this.lblTitle.Text = title;
            this.Text = title;
            // 続けざまに表示するため this.lblTitle.Left = 0;
            this.picDisplay.Refresh();
        }

        /// <summary>
        /// 通知ウインドウを設定する
        /// </summary>
        /// <param name="gi"></param>
        public void setNotificationWindow(GridItemInfo gi)
        {
            popupNotifier.Delay = (int) (LinearGlobal.LinearConfig.ViewConfig.PIPViewDuration*1000);
            popupNotifier.TitleText = gi.Title;
            string year;
            if (String.IsNullOrEmpty(gi.Year))
            {
                year = "";
            }
            else
            {
                year = "[" + gi.Year + "]";
            }

            string ext = "";
            if (!String.IsNullOrEmpty(Path.GetExtension(gi.FilePath)))
            {
                ext = Path.GetExtension(gi.FilePath).ToUpper().Substring(1);
            }

            popupNotifier.ContentText =
                gi.Artist + "\n"
                + gi.Album + " " + year + "\n"
                + gi.Time + "  "
                + ext + "  "
                + gi.Bitrate + " kbps" + "  " + gi.PlayCount + " plays";

            //if (TextRenderer.MeasureText(popupNotifier.TitleText, popupNotifier.TitleFont).Width + popupNotifier.ImageSize.Width > popupNotifier.Size.Width)
            //{
            popupNotifier.Size =
                new Size(
                    TextRenderer.MeasureText(popupNotifier.TitleText, popupNotifier.TitleFont).Width +
                    popupNotifier.ImageSize.Width + 100, popupNotifier.Size.Height);
            //}

            if (TextRenderer.MeasureText(popupNotifier.ContentText, popupNotifier.ContentFont).Width +
                popupNotifier.ImageSize.Width + 100 > popupNotifier.Size.Width)
            {
                popupNotifier.Size =
                    new Size(
                        TextRenderer.MeasureText(popupNotifier.ContentText, popupNotifier.ContentFont).Width +
                        popupNotifier.ImageSize.Width + 100, popupNotifier.Size.Height);
            }

            popupNotifier.Image = gi.Picture;
            popupNotifier.ImageUrl = gi.PictureUrl;





        }

        public void setTime(string time)
        {
            this.lblTime.Text = time;
        }



        public void setRating(long rating)
        {
            switch (rating)
            {
                case (int)LinearEnum.RatingValue.NORMAL:
                case (int)LinearEnum.RatingValue.NOTRATING:
                    picRating.Image = ratingNormalImage;
                    picRating.Tag = LinearEnum.RatingValue.NORMAL;
                    LinearGlobal.CurrentPlayItemInfo.Rating = (int)LinearEnum.RatingValue.NORMAL;
                    break;
                case (int)LinearEnum.RatingValue.FAVORITE:
                    picRating.Image = ratingFavoriteImage;
                    picRating.Tag = LinearEnum.RatingValue.FAVORITE;
                    LinearGlobal.CurrentPlayItemInfo.Rating = (int)LinearEnum.RatingValue.FAVORITE;
                    break;
                case (int)LinearEnum.RatingValue.EXCLUSION:
                    picRating.Image = null;
                    picRating.Tag = LinearEnum.RatingValue.EXCLUSION;
                    LinearGlobal.CurrentPlayItemInfo.Rating = (int)LinearEnum.RatingValue.EXCLUSION;
                    break;

            }
        }

        /// <summary>
        /// ミニプログレスバーの最大値を設定する
        /// </summary>
        /// <param name="maxValue">最大値</param>
        public void setMiniProgressSeekBarMax(int maxValue)
        {

            this.miniProgressSeekBar.Maximum = maxValue;

        }

        /// <summary>
        /// プログレスバーの現在値を設定する
        /// </summary>
        /// <param name="currentValue">現在値</param>
        public void setMiniProgressSeekBarValue(float currentValue)
        {
            // 100%の値をもとめる
            float value = currentValue / miniProgressSeekBar.Maximum;

            if (value < 0.04f)
            {
                value = 0.04f;
            }

            this.miniProgressSeekBar.Value = value * 100f;
        }


        public void switchViewPlaylist()
        {

            if (IsOpenListForm == false)
            {
                // ListFormが表示されていないとき

                //位置、サイズをMainFormに合わせる
                ListForm.Show();
                ListForm.Height = LinearGlobal.LinearConfig.ViewConfig.ListSize.Height;
                // クリックレス機能のためアクティブにする
                this.Activate();
                syncFormPosSize();
                // フォーカス移動
                LinearAudioPlayer.GridController.setFocusRowNo(
                    LinearAudioPlayer.GridController.Find((int) GridController.EnuGrid.ID,
                                                          LinearGlobal.CurrentPlayItemInfo.Id.ToString()));

                this.picPlaylist.Load(LinearGlobal.StyleDirectory + "control_eject_on.png");
                IsOpenListForm = true;
            }
            else
            {
                ListForm.Hide();
                //ListForm.hideInterruptForm();
                IsOpenListForm = false;
                this.picPlaylist.Load(LinearGlobal.StyleDirectory + "control_eject.png");
            }
            LinearGlobal.LinearConfig.PlayerConfig.IsOpenPlaylist = IsOpenListForm;
        }

        #endregion


        #region PublicMethod

        /// <summary>
        /// 設定を復元する。
        /// </summary>
        public void restoreSetting()
        {
            // 位置を復元
            this.SetBounds(
                LinearGlobal.LinearConfig.ViewConfig.MainLocation.X,
                LinearGlobal.LinearConfig.ViewConfig.MainLocation.Y,
                LinearGlobal.LinearConfig.ViewConfig.MainSize.Width,
                LinearGlobal.LinearConfig.ViewConfig.MainSize.Height,
                BoundsSpecified.All);


            // 設定を復元
            LinearGlobal.PlayMode =
                (LinearEnum.PlayMode) LinearGlobal.LinearConfig.PlayerConfig.PlayMode;
            setPlayMode(LinearGlobal.PlayMode);

            // 常に手前復元
            this.TopMost =
                LinearGlobal.LinearConfig.ViewConfig.TopMost;
            this.ListForm.TopMost =
                LinearGlobal.LinearConfig.ViewConfig.TopMost;
            if (LinearGlobal.LinearConfig.ViewConfig.TopMost)
            {
                this.topMosttoolStripMenuItem.Checked = true;
            }

            // フェード効果復元
            if (LinearGlobal.LinearConfig.SoundConfig.FadeEffect)
            {
                this.fadeEffectToolStripMenuItem.Checked = true;
            }

            // 画像の表示
            


            // カラープロファイルを設定
            //setColorProfile();

            // 透明度を復元
            this.Opacity = LinearGlobal.LinearConfig.ViewConfig.Opacity;

        }

        /// <summary>
        /// カラープロファイルを設定
        /// </summary>
        public void setColorProfile(ColorConfig colorConfig)
        {
            this.BackColor = colorConfig.FormBackgroundColor;
            lblTitle.ForeColor = colorConfig.FontColor;
            picDisplay.BackColor = colorConfig.DisplayBackgroundColor;
            this.picDisplay.BorderColor = colorConfig.DisplayBorderColor;
            //lblBitRate.ForeColor = colorConfig.BitRateColor;
            lblTime.ForeColor = colorConfig.PlayTimeColor;
            lblPlayMode.ForeColor = colorConfig.PlayModeColor;

            this.miniProgressSeekBar.BorderColor = colorConfig.MiniProgressSeekBarBorderColor;
            this.miniProgressSeekBar.ProgressBarMainBottomBackgroundColor =
                colorConfig.MiniProgressSeekBarMainBottomBackgroundColor;
            this.miniProgressSeekBar.ProgressBarMainUnderBackgroundColor =
                colorConfig.MiniProgressSeekBarMainUnderBackgroundColor;
            this.miniProgressSeekBar.ProgressBarUpBottomBackgroundColor =
                colorConfig.MiniProgressSeekBarUpBottomBackgroundColor;
            this.miniProgressSeekBar.ProgressBarUpUnderBackgroundColor =
                colorConfig.MiniProgressSeekBarUpUnderBackgroundColor;
            this.miniProgressSeekBar.BorderColor = colorConfig.MiniProgressSeekBarBorderColor;
            this.miniProgressSeekBar.Theme = colorConfig.MiniProgressSeekBarTheme;

            if (miniProgressSeekBar.Theme == VistaProgressBarTheme.Custom)
            {
                miniProgressSeekBar.ProgressBarMainBottomActiveColor =
                    colorConfig.MiniProgressSeekBarMainBottomActiveColor;
                miniProgressSeekBar.ProgressBarMainUnderActiveColor =
                    colorConfig.MiniProgressSeekBarMainUnderActiveColor;
                miniProgressSeekBar.ProgressBarUpBottomActiveColor =
                    colorConfig.MiniProgressSeekBarUpBottomActiveColor;
                miniProgressSeekBar.ProgressBarUpUnderActiveColor =
                    colorConfig.MiniProgressSeekBarUpUnderActiveColor;

            }

            popupNotifier.HeaderColor = colorConfig.NotificationHeaderColor;
            popupNotifier.TitleColor = colorConfig.NotficationFontColor;
            popupNotifier.ContentColor = colorConfig.NotficationFontColor;
            popupNotifier.ContentHoverColor = colorConfig.NotficationFontColor;
            popupNotifier.BodyColor = colorConfig.NotficationBodyFirstColor;
            popupNotifier.BodyColor2 = colorConfig.NotficationBodySecondColor;
            popupNotifier.isChangeColor = true;

            Graphics g = picSpectrum.CreateGraphics();
            gb = new LinearGradientBrush(
                g.VisibleClipBounds,
                colorConfig.SpectrumLevelHightLevelColor,
                colorConfig.SpectrumLevelLowLevelColor,
                LinearGradientMode.Vertical);
        }

        /// <summary>
        /// フォームサイズを同期する
        /// </summary>
        private void syncFormPosSize()
        {

            // 位置、サイズをMainFormにあわせる
            if (this.Location.Y + this.Size.Height + ListForm.Height > Screen.GetWorkingArea(this).Height)
            {
                // ワークエリアを越えたら上に表示
                ListForm.SetBounds(this.Location.X, this.Location.Y - ListForm.Size.Height, this.Size.Width,
                                   ListForm.Size.Height, BoundsSpecified.All);
            }
            else
            {
                // ワークエリアを越えなかったら下に表示
                ListForm.SetBounds(this.Location.X, this.Location.Y + this.Size.Height, this.Size.Width,
                                   ListForm.Size.Height, BoundsSpecified.All);
            }
        }

        /// <summary>
        /// プレイモードを設定する。
        /// </summary>
        private void setPlayMode(LinearEnum.PlayMode playMode)
        {

            // オーバしたら戻す
            if (playMode == LinearEnum.PlayMode.OVER)
            {
                playMode = 0;
            }

            switch (playMode)
            {
                case LinearEnum.PlayMode.NORMAL:
                    this.lblPlayMode.Text = "Normal";
                    break;
                case LinearEnum.PlayMode.ONEOFF:
                    this.lblPlayMode.Text = "OneOff";
                    break;
                case LinearEnum.PlayMode.REPEAT:
                    this.lblPlayMode.Text = "Repeat";
                    break;
                case LinearEnum.PlayMode.ENDLESS:
                    this.lblPlayMode.Text = "Endless";
                    break;
                case LinearEnum.PlayMode.RANDOM:
                    this.lblPlayMode.Text = "Random";
                    break;
            }

            //LinearGlobal.NextPlaylist.Clear();


            LinearGlobal.PlayMode = playMode;
        }

        public void setPlayEngineInfo(string peinfo)
        {
            this.PoweredToolStripMenuItem.Text = peinfo;
        }

        public void clearSpectrum()
        {
            picSpectrum.Image = null;
        }

        #endregion



        /*
         * コンテキストメニュー
         */

        #region Context Menu

        /// <summary>
        /// Exit選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーションを終了する
            LinearAudioPlayer.PlayController.MedleyInfo.Enable = false;
            Application.Exit();

        }

        /// <summary>
        /// Version選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AboutForm abForm = new AboutForm();
            abForm.ShowDialog();
        }

        /// <summary>
        /// 常に手前選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topMosttoolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.TopMost = !this.TopMost;
            this.ListForm.TopMost = !this.ListForm.TopMost;

        }

        /// <summary>
        /// フェード効果選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fadeEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.SoundConfig.FadeEffect =
                !LinearGlobal.LinearConfig.SoundConfig.FadeEffect;
        }


        /// <summary>
        /// オートフィットクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoFittoolStripMenuItem_Click(object sender, EventArgs e)
        {
            // フォームの位置を変更
            this.Location
                = new Point(System.Windows.Forms.Screen.GetWorkingArea(this).Location.X,
                            System.Windows.Forms.Screen.GetWorkingArea(this).Height - this.Height);
            // フォームのサイズを変更
            this.Size = new Size(System.Windows.Forms.Screen.GetWorkingArea(this).Width, this.Size.Height);
        }

        /// <summary>
        /// タスクトレイ格納クリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isInputTaskTray = true;
            this.Close();

        }

        //マウスのクリック位置を記憶
        private Point mousePoint;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }

            // 進むボタン
            if (e.Button == MouseButtons.XButton2)
            {
                // 次の曲に
                MouseEventArgs mea = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
                picFwd_MouseDown(sender, mea);
            }

            // 戻るボタン
            if (e.Button == MouseButtons.XButton1)
            {
                // 前の曲に
                LinearAudioPlayer.PlayController.prebiousPlay();
            }

        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left)== MouseButtons.Left)
            {

                if (!((e.Button & MouseButtons.Right) == MouseButtons.Right))
                {
                    // 移動をワーキングエリアに限定する。
                    if (0 <= this.Top + e.Y - mousePoint.Y
                        && Screen.GetWorkingArea(this).Height - this.Height >= this.Top + e.Y - mousePoint.Y)
                    {
                        this.Top += e.Y - mousePoint.Y;
                    }

                    if (0 <= this.Left + e.X - mousePoint.X
                        && Screen.GetWorkingArea(this).Width - this.Width >= this.Left + e.X - mousePoint.X)
                    {
                        this.Left += e.X - mousePoint.X;
                    }

                }
                else
                {
                    // 右クリックも同時押しの場合は無制限に移動できるようにする。
                    this.Top += e.Y - mousePoint.Y;
                    this.Left += e.X - mousePoint.X;
                }
            }

            //this.ResumeLayout();
        }

        #endregion


        #region PublicMehod

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true; // フォームの表示
            if (IsTaskTrayBeforeOpen)
            {
                this.ListForm.Visible = true;
            }
            this.notifyIcon.Visible = false; // Notifyアイコン非表示
            //this.Activate();
        }

        /// <summary>
        /// タスクトレイから出す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TTTaskTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {

            notifyIcon_MouseDoubleClick(sender, null);

        }

        /// <summary>
        /// 設定画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!ConfigForm.Visible)
            {
                ConfigForm.Visible = true;
            }



        }

        private void TTExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーションを終了する
            Application.Exit();
        }



        #endregion


        private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // 次の曲に
                picFwd_MouseDown(sender, e);

            }
        }



        private void drawSpectrum(Graphics g)
        {

            int count2 = 0;


            /*
                    DRAW SPECTRUM
            */
            //for (count = 0; count < 1; count++)
            //{
            //float max = 0.005f;

            float[] spectrum = LinearAudioPlayer.PlayController.getSpectrum();

            /*
            for (count2 = 511; count2 < 2047; count2++)
            {

                if (max < spectrum[count2])
                {
                    max = spectrum[count2];
                }

            }*/


            /*
                The upper band of frequencies at 44khz is pretty boring (ie 11-22khz), so we are only
                going to display the first 256 frequencies, or (0-11khz) 
            */
            count2 = 1;
            float count3 = 2;
            int count4;

            //picSpectrum.Location = new Point(1000 , picSpectrum.Location.Y);
            //picSpectrum.Size = new Size(1000, picSpectrum.Size.Height);
            for (count4 = 0; count4 < 6;)
            {
                //if (count2 % 512 == 0)
                //{
                float height;

                height = (spectrum[count2 - 1]/(LinearGlobal.LinearConfig.ViewConfig.MiniVisualizationLevel*0.1f))*
                         picSpectrum.Size.Height;
                //height = (spectrum[count2 - 1] )*
                //         picSpectrum.Size.Height;


                if (height >= picSpectrum.Size.Height)
                {
                    height = picSpectrum.Size.Height;
                }

                if (height < 0)
                {
                    height = 0;
                }

                height = picSpectrum.Size.Height - height;


                gb.GammaCorrection = true;

                g.FillRectangle(gb, count3, (int) height - 2.0f, 2.0f,
                                (float) Math.Ceiling(picSpectrum.Size.Height - height));
                count3 += 2.0f;
                count3++;

                count4++;
                //}

                count2 += 1;
            }
            //}
        }


        private void picSpectrum_Paint(object sender, PaintEventArgs e)
        {
            if (LinearAudioPlayer.PlayController != null && LinearAudioPlayer.PlayController.isPlaying())
            {
                //picSpectrum.Image = new Bitmap(picSpectrum.Width, picSpectrum.Height);
                //Graphics grfx = Graphics.FromImage(picSpectrum.Image);

                drawSpectrum(e.Graphics);
            }

        }

        private void popupNotifier_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();
            lf.execTagEditor(LinearGlobal.CurrentPlayItemInfo.Id);

            lf = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int penWidth = 1;
            Pen outSideBottomLeftPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.OutSideBottomLeftLineColor), penWidth);
            Pen inSideBottomLeftPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.InSideBottomLeftLineColor), penWidth);
            Pen outSideUnderRightPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.OutSideUnderRightLineColor), penWidth);
            Pen inSideUnderRightPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.InSideUnderRightLineColor), penWidth);
           
            // 立体感を出す

            // 左ライン
            e.Graphics.DrawLine(outSideBottomLeftPen,
               0,
               0,
               0,
               this.Size.Height);
            e.Graphics.DrawLine(inSideBottomLeftPen,
               1,
               0,
               1,
               this.Size.Height);

            // 上ライン
            e.Graphics.DrawLine(outSideBottomLeftPen,
               0,
               0,
               this.Size.Width,
               0);
            e.Graphics.DrawLine(inSideBottomLeftPen,
               0,
               1,
               this.Size.Width,
               1);

            // 右ライン
            e.Graphics.DrawLine(outSideUnderRightPen,
               this.Size.Width-1,
                0,
                this.Size.Width-1,
                this.Size.Height);
            e.Graphics.DrawLine(inSideUnderRightPen,
              this.Size.Width - 2,
               0,
               this.Size.Width - 2,
               this.Size.Height);

            // 下ライン
            e.Graphics.DrawLine(outSideUnderRightPen,
                0,
                this.Size.Height-1,
                this.Size.Width,
                this.Size.Height-1);
            e.Graphics.DrawLine(inSideUnderRightPen,
                0,
                this.Size.Height - 2,
                this.Size.Width,
                this.Size.Height - 2);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        private void miniProgressSeekBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // シークがクリックされたらメドレーをとめる。
                LinearAudioPlayer.PlayController.MedleyInfo.Enable = false;
                LinearGlobal.MainForm.ListForm.setMedleyModeColor(LinearGlobal.ColorConfig);

                double clickPersent = ((double) e.X/(double) this.miniProgressSeekBar.Width);

                double value = ((double) LinearAudioPlayer.PlayController.getLength())*clickPersent;
                LinearAudioPlayer.PlayController.setPosition((uint) value);
                LinearGlobal.MainForm.setMiniProgressSeekBarValue((int) value);
            }
            //Debug.WriteLine(clickPersent.ToString());
        }

        private void picDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switchViewPlaylist();
            }
        }

        private void picRating_MouseDown(object sender, MouseEventArgs e)
        {
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            long id = LinearGlobal.CurrentPlayItemInfo.Id;
            int rowNo = LinearAudioPlayer.GridController.Find((int) GridController.EnuGrid.ID, id.ToString());

            if (picRating.Image != null)
            {
                // EXCLUSION以外のとき
                if (e.Button == MouseButtons.Left)
                {
                    if (ratingFavoriteImage.Equals(picRating.Image))
                    {
                        // FAVORITE -> NORMAL
                        picRating.Image = ratingNormalImage;
                        SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    id, LinearEnum.RatingValue.NORMAL));
                        LinearGlobal.CurrentPlayItemInfo.Rating = (int)LinearEnum.RatingValue.NORMAL;

                        if (rowNo != -1)
                        {
                            LinearAudioPlayer.GridController.setRatingIcon(rowNo, LinearEnum.RatingValue.NORMAL);
                        }

                        if (LinearGlobal.PlaylistMode == LinearEnum.PlaylistMode.FAVORITE)
                        {
                            LinearAudioPlayer.GridController.Grid.Rows.Remove(rowNo);
                        }

                    }
                    else
                    {
                        // NOMAL -> FAVORITE
                        picRating.Image = ratingFavoriteImage;
                        // FAVORITEに変更
                        SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    id, LinearEnum.RatingValue.FAVORITE));
                        LinearGlobal.CurrentPlayItemInfo.Rating = (int)LinearEnum.RatingValue.FAVORITE;

                        if (rowNo != -1)
                        {
                            LinearAudioPlayer.GridController.setRatingIcon(rowNo, LinearEnum.RatingValue.FAVORITE);
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right 
                    && LinearGlobal.PlaylistMode == LinearEnum.PlaylistMode.NORMAL 
                    && ratingNormalImage.Equals(picRating.Image))
                {
                    // 除外にする
                    // EXCLUSIONに変更
                    SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    id, LinearEnum.RatingValue.EXCLUSION));
                    picRating.Image = null;
                    LinearGlobal.CurrentPlayItemInfo.Rating = (int)LinearEnum.RatingValue.EXCLUSION;

                    if (rowNo != -1)
                    {
                        LinearAudioPlayer.GridController.Grid.Rows.Remove(rowNo);
                    }
                }
            }
        }

        /// <summary>
        /// 上下キー
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            int vol = LinearGlobal.Volume;

            if (keyData  == Keys.Up)
            {
                vol++;
                if (vol > 100)
                {
                    vol = 100;
                }
                LinearGlobal.Volume = vol;
                LinearGlobal.MainForm.ListForm.setVolume();
                this.lblTitle.Text = LinearAudioPlayer.PlayController.createTitle() + " (volume: " + LinearGlobal.Volume + "%)";

            }
            else if (keyData == Keys.Down)
            {
                vol--;
                if (vol < 0)
                {
                    vol = 0;
                }
                LinearGlobal.Volume = vol;
                LinearGlobal.MainForm.ListForm.setVolume();
                this.lblTitle.Text = LinearAudioPlayer.PlayController.createTitle() + " (volume: " + LinearGlobal.Volume + "%)";
            }
            
            return true;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.lblTitle.Text = LinearAudioPlayer.PlayController.createTitle();
        }



    }
}
