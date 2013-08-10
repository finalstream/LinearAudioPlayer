using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FINALSTREAM.Commons.Grid;
using FINALSTREAM.Commons.Parser;
using FINALSTREAM.LinearAudioPlayer.Grid;
using System.IO;
using FINALSTREAM.LinearAudioPlayer;
using FINALSTREAM.LinearAudioPlayer.Database;
using FINALSTREAM.Commons.Database;
using System.Data.SQLite;
using System.Diagnostics;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Utils;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Resources;
using FINALSTREAM.Commons.Forms;
using FINALSTREAM.LinearAudioPlayer.Core;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Exceptions;
using FINALSTREAM.Commons.Info;
using SourceGrid;

namespace FINALSTREAM.LinearAudioPlayer.GUI
{

    class ListFunction
    {

        #region Enum

        public enum RegistMode
        {
            NORMAL,
            EXCLUSION,
            FILEUPDATEDATE,
            AUTOREGIST
        }

        #endregion


        #region Public Method


        public void addGridFromString(SourceGrid.Grid grid, String path)
        {
            List<string> list = new List<string>();
            list.Add(path);
            this.addGridFromList(list.ToArray(), RegistMode.NORMAL);
        }

        /*
        /// <summary>
        /// ファイルリストをグリッドに追加
        /// </summary>
        /// <param name="grid">グリッド</param>
        /// <param name="fileList">ファイルリスト</param>
        /// <param name="registMode">レジストモード</param>
        public void addGridFromList(SourceGrid.Grid grid ,
            IList<string> fileList,
            RegistMode registMode
            ) {
            
            // Todo: 同じようなメソッドがあるのでこのメソッドは削除する。

            // ファイル更新日時の降順でソート
            Dictionary<string, int> dic = new Dictionary<string, int>();
            List<string[]> sortFilelist = new List<string[]>();
            foreach (var filepath in fileList)
            {
                string[] fileInfo = new string[2];

                fileInfo[0] = filepath;
                fileInfo[1] = File.GetLastWriteTime(filepath).ToString("yyyyMMddHHmmss");

                sortFilelist.Add(fileInfo);

            }
            sortFilelist.Sort(delegate(string[] a, string[] b)
                                  {
                                      if (a[1] != b[1])
                                      {
                                          return string.Compare(a[1], b[1]);
                                      } else
                                      {
                                          return StringUtils.CompareNatural(b[0], a[0]);
                                      }
                                  });

            ProgressDialog pd = new ProgressDialog("ファイル登録中",
                registAudioFile_doWork,
                registAudioFile_runComplete,
                new object[] { sortFilelist , registMode });

            pd.Show();
            
        }*/

        /// <summary>
        /// ファイルリストをグリッドに追加
        /// </summary>
        /// <param name="fileList">ファイルリスト</param>
        /// <param name="registMode">レジストモード</param>
        public void addGridFromList(string[] fileList,
            RegistMode registMode
            )
        {

            // ファイル更新日時の降順でソート
            List<string[]> sortFilelist = new List<string[]>();
            foreach (var filepath in fileList)
            {
                string[] fileInfo;

                if (Directory.Exists(filepath))
                {
                    // ディレクトリの場合
                    //IList<string> dirFiles = FileUtils.getFilePathList(filepath, SearchOption.AllDirectories);

                    // ディレクトリ自身
                    if (!LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.MonitoringDirectory.Equals(filepath))
                    {
                        fileInfo = new string[3];
                        fileInfo[0] = filepath;
                        fileInfo[1] = Directory.GetCreationTime(filepath).ToString("yyyyMMddHHmmss");

                        sortFilelist.Add(fileInfo);
                    } else
                    {
                        // 監視ディレクトリの場合は同階層のファイルをリストアップする
                        var monitorFileList = FileUtils.getFilePathListWithExtFilter(filepath,
                                                                                   SearchOption.TopDirectoryOnly,
                                                                                   LinearGlobal.SupportAudioExtensionAry);

                        foreach (var f in monitorFileList)
                        {
                            fileInfo = new string[3];
                            fileInfo[0] = f;
                            fileInfo[1] = File.GetCreationTime(f).ToString("yyyyMMddHHmmss");

                            sortFilelist.Add(fileInfo);
                        }
                    }

                    // ディレクトリ配下のアーカイブファイル
                    IList<string> dirFiles = FileUtils.getFilePathListWithExtFilter(filepath,
                                                                                    SearchOption.AllDirectories,
                                                                                    LinearConst.
                                                                                        ARCHIVE_EXTENSION_ARY);
                    foreach (var dirFile in dirFiles)
                    {
                        fileInfo = new string[3];
                        fileInfo[0] = dirFile;
                        fileInfo[1] = File.GetCreationTime(dirFile).ToString("yyyyMMddHHmmss");

                        sortFilelist.Add(fileInfo);
                    }

                    // 配下のディレクトリ
                   dirFiles = Directory.GetDirectories(filepath, "*", SearchOption.AllDirectories);
                    foreach (var dirFile in dirFiles)
                    {
                        //if (((IList<string>)LinearGlobal.SupportExtensionAry).Contains(
                        //    Path.GetExtension(dirFile).ToLower()))
                        //{
                            fileInfo = new string[3];
                            fileInfo[0] = dirFile;
                        fileInfo[1] = Directory.GetCreationTime(dirFile).ToString("yyyyMMddHHmmss");

                            sortFilelist.Add(fileInfo);
                        //}
                    }
                }
                else
                {
                    // ファイル
                    fileInfo = new string[3];
                    fileInfo[0] = filepath;
                    fileInfo[1] = File.GetCreationTime(filepath).ToString("yyyyMMddHHmmss");

                    sortFilelist.Add(fileInfo);
                }

            }

            sortFilelist.Sort(delegate(string[] a, string[] b)
            {
                if (a[1] != b[1])
                {
                    return string.Compare(a[1], b[1]);
                }
                else
                {
                    return StringUtils.CompareNatural(b[0], a[0]);
                }
            });


            if (registMode != RegistMode.AUTOREGIST)
            {
                ProgressDialog pd = new ProgressDialog("ファイル登録中",
                                                       registAudioFile_doWork,
                                                       registAudioFile_runComplete,
                                                       new object[] { sortFilelist, registMode });

                pd.Show();

            }
            else
            {
                registAudioFile_doWork(null, new DoWorkEventArgs(new object[] { sortFilelist, registMode }));
            }

        }

