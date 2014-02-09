using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using FINALSTREAM.Commons.Controls;
using FINALSTREAM.Commons.Controls.Toast;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Grid;
using FINALSTREAM.Commons.Network;
using FINALSTREAM.Commons.Parser;
using System.Diagnostics;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.Commons.Utils;
using System.IO;
using FINALSTREAM.LinearAudioPlayer.GUI.option;
using FINALSTREAM.LinearAudioPlayer.Plugin;
using FINALSTREAM.LinearAudioPlayer.Setting;
using FINALSTREAM.LinearAudioPlayer.Utils;
using FINALSTREAM.LinearAudioPlayer.Resources;
using FINALSTREAM.Commons.Forms;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.LinearAudioPlayer.Info;
using Newtonsoft.Json;
using SourceGrid;


namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    public partial class ListForm : Form
    {

        /*
            プライベートメンバ
        */
        #region PrivateMember

        
        private int _tmpVolume = -1;                          // サイレントモード前ボリューム
        //private InterruptForm _interruptForm;
        private bool _isInterruptFormShowSizeChange;
        private string[] _dragFiles;
        private BalloonForm _balloonForm;

        #endregion


        /*
            プロパティ
        */
        #region Property

        /// <summary>
        /// グリッド
        /// </summary>
        public SourceGrid.Grid Grid
        {
            get { return grid; }
        }

        /// <summary>
        /// データベースリスト
        /// </summary>
        public ComboBoxEx DatabaseList
        {
            get { return databaseList; }
        }

        public int TmpVolume
        {
            get { return _tmpVolume; }
        }

        /*
        public InterruptForm InterruptForm
        {
            get
            {
                return _interruptForm;
            }
        }*/

        /// <summary>
        /// 割り込みフォーム表示時にサイズチェンジしたかフラグ
        /// </summary>
        public bool IsInterruptFormShowSizeChange
        {
            get
            {
                return _isInterruptFormShowSizeChange;
            }
        }

        #endregion

        /*
            プライベートイベント 
        */
        #region Private Event

        private void ListForm_Load(object sender, EventArgs e)
        {
            setGridInfoString(grid.RowsCount - 1);

            setVolume();

            // 自動登録スレッド開始
            if (LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.IsEnable
                && LinearGlobal.LinearConfig.PlayerConfig.SelectDatabase.Equals(LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.TargetDatabase))
            {

                LinearAudioPlayer.PlayController.executeAutoAudioFileRegist();
                
            }

            

        }

        

        /// <summary>
        /// サイズ変更したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListForm_Resize(object sender, EventArgs e)
        {
            //loadStyle();
            this.SuspendLayout();

            int xStartPosition = 5;
            int yStartPosition = 10;
            int diffYComboPosition = yStartPosition - 4;
            int intervalGridHeight = 10;
            int diffYFilteringBox = 0;
            if (LinearGlobal.StyleConfig.DisplayBorderStyle == BorderStyle.None)
            {
                yStartPosition += 3;
                diffYComboPosition += 3;
                diffYFilteringBox += 3;
            }
            else if (LinearGlobal.StyleConfig.DisplayBorderStyle == BorderStyle.FixedSingle)
            {
                yStartPosition += 0;
                diffYComboPosition += 0;
                diffYFilteringBox += 4;
            }

            //this.picDatabase.Load(LinearGlobal.StyleDirectory + "database.png");
            this.picDatabase.Location = new Point(xStartPosition, yStartPosition);
            
            this.databaseList.Size = new Size(150, this.filteringBox.Height);
            this.databaseList.Location = new Point(this.picDatabase.Location.X + this.picDatabase.Width + 2, diffYComboPosition);

            this.picPlaylistMode.Location = new Point(this.databaseList.Location.X + this.databaseList.Width + 5, yStartPosition);

            this.lblPlaylistMode.Size = new Size(65, this.databaseList.Height-3);
            this.lblPlaylistMode.Location = new Point(this.picPlaylistMode.Location.X + this.picPlaylistMode.Width + 3, yStartPosition);

            //this.picCondMode.Load(LinearGlobal.StyleDirectory + "filter.png");
            //this.lblLimit.Location = new Point(this.lblPlaylistMode.Location.X + lblPlaylistMode.Width, yStartPosition);
            this.picLimit.Location = new Point(lblPlaylistMode.Location.X + lblPlaylistMode.Width -3, yStartPosition);

            this.picShuffle.Location = new Point(picLimit.Location.X + picLimit.Width + 13, yStartPosition+1);
            this.lblShuffle.Location = new Point(picShuffle.Location.X + picShuffle.Width, yStartPosition);
            this.picMedley.Location = new Point(lblShuffle.Location.X + lblShuffle.Width, yStartPosition);
            this.lblMedley.Location = new Point(picMedley.Location.X + picMedley.Width + 2, yStartPosition);

            this.filteringBox.Size = new Size(300, this.filteringBox.Size.Height);

            this.picLinkLibrary.Location = new Point(lblMedley.Location.X + lblMedley.Width, yStartPosition);
            this.lblLinkLibrary.Location = new Point(picLinkLibrary.Location.X + picLinkLibrary.Width +2, yStartPosition);


            this.picSearch.Location = new Point(this.lblLinkLibrary.Location.X + this.lblLinkLibrary.Width + 5, yStartPosition);
            //this.picSearch.Location = new Point(this.lblLinkLibrary.Location.X + this.lblLinkLibrary.Width +5, yStartPosition);

            this.filteringBox.Location = new Point(this.picSearch.Location.X + this.picSearch.Width + 4, diffYComboPosition + diffYFilteringBox);
            this.picTag.Location = new Point(filteringBox.Location.X + filteringBox.Width + 5, yStartPosition);

            this.gridInfo.Location = new Point(this.picTag.Location.X + this.picTag.Width + 14, yStartPosition);
            //this.gridInfo.Location = new Point(filteringBox.Location.X + filteringBox.Width + 5, yStartPosition);
            //this.gridInfo.Location = new Point(lblShuffle.Location.X + lblShuffle.Width + 5, yStartPosition);
            this.gridInfo.Size = new Size(this.Width - this.picDatabase.Width - this.databaseList.Width - this.picPlaylistMode.Width - this.picShuffle.Width -this.picMedley.Width -this.picLinkLibrary.Width - this.lblShuffle.Width - this.lblMedley.Width - this.filteringBox.Width - this.picSearch.Width - this.lblLinkLibrary.Width - this.picTag.Width - this.lblPlaylistMode.Width - this.picClose.Width - this.picLimit.Width - 72, this.filteringBox.Height);

            //this.picTag.Location = new Point(this.gridInfo.Location.X + this.gridInfo.Width + 7, yStartPosition);
            //this.picClose.Location = new Point(this.picTag.Location.X + this.picTag.Width + 14, yStartPosition);
            this.picClose.Location = new Point(this.gridInfo.Location.X + this.gridInfo.Width + 7, yStartPosition);
            //this.picSearch.Location = new Point(this.gridInfo.Location.X + this.gridInfo.Width + 5, yStartPosition);
            //this.lblQuickFilter.Location = new Point(this.picSearch.Location.X + this.picSearch.Width, yStartPosition);
            //this.filteringBox.Location = new Point(this.lblQuickFilter.Location.X + this.lblQuickFilter.Width + 2, diffYComboPosition + diffYFilteringBox);
            //this.picTag.Location = new Point(this.filteringBox.Location.X + this.filteringBox.Width + 7, yStartPosition);
            //this.picClose.Location = new Point(this.picTag.Location.X + this.picTag.Width + 14, yStartPosition);


            this.gridFiltering.Location = new Point(xStartPosition, picDatabase.Location.Y + picDatabase.Size.Height + intervalGridHeight);
            this.gridFiltering.Size = new Size(275, (int) (this.Size.Height * 0.4));

            this.lblArtist.Location = new Point(xStartPosition, picDatabase.Location.Y + picDatabase.Size.Height + gridFiltering.Height + intervalGridHeight + 7);
            this.lblAlbum.Location = new Point(lblArtist.Location.X + 50, lblArtist.Location.Y);
            this.lblGenre.Location = new Point(lblAlbum.Location.X + 50, lblArtist.Location.Y);
            this.lblYear.Location = new Point(lblGenre.Location.X + 50, lblArtist.Location.Y);
            this.lblTag.Location = new Point(lblYear.Location.X + 40, lblArtist.Location.Y);
            this.lblFolder.Location = new Point(lblTag.Location.X + 35, lblArtist.Location.Y);

            this.gridGroup.Location = new Point(xStartPosition, picDatabase.Location.Y + picDatabase.Size.Height + gridFiltering.Height + intervalGridHeight + 32);
            this.gridGroup.Size = new Size(275, this.Size.Height - gridFiltering.Height - picDatabase.Height - 60);


            this.grid.Location = new Point(xStartPosition + gridFiltering.Width + 5, picDatabase.Location.Y + picDatabase.Size.Height + intervalGridHeight);
            this.grid.Size = new Size(this.Size.Width - 15 - gridFiltering.Width, this.Size.Height - 40 - yStartPosition - intervalGridHeight);
            

            // LinkLibrary Beta
            if (LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary)
            {
                picArtwork.Visible = true;
                txtAlbumDescription.Visible = true;
                //ltTitle.Visible = true;
                labelTitle.Visible = true;
                //ltArtist.Visible = true;
                labelArtist.Visible = true;
                //ltAlbum.Visible = true;
                labelAlbum.Visible = true;
                ltLastfm.Visible = true;
                labelLastfm.Visible = true;
                gridLink.Visible = true;

                this.gridLink.Size = new Size(280, 150);
                this.gridLink.Location = new Point(this.Width - gridLink.Width - 7, this.grid.Location.Y);
                this.picArtwork.Location = new Point(this.grid.Location.X, this.grid.Location.Y);
                this.picArtwork.Size = new Size(150, 150);
                ltTitle.Location = new Point(this.picArtwork.Location.X + this.picArtwork.Width + 20, this.picArtwork.Location.Y);
                labelTitle.Location = new Point(ltTitle.Location.X + ltTitle.Width, ltTitle.Location.Y);
                ltArtist.Location = new Point(ltTitle.Location.X, this.ltTitle.Location.Y + ltTitle.Height + 4);
                labelArtist.Location = new Point(ltArtist.Location.X + ltArtist.Width, ltArtist.Location.Y);
                ltAlbum.Location = new Point(ltArtist.Location.X - 2, this.ltArtist.Location.Y + ltArtist.Height + 4);
                labelAlbum.Location = new Point(ltAlbum.Location.X + ltAlbum.Width, ltAlbum.Location.Y);
                ltLastfm.Location = new Point(ltAlbum.Location.X - 7, this.ltAlbum.Location.Y + ltAlbum.Height + 4);
                labelLastfm.Location = new Point(ltLastfm.Location.X + ltLastfm.Width, ltLastfm.Location.Y);
                this.txtAlbumDescription.Size = new Size(this.Width - this.picArtwork.Width - gridFiltering.Width - gridLink.Width - 30, 57);
                this.txtAlbumDescription.Location = new Point(this.picArtwork.Location.X + this.picArtwork.Width + 5, this.picArtwork.Location.Y + 99);
                this.grid.Location = new Point(this.grid.Location.X, this.grid.Location.Y + 160);
                this.grid.Size = new Size(this.grid.Size.Width, this.grid.Size.Height - 160);
                labelTitle.Width = txtAlbumDescription.Width - ltTitle.Width - 20;
                labelArtist.Width = labelTitle.Width;
                labelAlbum.Width = labelTitle.Width;
            }
            else
            {
                picArtwork.Visible = false;
                txtAlbumDescription.Visible = false;
                ltTitle.Visible = false;
                labelTitle.Visible = false;
                ltArtist.Visible = false;
                labelArtist.Visible = false;
                ltAlbum.Visible = false;
                labelAlbum.Visible = false;
                ltLastfm.Visible = false;
                labelLastfm.Visible = false;
                gridLink.Visible = false;
            }

            this.progressSeekBar.Location = new Point(this.grid.Location.X, this.grid.Location.Y + this.grid.Size.Height + 5);
            this.progressSeekBar.Size = new Size(this.grid.Size.Width - picSpeaker.Width - picVolumeBar.Width -10, 10);


            //this.gridInfo.Location = new Point(xStartPosition, this.progressSeekBar.Location.Y + diffYGridInfo);
            this.picVolumeBar.Controls.Add(this.picVolumeSwitch);
            //this.picVolumeBar.Load(LinearGlobal.StyleDirectory + "volume_bar.png");
            this.picVolumeBar.Location = new Point(this.Width - this.picVolumeBar.Width - 8, this.progressSeekBar.Location.Y +1);
            //this.picVolumeSwitch.Load(LinearGlobal.StyleDirectory + "volume_switch.png");
            this.picVolumeSwitch.Location = new Point(0, 0);
            this.picVolumeSwitch.Left = LinearGlobal.Volume;

            //this.picSpeaker.Load(LinearGlobal.StyleDirectory + "speaker_on.png");
            this.picSpeaker.Location = new Point(this.picVolumeBar.Location.X - this.picSpeaker.Width , this.picVolumeBar.Location.Y - 4);


            // サイズ変更グリップ
            this.areaSizeChangeHeight.Size = new Size(this.Width, 3);
            if (this.Owner != null && this.Owner.Location.Y < this.Location.Y)
            {
                // 下に表示
                this.areaSizeChangeHeight.Location = new Point(1, this.Height -6);
            }
            else
            {
                // 上に表示
                this.areaSizeChangeHeight.Location = new Point(1, 0);
            }

            this.ResumeLayout();
        }

        public void loadStyle(StyleConfig styleConfig)
        {
            this.picDatabase.Load(LinearGlobal.StyleDirectory + "database.png");
            //this.picCondMode.Load(LinearGlobal.StyleDirectory + "filter.png");
            if (File.Exists(LinearGlobal.StyleDirectory + "limit.png"))
            {
                this.picLimit.Load(LinearGlobal.StyleDirectory + "limit.png");
            }
            else
            {
                this.picLimit.Load(LinearGlobal.StyleDirectory + "setting.png");
            }
            
            if (File.Exists(LinearGlobal.StyleDirectory + "shuffle.png"))
            {
                this.picShuffle.Load(LinearGlobal.StyleDirectory + "shuffle.png");
            }
            if (File.Exists(LinearGlobal.StyleDirectory + "medley.png"))
            {
                this.picMedley.Load(LinearGlobal.StyleDirectory + "medley.png");
            }
            if (File.Exists(LinearGlobal.StyleDirectory + "linklibrary.png"))
            {
                this.picLinkLibrary.Load(LinearGlobal.StyleDirectory + "linklibrary.png");
            }
            this.picTag.Load(LinearGlobal.StyleDirectory + "tag.png");
            this.picSearch.Load(LinearGlobal.StyleDirectory + "search.png");
            //this.picInterrupt.Load(LinearGlobal.StyleDirectory + "interrupt_off.png");
            this.picClose.Load(LinearGlobal.StyleDirectory + "close.png");
            this.picVolumeBar.Load(LinearGlobal.StyleDirectory + "volume_bar.png");
            this.picVolumeSwitch.Load(LinearGlobal.StyleDirectory + "volume_switch.png");
            this.picSpeaker.Load(LinearGlobal.StyleDirectory + "speaker_on.png");

            string iconPath = "";
            switch (LinearGlobal.PlaylistMode)
            {
                case LinearEnum.PlaylistMode.NORMAL:
                    iconPath = "playlistmode_normal.png";
                    break;
                case LinearEnum.PlaylistMode.FAVORITE:
                    iconPath = "playlistmode_favorite.png";
                    break;
                case LinearEnum.PlaylistMode.EXCLUSION:
                    iconPath = "playlistmode_exclusion.png";
                    break;
            }
            this.picPlaylistMode.Load(LinearGlobal.StyleDirectory + iconPath);

            this.grid.BorderStyle = styleConfig.DisplayBorderStyle;
            this.gridFiltering.BorderStyle = styleConfig.DisplayBorderStyle;
            this.gridGroup.BorderStyle = styleConfig.DisplayBorderStyle;
            if (styleConfig.DisplayBorderStyle == BorderStyle.Fixed3D)
            {
                this.filteringBox.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                this.filteringBox.BorderStyle = BorderStyle.None;
            }

            if (LinearGlobal.LinearConfig.ViewConfig.FontBold)
            {
                styleConfig.PlaylistFontConfig.FontStyle = FontStyle.Bold;
            }

            this.databaseList.DropDownStyle = styleConfig.ComboBoxStyle;

            Font gridFont = new Font(styleConfig.PlaylistFontConfig.FontName,
                                      styleConfig.PlaylistFontConfig.FontSize,
                                      styleConfig.PlaylistFontConfig.FontStyle);
            this.grid.Font = gridFont;
            this.gridGroup.Font = gridFont;
            this.gridFiltering.Font = gridFont;

            
            this.ListForm_Resize(null, null);
            this.Refresh();
            this.Grid.Refresh();
            this.gridFiltering.Refresh();
            this.gridGroup.Refresh();
            this.gridLink.Refresh();
        }

        /// <summary>
        /// グリッドにドラッグ&ドロップされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void grid_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ファイルのD&D
                _dragFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                // NORMAL以外のときプレイリストモードのリセット
                if (LinearGlobal.PlaylistMode != LinearEnum.PlaylistMode.NORMAL)
                {
                    setPlaylistMode(LinearEnum.PlaylistMode.NORMAL);
                    // TODO:同じところがある
                    // コンディションリストクリア
                    reloadDatabase(true);
                    setFilteringList();
                }
                grid.Focus();
                dragContextMenuStrip.Show(this, this.PointToClient(Cursor.Position));

            }
            else
            {
                if (LinearAudioPlayer.FilteringGridController.isShowPlayingList())
                {
                    // 再生中リストを表示していたとき
                    int playingno = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID,
                                                                          LinearGlobal.CurrentPlayItemInfo.Id.ToString());
                    if (playingno == -1)
                    {
                        playingno = 1;
                    }
                    LinearAudioPlayer.PlayController.reInsertPlayingList(playingno);
                }

                if (LinearGlobal.SortMode == LinearEnum.SortMode.ORIGINAL_ARTIST && filteringBox.Text == "")
                {
                    ListFunction lf = new ListFunction();
                    lf.updateSort();
                }

            }
        }

        /// <summary>
        /// グリッドにドラッグされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                // ドラッグ中のファイルやディレクトリの取得
                string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                e.Effect = DragDropEffects.Link;
            }
        }

        /// <summary>
        /// プログレスシークバーでマウスが押されたら
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void progressSeekBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // シークがクリックされたらメドレーをとめる。
                LinearAudioPlayer.PlayController.MedleyInfo.Enable = false;
                setMedleyModeColor(LinearGlobal.ColorConfig);

                double clickPersent = ((double) e.X/(double) this.progressSeekBar.Width);

                double value = ((double) LinearAudioPlayer.PlayController.getLength())*clickPersent;
                LinearAudioPlayer.PlayController.setPosition((uint) value);
                LinearGlobal.MainForm.ListForm.setProgressBarValue((int) value);

                Debug.WriteLine(clickPersent.ToString());
            }
        }

        /// <summary>
        /// ボリュームのつまみが移動されたら
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picVolumeSwitch_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rectangle volumeArea = this.RectangleToScreen(new Rectangle(this.picVolumeBar.Location.X,this.picVolumeBar.Location.Y+4,this.picVolumeBar.Size.Width-this.picVolumeSwitch.Width + 1,1));
                Cursor.Clip = volumeArea;
                picVolumeSwitch.Left = picVolumeSwitch.Left +e.X;
                LinearGlobal.Volume = picVolumeSwitch.Left;
                LinearAudioPlayer.PlayController.setVolume(LinearGlobal.Volume);
                LinearGlobal.MainForm.setTitle(LinearAudioPlayer.PlayController.createTitle() + String.Format(LinearConst.TITLE_VOLUME_VIEW_FORMAT, LinearGlobal.Volume));
                Debug.WriteLine("Left:" + picVolumeSwitch.Left + " , X:" + e.X);
            }
        }

        private void picVolumeSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Clip = Rectangle.Empty;
            LinearGlobal.MainForm.setTitle(LinearAudioPlayer.PlayController.createTitle());
        }


        private void picVolumeBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > 100)
            {
                LinearGlobal.Volume = 100;
            }
            else
            {
                LinearGlobal.Volume = e.X;
            }
            setVolume();
        }

        /// <summary>
        /// フィルタリングされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filteringBox_TextChanged(object sender, EventArgs e)
        {
            // ALL MUSICを選択する
            if (filteringBox.Text != "" && LinearAudioPlayer.FilteringGridController.getActiveRowNo() != 0)
            {
                LinearGlobal.FilteringMode = LinearEnum.FilteringMode.DEFAULT;
                LinearAudioPlayer.FilteringGridController.setRowColor(
                    LinearAudioPlayer.FilteringGridController.LastSelectRowNo,
                    FilteringGridController.EnuPlayType.NOPLAY);
                LinearAudioPlayer.FilteringGridController.setRowColor(0, FilteringGridController.EnuPlayType.PLAYING);
                LinearAudioPlayer.FilteringGridController.setFocusRowNo(0);
                filteringBox.Focus();
            }
            reloadDatabase(null);
            setGroupList();
        }

        /// <summary>
        /// プレイモードアイコンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPlaylistMode_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.PlaylistMode++;

            }
            else
            {
                LinearGlobal.PlaylistMode--;
            }

            setPlaylistMode(LinearGlobal.PlaylistMode);

            reloadDatabase(false);
            setGroupList();

        }

        /// <summary>
        /// プレイモードクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlaylistMode_MouseDown(object sender, MouseEventArgs e)
        {
            picPlaylistMode_MouseDown(sender, e);
        }

        /// <summary>
        /// コンディションアイコンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picCondMode_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.FilteringMode++;
            }
            else
            {
                LinearGlobal.FilteringMode--;
            }
            setFilteringList();
        }

        /// <summary>
        /// コンディションモードクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFilteringMode_MouseDown(object sender, MouseEventArgs e)
        {
            picCondMode_MouseDown(sender, e);
        }


        /// <summary>
        /// 閉じるボタンマウスムーブ時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_MouseMove(object sender, MouseEventArgs e)
        {
            this.picClose.Load(LinearGlobal.StyleDirectory + "close_on.png");
        }

        /// <summary>
        /// 閉じるボタンマウスフォーカスアウト時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            this.picClose.Load(LinearGlobal.StyleDirectory + "close.png");
        }

        /// <summary>
        /// 閉じるボタンマウスクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.MainForm.switchViewPlaylist();
            }

        }

        /// <summary>
        /// データベースアイコンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picDatabase_MouseDown(object sender, MouseEventArgs e)
        {
            databaseContextMenuStrip.Show(Cursor.Position);
        }

        /// <summary>
        /// タグアイコンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picTag_MouseDown(object sender, MouseEventArgs e)
        {
            tagContextMenuStrip.Show(Cursor.Position);
        }

        /// <summary>
        /// データベース変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void databaseList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(databaseList.Text))
            {
                SQLiteManager.Instance.closeDatabase();

                // 一時無効リストクリア
                LinearGlobal.invalidIdTable.Clear();

                LinearUtils.connectDatabase(databaseList.Text);
                // フィルタリングリストクリア

                // CurrentIdリセット
                LinearGlobal.CurrentPlayItemInfo.Id = -1;

                if (LinearGlobal.isCompleteStartup)
                {
                    reloadDatabase(true);
                }
                
                setFilteringList();
                setGroupList();
                

                // DBモード設定
                if (LinearConst.RADIO_MODE_TITLE.Equals(databaseList.Text))
                {
                    LinearGlobal.DatabaseMode = LinearEnum.DatabaseMode.RADIO;
                    grid[0, (int)GridController.EnuGrid.TITLE].Value = "Station";
                    grid[0, (int)GridController.EnuGrid.ARTIST].Value = "Now Playing";
                    grid[0, (int)GridController.EnuGrid.ALBUM].Value = "Listener";
                    timerRadio.Start();
                    timerRadio_Tick(null, null);
                } else
                {
                    LinearGlobal.DatabaseMode = LinearEnum.DatabaseMode.MUSIC;
                    grid[0, (int)GridController.EnuGrid.TITLE].Value = "Title";
                    grid[0, (int)GridController.EnuGrid.ARTIST].Value = "Artist";
                    grid[0, (int)GridController.EnuGrid.ALBUM].Value = "Album";
                    timerRadio.Stop();
                }

                if (LinearGlobal.MainForm != null)
                {
                    LinearGlobal.MainForm.setTitle(LinearAudioPlayer.PlayController.createTitle());
                }
                /*
                if (LinearAudioPlayer.PlayController != null)
                {
                    LinearAudioPlayer.PlayController.clearPlayingLiset();
                }*/

            }

        }

        

        /// <summary>
        /// スピーカークリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picSpeaker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (LinearGlobal.SilentMode)
                {
                    case LinearEnum.SilentMode.OFF:
                        // ONにする
                        LinearGlobal.SilentMode = LinearEnum.SilentMode.ON;
                        this.picSpeaker.Load(LinearGlobal.StyleDirectory + "speaker_off.png");
                        _tmpVolume = LinearGlobal.Volume;
                        LinearGlobal.Volume = 
                            LinearGlobal.LinearConfig.SoundConfig.SilentVolume;
                        break;
                    case LinearEnum.SilentMode.ON:
                        // OFFにする
                        LinearGlobal.LinearConfig.SoundConfig.SilentVolume =
                            LinearGlobal.Volume;
                        LinearGlobal.SilentMode = LinearEnum.SilentMode.OFF;
                        this.picSpeaker.Load(LinearGlobal.StyleDirectory + "speaker_on.png");
                        LinearGlobal.Volume = _tmpVolume;
                        break;
                }
                setVolume();
            }
        }

        /*
        /// <summary>
        /// 割り込みアイコンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picInterrupt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                
                if (_interruptForm.Visible) {
                    hideInterruptForm();
                    picInterrupt.Load(LinearGlobal.StyleDirectory + "interrupt_off.png");
                } else {
                    
                    _interruptForm.Show();
                    _interruptForm.Visible = false;
                    syncFormPosSize();
                    _interruptForm.Visible = true;
                    picInterrupt.Load(LinearGlobal.StyleDirectory + "interrupt_on.png");
                }
            }
        }*/

        /*
        private void ListForm_VisibleChanged(object sender, EventArgs e)
        {
            // TODO:あとできれいにする
            picInterrupt.Load(LinearGlobal.StyleDirectory + "interrupt_off.png");
        }*/

        #endregion




        /*
            SourceGrid用イベント 
        */
        #region SGEvent

        // 選択行変更時
        private void Selection_SelectionChanged(object sender, SourceGrid.RangeRegionChangedEventArgs e)
        {
            
        }

        #endregion







        /*
            パブリックメソッド
        */
        #region Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ListForm()
        {
            InitializeComponent();

            //_interruptForm = new InterruptForm();
            //_interruptForm.Owner = this;

            _balloonForm = new BalloonForm();

            // グリッドの初期設定
            LinearAudioPlayer.GridController.Grid = Grid;
            LinearAudioPlayer.GridController.initialGrid();
            LinearAudioPlayer.FilteringGridController = new FilteringGridController(gridFiltering);
            LinearAudioPlayer.FilteringGridController.initialGrid();
            LinearAudioPlayer.GroupGridController = new GroupGridController(gridGroup);
            LinearAudioPlayer.GroupGridController.initialGrid();
            LinearAudioPlayer.LinkGridController = new LinkGridController(gridLink);
            LinearAudioPlayer.LinkGridController.initialGrid();

            restoreSetting();

            // DBコンボにデータをセット
            setDatabaseList(
                LinearGlobal.LinearConfig.PlayerConfig.SelectDatabase);

            // シャッフルモードセット
            setShuffleModeColor(LinearGlobal.ColorConfig);

            // メドレーモードセット
            //setMedleyModeColor(LinearGlobal.ColorConfig);

            // リンクライブラリカラーセット
            setLinkLibraryColor();

            // フィルタリングリスト、グループリストの選択を復元
            resotreSelectFilteringMode();

            // タグリスト生成
            initialTagList();

            //LinearUtils.connectDatabase(databaseList.Text);

            // DBのデータをロード
            //ListFunction lf = new ListFunction();
            //lf.addGridFromDatabase(grid);

            
            //if (LinearGlobal.TotalSec == 0)
            //{
                
            //}

            reloadDatabase(true);

            ListFunction lf = new ListFunction();
            lf.updateGridInformation();

            Bitmap bmp =
    new Bitmap(picArtwork.Size.Width, picArtwork.Size.Height);
            picArtwork.Image = bmp;

            
            

        }

        


        /// <summary>
        /// フィルタリングリスト、グループリストの選択を復元
        /// </summary>
        public void resotreSelectFilteringMode()
        {
            if (LinearGlobal.FilteringMode == 0)
            {
                int rowno = LinearAudioPlayer.FilteringGridController.Find(0,
                                                                           LinearGlobal.LinearConfig.PlayerConfig.
                                                                               SelectFilter);
                if (rowno == -1)
                {
                    rowno = 1;
                }
                LinearAudioPlayer.FilteringGridController.setFocusRowNo(rowno);
            }
            else
            {
                int rowno = LinearAudioPlayer.GroupGridController.Find(0,
                                                                           LinearGlobal.LinearConfig.PlayerConfig.
                                                                               SelectFilter);
                if (rowno == -1)
                {
                    rowno = 1;
                }
                LinearAudioPlayer.GroupGridController.setFocusRowNo(rowno);
            }
        }

        private void setShuffleModeColor(ColorConfig colorConfig)
        {
            switch (LinearGlobal.ShuffleMode)
            {
                case LinearEnum.ShuffleMode.OFF:
                    lblShuffle.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    break;
                case LinearEnum.ShuffleMode.ON:
                    lblShuffle.ForeColor = colorConfig.PlaylistInfoColor;
                    break;
            }
        }


        /// <summary>
        /// 設定を復元する。
        /// </summary>
        public void restoreSetting()
        {
            
            // 設定を復元
            this.Size = new Size(this.Size.Width ,LinearGlobal.LinearConfig.ViewConfig.ListSize.Height);
            
            LinearGlobal.Volume = LinearGlobal.LinearConfig.SoundConfig.Volume;
            LinearGlobal.PlaylistMode = 
                (LinearEnum.PlaylistMode)LinearGlobal.LinearConfig.PlayerConfig.PlaylistMode;
            LinearGlobal.FilteringMode =
                (LinearEnum.FilteringMode) LinearGlobal.LinearConfig.PlayerConfig.FilteringMode;
            LinearGlobal.ShuffleMode =
                (LinearEnum.ShuffleMode)LinearGlobal.LinearConfig.PlayerConfig.ShuffleMode;
            setPlaylistMode(LinearGlobal.PlaylistMode);
            setGroupMode((LinearEnum.FilteringMode) LinearGlobal.LinearConfig.PlayerConfig.GroupMode);
            LinearGlobal.SortMode = (LinearEnum.SortMode) LinearGlobal.LinearConfig.PlayerConfig.SortMode;

            // TODO: 画像の表示

            // カラープロファイルを設定
            //setColorProfile();

            // 透明度を復元
            this.Opacity = LinearGlobal.LinearConfig.ViewConfig.Opacity;
        }

        public void setGroupMode(LinearEnum.FilteringMode filteringMode)
        {
            
            
            //if (filteringMode != LinearEnum.FilteringMode.DEFAULT)
            //{
                LinearGlobal.LinearConfig.PlayerConfig.GroupMode = (int) filteringMode;
            //}
            switch (filteringMode)
            {
                case LinearEnum.FilteringMode.ARTIST:
                    lblArtist.ForeColor = LinearGlobal.ColorConfig.PlaylistInfoColor;
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    LinearAudioPlayer.GroupGridController.LastFilteringMode = filteringMode;
                    break;
                case LinearEnum.FilteringMode.ALBUM:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = LinearGlobal.ColorConfig.PlaylistInfoColor;
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    LinearAudioPlayer.GroupGridController.LastFilteringMode = filteringMode;
                    break;
                case LinearEnum.FilteringMode.GENRE:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = LinearGlobal.ColorConfig.PlaylistInfoColor;
                    lblYear.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    LinearAudioPlayer.GroupGridController.LastFilteringMode = filteringMode;
                    break;
                case LinearEnum.FilteringMode.YEAR:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = LinearGlobal.ColorConfig.PlaylistInfoColor;
                    lblTag.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    LinearAudioPlayer.GroupGridController.LastFilteringMode = filteringMode;
                    break;
                case LinearEnum.FilteringMode.TAG:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = LinearGlobal.ColorConfig.PlaylistInfoColor;
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    LinearAudioPlayer.GroupGridController.LastFilteringMode = filteringMode;
                    break;
                case LinearEnum.FilteringMode.FOLDER:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = LinearGlobal.ColorConfig.PlaylistInfoColor;
                    LinearAudioPlayer.GroupGridController.LastFilteringMode = filteringMode;
                    break;
                default:
                    /*
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
                    */
                    break;
            }
            
        }

        public string getAlbumDescription()
        {
            return txtAlbumDescription.Text;
        }

        public void setLinkLibrary(GridItemInfo gi, bool isSetAlbumDescription)
        {
            if (isSetAlbumDescription)
            {
                txtAlbumDescription.Text = gi.AlbumDescription;
                return;
            }

            ltTitle.Visible = true;
            ltArtist.Visible = true;
            ltAlbum.Visible = true;
            labelTitle.Text = gi.Title;
            labelArtist.Text = gi.Artist;
            labelAlbum.Text = gi.Album + " [" + gi.Year +  "]";
            labelAlbum.Tag = gi.Album;
            
            ltLastfm.Text = "     Info    ";

            // TODO:同じ所がある
            string ext = "";
            if (!String.IsNullOrEmpty(Path.GetExtension(gi.FilePath)))
            {
                ext = Path.GetExtension(gi.FilePath).ToUpper().Substring(1);
            }
            labelLastfm.Text = gi.Time + "  " + ext + "  " + gi.Bitrate + " kbps  " + gi.PlayCount + " plays";
        }

        public void refreshArtwork(bool isNoPicture)
        {
            if (LinearAudioPlayer.PlayController != null && artworkImage != null)
            {
                Graphics g = Graphics.FromImage(picArtwork.Image);
                //DrawFadedImage(e.Graphics, LinearAudioPlayer.PlayController.artworkImage, LinearAudioPlayer.PlayController.artworkCount * 0.1F);
                //ImageAttributesを使用して背景に描画
                g.InterpolationMode =
                            System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // 透過度
                float alpha = artworkCount * 0.1F;

                /*
                if (isNoPicture)
                {
                    g.Clear(LinearGlobal.ColorConfig.FormBackgroundColor);
                    artworkBeforeImage = null;
                }*/

                Image img;
                if (artworkBeforeImage != null)
                {
                    // 前の画像があるときは前の画像と合成する
                    img = ImageUtils.GetCompositeAlphaImage(artworkBeforeImage,
                                                      artworkImage, alpha, isNoPicture, LinearGlobal.ColorConfig.FormBackgroundColor);
                }
                else
                {
                    // 前の画像がないときはそのまま表示
                    img = artworkImage;
                }
                g.DrawImage(img, 0, 0, picArtwork.Width, picArtwork.Height);

                if (alpha >= 1.0F)
                {
                    artworkBeforeImage =
                        ImageUtils.GetResizedImage(img, picArtwork.Width, picArtwork.Height);
                }

            }

            
        }


        /// <summary>
        /// カラープロファイルを設定
        /// </summary>
        public void setColorProfile(ColorConfig colorConfig)
        {
            this.BackColor = colorConfig.FormBackgroundColor;

            databaseList.ForeColor = colorConfig.FontColor;
            databaseList.BackColor = colorConfig.DisplayBackgroundColor;

            grid.BorderColor = colorConfig.DisplayBorderColor;
            gridFiltering.BorderColor = colorConfig.DisplayBorderColor;
            gridGroup.BorderColor = colorConfig.DisplayBorderColor;
            Color borderColor = ColorUtils.GetReverseColor(colorConfig.DisplayBorderColor, 40);
            gridLink.BorderColor = borderColor;

            if (LinearGlobal.StyleConfig.ComboBoxStyle == ComboBoxStyle.DropDown)
            {
                databaseList.Color1 = colorConfig.DisplayBorderColor;
                databaseList.Color2 = colorConfig.DisplayBorderColor;
                databaseList.Color3 = colorConfig.DisplayBorderColor;
                databaseList.Color4 = colorConfig.DisplayBorderColor;
            }

            filteringBox.ForeColor = colorConfig.FontColor;
            filteringBox.BackColor = colorConfig.DisplayBackgroundColor;
            filteringBox.BorderColor = borderColor;


            this.progressSeekBar.ProgressBarMainBottomBackgroundColor =
                colorConfig.ProgressSeekBarMainBottomBackgroundColor;
            this.progressSeekBar.ProgressBarMainUnderBackgroundColor =
                colorConfig.ProgressSeekBarMainUnderBackgroundColor;
            this.progressSeekBar.ProgressBarUpBottomBackgroundColor =
                colorConfig.ProgressSeekBarUpBottomBackgroundColor;
            this.progressSeekBar.ProgressBarUpUnderBackgroundColor =
                colorConfig.ProgressSeekBarUpUnderBackgroundColor;
            this.progressSeekBar.BorderColor =
                colorConfig.ProgressSeekBarBorderColor;


            progressSeekBar.Theme = colorConfig.ProgressSeekBarTheme;

            if (progressSeekBar.Theme == VistaProgressBarTheme.Custom)
            {
                progressSeekBar.ProgressBarMainBottomActiveColor =
                    colorConfig.ProgressSeekBarMainBottomActiveColor;
                progressSeekBar.ProgressBarMainUnderActiveColor =
                    colorConfig.ProgressSeekBarMainUnderActiveColor;
                progressSeekBar.ProgressBarUpBottomActiveColor =
                    colorConfig.ProgressSeekBarUpBottomActiveColor;
                progressSeekBar.ProgressBarUpUnderActiveColor =
                    colorConfig.ProgressSeekBarUpUnderActiveColor;

            }

            /*
            lblArtist.ForeColor = GetLighterColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
            lblAlbum.ForeColor = GetLighterColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
            lblGenre.ForeColor = GetLighterColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
            lblYear.ForeColor = GetLighterColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
            lblTag.ForeColor = GetLighterColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
            lblFolder.ForeColor = GetLighterColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
            */

            lblPlaylistMode.ForeColor = colorConfig.PlaylistInfoColor;
            gridInfo.ForeColor = colorConfig.PlaylistInfoColor;
            lblLinkLibrary.ForeColor = colorConfig.PlaylistInfoColor;

            // LinkLibrary
            ltTitle.ForeColor = colorConfig.PlaylistInfoColor;
            labelTitle.ForeColor = colorConfig.PlaylistInfoColor;
            ltArtist.ForeColor = colorConfig.PlaylistInfoColor;
            labelArtist.ForeColor = colorConfig.PlaylistInfoColor;
            ltAlbum.ForeColor = colorConfig.PlaylistInfoColor;
            labelAlbum.ForeColor = colorConfig.PlaylistInfoColor;
            ltLastfm.ForeColor = colorConfig.PlaylistInfoColor;
            labelLastfm.ForeColor = colorConfig.PlaylistInfoColor;
            txtAlbumDescription.BackColor = colorConfig.FormBackgroundColor;
            txtAlbumDescription.ForeColor = colorConfig.PlaylistInfoColor;

            switch ((LinearEnum.FilteringMode)LinearGlobal.LinearConfig.PlayerConfig.GroupMode)
            {
                case LinearEnum.FilteringMode.ARTIST:
                    lblArtist.ForeColor = colorConfig.PlaylistInfoColor;
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    break;
                case LinearEnum.FilteringMode.ALBUM:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = colorConfig.PlaylistInfoColor;
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    break;
                case LinearEnum.FilteringMode.GENRE:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = colorConfig.PlaylistInfoColor;
                    lblYear.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    break;
                case LinearEnum.FilteringMode.YEAR:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = colorConfig.PlaylistInfoColor;
                    lblTag.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    break;
                case LinearEnum.FilteringMode.TAG:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = colorConfig.PlaylistInfoColor;
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    break;
                case LinearEnum.FilteringMode.FOLDER:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = colorConfig.PlaylistInfoColor;
                    break;
                default:
                    lblArtist.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblAlbum.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblGenre.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblYear.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblTag.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    lblFolder.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
                    break;
            }

            setShuffleModeColor(colorConfig);
            setMedleyModeColor(colorConfig);

        }

        public void setMedleyModeColor(ColorConfig colorConfig)
        {
            if (LinearAudioPlayer.PlayController.MedleyInfo.Enable)
            {
                lblMedley.ForeColor = colorConfig.PlaylistInfoColor;
            } else
            {
                lblMedley.ForeColor = ColorUtils.GetReverseColor(colorConfig.PlaylistInfoColor);
            }
        }

        /// <summary>
        /// プログレスバーの最大値を設定する
        /// </summary>
        /// <param name="maxValue">最大値</param>
        public void setProgressBarMax(int maxValue){

            this.progressSeekBar.Maximum = maxValue;

        }

        /// <summary>
        /// プログレスバーの現在値を設定する
        /// </summary>
        /// <param name="currentValue">現在値</param>
        public void setProgressBarValue(float currentValue)
        {
            // 100%の値をもとめる
            float value = currentValue / progressSeekBar.Maximum;

            this.progressSeekBar.Value = value * 100f;
        }

        /// <summary>
        /// グリッド情報(アイテム数など)を取得する。
        /// </summary>
        /// <param name="rowCount"></param>
        public void setGridInfoString(int rowCount)
        {
            TimeSpan ts = TimeSpan.FromSeconds(LinearGlobal.TotalSec);

            string timestring;

            if (ts.Hours > 0)
            {
                timestring = String.Format("{0,0:D}:{1,0:D2}:{2,0:D2}", (int) ts.TotalHours, (int) ts.Minutes,
                                           (int) ts.Seconds);
            }
            else
            {
                timestring = String.Format("{0,0:D}:{1,0:D2}", (int)ts.Minutes,
                                           (int)ts.Seconds);
            }

            this.gridInfo.Text = rowCount.ToString() + " items [" + timestring + "]";
        }

        public void showToastMessage(string message)
        {
            ToastManager.Show(LinearGlobal.MainForm.ListForm, message,
                              LinearGlobal.ColorConfig.PlayingColor,
                              LinearGlobal.ColorConfig.FirstRowBackgroundColor,
                              LinearGlobal.ColorConfig.FontColor,
                3000);
        }

        /*
        /// <summary>
        /// 割り込みフォームを非表示にする。
        /// </summary>
        public void hideInterruptForm()
        {
            if (_isInterruptFormShowSizeChange) {
                // ワークエリアを越えたら長くする
                LinearGlobal.MainForm.Width = LinearGlobal.MainForm.Width + LinearConst.INTERRUPTFORM_WIDTH;
                this.Width = LinearGlobal.MainForm.Width;
                _isInterruptFormShowSizeChange = false;
            }
            _interruptForm.Hide();
        }*/


        #endregion


        #region Private Method

        /// <summary>
        /// データベースをリロードする
        /// </summary>
        /// <param name="isMoveFocus"></param>
        public void reloadDatabase(bool? isMoveFocus)
        {

            // DBのデータをロード
            ListFunction lf = new ListFunction();
            int limitPlayingList = 0;

            // コンディションアイテム
            ConditionGridItemInfo cii = new ConditionGridItemInfo();
            if (LinearGlobal.FilteringMode == LinearEnum.FilteringMode.DEFAULT)
            {
                IList<int> rows = LinearAudioPlayer.FilteringGridController.getSelectRowNoList();
                if (rows.Count > 0)
                {
                    ConditionGridItemInfo cgi =
                        (ConditionGridItemInfo)
                        LinearAudioPlayer.FilteringGridController.Grid[
                            rows[0], (int)FilteringGridController.EnuGrid.FILTERING].Tag;

                    cii.Value = cgi.Value;
                    cii.DisplayValue = cgi.DisplayValue;

                    // 再生中リストでNormalのときは残りカウントでLIMITをかける
                    if (LinearAudioPlayer.FilteringGridController.isShowPlayingList())
                    {
                        if (LinearGlobal.PlayMode == LinearEnum.PlayMode.NORMAL
                            && LinearAudioPlayer.PlayController != null
                            && LinearAudioPlayer.PlayController.getPlayingRestCont() > 0)
                        {
                                limitPlayingList =
                                            LinearAudioPlayer.PlayController.getPlayingRestCont();
                        }
                    }
                }
            }
            else
            {
                IList<int> rows = LinearAudioPlayer.GroupGridController.getSelectRowNoList();
                if (rows.Count > 0)
                {
                    cii =
                        (ConditionGridItemInfo)
                        LinearAudioPlayer.GroupGridController.Grid[
                            rows[0], (int)GroupGridController.EnuGrid.GROUP].Tag;
                }
            }

            //if (filteringList.SelectedIndex != -1 && !String.IsNullOrEmpty(filteringList.Text) && filteringList.Items.Count > 0) {
            //    cii = (ConditionGridItemInfo)this.filteringList.Items[this.filteringList.SelectedIndex];
            //}


            if (!LinearAudioPlayer.FilteringGridController.isShowPlayingList())
            {
                lf.addGridFromDatabase(
                    grid,
                    this.filteringBox.Text,
                    cii);
            }
            else
            {
                // 再生中
                lf.addGridFromPlayinglist(grid, limitPlayingList);
            }
            lf = null;


            if (LinearAudioPlayer.PlayController != null && LinearAudioPlayer.PlayController.isPlaying())
            {
                int rowNo = LinearAudioPlayer.GridController.Find((int) GridController.EnuGrid.ID,
                                                                  LinearGlobal.CurrentPlayItemInfo.Id.ToString());
                // 現在再生のものにアイコンを設定する
                LinearAudioPlayer.GridController.setPlayIcon(rowNo, GridController.EnuGridIcon.PLAYING, true);
                // 現在RowNoの色を変更する
                LinearAudioPlayer.GridController.setRowColor(rowNo, GridController.EnuPlayType.PLAYING);

                
            }

            if (isMoveFocus != null)
            {
                /*
                if ((bool)isMoveFocus)
                {
                    int rowNo = LinearAudioPlayer.GridController.Find((int)GridController.EnuGrid.ID,
                                                                      LinearGlobal.CurrentPlayItemInfo.Id.ToString());
                    // フォーカス移動
                    if (rowNo != -1)
                    {
                        LinearAudioPlayer.GridController.setFocusRowNo(rowNo);
                    }
                    else
                    {
                        Grid.Selection.FocusFirstCell(true);
                    }
                    //Grid.Selection.FocusFirstCell(true);
                }
                else
                {
                    Grid.Selection.FocusFirstCell(true);
                }*/
                Grid.Selection.FocusFirstCell(true);
            }
            
            /*
            if (!isMoveFocus)
            {
                // フォーカス移動
                //Grid.Selection.ResetSelection(false);
                //Grid.Selection.SelectRow(1, true);
                
                //Grid.Selection.FocusRow(1);
            }*/

        }
       
        /// <summary>
        /// データベース一覧を設定する。
        /// </summary>
        /// <param name="selectDatabase">設定後、選択するデータベース</param>
        private void setDatabaseList(string selectDatabase)
        {

            IList<string> dbList =
                FileUtils.getFilePathListWithExtFilter(
                new string[] { LinearGlobal.DatabaseDirectory },
                SearchOption.AllDirectories,
                new string[]{".db"});

            databaseList.Items.Clear();
            foreach (string dbfile in dbList)
            {
                if (!LinearConst.RADIO_MODE_TITLE.Equals(Path.GetFileNameWithoutExtension(dbfile)))
                {
                    databaseList.Items.Add(
                    Path.GetFileNameWithoutExtension(dbfile));
                }
                
            }

            // radio を追加
            if (!File.Exists(Application.StartupPath + LinearConst.DATABASE_DIRECTORY_NAME + LinearConst.RADIO_DBFILE))
            {
                // なかったらDBファイル作成
                FileUtils.copy(Application.StartupPath + "\\" + LinearConst.BLANK_DBFILE,
                Application.StartupPath + LinearConst.DATABASE_DIRECTORY_NAME + LinearConst.RADIO_DBFILE,
                false);
            }
            databaseList.Items.Add(LinearConst.RADIO_MODE_TITLE);

            // 存在しなかったらデフォルトデータベースを読み込む
            if (!File.Exists(Application.StartupPath + LinearConst.DATABASE_DIRECTORY_NAME + selectDatabase + ".db"))
            {
                selectDatabase = "linear";
            }

            databaseList.SelectedIndex =
                databaseList.FindString(selectDatabase);



            dbList = null;

        }

        /// <summary>
        /// プレイリストモードを設定する。
        /// </summary>
        private void setPlaylistMode(LinearEnum.PlaylistMode playlistMode)
        {

            // オーバしたら戻す
            if ((int)playlistMode == -1)
            {
                playlistMode = LinearEnum.PlaylistMode.OVER - 1;
            }
            if (playlistMode == LinearEnum.PlaylistMode.OVER)
            {
                playlistMode = 0;
            }

            string iconPath = null;

            switch (playlistMode)
            {
                case LinearEnum.PlaylistMode.NORMAL:
                    lblPlaylistMode.Text = "Normal";
                    iconPath = "playlistmode_normal.png";
                    selectionNormaltoolStripMenuItem.Visible = true;
                    selectionFavoriteAddToolStripMenuItem.Visible = true;
                    selectionExclusionAddToolStripMenuItem.Visible = true;
                    selectionSpeciallistDelToolStripMenuItem.Visible = false;
                    break;
                case LinearEnum.PlaylistMode.FAVORITE:
                    lblPlaylistMode.Text = "Favorite";
                    iconPath = "playlistmode_favorite.png";
                    selectionSpeciallistDelToolStripMenuItem.Text = "お気に入りから削除";
                    selectionNormaltoolStripMenuItem.Visible = false;
                    selectionFavoriteAddToolStripMenuItem.Visible = false;
                    selectionExclusionAddToolStripMenuItem.Visible = false;
                    selectionSpeciallistDelToolStripMenuItem.Visible = true;
                    break;
                case LinearEnum.PlaylistMode.EXCLUSION:
                    lblPlaylistMode.Text = "Exclude";
                    iconPath = "playlistmode_exclusion.png";
                    selectionSpeciallistDelToolStripMenuItem.Text = "除外から削除";
                    selectionNormaltoolStripMenuItem.Visible = false;
                    selectionFavoriteAddToolStripMenuItem.Visible = false;
                    selectionExclusionAddToolStripMenuItem.Visible = false;
                    selectionSpeciallistDelToolStripMenuItem.Visible = true;
                    break;
            }

            this.picPlaylistMode.Load(LinearGlobal.StyleDirectory + iconPath);

            LinearGlobal.PlaylistMode = playlistMode;

        }

        public void ReloadAllGrid()
        {
            setFilteringList();
            setGroupList();
            LinearGlobal.MainForm.ListForm.resotreSelectFilteringMode();
            if (LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary)
            {
                LinearAudioPlayer.LinkGridController.reloadGrid();
            }
            reloadDatabase(true);

            
        }

        /// <summary>
        /// フィルタリングリストを設定する。
        /// </summary>
        private void setFilteringList()
        {
            LinearAudioPlayer.FilteringGridController.clearGrid();

            // jsonからデータ取得
            IList<string> conditionFileList = FileUtils.getFilePathListWithExtFilter(Application.StartupPath + LinearConst.SQL_DIRECTORY_NAME,
                                                    SearchOption.TopDirectoryOnly, ".json");
            foreach (string filepath in conditionFileList)
            {
                var sqlConds = JsonConvert.DeserializeObject<List<ConditionGridItemInfo>>(File.ReadAllText(filepath));

                foreach (ConditionGridItemInfo cii in sqlConds)
                {
                    LinearAudioPlayer.FilteringGridController.addItem(cii);
                }
            }

        }

        public void setGroupList()
        {
            LinearEnum.FilteringMode filteringMode;
            if (LinearGlobal.FilteringMode != LinearEnum.FilteringMode.DEFAULT)
            {
                filteringMode = LinearGlobal.FilteringMode;
            }
            else
            {
                if (LinearAudioPlayer.GroupGridController.LastFilteringMode == LinearEnum.FilteringMode.DEFAULT)
                {
                    return;
                }
                filteringMode = LinearAudioPlayer.GroupGridController.LastFilteringMode;
            }
            if (filteringMode != LinearEnum.FilteringMode.TAG)
                {

                    object[][] resultList = SQLiteManager.Instance.executeQueryNormal(
                        SQLBuilder.selectConditionList(LinearGlobal.PlaylistMode, filteringMode, filteringBox.Text));


                    //filteringList.Items.Add(String.Empty);
                    LinearAudioPlayer.GroupGridController.clearGrid();
                    foreach (object[] record in resultList)
                    {
                        ConditionGridItemInfo cii = new ConditionGridItemInfo();
                        if (!String.IsNullOrEmpty(record[0].ToString()))
                        {
                            cii.DisplayValue = record[0].ToString() + " (" + record[1].ToString() + ")";
                            cii.Value = record[0].ToString();
                            LinearAudioPlayer.GroupGridController.addItem(cii);
                        }
                        else
                        {
                            cii.DisplayValue = "<EMPTY> (" + record[1].ToString() + ")";
                            cii.Value = String.Empty;
                            LinearAudioPlayer.GroupGridController.addItem(cii);
                        }
                    }
                }
                else
                {
                    ListFunction lf = new ListFunction();
                    Dictionary<string, int> tagDic = lf.createTagDictonaryFromDatabase();

                    LinearAudioPlayer.GroupGridController.clearGrid();
                    foreach (KeyValuePair<string, int> tagkv in tagDic)
                    {
                        ConditionGridItemInfo cii = new ConditionGridItemInfo();
                        cii.DisplayValue = tagkv.Key + " (" + tagkv.Value + ")";
                        cii.Value = tagkv.Key;
                        LinearAudioPlayer.GroupGridController.addItem(cii);

                    }
            }
        }

        /// <summary>
        /// フォームサイズを同期する
        /// </summary>
        private void syncFormPosSize()
        {
            /*
            if (!_interruptForm.Visible)
            {
                _isInterruptFormShowSizeChange = false;
            }*/

            // 位置、サイズをListFormにあわせる
            if (this.Location.X + this.Size.Width + LinearConst.INTERRUPTFORM_WIDTH > Screen.GetWorkingArea(this).Width)
            {
                // ワークエリアを越えたら短くする
                LinearGlobal.MainForm.Width = LinearGlobal.MainForm.Width - LinearConst.INTERRUPTFORM_WIDTH;
                this.Width = LinearGlobal.MainForm.Width;
                _isInterruptFormShowSizeChange = true;
            }

            /*
            if (this.Location.Y < LinearGlobal.MainForm.Location.Y)
            {
                // 上配置
                _interruptForm.SetBounds(this.Location.X + this.Width, this.Location.Y, LinearConst.INTERRUPTFORM_WIDTH, LinearGlobal.MainForm.Size.Height + this.Size.Height, BoundsSpecified.All);
            }
            else
            {
                // 下配置
                _interruptForm.SetBounds(this.Location.X + this.Width, LinearGlobal.MainForm.Location.Y, LinearConst.INTERRUPTFORM_WIDTH, LinearGlobal.MainForm.Size.Height + this.Size.Height, BoundsSpecified.All);
            }*/
            
        }

        /// <summary>
        /// タグリストを初期化する
        /// </summary>
        private void initialTagList()
        {
            tagContextMenuStrip.Items.Clear();

            // すべてのタグを削除
            ToolStripMenuItem stsmi = new ToolStripMenuItem();
            stsmi.Text = "すべてのタグを削除";
            stsmi.Tag = String.Empty;
            stsmi.Click += new EventHandler(tsmi_Click);
            tagContextMenuStrip.Items.Add(stsmi);


            // セパレータ
            ToolStripSeparator tss = new ToolStripSeparator();
            tagContextMenuStrip.Items.Add(tss);

            // タグリスト
            ListFunction lf = new ListFunction();
            Dictionary<string, int> tagDic = lf.createTagDictonaryFromDatabase();

            ToolStripMenuItem tsmi;
            foreach (string tag in tagDic.Keys)
            {
                tsmi = new ToolStripMenuItem();
                tsmi.Text = tag;
                tsmi.Tag = tag;
                tsmi.Click += new EventHandler(tsmi_Click);
                tagContextMenuStrip.Items.Add(tsmi);
                tsmi = null;
            }

            // セパレータ
            tss = new ToolStripSeparator();
            tagContextMenuStrip.Items.Add(tss);

            // テキストボックス
            ToolStripTextBox tstb = new ToolStripTextBox();
            tstb.KeyPress += new KeyPressEventHandler(tstb_KeyPress);
            tagContextMenuStrip.Items.Add(tstb);
        }

        void tstb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
            {
                ListFunction lf = new ListFunction();
                ToolStripTextBox tstb = (ToolStripTextBox)sender;
                if (!String.IsNullOrEmpty(tstb.Text))
                {
                    lf.updateTag(this.grid, tstb.Text);
                }
                
                tstb.Clear();
                tagContextMenuStrip.Hide();

                initialTagList();
            }
        }


        void tsmi_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();

            string tag;

            if (((ToolStripMenuItem)sender).Tag == null)
            {
                tag = String.Empty;
            }
            else
            {
                tag = ((ToolStripMenuItem)sender).Tag.ToString();
            }

            lf.updateTag(this.grid, tag);
        }

        #endregion





        #region ContextMenu

        /// <summary>
        /// 選択削除クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();
            lf.deleteSelection(false, this.grid);
            lf = null;
        }

        /// <summary>
        /// ファイルごと削除クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();
            lf.deleteSelection(true, this.grid);
            lf = null;
        }

        /// <summary>
        /// セレクション：除外追加クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionExclusionAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();
            lf.operateSelection(LinearEnum.PlaylistMode.EXCLUSION);
            lf = null;
        }

        /// <summary>
        /// セレクション：お気に入りに追加クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionFavoriteAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();
            lf.operateSelection(LinearEnum.PlaylistMode.FAVORITE);
        }

        private void selectionNormaltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();
            lf.operateSelection(LinearEnum.PlaylistMode.NORMAL);
        }

        /// <summary>
        /// セレクション：ＸＸＸから削除をクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionSpeciallistDelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();
            lf.operateSelection(LinearGlobal.PlaylistMode);
        }

        /// <summary>
        /// すべてのタグを取得をクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RowInfo[] rowInfos = LinearAudioPlayer.GridController.getSelectRowsInfo();
            List<ISourceGridItem> gridItemList = new List<ISourceGridItem>();
            foreach (var rowInfo in rowInfos)
            {
                gridItemList.Add(LinearAudioPlayer.GridController.getRowGridItem(rowInfo.Index));
            }

            ListFunction.getAudioInfo(gridItemList);

        }

        /// <summary>
        /// タグ編集クリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();

            if (LinearAudioPlayer.GridController.getSelectRowsInfo().Length == 1)
            {
                lf.execTagEditor(long.Parse(LinearAudioPlayer.GridController.getValue(
                    LinearAudioPlayer.GridController.getSelectRowsInfo()[0].Index,
                    (int) GridController.EnuGrid.ID)));
            }
            else
            {
                lf.execTagEditor();
            }
            
        }


        #endregion

        


        #region databaseContextMenu

        /// <summary>
        /// データベース新規作成クリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewDatabase_Click(object sender, EventArgs e)
        {
            InputDialog ind = new InputDialog("新規データベース名称入力");
            ind.ShowDialog();

            if (!ind.Cancel)
            {
                // ブランクデータベースファイルをコピー
                FileUtils.copy(
                    Application.StartupPath + "\\" + LinearConst.BLANK_DBFILE,
                    LinearGlobal.DatabaseDirectory + ind.ResultText + ".db",
                    false);

                // データベースリストを再構築
                setDatabaseList(ind.ResultText);
            }

        }

        /// <summary>
        /// データベース複製クリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyDatabase_Click(object sender, EventArgs e)
        {
            InputDialog ind = new InputDialog("複製データベース名称入力");
            ind.ShowDialog();

            if (!ind.Cancel)
            {
                // ブランクデータベースファイルをコピー
                FileUtils.copy(
                    LinearGlobal.DatabaseDirectory + databaseList.Text + ".db",
                    LinearGlobal.DatabaseDirectory + ind.ResultText + ".db",
                    false);

                // データベースリストを再構築
                setDatabaseList(ind.ResultText);
            }

        }


        /// <summary>
        /// データベース名称変更クリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameDatabase_Click(object sender, EventArgs e)
        {
            InputDialog ind = new InputDialog("データベース変更後名称入力", databaseList.Text);
            ind.ShowDialog();

            string oldName = databaseList.Text;

            if (!ind.Cancel && !oldName.Equals(ind.ResultText))
            {
                // 削除するためクローズ
                SQLiteManager.Instance.closeDatabase();
                // ブランクデータベースファイルをコピー
                FileUtils.move(
                    LinearGlobal.DatabaseDirectory + oldName + ".db",
                    LinearGlobal.DatabaseDirectory + ind.ResultText + ".db");

                // データベースリストを再構築
                setDatabaseList(ind.ResultText);
            }


        }

        /// <summary>
        /// データベース削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteDatabase_Click(object sender, EventArgs e)
        {
            // １個しかなければ削除しない
            if (databaseList.Items.Count == 1)
            {
                MessageUtils.showMessage(MessageBoxIcon.Information, MessageResource.I0001);
                return;
            }
            if (databaseList.Text == "linear")
            {
                MessageUtils.showMessage(MessageBoxIcon.Information, "デフォルトデータベースは削除できません");
                return;
            }

            if (DialogResult.OK 
                == MessageUtils.showQuestionMessage(
                String.Format(MessageResource.Q0001, databaseList.Text)))
            {

                // 削除するためクローズ
                SQLiteManager.Instance.closeDatabase();

                // バックアップ
                string dbFilePath = Application.StartupPath + LinearConst.DATABASE_DIRECTORY_NAME + databaseList.Text + ".db";
                FileUtils.copy(dbFilePath, dbFilePath + ".backup", true);

                FileUtils.moveRecycleBin(LinearGlobal.DatabaseDirectory + databaseList.Text + ".db");

                // データベースリストを再構築
                setDatabaseList(LinearGlobal.LinearConfig.PlayerConfig.SelectDatabase);

            }

        }

        // データベース最適化クリック時
        private void vacuumDatabase_Click(object sender, EventArgs e)
        {
            string dbfilePath = LinearGlobal.DatabaseDirectory + databaseList.Text + ".db";
            string beforeSize = "最適化前: " + FileUtils.getFileSizeString(dbfilePath);

            SQLiteManager.Instance.vacuum();

            string afterSize = "最適化後: " + FileUtils.getFileSizeString(dbfilePath);

            MessageUtils.showMessage(MessageBoxIcon.Information, beforeSize + "\n" + afterSize);

        }

        /// <summary>
        /// ボリュームをセットする。
        /// </summary>
        public void setVolume()
        {
            picVolumeSwitch.Left = LinearGlobal.Volume;
            LinearAudioPlayer.PlayController.setVolume(LinearGlobal.Volume);
        }

        #endregion

        private void areaSizeChangeHeight_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNS;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int sizeChangeValue = e.Y - sizeChangeStartPos;
                if (LinearGlobal.MainForm.Location.Y < this.Location.Y)
                {
                    // under
                    this.Height = this.Height + sizeChangeValue;
                }
                else
                {
                    // up
                    this.Location = new Point(this.Location.X, this.Location.Y + sizeChangeValue);
                    this.Height -= sizeChangeValue;
                }
                LinearGlobal.LinearConfig.ViewConfig.ListSize = new Size(this.Width, this.Height);
                /*
                if (this._interruptForm.Visible)
                {
                    this.syncFormPosSize();
                }*/
           }
        }

        int sizeChangeStartPos;
        private void areaSizeChangeHeight_MouseDown(object sender, MouseEventArgs e)
        {
            sizeChangeStartPos = e.Y;
        }

        /// <summary>
        /// すべてを登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction listFunc = new ListFunction();
            listFunc.addGridFromList(
                _dragFiles,
                ListFunction.RegistMode.NORMAL);
        }

        /// <summary>
        /// 除外を除いて登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exclusionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction listFunc = new ListFunction();
            listFunc.addGridFromList(
                _dragFiles,
                ListFunction.RegistMode.EXCLUSION);
        }

        /// <summary>
        /// ファイル更新日時で登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileUpdateDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageUtils.showQuestionMessage("ファイルの作成日時が更新日時と同じになります。よろしいですか？") == DialogResult.OK)
            {
                ListFunction listFunc = new ListFunction();
                listFunc.addGridFromList(
                        _dragFiles,
                    ListFunction.RegistMode.FILEUPDATEDATE);
            }
        }

        private void addURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputDialog ind = new InputDialog("URLを入力してください。");
            ind.ShowDialog();

            if (!ind.Cancel)
            {
                string url = ind.ResultText;
                ListFunction listfunc = new ListFunction();

                if (url.IndexOf(".pls") == -1)
                {
                    // single url
                    listfunc.addGridFromString(this.grid, url);
                } else
                {
                    // pls
                    WebResponse res = new WebManager().request(url);
                    if (res != null)
                    {
                        PlsParser plsParser = new PlsParser(
                        new StreamReader(res.GetResponseStream()));
                        res.Close();
                        listfunc.addGridFromList(
                         ((List<string>)plsParser.getUrlList()).ToArray(), ListFunction.RegistMode.NORMAL);
                    }
                    
                }

                
                
            }
        }

        private void playlistContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (LinearGlobal.DatabaseMode == LinearEnum.DatabaseMode.MUSIC)
            {
                addToolStripMenuItem.Visible = false;
            }
            else
            {
                addToolStripMenuItem.Visible = true;
            }

            if (LinearGlobal.ClipboardStack.Count == 0)
            {
                pathpasteToolStripMenuItem.Visible = false;
            }
            else
            {
                pathpasteToolStripMenuItem.Visible = true;
            }

            if (LinearGlobal.FilteringMode == LinearEnum.FilteringMode.ARTIST)
            {
                SortToolStripMenuItem.Visible = true;
                switch (LinearGlobal.SortMode)
                {
                    case LinearEnum.SortMode.DEFAULT:
                        sortDefaultToolStripMenuItem.Checked = true;
                        sortManualToolStripMenuItem.Checked = false;
                        break;
                    case LinearEnum.SortMode.ORIGINAL_ARTIST:
                        sortDefaultToolStripMenuItem.Checked = false;
                        sortManualToolStripMenuItem.Checked = true;
                        break;
                }
            }
            else
            {
                SortToolStripMenuItem.Visible = false;
            }
            
        }

        private void timerRadio_Tick(object sender, EventArgs e)
        {
            int i = 1;
            // グリッド更新

            while (i <= Grid.Rows.Count - 1)
            {
                LinearAudioPlayer.GridController.setRadioStatus(i);

                Application.DoEvents();

                i++;
            }

        }

        private void addInsertToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LinearAudioPlayer.PlayController.addInterruptItem(LinearAudioPlayer.GridController.getSelectRowNoList());
        }

        private void picSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                filteringBox.Clear();
            }
        }

        /// <summary>
        /// コピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pathcopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceGrid.RowInfo[] rows = LinearAudioPlayer.GridController.getSelectRowsInfo();
            foreach (SourceGrid.RowInfo r in rows)
            {
                PlayItemInfo pathInfo = new PlayItemInfo();

                pathInfo.FilePath =  LinearAudioPlayer.GridController.getValue(r.Index, (int)GridController.EnuGrid.FILEPATH);
                pathInfo.Option = LinearAudioPlayer.GridController.getValue(r.Index, (int)GridController.EnuGrid.OPTION);
                
                LinearGlobal.ClipboardStack.Push(pathInfo);
            }
        }

        /// <summary>
        /// 貼り付け
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pathpasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();

            lf.addGridFromClipboard();
            LinearGlobal.ClipboardStack.Clear();
        }

        private void grid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.XButton2)
            {
                if (LinearAudioPlayer.PlayController.isPlaying())
                {
                    LinearAudioPlayer.PlayController.endOfStream();
                }
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                LinearAudioPlayer.PlayController.previousPlay();
            }
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
               this.Size.Width - 1,
                0,
                this.Size.Width - 1,
                this.Size.Height);
            e.Graphics.DrawLine(inSideUnderRightPen,
              this.Size.Width - 2,
               0,
               this.Size.Width - 2,
               this.Size.Height);

            // 下ライン
            e.Graphics.DrawLine(outSideUnderRightPen,
                0,
                this.Size.Height - 1,
                this.Size.Width,
                this.Size.Height - 1);
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

        private void cleardatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format(MessageResource.Q0005, databaseList.Text), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // バックアップ
                string dbFilePath = Application.StartupPath + LinearConst.DATABASE_DIRECTORY_NAME + databaseList.Text + ".db";
                FileUtils.copy(dbFilePath, dbFilePath + ".backup", true);

                ListFunction lf = new ListFunction();
                lf.clearGridFromDatabase(this.grid);
                lf = null;
            }
        }

        private void ディレクトリ直下ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();

            lf.copyFile(grid, true);

            lf = null;
        }

        private void 階層を保持ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListFunction lf = new ListFunction();

            lf.copyFile(grid, false);

            lf = null;
        }

        private void lblArtist_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.FilteringMode = LinearEnum.FilteringMode.ARTIST;
                setGroupMode(LinearEnum.FilteringMode.ARTIST);
                setGroupList();
            }
        }

        private void lblAlbum_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.FilteringMode = LinearEnum.FilteringMode.ALBUM;
                setGroupMode(LinearEnum.FilteringMode.ALBUM);
                setGroupList();
            }
        }

        private void lblGenre_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.FilteringMode = LinearEnum.FilteringMode.GENRE;
                setGroupMode(LinearEnum.FilteringMode.GENRE);
                setGroupList();
            }
        }

        private void lblYear_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.FilteringMode = LinearEnum.FilteringMode.YEAR;
                setGroupMode(LinearEnum.FilteringMode.YEAR);
                setGroupList();
            }
        }

        private void lblTag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.FilteringMode = LinearEnum.FilteringMode.TAG;
                setGroupMode(LinearEnum.FilteringMode.TAG);
                setGroupList();
            }
        }

        private void lblFolder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.FilteringMode = LinearEnum.FilteringMode.FOLDER;
                setGroupMode(LinearEnum.FilteringMode.FOLDER);
                setGroupList();
            }
        }



        private void picSetting_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!_balloonForm.Visible)
                {
                    _balloonForm.Shadow = true;
                    _balloonForm.Show(picLimit);
                }

            }
        }

        private void lblShuffle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (LinearGlobal.ShuffleMode)
                {
                    case LinearEnum.ShuffleMode.OFF:
                        LinearGlobal.ShuffleMode = LinearEnum.ShuffleMode.ON;
                        break;
                    case LinearEnum.ShuffleMode.ON:
                        LinearGlobal.ShuffleMode = LinearEnum.ShuffleMode.OFF;
                        break;
                }
                setShuffleModeColor(LinearGlobal.ColorConfig);
                reloadDatabase(true);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.addInterruptItem(LinearAudioPlayer.GridController.getSelectRowNoList());
        }

        private void gridInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.MainForm.switchViewPlaylist();
            }
        }

        private void filtercutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filteringBox.Cut();
        }

        private void filtercopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filteringBox.Copy();
        }

        private void filterpasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filteringBox.Paste();
        }

        private void registUserDicttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filteringBox.TextLength > 0)
            {
                InputDialog ind = new InputDialog(String.Format("{0}で検索する文字列を入力してください。", filteringBox.Text));
                ind.ShowDialog();

                if (!ind.Cancel)
                {

                    string filePath = Application.StartupPath + LinearConst.MIGEMO_USERDICTIONARY_NAME;
                    if (!File.Exists(filePath))
                    {
                        // ユーザ辞書がなければ作成する。
                        File.Create(filePath).Close();
                    }

                    // 重複登録があるかチェック
                    string[] lines = File.ReadAllLines(
                        filePath, System.Text.Encoding.GetEncoding("shift_jis"));

                    bool isMatch = false;
                    int i = 0;
                    foreach (var line in lines)
                    {
                       string[] words = line.Split('\t');
                       if (words[0] == filteringBox.Text)
                       {
                           lines[i] = filteringBox.Text + "\t" + ind.ResultText;
                           isMatch = true;
                           break;
                       }
                        i++;
                    }

                    if (isMatch)
                    {
                        File.WriteAllLines(Application.StartupPath + LinearConst.MIGEMO_USERDICTIONARY_NAME,
                            lines, System.Text.Encoding.GetEncoding("shift_jis"));
                    } else
                    {
                        File.AppendAllText(Application.StartupPath + LinearConst.MIGEMO_USERDICTIONARY_NAME,
                           filteringBox.Text + "\t" + ind.ResultText + "\n", System.Text.Encoding.GetEncoding("shift_jis"));
                    }

                    IsMatchMigemoSQLiteFunction.refreshUserDict();
                    //SQLiteUtils.registFunction(typeof(IsMatchMigemoSQLiteFunction));
                    ReloadAllGrid();
                }
            }
        }

        private void filterclearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filteringBox.Clear();
        }

        private void filteringBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                filteringBox.SelectAll();
            }
        }

        private void groupGridContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            LinearEnum.EnumGroupListOrder enumGroupListOrder 
                = (LinearEnum.EnumGroupListOrder) LinearGlobal.LinearConfig.ViewConfig.GroupListOrder;
            switch (enumGroupListOrder)
            {
                case LinearEnum.EnumGroupListOrder.COUNT_DESC:
                    countDescToolStripMenuItem.Checked = true;
                    alphabetAscToolStripMenuItem.Checked = false;
                    break;
                case LinearEnum.EnumGroupListOrder.ALPHABET_ASC:
                    countDescToolStripMenuItem.Checked = false;
                    alphabetAscToolStripMenuItem.Checked = true;
                    break;
            }

        }

        private void countDescToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.GroupListOrder = (int) LinearEnum.EnumGroupListOrder.COUNT_DESC;
            setGroupList();
        }

        private void alphabetAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.GroupListOrder = (int)LinearEnum.EnumGroupListOrder.ALPHABET_ASC;
            setGroupList();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string m3uFilePath = FileUtils.showFileDialog(
                "ライブラリにインポートするm3u形式のファイルを選択してください。",
                "M3Uファイル(*.m3u;*.m3u8)|*.m3u;*.m3u8");
            
            if (!String.IsNullOrEmpty(m3uFilePath))
            {
                ListFunction lf = new ListFunction();
                lf.addGridFromM3u(m3uFilePath);
            }
        }

        private void exportWithoutArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = "エクスポートしたファイルの保存先を選択してください";
            sfd.FileName = databaseList.Text + ".m3u8";

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                ListFunction lf = new ListFunction();
                lf.exportM3u(SQLiteManager.Instance.executeQueryNormal(SQLResource.SQL043), sfd.FileName);
                showToastMessage(MessageResource.I0003);
            }
        }

        private void sortDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearGlobal.SortMode =  LinearEnum.SortMode.DEFAULT;
            reloadDatabase(false);
        }

        private void sortManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearGlobal.SortMode = LinearEnum.SortMode.ORIGINAL_ARTIST;
            reloadDatabase(false);
        }


        public void setLinkLibrary(LinkLibraryInfo linkLibraryInfo)
        {
           

            // similar artist
            if (linkLibraryInfo.SimilarArtists != null && linkLibraryInfo.SimilarArtists.Count > 0)
            {
                LinearAudioPlayer.LinkGridController.SimilarArtistList = linkLibraryInfo.SimilarArtists;
            }


            // last.fm
            ltLastfm.Text = linkLibraryInfo.Title;
            ltLastfm.Tag = linkLibraryInfo.Url;
            labelLastfm.Text = linkLibraryInfo.Description;


            if (LinearAudioPlayer.LinkGridController.mode == LinkGridController.EnuMode.SIMILARARTIST)
            {
                LinearAudioPlayer.LinkGridController.reloadGrid();
            }
        }

        public void setFilteringText(string text)
        {
            filteringBox.Text = text;
        }

        private void txtAlbumDescription_MouseWheel(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if ((textBox.SelectionStart == textBox.TextLength && e.Delta < 0)
                || textBox.SelectionStart == 0 && e.Delta > 0)
            {
                return;
            }

            int move = (((txtAlbumDescription.TextLength / txtAlbumDescription.GetLineFromCharIndex(txtAlbumDescription.TextLength)) *(e.Delta/120)) * -1);
            //int move = textBox.SelectionStart + e.Delta;
            if (textBox.SelectionStart + move <= 0)
            {
                // 最初までスクロールしたら
                textBox.SelectionStart = 0;
            }
            else if (textBox.SelectionStart + move >= textBox.TextLength)
            {
                // 最後までスクロールしたら
                textBox.SelectionStart = textBox.TextLength;
            }
            else
            {
                textBox.SelectionStart += move;
            }
            textBox.ScrollToCaret();
        }

        private void labelArtist_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                filteringBox.Text = labelArtist.Text;
            }
        }

        private void labelAlbum_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                filteringBox.Text = labelAlbum.Tag.ToString();
            }
        }

        private void lblLinkLibrary_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary =
                    !LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary;
                setLinkLibraryColor();
                this.ListForm_Resize(null, null);
            }
        }

        private void setLinkLibraryColor()
        {
            if (LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary)
            {
                lblLinkLibrary.ForeColor = LinearGlobal.ColorConfig.PlaylistInfoColor;
            }
            else
            {
                lblLinkLibrary.ForeColor = ColorUtils.GetReverseColor(LinearGlobal.ColorConfig.PlaylistInfoColor);
            }
        }

        private void lblMedley_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LinearAudioPlayer.PlayController.MedleyInfo.Enable = !LinearAudioPlayer.PlayController.MedleyInfo.Enable;
                setMedleyModeColor(LinearGlobal.ColorConfig);
                if (LinearAudioPlayer.PlayController.MedleyInfo.Enable)
                {
                    int gridNo = LinearAudioPlayer.GridController.getActiveRowNo();
                    if (gridNo != -1)
                    {
                        LinearAudioPlayer.PlayController.playForGridNo(gridNo, false);
                    }
                }
            }
        }

        private void startPositionDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyStartPosition(MedleyInfo.EnumStartPosition.DEFAULT);
        }

        private void startPositionOpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyStartPosition(MedleyInfo.EnumStartPosition.OPENING);
        }

        private void startPositionMiddleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyStartPosition(MedleyInfo.EnumStartPosition.MIDDLE);
        }

        private void startPositionEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyStartPosition(MedleyInfo.EnumStartPosition.ENDING);
        }

        private void playtimeDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyPlaytime(MedleyInfo.EnumPlayTime.DEFAULT);
        }

        private void playtimeShortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyPlaytime(MedleyInfo.EnumPlayTime.SHORT);
        }

        private void playtimeLongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyPlaytime(MedleyInfo.EnumPlayTime.LONG);
        }

        private void playtimeHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearAudioPlayer.PlayController.MedleyInfo.setMedleyPlaytime(MedleyInfo.EnumPlayTime.HALF);
        }

        private void medleyContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            switch (LinearAudioPlayer.PlayController.MedleyInfo.StartPosition)
            {
                case MedleyInfo.EnumStartPosition.DEFAULT:
                    startPositionDefaultToolStripMenuItem.Checked = true;
                    startPositionOpeningToolStripMenuItem.Checked = false;
                    startPositionMiddleToolStripMenuItem.Checked = false;
                    startPositionEndingToolStripMenuItem.Checked = false;
                    break;
                case MedleyInfo.EnumStartPosition.OPENING:
                    startPositionDefaultToolStripMenuItem.Checked = false;
                    startPositionOpeningToolStripMenuItem.Checked = true;
                    startPositionMiddleToolStripMenuItem.Checked = false;
                    startPositionEndingToolStripMenuItem.Checked = false;
                    break;
                case MedleyInfo.EnumStartPosition.MIDDLE:
                    startPositionDefaultToolStripMenuItem.Checked = false;
                    startPositionOpeningToolStripMenuItem.Checked = false;
                    startPositionMiddleToolStripMenuItem.Checked = true;
                    startPositionEndingToolStripMenuItem.Checked = false;
                    break;
                case MedleyInfo.EnumStartPosition.ENDING:
                    startPositionDefaultToolStripMenuItem.Checked = false;
                    startPositionOpeningToolStripMenuItem.Checked = false;
                    startPositionMiddleToolStripMenuItem.Checked = false;
                    startPositionEndingToolStripMenuItem.Checked = true;
                    break;
            }

            switch (LinearAudioPlayer.PlayController.MedleyInfo.PlayTime)
            {
                case MedleyInfo.EnumPlayTime.DEFAULT:
                    playtimeDefaultToolStripMenuItem.Checked = true;
                    playtimeShortToolStripMenuItem.Checked = false;
                    playtimeLongToolStripMenuItem.Checked = false;
                    playtimeHalfToolStripMenuItem.Checked = false;
                    break;
                case MedleyInfo.EnumPlayTime.SHORT:
                    playtimeDefaultToolStripMenuItem.Checked = false;
                    playtimeShortToolStripMenuItem.Checked = true;
                    playtimeLongToolStripMenuItem.Checked = false;
                    playtimeHalfToolStripMenuItem.Checked = false;
                    break;
                case MedleyInfo.EnumPlayTime.LONG:
                    playtimeDefaultToolStripMenuItem.Checked = false;
                    playtimeShortToolStripMenuItem.Checked = false;
                    playtimeLongToolStripMenuItem.Checked = true;
                    playtimeHalfToolStripMenuItem.Checked = false;
                    break;
                case MedleyInfo.EnumPlayTime.HALF:
                    playtimeDefaultToolStripMenuItem.Checked = false;
                    playtimeShortToolStripMenuItem.Checked = false;
                    playtimeLongToolStripMenuItem.Checked = false;
                    playtimeHalfToolStripMenuItem.Checked = true;
                    break;
            }
        }

        // 画像
        private  Image artworkImage, artworkBeforeImage;
        private  int artworkOffset, artworkCount;
        private object artworklock = new Object();
        public void setArtworkFadeChange(Image picture, bool isNoPicture)
        {

            artworkOffset = +1;
            artworkCount = 0;

            picArtwork.Tag = picture;


            artworkImage = ImageUtils.GetResizedImage(picture, 150, 150);

            bool isEnd = false;
            while (!isEnd)
            {
                artworkCount += artworkOffset;


                Action uiAction = () =>
                    {
                        refreshArtwork(isNoPicture);
                        picArtwork.Refresh();
                    };
                LinearGlobal.MainForm.ListForm.BeginInvoke(uiAction);

                if (artworkCount >= 10)
                {
                    isEnd = true;
                }
                System.Threading.Thread.Sleep(100);
            }

        }

        private void exportPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = "エクスポートしたファイルの保存先を選択してください";
            sfd.FileName = "linear_grid.m3u8";

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                ListFunction lf = new ListFunction();

                List<object[]> gridList = new List<object[]>();
                foreach (var rowInfo in LinearAudioPlayer.GridController.getAllRowsInfo())
                {
                    if (Grid[rowInfo.Index, (int)GridController.EnuGrid.OPTION].Value == null 
                        || String.IsNullOrEmpty(Grid[rowInfo.Index, (int)GridController.EnuGrid.OPTION].Value.ToString()))
                    {
                        object[] rowData = new object[4];
                        rowData[0] = Grid[rowInfo.Index, (int) GridController.EnuGrid.FILEPATH].Value;
                        rowData[1] = Grid[rowInfo.Index, (int) GridController.EnuGrid.TITLE].Value;
                        rowData[2] = Grid[rowInfo.Index, (int) GridController.EnuGrid.ARTIST].Value;
                        rowData[3] = Grid[rowInfo.Index, (int) GridController.EnuGrid.TIME].Value;
                        gridList.Add(rowData);
                    }
                }

                lf.exportM3u(gridList.ToArray(), sfd.FileName);
                showToastMessage(MessageResource.I0003);
            }
        }

        private void txtAlbumDescription_Leave(object sender, EventArgs e)
        {

            if (!LinearGlobal.CurrentPlayItemInfo.AlbumDescription.Equals(getAlbumDescription()))
            {
                LinearGlobal.CurrentPlayItemInfo.AlbumDescription = getAlbumDescription();

                List<DbParameter> paramList = new List<DbParameter>();
                paramList.Add(new SQLiteParameter("Artist", LinearGlobal.CurrentPlayItemInfo.Artist));
                paramList.Add(new SQLiteParameter("Album", LinearGlobal.CurrentPlayItemInfo.Album));
                paramList.Add(new SQLiteParameter("Description", getAlbumDescription()));

                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL053, paramList);

                showToastMessage(MessageResource.I0008);
            }
        }

        private void txtAlbumDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                txtAlbumDescription.SelectAll();
            }
        }

        private void adAllselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtAlbumDescription.SelectAll();
        }

        private void adCopyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtAlbumDescription.SelectedText);
        }

        private void adPasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtAlbumDescription.SelectedText = Clipboard.GetText();
        }

        private void adAmazonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.amazon.co.jp/gp/search/?field-keywords=" + labelAlbum.Tag);
        }

        private ArtworkViewForm artworkViewForm = null;
        private Image nowImage = null;
        private void picArtwork_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && picArtwork.Tag != null)
            {
                if (artworkViewForm == null)
                {
                    nowImage = (Image) picArtwork.Tag;
                    artworkViewForm = new ArtworkViewForm(nowImage);
                    artworkViewForm.Show();
                }
                else
                {
                    if (!nowImage.Equals((Image) picArtwork.Tag))
                    {
                        nowImage = (Image)picArtwork.Tag;
                        artworkViewForm.setImage((Image) picArtwork.Tag);
                    }
                    artworkViewForm.Show();
                }
            }
        }

        private void ltLastfm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ltLastfm.Tag != null && !String.IsNullOrEmpty(ltLastfm.Tag.ToString()))
                {
                    System.Diagnostics.Process.Start(ltLastfm.Tag.ToString());
                }
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowNo = LinearAudioPlayer.GridController.getActiveRowNo();

            if (rowNo != -1)
            {
                LinearAudioPlayer.PlayController.play(
                    (long)LinearAudioPlayer.GridController.Grid[rowNo, (int)GridController.EnuGrid.ID].Value, false, true);
            }

        }

        private void folderOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowNo = LinearAudioPlayer.GridController.getActiveRowNo();

            if (rowNo != -1)
            {
                // エクスプローラで格納されている場所を開く
                System.Diagnostics.Process.Start(
                    "EXPLORER.EXE", @"/n,/select," + StringUtils.addDoubleQuotation(
                        LinearAudioPlayer.GridController.Grid[rowNo, (int)GridController.EnuGrid.FILEPATH].Value.ToString()));
            }
        }

    }

    
}
