using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Network;
using FINALSTREAM.Commons.Service;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Resources;
using System.Globalization;
using FINALSTREAM.Commons.Info;
using FINALSTREAM.Commons.Archive;
using System.IO;
using TagLib;
using File = System.IO.File;

namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    public partial class TagEditDialog : Form
    {
        private bool beforeCheckArtwork = false;

        #region Enum

        /// <summary>
        /// モード
        /// </summary>
        public enum EnumMode : int
        {
            SINGLE = 0,
            MULTI = 1
        }

        #endregion

        #region Property

        private bool _result = false;
        private IList<TagEditFileInfo> _fileInfoList;

        private IList<AmazonItemInfo> amazonItemInfoList;

        private GridItemInfo gi;
        
        private EnumMode _mode;
        
        /// <summary>
        /// 結果
        /// </summary>
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary>
        /// ファイル情報リスト
        /// </summary>
        public IList<TagEditFileInfo> FileInfoList
        {
            get { return _fileInfoList; }
            set { _fileInfoList = value; }
        }

        /// <summary>
        /// モード
        /// </summary>
        public EnumMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        #endregion

        /// <summary>
        /// コンストラクタ(単数)
        /// </summary>
        /// <param name="id"></param>
        public TagEditDialog(long id)
        {
            InitializeComponent();

            setTagEditInfo(id);

        }

        /// <summary>
        /// タグ編集情報を設定する。
        /// </summary>
        /// <param name="id"></param>
        public void setTagEditInfo(long id)
        {

            FileInfoList = new List<TagEditFileInfo>();
            TagEditFileInfo tagEdit = new TagEditFileInfo();

            // 初期化
            picArtwork.Image = null;
            

            tagEdit.Id = id;

            object result = SQLiteManager.Instance.executeQueryOneRecord(
                SQLResource.SQL019, new SQLiteParameter("Id", id));

            if (System.DBNull.Value != result)
            {
                IList<object> record = (IList<object>)result;

                tagEdit.FilePath = record[0].ToString();
                tagEdit.TargetPath = record[1].ToString();

                tagEdit.ArchiveFilePath = "";

                if (!String.IsNullOrEmpty(tagEdit.TargetPath))
                {

                    // 圧縮ファイルの場合は存在するか確認し、なかったら解凍する
                    tagEdit.ArchiveFilePath = tagEdit.FilePath;
                    tagEdit.FilePath = Path.Combine(LinearGlobal.TempDirectory, tagEdit.TargetPath);
                    if (!File.Exists(tagEdit.FilePath))
                    {
                        SevenZipManager.Instance.extract(tagEdit.ArchiveFilePath,
                            tagEdit.TargetPath,
                            LinearGlobal.TempDirectory);
                    }


                }

                gi = new GridItemInfo();
                gi.Id = tagEdit.Id;
                gi.FilePath = tagEdit.FilePath;
                gi.Tag = tagEdit.ArchiveFilePath;
                gi.Option = tagEdit.TargetPath;
                // タグ取得
                LinearAudioPlayer.PlayController.getTag(gi);
                

                // 設定
                buttonConvertZip.Enabled = false;
                btnRename.Enabled = false;
                this._mode = EnumMode.SINGLE;
                txtFilePath.Text = gi.FilePath;
                txtArchivePath.Text = tagEdit.ArchiveFilePath;

                initalData();


                textSearch.Text = gi.Album;

                // GoogleImageローテート用
                bg_album = txtAlbum.Text;
                bg_artist = txtArtist.Text;
                bg_title = txtTitle.Text;

                picArtwork.InitialImage = Image.FromFile(LinearGlobal.StyleDirectory + "\\loading.gif");
                picArtwork.LoadCompleted += new AsyncCompletedEventHandler(picArtwork_LoadCompleted);
                isArtworkLoadComplete = false;

                if (!String.IsNullOrEmpty(tagEdit.ArchiveFilePath) && !".zip".Equals(Path.GetExtension(tagEdit.ArchiveFilePath).ToLower()))
                {
                    buttonConvertZip.Enabled = true;
                }
                if (!String.IsNullOrEmpty(tagEdit.ArchiveFilePath))
                {
                    btnRename.Enabled = true;
                }

                FileInfoList.Add(tagEdit);

                if (gi.Picture == null || gi.IsNoPicture)
                {
                    Action getPcitureAction = () =>
                        {
                            LinearAudioPlayer.PlayController.getPicture(gi);

                            
                            Action uiAction = () =>
                                {
                                    if (this.IsHandleCreated)
                                    {
                                        ArtworkLoad(gi);
                                    }
                                };
                            this.BeginInvoke(uiAction);
                        };
                    LinearAudioPlayer.WorkerThread.EnqueueTask(getPcitureAction);


                } else
                {
                    isArtworkLoadComplete = true;
                    lblArtworkType.Text = "FILE";
                    picArtwork.Image = gi.Picture;
                    checkArtworkSave.Checked = true;
                    beforeCheckArtwork = true;
                }
            }

        }

        private void ArtworkLoad(GridItemInfo gi)
        {
            if (!String.IsNullOrEmpty(gi.PictureUrl))
            {
                lblArtworkType.Text = "WEB";
            }
            else
            {
                isArtworkLoadComplete = true;
                lblArtworkType.Text = "NONE";
            }
            picArtwork.Image = gi.Picture;
        }

        private void initalData()
        {
            txtTitle.Text = gi.Title;
            txtArtist.Text = gi.Artist;
            txtAlbum.Text = gi.Album;
            txtTrackNo.Text = gi.Track.ToString();
            txtGenre.Text = gi.Genre;
            txtYear.Text = gi.Year;
        }

        /// <summary>
        /// コンストラクタ(複数）
        /// </summary>
        public TagEditDialog()
        {
            InitializeComponent();

            buttonConvertZip.Enabled = false;
            btnRename.Enabled = false;

            FileInfoList = new List<TagEditFileInfo>();

            this._mode = EnumMode.MULTI;


            SourceGrid.RowInfo[] rows = LinearAudioPlayer.GridController.getSelectRowsInfo();
            foreach (SourceGrid.RowInfo r in rows)
            {
                TagEditFileInfo tefi = new TagEditFileInfo();

                GridItemInfo gi = (GridItemInfo)LinearAudioPlayer.GridController.getRowGridItem(r.Index);

                tefi.Id = gi.Id;
                tefi.TargetPath = gi.Option;

                if (String.IsNullOrEmpty(tefi.TargetPath))
                {
                    // オーディオファイル
                    tefi.FilePath = gi.FilePath;
                }
                else
                {
                    // 圧縮ファイル
                    tefi.FilePath = Path.Combine(LinearGlobal.TempDirectory, gi.Option);
                    tefi.ArchiveFilePath = gi.FilePath;
                }

                FileInfoList.Add(tefi);
            }
        }

        private bool isArtworkLoadComplete;

        

        private void picArtwork_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            isArtworkLoadComplete = true;
        }

        #region Event

        /// <summary>
        /// フォームロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagEditDialog_Load(object sender, EventArgs e)
        {
            this.Location = LinearGlobal.LinearConfig.ViewConfig.TagEditDialogLocation;
            this.Height = 220;

            // モードによって画面を切り替える
            switch (this._mode)
            {
                case EnumMode.SINGLE:
                    txtFilePath.ReadOnly = true;
                    txtArchivePath.ReadOnly = true;
                    txtTitle.Enabled = true;
                    txtTrackNo.Enabled = true;
                    lblMessage.Visible = false;
                    
                    break;
                case EnumMode.MULTI:
                    txtFilePath.Enabled = false;
                    txtArchivePath.ReadOnly = true;
                    txtArchivePath.Clear();
                    txtTitle.Enabled = false;
                    txtTitle.Clear();
                    txtTrackNo.Enabled = false;
                    txtTrackNo.Clear();
                    lblMessage.Visible = true;
                    buttonReset.Enabled = false;
                    checkTagSearchShow.Enabled = false;
                    break;
            }

        }

        /// <summary>
        /// OKボタンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {

            // 更新がなかったらそのまま閉じる
            if (gi != null && txtTitle.Text.Equals(gi.Title)
                && txtArtist.Text.Equals(gi.Artist)
                && txtAlbum.Text.Equals(gi.Album)
                && txtTrackNo.Text.Equals(gi.Track.ToString())
                && txtGenre.Text.Equals(gi.Genre)
                && txtYear.Text.Equals(gi.Year)
                && checkArtworkSave.Checked == beforeCheckArtwork)
            {
                _result = false;
                this.Close();
                return;
            }

            // タグ情報を保存
            if (this.isValid())
            {

                if (this.Mode == EnumMode.MULTI) {
                    if(MessageUtils.showQuestionMessage(
                        String.Format(
                            MessageResource.Q0002, this.FileInfoList.Count.ToString())) == DialogResult.Cancel) {
                                return;
                    }
                }

                foreach (TagEditFileInfo tefi in _fileInfoList)
                {
                    // タグ編集情報
                    GridItemInfo tagEditInfo = new GridItemInfo();
                    
                    tagEditInfo.Id = tefi.Id;
                    if(!String.IsNullOrEmpty(tefi.ArchiveFilePath))
                    {
                        tagEditInfo.FilePath = tefi.FilePath;
                        tagEditInfo.Tag = tefi.ArchiveFilePath;
                        tagEditInfo.Option = tefi.TargetPath;
                        
                    } else
                    {
                        tagEditInfo.FilePath = tefi.FilePath;
                    }

                    if (this.Mode == EnumMode.MULTI)
                    {
                        tagEditInfo.Artist = txtArtist.Text;
                        tagEditInfo.Album = txtAlbum.Text;
                        if (!String.IsNullOrEmpty(txtTrackNo.Text))
                        {
                            tagEditInfo.Track = int.Parse(txtTrackNo.Text);
                        }
                        tagEditInfo.Genre = txtGenre.Text;
                        tagEditInfo.Year = txtYear.Text;

                        tagEditInfo.Selection = (int)EnumMode.MULTI;
                    }
                    else
                    {
                        tagEditInfo.Title = txtTitle.Text;
                        tagEditInfo.Artist = txtArtist.Text;
                        tagEditInfo.Album = txtAlbum.Text;
                        tagEditInfo.Track = int.Parse(txtTrackNo.Text);
                        tagEditInfo.Genre = txtGenre.Text;
                        tagEditInfo.Year = txtYear.Text;
                        if (checkArtworkSave.Checked)
                        {
                            tagEditInfo.Picture = new Bitmap(picArtwork.Image);
                        }
                        else
                        {
                            tagEditInfo.Picture = null;
                        }

                        tagEditInfo.Selection = (int)EnumMode.SINGLE;
                    }
                    
                    LinearGlobal.StockTagEditList.Add(tagEditInfo);
                }

                //LinearGlobal.StockTagEditInfo = tagEditInfo;
                Action saveTagAction = () =>
                    {
                        var saveTagResult = LinearAudioPlayer.PlayController.saveTag();
                        if (saveTagResult.Count > 0)
                        {
                            Action uiAction = () =>
                                {
                                    LinearAudioPlayer.PlayController.saveTagEnd(saveTagResult);
                                };
                            LinearGlobal.MainForm.ListForm.BeginInvoke(uiAction);
                        }
                    };
                LinearAudioPlayer.WorkerThread.EnqueueTask(saveTagAction);

                this.Hide();
                
            }
            
        }


        /// <summary>
        /// キャンセルボタンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._result = false;
            this.Close();
        }
        
        /// <summary>
        /// フォーム閉じるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!btnClickFlg && e.CloseReason == CloseReason.UserClosing)
            //{
            //    this._result = true;
            //}

            LinearGlobal.LinearConfig.ViewConfig.TagEditDialogLocation = this.Location;
        }


        #endregion


        #region Private Method

        /// <summary>
        /// 検証する。
        /// </summary>
        /// <returns></returns>
        private bool isValid()
        {
            bool result = true;
            UInt32 ret;
            
            if (!String.IsNullOrEmpty(txtTrackNo.Text)
                && !UInt32.TryParse(txtTrackNo.Text.Trim(), NumberStyles.Number, null, out ret))
            {
                MessageUtils.showMessage(MessageBoxIcon.Warning,
                    MessageResource.W0002);
                txtTrackNo.Focus();
                txtTrackNo.SelectAll();
                return false;
            }

            if (!String.IsNullOrEmpty(txtYear.Text)
                && !UInt32.TryParse(txtYear.Text, NumberStyles.Integer, null, out ret))
            {
                MessageUtils.showMessage(MessageBoxIcon.Warning,
                    MessageResource.W0002);
                txtYear.Focus();
                txtYear.SelectAll();
                return false;
            }
            if (!String.IsNullOrEmpty(txtArchivePath.Text) && !Path.GetExtension(txtArchivePath.Text).ToLower().Equals(".zip")) {
                MessageUtils.showMessage(MessageBoxIcon.Warning, txtArchivePath.Text + "\nはZIPファイルではありません");
                return false;
            }

            return result;

        }


        

        #endregion

        /// <summary>
        /// ZIP変換ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConvertZip_Click(object sender, EventArgs e)
        {
            buttonConvertZip.Enabled = false;
            btnRename.Enabled = false;
            btnOk.Enabled = false;
            btnCancel.Enabled = false;
            if (convertZip())
            {
                LinearGlobal.MainForm.ListForm.showToastMessage(MessageResource.I0005);
            }
            btnOk.Enabled = true;
            btnCancel.Enabled = true;
        }

        /// <summary>
        /// ZIP変換する。
        /// </summary>
        /// <returns></returns>
        private bool convertZip()
        {
            // Todo: ファイルからまとめてZIP圧縮をサポートする。

            bool result = false;

            try
            {
                TagEditFileInfo fileInfo = _fileInfoList[0];

                string oldArchiveFilePath = fileInfo.ArchiveFilePath;

                string newArchiveFilePath = oldArchiveFilePath.Replace(Path.GetExtension(oldArchiveFilePath), ".zip");

                if (File.Exists(fileInfo.ArchiveFilePath))
                {
                    // アーカイブファイル内一覧取得
                    IList<string> targetPathList = SevenZipManager.Instance.getFileNames(oldArchiveFilePath);

                    // アーカイブを解凍する
                    SevenZipManager.Instance.extract(oldArchiveFilePath,
                            Path.Combine(LinearGlobal.TempDirectory, "conv"));

                    // ZIPで再圧縮
                    SevenZipManager.Instance.compress(newArchiveFilePath, targetPathList, Path.Combine(LinearGlobal.TempDirectory, "conv"));

                    // DB更新
                    updateDB(oldArchiveFilePath, newArchiveFilePath);

                    // フォーム内変数更新
                    foreach (TagEditFileInfo tefi in _fileInfoList)
                    {
                        tefi.ArchiveFilePath = newArchiveFilePath;
                    }

                    // グリッド更新
                    int i = 1;
                    while (i <= LinearAudioPlayer.GridController.Grid.Rows.Count - 1)
                    {
                        if (oldArchiveFilePath.Equals(LinearAudioPlayer.GridController.getValue(i, (int)GridController.EnuGrid.FILEPATH)))
                        {
                            LinearAudioPlayer.GridController
                                .Grid[i, (int) GridController.EnuGrid.FILEPATH].Value
                                    = newArchiveFilePath;
                        }
                        i++;
                    }

                    // ファイルを削除
                    FileUtils.moveRecycleBin(oldArchiveFilePath);

                }

                txtArchivePath.Text = newArchiveFilePath;

                result = true;

            }
            catch (Exception ex)
            {
                MessageUtils.showMessage(MessageBoxIcon.Error,
                    MessageResource.E0003 + "\n" + ex.Message);
            }

            return result;
            
        }

        /// <summary>
        /// アーカイブ変換時のDB更新
        /// </summary>
        private void updateDB(string oldFilePath, string NewFilePath)
        {

                // タグ更新(DB)
                List<DbParameter> paramList = new List<DbParameter>();
                paramList.Add(new SQLiteParameter("OldFilePath", oldFilePath));
                paramList.Add(new SQLiteParameter("NewFilePath", NewFilePath));
                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL018, paramList);

        }

        private void picArtwork_Paint(object sender, PaintEventArgs e)
        {

            if (isArtworkLoadComplete && picArtwork.Image != null)
            {
                if (!(picArtwork.Image.Width == 32 && picArtwork.Image.Height == 32))
                {
                    //補間方法として高品質双三次補間を指定する
                    e.Graphics.Clear(SystemColors.Control);
                    e.Graphics.InterpolationMode =
                        System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //画像を縮小表示
                    e.Graphics.DrawImage(picArtwork.Image, 0, 0, picArtwork.Width, picArtwork.Height);
                }
            }
            
        }

        private void picArtwork_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                execRotateGoogleImage();
            }
        }

        /// <summary>
        /// GoogleImageの画像をローテーションする。
        /// </summary>
        private string bg_title;
        private string bg_artist;
        private string bg_album;
        private string[] rotateGooleImageUrls = new string[] {};
        private int rotateIndex;

        private void execRotateGoogleImage()
        {
            bool isChangeKeyword = false;
            bool isGetNewList = false;

            // 初期化
            picArtwork.Image = null;

            // キーワードに変更があったか
            if (!bg_album.Equals(txtAlbum.Text) 
                || !bg_artist.Equals(txtArtist.Text)
                || !bg_title.Equals(txtTitle.Text))
            {
                isChangeKeyword = true;
            }

            // GoogleImageURL Get
            if (rotateGooleImageUrls.Length == 0 || isChangeKeyword)
            {
                rotateGooleImageUrls
                    = new WebManager().getAlbumArtworkUrlListFromGoogleImage(
                        txtArtist.Text,
                        txtAlbum.Text,
                        txtTitle.Text);

                
                isGetNewList = true;

                if (rotateGooleImageUrls.Length == 0)
                {
                    return;
                }
            }

            if (isGetNewList && !rotateGooleImageUrls[0].Equals(gi.PictureUrl))
            {
                rotateIndex = 0;
            } else
            {
                rotateIndex++;
            }

            if (rotateGooleImageUrls.Length == rotateIndex)
            {
                rotateIndex = 0;
            }

            if (rotateGooleImageUrls.Length > 0)
            {
                string imageUrl = rotateGooleImageUrls[rotateIndex];

                picArtwork.ImageLocation = imageUrl;
                beforeCheckArtwork = false;

            }
            
            // 退避更新
            bg_album = txtAlbum.Text;
            bg_artist = txtArtist.Text;
            bg_title = txtTitle.Text;

        }

        private void buttonAmazonSearch_Click(object sender, EventArgs e)
        {
            if (!isArtworkSearchMode)
            {
                execAmazonSearch();
            }
            else
            {
                rotateGooleImageUrls
                    = new WebManager().getAlbumArtworkUrlListFromGoogleImage(
                        "",
                        textSearch.Text,
                        "");
            }
            
        }

        /// <summary>
        /// Amazonから検索する。
        /// </summary>
        private void execAmazonSearch()
        {
            AmazonHelper amazonHelper = new AmazonHelper();

            amazonItemInfoList = amazonHelper.execItemSearch("Large", textSearch.Text);

            albumView.AutoGenerateColumns = false;

            albumView.DataSource = amazonItemInfoList;
        }

        private void albumView_SelectionChanged(object sender, EventArgs e)
        {
          if (albumView.SelectedRows.Count > 0)
          {
              picAmazonImage.Image = null;
              AmazonItemInfo aii = amazonItemInfoList[albumView.SelectedRows[0].Index];
              picAmazonImage.InitialImage = Image.FromFile(LinearGlobal.StyleDirectory + "\\loading.gif");
              picAmazonImage.ImageLocation = aii.ImageUrl;

              trackView.DataSource = aii.Tracks;
          }
            

        }

        private void picAmazonImage_Paint(object sender, PaintEventArgs e)
        {
            if (picAmazonImage.Image != null )
            {
                if (!(picAmazonImage.Image.Width == 32 && picAmazonImage.Image.Height == 32))
                {
                    //補間方法として高品質双三次補間を指定する
                    e.Graphics.Clear(SystemColors.Control);
                    e.Graphics.InterpolationMode =
                        System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //画像を縮小表示
                    e.Graphics.DrawImage(picAmazonImage.Image, 0, 0, picAmazonImage.Width, picAmazonImage.Height);
                }
            }
            
        }

        private void textSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                execAmazonSearch();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            initalData();
        }

        private void trackView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (albumView.SelectedRows.Count >0)
            {
                int albumIndex = albumView.SelectedRows[0].Index;
                int trackIndex = trackView.SelectedRows[0].Index;
                AmazonItemInfo aii = amazonItemInfoList[albumIndex];

                txtTitle.Text = aii.Tracks[trackIndex].TrackName;
                txtArtist.Text = aii.Artist;
                txtAlbum.Text = aii.Title;
                txtTrackNo.Text = aii.Tracks[trackIndex].TrackNumber.ToString();
                //txtGenre.Text = gi.Genre;
                txtYear.Text = aii.ReleaseDate.Split('-')[0].Trim();

            }
        }

        private bool isArtworkSearchMode = false;
        private void lblTagSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isArtworkSearchMode = !isArtworkSearchMode;
                if (isArtworkSearchMode)
                {
                    lblTagSearch.Text = "Artwork Search";
                }
                else
                {
                    lblTagSearch.Text = "Tag Search";
                }
            }
        }

        private void checkTagSearchShow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTagSearchShow.Checked)
            {
                this.Height = 450;
            }
            else
            {
                this.Height = 220;
            }
        }

        private void albumView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {

                Process.Start(amazonItemInfoList[e.RowIndex].DetailPageURL);

            }
        }

        private void picAmazonImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                picArtwork.Image = picAmazonImage.Image;
                beforeCheckArtwork = false;
                checkArtworkSave.Checked = true;
            }
        }

        /// <summary>
        /// アルバム一括タグ更新
        /// </summary>
        private void allAlbumTrackUpdate(bool isTrackNoSort)
        {
            if (!isValid())
            {
                return;
            }

            // IDを取得する
            long id = FileInfoList[0].Id;

            // 更新対象を取得する
            List<DbParameter> paramList = new List<DbParameter>();
            paramList.Add(new SQLiteParameter("Id", id));

            string sql = SQLResource.SQL048;

            if (isTrackNoSort)
            {
                sql += "ORDER BY ALBUM, ABS(TRACK), ID";
            }
            else
            {
                sql += "ORDER BY ID";
            }

            object[][] resultList = SQLiteManager.Instance.executeQueryNormal(sql, paramList);

            // amazonAlbumListをループ
            int i = 0;
            int albumIndex = albumView.SelectedRows[0].Index;
            AmazonItemInfo aii = amazonItemInfoList[albumIndex];
            foreach (var amazonItemTrackInfo in aii.Tracks)
            {

                if (resultList.Length >= i + 1)
                {
                    GridItemInfo gi = new GridItemInfo();
                    gi.Id = long.Parse(resultList[i][0].ToString());
                    if (String.IsNullOrEmpty(resultList[i][2].ToString()))
                    {
                        gi.FilePath = resultList[i][1].ToString();
                    }
                    else
                    {
                        gi.FilePath = Path.Combine(LinearGlobal.TempDirectory, resultList[i][2].ToString());
                        gi.Tag = resultList[i][1].ToString();
                        gi.Option = resultList[i][2].ToString();
                    }

                    gi.Genre = resultList[i][4].ToString();

                    // 更新する対象
                    gi.Title = amazonItemTrackInfo.TrackName;
                    gi.Artist = aii.Artist;
                    gi.Album = aii.Title;
                    gi.Track = amazonItemTrackInfo.TrackNumber;
                    if (aii.ReleaseDate.Length >= 4)
                    {
                        gi.Year = aii.ReleaseDate.Substring(0, 4);
                    }
                    gi.Picture = picAmazonImage.Image;

                    gi.Selection = (int)EnumMode.SINGLE;

                    LinearGlobal.StockTagEditList.Add(gi);

                }
                i++;
            }

            /*
            // id, filepath, option, track
            foreach (var record in resultList)
            {
                GridItemInfo gi = new GridItemInfo();
                gi.Id = long.Parse(record[0].ToString());
                if (String.IsNullOrEmpty(record[2].ToString()))
                {
                    gi.FilePath = record[1].ToString();
                } else
                {
                    gi.FilePath = Path.Combine(LinearGlobal.TempDirectory, record[2].ToString());
                    gi.Tag = record[1].ToString();
                    gi.Option = record[2].ToString();
                }
                
                gi.Track = int.Parse(record[3].ToString());
                gi.Genre = record[4].ToString();

                if (gi.Track-1 >= 0)
                {
                    //int albumIndex = albumView.SelectedRows[0].Index;
                    int trackIndex = gi.Track - 1;
                    //AmazonItemInfo aii = amazonItemInfoList[albumIndex];

                    // 更新する対象
                    gi.Title = aii.Tracks[trackIndex].TrackName;
                    gi.Artist = aii.Artist;
                    gi.Album = aii.Title;
                    gi.Track = aii.Tracks[trackIndex].TrackNumber;
                    gi.Year = aii.ReleaseDate.Split('-')[0].Trim();
                    gi.Picture = picAmazonImage.Image;
                    
                    gi.Selection = (int) EnumMode.SINGLE;

                    LinearGlobal.StockTagEditList.Add(gi);

                }
            }*/

            if (LinearGlobal.StockTagEditList.Count > 0)
            {
                Action getPcitureAction = () =>
                {
                    LinearAudioPlayer.PlayController.getPicture(gi);


                    Action uiAction = () =>
                    {
                        if (this.IsHandleCreated)
                        {
                            ArtworkLoad(gi);
                        }
                    };
                    this.BeginInvoke(uiAction);
                };
                LinearAudioPlayer.WorkerThread.EnqueueTask(getPcitureAction);

                this.Hide();
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            btnRename.Enabled = false;

            string oldfilePath = gi.Tag;
            string newfilePath = ListFunction.renameAlbum(gi.Tag);

            if (!String.IsNullOrEmpty(newfilePath) && !oldfilePath.Equals(newfilePath))
            {
                List<DbParameter> paramList = new List<DbParameter>();
                paramList.Add(new SQLiteParameter("OldFilePath", oldfilePath));
                paramList.Add(new SQLiteParameter("NewFilePath", newfilePath));
                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL028, paramList);
                txtArchivePath.Text = newfilePath;
                gi.Tag = newfilePath;
            }

            btnRename.Enabled = true;
        }

        private void openArtworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
                string result = FileUtils.showFileDialog("アルバムアートワーク選択", "画像ファイル(*.jpeg;*.jpg;*.png;*.gif)|*.jpeg;*.jpg;*.png;*.gif|すべてのファイル(*.*)|*.*");
                if (!String.IsNullOrEmpty(result))
                {
                    lblArtworkType.Text = "LOCAL";
                    picArtwork.Image = Image.FromFile(result);
                    checkArtworkSave.Checked = true;
                    beforeCheckArtwork = false;
                }
        }

        private void saveArtworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picArtwork.Image != null)
            {
                string savepath;
                if (!String.IsNullOrEmpty(txtArchivePath.Text))
                {
                    savepath = Path.GetDirectoryName(txtArchivePath.Text);
                }
                else
                {
                    savepath = Path.GetDirectoryName(txtFilePath.Text);
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = savepath;
                //[ファイルの種類]ではじめに
                //「すべてのファイル」が選択されているようにする
                sfd.FilterIndex = 2;
                //タイトルを設定する
                sfd.Title = "アートワークの保存先を選択してください";
                sfd.FileName = txtAlbum.Text + " - " + txtArtist.Text + ".png";

                //ダイアログを表示する
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //OKボタンがクリックされたとき
                    picArtwork.Image.Save(sfd.FileName, ImageFormat.Png);
                    LinearGlobal.MainForm.ListForm.showToastMessage(MessageResource.I0006);
                }
                
            }
        }

        private void clearArtworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gi.PictureUrl == null)
            {
                lblArtworkType.Text = "FILE";
                picArtwork.Image = gi.Picture;
                checkArtworkSave.Checked = true;
                beforeCheckArtwork = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_mode == EnumMode.SINGLE)
            {
                allAlbumTrackUpdateToolStripMenuItem.Enabled = true;
            } else
            {
                allAlbumTrackUpdateToolStripMenuItem.Enabled = false;
            }
        }

        private void allAlbumTrackUpdateTrackNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allAlbumTrackUpdate(true);
        }

        private void allAlbumTrackUpdateFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allAlbumTrackUpdate(false);
        }


    }

}