        private void registAudioFile_runComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            LinearGlobal.MainForm.ListForm.reloadDatabase(false);
            // 最初にフォーカスを移動する
            LinearAudioPlayer.GridController.setFocusRowNo(1);
        }

        /// <summary>
        /// オーディオファイルを登録する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registAudioFile_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker) sender;
            object[] args = (object[]) e.Argument;
            bool isArchive = false;
            List<DbParameter> paramList = new List<DbParameter>();
            List<string[]> sortFilelist = (List<string[]>) args[0];
            RegistMode registMode = (RegistMode) args[1];
            int registAllCount = getFileCount(sortFilelist);
            List<string> trashDirList = new List<string>();

            int i = 0;
            if (bw != null)
            {
                bw.ReportProgress(0, String.Format(MessageResource.D0001, i, registAllCount));
            }
            SQLiteTransaction sqltran = null;
            try
            {
                sqltran = SQLiteManager.Instance.beginTransaction();

                // FilePath, Option重複チェックリスト
                List<object> checkFilePathOptionList = new List<object>(SQLiteManager.Instance.executeQueryOneColumn(SQLResource.SQL041));

                // ファイル重複チェックリスト(タイトル＋アーティスト＋ファイルサイズをキーとする)
                List<object> checkFileDuplicationKeyList = new List<object>(SQLiteManager.Instance.executeQueryOneColumn(SQLResource.SQL040));

                foreach (string[] fileInfo in sortFilelist)
                {
                    string filePath = fileInfo[0];
                    string checkfileoption;
                    long filesize = 0;
                    bool isRegist = false;
                    
                    IList<string> audioFileList = new List<string>();
                    if (!String.IsNullOrEmpty(fileInfo[2]))
                    {
                        // クリップボードから
                        isArchive = true;
                        audioFileList.Add(fileInfo[2]);
                    }
                    else if (!((IList<string>)LinearConst.ARCHIVE_EXTENSION_ARY).Contains(
                        Path.GetExtension(filePath).ToLower()))
                    {

                        isArchive = false;
                        if (Directory.Exists(filePath))
                        {
                            // ディレクトリ
                            audioFileList = FileUtils.getFilePathListWithExtFilter(filePath,
                                                                                   SearchOption.TopDirectoryOnly,
                                                                                   LinearGlobal.SupportAudioExtensionAry);

                            if (registMode == RegistMode.AUTOREGIST && audioFileList.Count == 0)
                            {
                                // 削除候補ディレクトリリストに追加
                                trashDirList.Add(filePath);
                            }

                            if (registMode == RegistMode.AUTOREGIST && audioFileList.Count > 0)
                            {
                                // TODO:フォルダ移動
                                TagLib.File workfileinfo = TagLib.File.Create(audioFileList[0]);

                                string stockDirectoryPath = moveAutoRegistFile(workfileinfo, filePath);

                                // audioFileListを書き換え
                                int j = 0;
                                string[] foreachworks = new string[audioFileList.Count];
                                audioFileList.CopyTo(foreachworks, 0);
                                foreach (var audioFilePath in foreachworks)
                                {
                                    audioFileList[j] = audioFilePath.Replace(filePath, stockDirectoryPath);
                                    j++;
                                }
                                filePath = stockDirectoryPath;

                            }

                            

                        }
                        else
                        {
                            // オーディオファイル
                            if (((IList<string>)LinearGlobal.SupportAudioExtensionAry).Contains(
                                Path.GetExtension(filePath).ToLower()))
                            {
                                if (registMode == RegistMode.AUTOREGIST)
                                {
                                    // ファイル移動
                                    TagLib.File workfileinfo = TagLib.File.Create(filePath);
                                    string stockDirectoryPath = moveAutoRegistFile(workfileinfo, filePath);
                                    filePath = stockDirectoryPath;
                                }
                                audioFileList.Add(filePath);
                            }
                        }

                    }
                    else
                    {

                        // 書庫ファイル
                        isArchive = true;
                        audioFileList = SevenZipManager.Instance.getFileNames(
                            filePath, LinearGlobal.SupportAudioExtensionAry);

                        if (audioFileList.Count > 0)
                        {

                            if (registMode == RegistMode.AUTOREGIST)
                            {
                                if (File.Exists(filePath))
                                {
                                    var fileNames = SevenZipManager.Instance.getFileNames(filePath,
                                                                                          LinearGlobal.
                                                                                              SupportAudioExtensionAry);

                                    if (fileNames.Count > 0)
                                    {
                                        if (!File.Exists(Path.Combine(LinearGlobal.TempDirectory, fileNames[0])))
                                        {
                                            SevenZipManager.Instance.extract(filePath, fileNames[0],
                                                                             LinearGlobal.TempDirectory);
                                        }

                                        TagLib.File workfileinfo = TagLib.File.Create(Path.Combine(LinearGlobal.TempDirectory, fileNames[0]));
                                        string stockDirectoryPath = moveAutoRegistFile(workfileinfo, filePath);
                                        filePath = stockDirectoryPath;

                                    }
                                }
                            }

                            // Album Auto Rename
                            if (File.Exists(filePath) && LinearGlobal.LinearConfig.PlayerConfig.isAlbumAutoRename)
                            {
                                filePath = renameAlbum(filePath);
                            }

                        }
                    }

                    string addDate = DateTime.Now.ToString(FormatUtils.FORMAT_SQLITE_DATETIME);
                    string addDateTime = DateTime.Now.ToString(LinearConst.DATEFORMAT_ADDDATE);
                    GridItemInfo gi = null;
                    string bk_proption = "";
                    foreach (string audioFile in audioFileList)
                    {


                        if (RegistMode.NORMAL.Equals(registMode) ||
                           (!RegistMode.NORMAL.Equals(registMode)
                            && !StringUtils.contains(audioFile, LinearGlobal.LinearConfig.PlayerConfig.ExclusionKeywords)))
                        {
                            // 既にDB存在するか確認
                            paramList.Clear();

                            if (isArchive)
                            {
                                paramList.Add(new SQLiteParameter("FilePath", filePath));
                                paramList.Add(new SQLiteParameter("Option", audioFile));
                                //filesize = SevenZipManager.Instance.getFileSize(filePath, audioFile);
                                checkfileoption = filePath + audioFile;
                            }
                            else
                            {
                                paramList.Add(new SQLiteParameter("FilePath", audioFile));
                                paramList.Add(new SQLiteParameter("Option", ""));
                                //filesize = FileUtils.getFileSize(audioFile);
                                checkfileoption = audioFile;
                            }


                            // 重複登録チェック一段回目(ファイルパスとアーカイブ内パス)

                            if (!checkFilePathOptionList.Contains(checkfileoption))
                            {

                                if (isArchive)
                                {
                                    gi = LinearAudioPlayer.GridController.createNewGridItem(filePath, audioFile);
                                    filesize = SevenZipManager.Instance.getFileSize(filePath, audioFile);
                                    paramList.Add(new SQLiteParameter("FileSize", filesize));
                                }
                                else
                                {
                                    gi = LinearAudioPlayer.GridController.createNewGridItem(audioFile, null);
                                    filesize = FileUtils.getFileSize(audioFile);
                                    paramList.Add(new SQLiteParameter("FileSize", filesize));
                                }
                                paramList.Add(new SQLiteParameter("Title", gi.Title));
                                paramList.Add(new SQLiteParameter("Artist", gi.Artist));
                                paramList.Add(new SQLiteParameter("Bitrate", gi.Bitrate));
                                paramList.Add(new SQLiteParameter("Album", gi.Album));
                                paramList.Add(new SQLiteParameter("Track", gi.Track));
                                paramList.Add(new SQLiteParameter("Genre", gi.Genre));
                                paramList.Add(new SQLiteParameter("Year", gi.Year));
                                paramList.Add(new SQLiteParameter("Time", gi.Time));
                                paramList.Add(new SQLiteParameter("NotFound", 0));
                                paramList.Add(new SQLiteParameter("Date", gi.Date));
                                paramList.Add(new SQLiteParameter("FileSize", filesize));


                                // 重複登録チェック二段階目(ファイルサイズ)

                                if (checkFileDuplicationKeyList.Contains(gi.Title + gi.Artist + filesize))
                                {

                                    // 同じものがあればハッシュチェック
                                    /*
                                    if (isArchive)
                                    {
                                        filehash = SevenZipManager.Instance.getCrc(filePath, audioFile);
                                    }
                                    else
                                    {
                                        filehash = FileUtils.getFileCrc32(audioFile);
                                    }*/

                                    // 重複しているIDを取得
                                    object[][] resultList = SQLiteManager.Instance.executeQueryNormal(
                                        SQLResource.SQL042, paramList);

                                    if (resultList.Length > 1)
                                    {
                                        // 同じファイルが２つ以上ある場合は警告ログ出力
                                        LinearAudioPlayer.Logger.TraceEvent(System.Diagnostics.TraceEventType.Warning, 0,
                                            String.Format("同じファイルが２つ以上あります。TITLE:{0}, ARTIST:{1}, FILESIZE:{2}",
                                            gi.Title, gi.Artist, filesize));
                                    }

                                    object[] o = resultList[0];

                                    // 重複しているIDのファイルパスを更新
                                    paramList.Add(new SQLiteParameter("Id", o[0]));
                                    SQLiteManager.Instance.executeNonQuery(SQLResource.SQL025, paramList);

                                    // 自動ファイルパス更新機能で更新したファイルパスをログに出力
                                    LinearAudioPlayer.Logger.TraceEvent(System.Diagnostics.TraceEventType.Warning, 0,
                                            String.Format("自動ファイルパス更新を行いました。OLD:{0},{1} → NEW:{2},{3}",
                                            o[1], o[2], gi.FilePath, gi.Option));


                                    /*
                                    bool isFileHashExist = false;
                                    foreach (var o in resultList)
                                    {
                                        string workhash = "";
                                        if (String.IsNullOrEmpty(o[1].ToString()))
                                        {
                                            if (File.Exists(o[0].ToString()))
                                            {
                                                workhash = FileUtils.getFileCrc32(o[0].ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (File.Exists(o[0].ToString()))
                                            {
                                                workhash = SevenZipManager.Instance.getCrc(o[0].ToString(),
                                                                                           o[1].ToString());
                                            }
                                        }
                                        if (workhash.Equals(filehash))
                                        {
                                            // すでにファイルサイズと同じファイルハッシュがあるときはFilePathとOptionを更新
                                            paramList.Add(new SQLiteParameter("FileHash", filehash));
                                            int result = SQLiteManager.Instance.executeNonQuery(SQLResource.SQL025, paramList);
                                            isFileHashExist = true;
                                            break;
                                        }
                                    }
                                    if (!isFileHashExist)
                                    {
                                        isRegist = true;
                                    }
                                     */
                                }
                                else
                                {
                                    // 重複が存在しないときは新規登録
                                    isRegist = true;
                                }

                            }

                            if (isRegist)
                            {
                                // DB登録

                                // addDateを揃える
                                if (bk_proption != "" && !bk_proption.Equals(Path.GetDirectoryName(audioFile)))
                                {
                                    // 親ディレクトリが変わったらDATEを更新
                                    addDate = DateTime.Now.ToString(FormatUtils.FORMAT_SQLITE_DATETIME);
                                    addDateTime = DateTime.Now.ToString(LinearConst.DATEFORMAT_ADDDATE);
                                }
                                gi.Adddate = addDate;
                                gi.AddDateTime = addDateTime;

                                if (registMode == RegistMode.FILEUPDATEDATE && !isArchive)
                                {
                                    gi.Date = FileUtils.getUpdateDateTime(gi.FilePath);
                                    gi.Adddate = gi.Date;
                                    gi.AddDateTime = File.GetLastWriteTime(filePath).ToString(LinearConst.DATEFORMAT_ADDDATE); ;
                                    File.SetCreationTime(gi.FilePath, DateTime.Parse(gi.Date));
                                }

                                gi = registDBData(gi, paramList);

                                // 重複チェック用リストに追加
                                checkFilePathOptionList.Add(checkfileoption);
                                checkFileDuplicationKeyList.Add(gi.Title + gi.Artist + filesize);

                                // オーディオ情報取得
                                //updateAudioInfo(gi);

                                //backup
                                bk_proption = Path.GetDirectoryName(audioFile);

                                //i++;
                                //int par = NumberUtils.getParcent(i, registAllCount);
                                //bw.ReportProgress(par, String.Format(MessageResource.D0001, i, registAllCount));
                            }
                            i++;
                            int par = NumberUtils.getParcent(i, registAllCount);
                            if (bw != null)
                            {
                                bw.ReportProgress(par, String.Format(MessageResource.D0001, i, registAllCount));
                            }
                        }

                    }


                }
                sqltran.Commit();

            }
            catch (SQLiteException sqle)
            {
                sqltran.Rollback();
                throw sqle;
            }
            finally
            {
                LinearAudioPlayer.Logger.Flush();
            }

            foreach (var trashDir in trashDirList)
            {
                if (!LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.MonitoringDirectory.Equals(trashDir))
                {
                    if (Directory.Exists(trashDir))
                    {
                        DirectoryUtils.moveRecycleBin(trashDir);
                    }
                }
            }
        }

        private string moveAutoRegistFile(TagLib.File workfileinfo, string path)
        {
            string stockDirectory = "";
            string genre = "";
            string genretag = workfileinfo.Tag.FirstGenre;
            if (!String.IsNullOrEmpty(genretag))
            {
                genre = workfileinfo.Tag.FirstGenre.ToLower().Trim();
            }
            if (String.IsNullOrEmpty(genre))
            {
                genre = "unknown";
            }
            string year = workfileinfo.Tag.Year.ToString().Trim();
            

            if (Directory.Exists(path))
            {
                if (String.IsNullOrEmpty(year) || "0".Equals(year))
                {
                    year = DirectoryUtils.getCreationDateTime(path).Substring(0, 4);
                }

                string artist = workfileinfo.Tag.FirstPerformer;
                string album = workfileinfo.Tag.Album;
                
                string dirName;
                if (artist == null && album == null)
                {
                    dirName = workfileinfo.Name;
                } else
                {
                    if (artist == null)
                    {
                        artist = "";
                    }
                    if (album == null)
                    {
                        album = "";
                    }
                    dirName = artist + " - " + album;
                }

                // ディレクトリ
               stockDirectory = Path.Combine(
                    Path.Combine(
                        Path.Combine(LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.StorageDirectory, genre), year),
                            dirName);
                Directory.CreateDirectory(stockDirectory);

                FileUtils.allcopy(path, stockDirectory);
                DirectoryUtils.moveRecycleBin(path);
            }
            else
            {
                if (String.IsNullOrEmpty(year) || "0".Equals(year))
                {
                    year = FileUtils.getCreationDateTime(path).Substring(0, 4);
                }
                // ファイル
                stockDirectory = 
                    Path.Combine(
                        Path.Combine(LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.StorageDirectory, genre), year);
                Directory.CreateDirectory(stockDirectory);
                stockDirectory = Path.Combine(stockDirectory, Path.GetFileName(path));
                FileUtils.copy(path, stockDirectory, true);
                FileUtils.moveRecycleBin(path);
            }

            return stockDirectory;
        }

        /// <summary>
        /// ファイル登録リストの合計を取得する。
        /// </summary>
        private int getFileCount(List<string[]> targetList)
        {
            int count = 0;
            foreach (var stringse in targetList)
            {
                if (!String.IsNullOrEmpty(stringse[2]))
                    {
                        // クリップボードから
                        count++;
                    }
                    else if (!((IList<string>)LinearConst.ARCHIVE_EXTENSION_ARY).Contains(
                        Path.GetExtension(stringse[0]).ToLower()))
                    {
                        if (Directory.Exists(stringse[0]))
                        {
                            // ディレクトリ
                            count += FileUtils.getFilePathListWithExtFilter(stringse[0],
                                                                                   SearchOption.TopDirectoryOnly, 
                                                                                   LinearGlobal.SupportAudioExtensionAry).Count;
                        } else
                        {
                            // オーディオファイル
                            if (((IList<string>) LinearGlobal.SupportAudioExtensionAry).Contains(
                                Path.GetExtension(stringse[0]).ToLower()))
                            {
                                count++;
                            }
                        }

                    }
                    else
                    {

                        // 書庫ファイル
                        count += SevenZipManager.Instance.getFileNames(
                            stringse[0], LinearGlobal.SupportAudioExtensionAry).Count;
                    }
            }
            return count;
        }

        public static string renameAlbum(string filePath)
        {

            string result = filePath;

            if (!File.Exists(filePath))
            {
                return result;
            }

            var fileNames = SevenZipManager.Instance.getFileNames(filePath, LinearGlobal.SupportAudioExtensionAry);

            bool isVariousArtists = false;
            TagLib.File fileinfo1;
            TagLib.File fileinfo2;
            if (fileNames.Count > 1)
            {
                if (!File.Exists(Path.Combine(LinearGlobal.TempDirectory , fileNames[0])))
                {
                    SevenZipManager.Instance.extract(filePath, fileNames[0], LinearGlobal.TempDirectory);
                }
                if (!File.Exists(Path.Combine(LinearGlobal.TempDirectory, fileNames[1])))
                {
                    SevenZipManager.Instance.extract(filePath, fileNames[1], LinearGlobal.TempDirectory);
                }
                fileinfo1 = TagLib.File.Create(Path.Combine(LinearGlobal.TempDirectory, fileNames[0]));
                fileinfo2 = TagLib.File.Create(Path.Combine(LinearGlobal.TempDirectory, fileNames[1]));
                if (fileinfo1.Tag.FirstPerformer != fileinfo2.Tag.FirstPerformer)
                {
                    isVariousArtists = true;
                }
            }
            else
            {
                fileinfo1 = TagLib.File.Create(Path.Combine(LinearGlobal.TempDirectory, fileNames[0]));
            }

            string newfilepath="";
            if (fileinfo1.Tag.Performers.Length > 0)
            {
                string year="";
                if (fileinfo1.Tag.Year != 0)
                {
                    year =  "[" + fileinfo1.Tag.Year.ToString() + "]";
                }
                else
                {
                    year = "";
                }
                string artistName = fileinfo1.Tag.Performers[0];
                if (isVariousArtists)
                {
                    artistName = "Various Artists";
                }
                string fileName = LinearGlobal.LinearConfig.PlayerConfig.albumAutoRenameTemplete.Replace("%R",
                                                                                                         artistName).Replace("%A", fileinfo1.Tag.Album).Replace("%Y", year).Trim();
                newfilepath =
                Path.Combine(Path.GetDirectoryName(filePath)
                , fileName + System.IO.Path.GetExtension(filePath));
                newfilepath = newfilepath.Replace("/", "／");
            }


            if (!String.IsNullOrEmpty(newfilepath) && !filePath.Equals(newfilepath) && !File.Exists(newfilepath))
            {
                try
                {
                    System.IO.File.Move(filePath, newfilepath);
                    result = newfilepath;
                }
                catch (Exception)
                {
                    
                }
                
            }

            return result;

        }

        /// <summary>
        /// ファイルリストをグリッドに追加
        /// </summary>
        /// <param name="grid">グリッド</param>
        public void addGridFromClipboard()
        {
            List<string[]> clipboardFilelist = new List<string[]>();

            foreach (PlayItemInfo playItemInfo in LinearGlobal.ClipboardStack)
            {
                string[] strings = new string[3];
                strings[0] = playItemInfo.FilePath;
                strings[2] = playItemInfo.Option;

                clipboardFilelist.Add(strings);
            }

            ProgressDialog pd = new ProgressDialog("ファイル登録中",
                registAudioFile_doWork,
                registAudioFile_runComplete,
                new object[] { clipboardFilelist, RegistMode.NORMAL });

            pd.Show();

        }

        /// <summary>
        /// ファイルリストをグリッドに追加
        /// </summary>
        /// <param name="grid">グリッド</param>
        public void addGridFromM3u(string m3uFilePath)
        {
            List<string[]> m3uFilelist = new List<string[]>();

            M3uParsar m3UParsar = new M3uParsar();
            string[] m3ufiles = m3UParsar.parse(m3uFilePath);
            Array.Reverse(m3ufiles);
            foreach (var filepath in m3ufiles)
            {
                string[] strings = new string[3];
                strings[0] = filepath;

                m3uFilelist.Add(strings);
            }

            ProgressDialog pd = new ProgressDialog("ファイル登録中",
                registAudioFile_doWork,
                registAudioFile_runComplete,
                new object[] { m3uFilelist, RegistMode.NORMAL });

            pd.Show();

        }

        /// <summary>
        /// データベースからグリッドに追加する
        /// </summary>
        /// <param name="grid"></param>
        public void addGridFromDatabase(SourceGrid.Grid grid, 
            string filterString,
            ConditionGridItemInfo conditionItem)
        {

            LinearAudioPlayer.GridController.clearGrid();

            object[][]  resultList = SQLiteManager.Instance.executeQueryNormal(
                SQLBuilder.selectPlaylist(
                    SQLResource.SQL001,
                    LinearGlobal.PlaylistMode, 
                    filterString,
                    LinearGlobal.FilteringMode,
                    conditionItem));

            // グリッドの領域確保
            //grid.Redim(resultList.Count, grid.ColumnsCount);
            
            foreach(object[] recordList in resultList){

                GridItemInfo gi = LinearAudioPlayer.GridController.createLoadGridItem(recordList);
                LinearAudioPlayer.GridController.addItem(gi);

            }
            
            if (LinearGlobal.MainForm != null)
            {
                updateGridInformation();
                
            }
            
        }

        /// <summary>
        /// m3u(アーカイブ以外）をエクスポートする
        /// </summary>
        /// <param name="outputFilePath"></param>
       public void exportM3uWithoutArchive(string outputFilePath)
        {


           object[][] resultList = SQLiteManager.Instance.executeQueryNormal(SQLResource.SQL043);

           //ファイルを上書きし、Shift JISで書き込む
           StreamWriter sw = new System.IO.StreamWriter(
               outputFilePath,
               false,
               Encoding.GetEncoding("utf-8"));

           sw.WriteLine("#EXTM3U");
           foreach (var o in resultList)
           {
               string extinf = "#EXTINF:";
               string time = o[3].ToString();
               if (!String.IsNullOrEmpty(time))
               {
                   if (time.Length < 6)
                   {
                       time = "0:" + time;
                   }
                   extinf += TimeSpan.Parse(time).TotalSeconds + ",";
                   extinf += o[2] + " - " + o[1];
                   sw.WriteLine(extinf);
                   sw.WriteLine(o[0]);
               }
           }

           //閉じる
           sw.Close();



        }
        public void updateSort()
        {
            
            RowInfo[] rowInfos = LinearAudioPlayer.GridController.getAllRowsInfo();
            int sort = 1;
            List<DbParameter> paramList = new List<DbParameter>();

            SQLiteTransaction sqlTran = null;
            try
            {
                sqlTran = SQLiteManager.Instance.beginTransaction();
                foreach (var rowInfo in rowInfos)
                {
                    paramList.Clear();

                    paramList.Add(new SQLiteParameter("Sort", sort));
                    paramList.Add(new SQLiteParameter("Id",
                                                      LinearAudioPlayer.GridController.Grid[
                                                          rowInfo.Index, (int)GridController.EnuGrid.ID]));

                    SQLiteManager.Instance.executeNonQuery(SQLResource.SQL044, paramList);

                    sort++;
                }
                sqlTran.Commit();
            }
            catch (SQLiteException)
            {
                sqlTran.Rollback();
            }

        }

        /// <summary>
        /// グリッドをクリアしてデータベースから削除する
        /// </summary>
        public void clearGridFromDatabase(SourceGrid.Grid grid)
        {


            // データをすべてクリアする
            SQLiteManager.Instance.executeNonQuery(SQLResource.SQL010);

            LinearAudioPlayer.GridController.clearGrid();

            updateGridInformation();

        }

        /// <summary>
        ///  選択項目を操作する。(セレクション)
        /// </summary>
        /// <param name="playlistMode">このセレクションに変更する</param>
        /// <param name="grid"></param>
        public void operateSelection(LinearEnum.PlaylistMode playlistMode)
        {
            Dictionary<string, object> paramDic = new Dictionary<string, object>();

            SQLiteTransaction sqltran = null;
            try
            {
                sqltran = SQLiteManager.Instance.beginTransaction();
                
                SourceGrid.RowInfo[] rows = LinearAudioPlayer.GridController.getSelectRowsInfo();
                foreach (SourceGrid.RowInfo r in rows)
                {
                    int targetId = int.Parse(
                        LinearAudioPlayer.GridController.getValue(r.Index, (int)GridController.EnuGrid.ID));
                    switch (playlistMode)
                    {
                        case LinearEnum.PlaylistMode.NORMAL:
                            // FAVORITE→NORMAL
                            // TODO:同じとこがあるのであとでメソッド化する。
                            SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    targetId, LinearEnum.RatingValue.NORMAL));
                            LinearAudioPlayer.GridController.setRatingIcon(r.Index, LinearEnum.RatingValue.NORMAL);
                            break;

                        case LinearEnum.PlaylistMode.EXCLUSION:
                            if (LinearGlobal.PlaylistMode == playlistMode)
                            {
                                // EXCLUSION→NORMAL
                                SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    targetId, LinearEnum.RatingValue.NORMAL));
                            }
                            else
                            {
                                // EXCLUSIONに変更
                                SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    targetId, LinearEnum.RatingValue.EXCLUSION));
                            }
                            LinearAudioPlayer.GridController.Grid.Rows.Remove(r.Index);
                            break;

                        case LinearEnum.PlaylistMode.FAVORITE:
                            if (LinearGlobal.PlaylistMode == playlistMode)
                            {
                                // FAVORITE→NORMAL
                                SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    targetId, LinearEnum.RatingValue.NORMAL));
                                LinearAudioPlayer.GridController.Grid.Rows.Remove(r.Index);
                            }
                            else
                            {
                                // FAVORITEに変更
                                SQLiteManager.Instance.executeNonQuery(
                                SQLBuilder.updateRating(
                                    targetId, LinearEnum.RatingValue.FAVORITE));
                            }
                            break;
                    }
                }
                sqltran.Commit();
            }
            catch (SQLiteException sqle)
            {
                sqltran.Rollback();
                throw sqle;
            }

            updateGridInformation();

        }

        /// <summary>
        /// 選択項目を削除する。
        /// </summary>
        public void deleteSelection(bool isFileDelete, SourceGrid.Grid grid)
        {
            Dictionary<string, object> paramDic = new Dictionary<string, object>();

            
                

                SourceGrid.RowInfo[] rows = LinearAudioPlayer.GridController.getSelectRowsInfo();

                // 削除確認
                string message = "";
                if (isFileDelete)
                {
                    message = MessageResource.Q0003;
                } else
                {
                    message = MessageResource.Q0004;
                }
                if (MessageUtils.showQuestionMessage(String.Format(message, rows.Length)) != DialogResult.OK)
                {
                    return;
                }

            SQLiteTransaction sqltran = null;
            try
            {
                sqltran = SQLiteManager.Instance.beginTransaction();

                foreach (SourceGrid.RowInfo r in rows)
                {
                    if (r.Index == 0)
                    {
                        continue;
                    }
                    int targetId = int.Parse(
                        LinearAudioPlayer.GridController.getValue(r.Index, (int)GridController.EnuGrid.ID));

                    string filePath = LinearAudioPlayer.GridController.getValue(
                                r.Index, (int)GridController.EnuGrid.FILEPATH);

                    string option = LinearAudioPlayer.GridController.getValue(
                        r.Index, (int) GridController.EnuGrid.OPTION);

                    // PLAYLISTから削除
                    SQLiteManager.Instance.executeNonQuery(
                        SQLBuilder.deleteRecord(
                            LinearEnum.PlaylistMode.NORMAL, targetId, false));
                    // SPECIALLISTから削除
                    //SQLiteUtils.executeNonQuery(
                    //    SQLBuilder.deleteRecord(
                    //        LinearEnum.PlaylistMode.EXCLUSION, targetId, false), null);

                    if (isFileDelete)
                    {
                        if (String.IsNullOrEmpty(option))
                        {
                            // アーカイブ以外だったら
                            if (File.Exists(filePath))
                            {
                                FileUtils.moveRecycleBin(filePath);
                            }

                        }
                    }

                }

                // グリッドから削除
                foreach (SourceGrid.RowInfo r in rows)
                {
                    LinearAudioPlayer.GridController.Grid.Rows.Remove(r.Index);
                }

                sqltran.Commit();
            }
            catch (SQLiteException sqle)
            {
                sqltran.Rollback();
                throw sqle;
            }

            updateGridInformation();

            LinearAudioPlayer.GridController.Grid.Selection.ResetSelection(true);
        }

        /// <summary>
        /// タグを更新する。
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="tag"></param>
        public void updateTag(SourceGrid.Grid grid, string tag)
        {

            SQLiteTransaction sqltran = null;
            try
            {
                sqltran = SQLiteManager.Instance.beginTransaction();

                SourceGrid.RowInfo[] rows = LinearAudioPlayer.GridController.getSelectRowsInfo();
                foreach (SourceGrid.RowInfo r in rows)
                {
                    bool isTagRegisted = false;
                    Dictionary<string, string> tagDictonary = new Dictionary<string, string>();
                    string tagUpdateString = String.Empty;

                    if (!String.IsNullOrEmpty(tag))
                    {
                        // タグディクショナリ生成
                        string tagValue = LinearAudioPlayer.GridController.getValue(r.Index, (int)GridController.EnuGrid.TAG);
                        if (!string.IsNullOrEmpty(tagValue))
                        {
                            string nowTag = grid[r.Index, (int)GridController.EnuGrid.TAG].Value.ToString();
                            string[] nowTagArray = nowTag.Split(new string[] { " , " }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string tagItem in nowTagArray)
                            {
                                tagDictonary.Add(tagItem, null);
                            }

                        }

                        // タグが登録済みでないかチェック
                        if (tagDictonary.ContainsKey(tag))
                        {
                            isTagRegisted = true;
                        }


                        // タグの登録
                        if (!isTagRegisted)
                        {
                            tagDictonary.Add(tag, null);
                        }
                        else
                        {
                            tagDictonary.Remove(tag);
                        }

                        // タグ文字列生成
                        string[] tagDicArray = new string[tagDictonary.Keys.Count];
                        tagDictonary.Keys.CopyTo(tagDicArray, 0);
                        tagUpdateString = string.Join(" , ", tagDicArray);

                    }

                    // タグ更新
                    grid[r.Index, (int)GridController.EnuGrid.TAG].Value = tagUpdateString;

                    // タグ更新(DB)
                    List<DbParameter> paramList = new List<DbParameter>();
                    paramList.Add(new SQLiteParameter("Tag", tagUpdateString));
                    paramList.Add(new SQLiteParameter("Id", LinearAudioPlayer.GridController.getValue(r.Index, (int)GridController.EnuGrid.ID)));
                    SQLiteManager.Instance.executeNonQuery(SQLResource.SQL008, paramList);
                }

                sqltran.Commit();


            }
            catch (SQLiteException sqle)
            {
                sqltran.Rollback();
                throw sqle;
            }
        }

        /// <summary>
        /// Tag Editorを実行する(単数)
        /// </summary>
        /// <param name="grid"></param>
        public void execTagEditor(long id)
        {

            if (LinearGlobal.TagEditDialog != null && LinearGlobal.TagEditDialog.Visible)
            {
                LinearGlobal.TagEditDialog.setTagEditInfo(id);
                return;
            }

            LinearGlobal.TagEditDialog = new TagEditDialog(id);

            LinearGlobal.TagEditDialog.Show();
            afterTagEdit(LinearGlobal.TagEditDialog);

        }

        /// <summary>
        /// Tag Editorを実行する(複数)
        /// </summary>
        public void execTagEditor()
        {

            TagEditDialog ted 
                = new TagEditDialog();
            ted.Show();
            afterTagEdit(ted);
        }

        /// <summary>
        /// タグ編集後処理
        /// </summary>
        private void afterTagEdit(TagEditDialog ted)
        {
            if (ted.Result)
            {
                foreach (TagEditFileInfo tefi in ted.FileInfoList)
                {
                    SourceGrid.Grid grid = LinearAudioPlayer.GridController.Grid;
                    int rowNo = LinearAudioPlayer.GridController.Find((int) GridController.EnuGrid.ID, tefi.Id.ToString());

                    if (rowNo > -1)
                    {
                        grid[rowNo, (int) GridController.EnuGrid.TITLE].Value
                            = ted.txtTitle.Text;
                        grid[rowNo, (int) GridController.EnuGrid.ARTIST].Value
                            = ted.txtArtist.Text;
                        grid[rowNo, (int) GridController.EnuGrid.ALBUM].Value
                            = ted.txtAlbum.Text;
                        if (!String.IsNullOrEmpty(ted.txtTrackNo.Text))
                        {
                            grid[rowNo, (int) GridController.EnuGrid.TRACK].Value
                                = ted.txtTrackNo.Text;
                        }
                        grid[rowNo, (int) GridController.EnuGrid.GENRE].Value
                            = ted.txtGenre.Text;
                        if (!String.IsNullOrEmpty(ted.txtYear.Text))
                        {
                            grid[rowNo, (int) GridController.EnuGrid.YEAR].Value
                                = ted.txtYear.Text;
                        }
                    }
                }

                // 再生中のタイトル更新
                if (LinearGlobal.CurrentPlayItemInfo.Id != -1)
                {
                    LinearGlobal.MainForm.setTitle(LinearAudioPlayer.PlayController.createTitle(
                        (GridItemInfo)LinearAudioPlayer.GridController.getRowGridItem(
                        LinearAudioPlayer.GridController.Find(
                        (int)GridController.EnuGrid.ID,
                        LinearGlobal.CurrentPlayItemInfo.Id.ToString()
                        ))));
                }
            }
            
            
        }


        /// <summary>
        /// DBからタグディクショナリを作成する。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> createTagDictonaryFromDatabase()
        {
            Dictionary<string, int> tagDic = new Dictionary<string, int>();

            object[][] resultList = SQLiteManager.Instance.executeQueryNormal(SQLResource.SQL014);

            foreach (object[] result in resultList)
            {
                string tagString = result[0].ToString();

                string[] tagArray = tagString.Split(new string[] { " , " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string tagItem in tagArray)
                {
                    if (!tagDic.ContainsKey(tagItem))
                    {
                        tagDic.Add(tagItem, 1);
                    }
                    else
                    {
                        tagDic[tagItem] = tagDic[tagItem] + 1;
                    }
                }

            }

            return tagDic;
        }


        /// <summary>
        /// ファイルをコピーする
        /// </summary>
        public void copyFile(SourceGrid.Grid grid, bool isDirect)
        {

            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "保存する場所を指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            //ユーザーが新しいフォルダを作成できるようにする
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(LinearGlobal.MainForm.ListForm) == DialogResult.OK)
            {
                string outDirPath = fbd.SelectedPath;

                RowInfo[] rowItems = LinearAudioPlayer.GridController.getSelectRowsInfo();

                foreach (RowInfo rowItem in rowItems)
                {
                    // TODO:同じところがあるので最適化する
                    string filePath = LinearAudioPlayer.GridController.getValue(rowItem.Index, (int)GridController.EnuGrid.FILEPATH);
                    string option = LinearAudioPlayer.GridController.getValue(rowItem.Index, (int)GridController.EnuGrid.OPTION);

                    if (!String.IsNullOrEmpty(option))
                    {
                        // 書庫ファイルだったら解凍する(オプションがあるのは書庫ファイルということ)

                        if (!File.Exists(Path.Combine(LinearGlobal.TempDirectory, option)))
                        {
                            SevenZipManager.Instance.extract(filePath, option, LinearGlobal.TempDirectory);
                        }
                        filePath = Path.Combine(LinearGlobal.TempDirectory, option);

                    }

                    string destFilePath = "";
                    if (isDirect)
                    {
                        destFilePath = outDirPath + "\\" + Path.GetFileName(filePath);
                    } else
                    {
                        destFilePath = outDirPath + "\\" + option;
                        string destDir = Path.GetDirectoryName(destFilePath);
                        if (!Directory.Exists(destDir))
                        {
                            // Todo: ここで失敗することがある
                            Directory.CreateDirectory(destDir);
                        }
                    }

                    if (File.Exists(destFilePath))
                    {
                        destFilePath = 
                            outDirPath + "\\" 
                            + Path.GetFileNameWithoutExtension(filePath) 
                            + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") 
                            + Path.GetExtension(filePath);
                    }

                    FileUtils.copy(filePath,destFilePath ,false);
                }
                LinearGlobal.MainForm.ListForm.showToastMessage(MessageResource.I0003);
            }

            
        }

        /// <summary>
        /// オーディオ情報を取得する。
        /// </summary>
        /// <param name="rowInfos"></param>
        public static void getAudioInfo(List<ISourceGridItem> gridItemList)
        {
            ProgressDialog pd = new ProgressDialog("オーディオ情報取得中",
                getAudioInfo_doWork,
                getAudioInfo_runComplete,
                gridItemList);

            pd.Show();

        }

        private static void getAudioInfo_runComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            LinearGlobal.MainForm.ListForm.reloadDatabase(false);
            LinearGlobal.MainForm.ListForm.showToastMessage(MessageResource.I0004);
        }

        private static void getAudioInfo_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            List<ISourceGridItem> gridItemList = (List<ISourceGridItem>)e.Argument;
            List<DbParameter> paramList = new List<DbParameter>();

            int i = 0;
            bw.ReportProgress( 0, String.Format(MessageResource.D0001, i, gridItemList.Count));

            SQLiteTransaction sqltran = null;
            try
            {
                sqltran = SQLiteManager.Instance.beginTransaction();

                foreach (var gridItem in gridItemList)
                {

                    updateAudioInfo(gridItem);

                    i++;
                    int par = NumberUtils.getParcent(i, gridItemList.Count);
                    bw.ReportProgress(par, String.Format(MessageResource.D0001, i, gridItemList.Count));
                }

                sqltran.Commit();

            } catch (SQLiteException sqlex)
            {
                sqltran.Rollback();
                throw sqlex;
            }

        }

        /// <summary>
        /// オーディオ情報を更新する。
        /// </summary>
        /// <param name="gridItem"></param>
        private static void updateAudioInfo(ISourceGridItem gridItem)
        {
            List<DbParameter> paramList = new List<DbParameter>();
            GridItemInfo gitem = (GridItemInfo)gridItem;
            string filePath = gitem.FilePath;
            string option = gitem.Option;
            string archiveTempFilePath = "";

            if (!String.IsNullOrEmpty(option) )
            {
                string arcfilePath = Path.Combine(LinearGlobal.TempDirectory, option);
                if (!File.Exists(arcfilePath))
                {
                    SevenZipManager.Instance.extract(filePath, option, LinearGlobal.TempDirectory);
                    filePath = arcfilePath;
                    archiveTempFilePath = arcfilePath;
                }
                else
                {
                    filePath = arcfilePath;
                }
                
            }

            GridItemInfo gi = LinearAudioPlayer.GridController.createNewGridItem(
                    gitem.Id.ToString());
            // オーディオファイルなら取得する
            if (File.Exists(filePath))
            {
                gi.FilePath = filePath;
                LinearAudioPlayer.PlayController.getTag(gi);


                paramList.Add(new SQLiteParameter("Title", gi.Title));
                paramList.Add(new SQLiteParameter("Artist", gi.Artist));
                paramList.Add(new SQLiteParameter("Bitrate", gi.Bitrate));
                paramList.Add(new SQLiteParameter("Album", gi.Album));
                paramList.Add(new SQLiteParameter("Track", gi.Track));
                paramList.Add(new SQLiteParameter("Genre", gi.Genre));
                paramList.Add(new SQLiteParameter("Year", gi.Year));
                paramList.Add(new SQLiteParameter("Time", gi.Time));
                paramList.Add(new SQLiteParameter("NotFound", 0));
                paramList.Add(new SQLiteParameter("Date", gi.Date));
                paramList.Add(new SQLiteParameter(":FilePath", gi.FilePath));
                paramList.Add(new SQLiteParameter(":Option", gi.Option));
                paramList.Add(new SQLiteParameter("Id", gi.Id));
                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL023, paramList);
            }
            else
            {
                paramList.Add(new SQLiteParameter("NotFound", 1));
                paramList.Add(new SQLiteParameter("Id", gi.Id));
                SQLiteManager.Instance.executeNonQuery(SQLResource.SQL024, paramList);
                archiveTempFilePath = "";
            }

            if (!String.IsNullOrEmpty(archiveTempFilePath))
            {
                // 使用後のファイルを削除
                FileUtils.delete(archiveTempFilePath);
            }

        }

        #endregion 

        #region PrivateMethod



        /// <summary>
        /// データベースにデータを登録する
        /// </summary>
        private GridItemInfo registDBData(GridItemInfo gi, List<DbParameter> paramList)
        {
            //Debug.WriteLine("IS:" + sb.getRecordInsertSQL(filePath));
            paramList.Add(new SQLiteParameter("PlayCount", gi.PlayCount));
            paramList.Add(new SQLiteParameter("Rating", gi.Rating));
            paramList.Add(new SQLiteParameter("AddDate", gi.Adddate));
            paramList.Add(new SQLiteParameter("AddDateTime", gi.AddDateTime));
            SQLiteManager.Instance.executeNonQuery(SQLResource.SQL003, paramList);
            /*
            // Last Insert Rowidを取得
            long lastRowId =
                (long)SQLiteManager.Instance.executeQueryOnlyOne(SQLResource.SQL004);
            // id更新
            gi.Id = lastRowId;
            // 拡張情報(GridItemm)でDBを更新する
            //Debug.WriteLine("US:" + sb.getBaseUpdateRecordSQL(gi));
            //paramList.Clear();
            //paramList.Add(new SQLiteParameter("Title", gi.Title));
            paramList.Add(new SQLiteParameter("PlayCount", gi.PlayCount));
            paramList.Add(new SQLiteParameter("Rating", gi.Rating));
            //paramList.Add(new SQLiteParameter("Date", gi.Date));
            paramList.Add(new SQLiteParameter("AddDate", gi.Adddate));
            paramList.Add(new SQLiteParameter("AddDateTime", gi.AddDateTime));
            //paramList.Add(new SQLiteParameter("NotFound", gi.NotFound));
            //paramList.Add(new SQLiteParameter("Id", gi.Id));
            SQLiteManager.Instance.executeNonQuery(SQLResource.SQL005, paramList);
            */
            return gi;
        }

        /// <summary>
        /// グリッド情報を更新
        /// </summary>
        /// <param name="grid"></param>
        public void updateGridInformation()
        {
            /*
            object result = SQLiteUtils.executeQueryOnlyOne(SQLBuilder.selectSumLengthSec(), null);

            if (System.DBNull.Value != result)
            {
                LinearGlobal.TotalSec = (double)result;
            }
            else
            {
                LinearGlobal.TotalSec = 0;
            }*/

            LinearGlobal.TotalSec = 0;
            for (int i = 1; i <= LinearAudioPlayer.GridController.getRowCount(); i++)
            {
                object duration = LinearAudioPlayer.GridController.Grid[i, (int) GridController.EnuGrid.TIME].Tag;
                if (duration != null)
                {
                    LinearGlobal.TotalSec += (double)duration;
                }
            }
            

            if (LinearGlobal.MainForm != null)
            {
                LinearGlobal.MainForm.ListForm.setGridInfoString(LinearAudioPlayer.GridController.getRowCount());
            }
            
        }



        #endregion

    }
}
