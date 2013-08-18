using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Forms;
using FINALSTREAM.Commons.Network;
using FINALSTREAM.Commons.Utils;
using System.Data.SQLite;
using System.Diagnostics;
using FINALSTREAM.LinearAudioPlayer.Info;

namespace FINALSTREAM.LinearAudioPlayer.Utils
{
    /// <summary>
    /// アップデートユーティリティクラス。
    /// </summary>
    static class UpdateUtils
    {
        public delegate void upgradeDatbase();
        static bool isEnd = false;

        private static WaitDialog waitDialog = null;
        /// <summary>
        /// データベースをアップデートする
        /// </summary>
        public static void updateDatabase()
        {
            upgradeDatbase asyncCall;
            IAsyncResult ar;
            IList<string> dbFileList;

            if (LinearGlobal.LinearConfig.Version == "")
            {
                return;
            }

            //アップデートする必要がある調べる
            if ("ver.0.8.0".CompareTo(LinearGlobal.LinearConfig.Version) > 0)
            {
                waitDialog = new WaitDialog("データベースをアップグレード中です");
                asyncCall = new upgradeDatbase(AsynchronousMethod);
                // asyncCall を非同期で呼び出す。
                ar = asyncCall.BeginInvoke(null, null);

                dbFileList =
                FileUtils.getFilePathListWithExtFilter(
                    new string[] { LinearGlobal.DatabaseDirectory },
                    System.IO.SearchOption.TopDirectoryOnly,
                    new string[] { ".db" });

                foreach (string dbfile in dbFileList)
                {
                    SQLiteManager.Instance.closeDatabase();
                    SQLiteManager.Instance.connectDatabase(dbfile);

                    SQLiteTransaction sqltran = null;
                    try
                    {
                        sqltran = SQLiteManager.Instance.beginTransaction();

                        SQLiteManager.Instance.executeNonQuery
                            ("ALTER TABLE PLAYLIST ADD COLUMN DESCRIPTION TEXT");

                        sqltran.Commit();
                    }
                    catch (SQLiteException)
                    {
                        sqltran.Rollback();
                    }

                    SQLiteManager.Instance.closeDatabase();
                }

            }

            /*

            //アップデートする必要がある調べる
            if ("ver.0.6.6".CompareTo(LinearGlobal.LinearConfig.Version) > 0)
            {
                waitDialog = new WaitDialog("データベースをアップグレード中です");
                asyncCall = new upgradeDatbase(AsynchronousMethod);
                // asyncCall を非同期で呼び出す。
                ar = asyncCall.BeginInvoke(null, null);

                LinearGlobal.LinearConfig.PlayerConfig.albumAutoRenameTemplete =
                    LinearConst.DEFAULT_ALBUM_RENAME_TEMPLETE;


                dbFileList =
                FileUtils.getFilePathListWithExtFilter(
                    new string[] { LinearGlobal.DatabaseDirectory },
                    System.IO.SearchOption.TopDirectoryOnly,
                    new string[] { ".db" });

                foreach (string dbfile in dbFileList)
                {
                    SQLiteManager.Instance.closeDatabase();
                    SQLiteManager.Instance.connectDatabase(dbfile);

                    SQLiteTransaction sqltran = null;
                    try
                    {
                        sqltran = SQLiteManager.Instance.beginTransaction();

                        SQLiteManager.Instance.executeNonQuery
                            ("ALTER TABLE PLAYLIST ADD COLUMN FILESIZE INTEGER");
                        SQLiteManager.Instance.executeNonQuery
                            ("ALTER TABLE PLAYLIST ADD COLUMN FILEHASH TEXT");
                        SQLiteManager.Instance.executeNonQuery
                            ("UPDATE PLAYLIST SET FILESIZE = GETFILESIZE(FILEPATH, OPTION) WHERE FILESIZE is null");
                        //SQLiteUtils.executeNonQuery
                        //    ("UPDATE PLAYLIST SET FILEHASH = GETFILEHASH(FILEPATH, OPTION) WHERE FILESIZE IN (SELECT FILESIZE FROM PLAYLIST WHERE FILESIZE > 0 GROUP BY FILESIZE HAVING COUNT(*) > 1)");
                        SQLiteManager.Instance.executeNonQuery
                            ("UPDATE PLAYLIST SET FILEHASH = GETFILEHASH(FILEPATH, OPTION) WHERE FILESIZE > 0");

                        sqltran.Commit();
                    }
                    catch (SQLiteException)
                    {
                        sqltran.Rollback();
                    }

                    SQLiteManager.Instance.closeDatabase();
                }

            }

            if ("ver.0.6.7".CompareTo(LinearGlobal.LinearConfig.Version) > 0)
            {
                if (waitDialog == null)
                {
                    waitDialog = new WaitDialog("データベースをアップグレード中です");
                    asyncCall = new upgradeDatbase(AsynchronousMethod);
                    // asyncCall を非同期で呼び出す。
                    ar = asyncCall.BeginInvoke(null, null);
                }

                dbFileList =
                FileUtils.getFilePathListWithExtFilter(
                    new string[] { LinearGlobal.DatabaseDirectory },
                    System.IO.SearchOption.TopDirectoryOnly,
                    new string[] { ".db" });

                foreach (string dbfile in dbFileList)
                {
                    SQLiteManager.Instance.closeDatabase();
                    SQLiteManager.Instance.connectDatabase(dbfile);

                    SQLiteTransaction sqltran = null;
                    try
                    {
                        sqltran = SQLiteManager.Instance.beginTransaction();

                        SQLiteManager.Instance.executeNonQuery
                            ("UPDATE PLAYLIST SET LENGTH = replace(substr(LENGTH,1,1),'0','') || substr(LENGTH,2)");

                        sqltran.Commit();
                    }
                    catch (SQLiteException)
                    {
                        sqltran.Rollback();
                    }

                    SQLiteManager.Instance.closeDatabase();
                }



            }


            if ("ver.0.6.8".CompareTo(LinearGlobal.LinearConfig.Version) > 0)
            {
                if (waitDialog == null)
                {
                    waitDialog = new WaitDialog("データベースをアップグレード中です");
                    asyncCall = new upgradeDatbase(AsynchronousMethod);
                    // asyncCall を非同期で呼び出す。
                    ar = asyncCall.BeginInvoke(null, null);
                }

                dbFileList =
                FileUtils.getFilePathListWithExtFilter(
                    new string[] { LinearGlobal.DatabaseDirectory },
                    System.IO.SearchOption.TopDirectoryOnly,
                    new string[] { ".db" });

                foreach (string dbfile in dbFileList)
                {
                    SQLiteManager.Instance.closeDatabase();
                    SQLiteManager.Instance.connectDatabase(dbfile);

                    SQLiteTransaction sqltran = null;
                    try
                    {
                        sqltran = SQLiteManager.Instance.beginTransaction();

                        SQLiteManager.Instance.executeNonQuery
                            ("CREATE TABLE PLAYINGLIST(ID INTEGER NOT NULL, SORT INTEGER NOT NULL)");

                        sqltran.Commit();
                    }
                    catch (SQLiteException)
                    {
                        sqltran.Rollback();
                    }

                    SQLiteManager.Instance.closeDatabase();
                }



            }*/

            isEnd = true;
        }

        static void AsynchronousMethod()
        {
            waitDialog.Show();
            while(!isEnd)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }
            waitDialog.Close();
        }

        /// <summary>
        /// アップデート確認メッセージを表示する。
        /// </summary>
        public static void showUpdateConfirmMessage(string newFileVersion)
        {
                if (MessageUtils.showQuestionMessage("Linear Audio Playerの最新バージョンが見つかりました。\nいますぐver." + newFileVersion + "にアップデートしますか？\nアップデート後、再起動します。") == DialogResult.OK)
                {
                    LinearGlobal.IsUpdateNewVersion = true;
                    //アプリケーションを終了する
                    Application.Exit();
                }
        }

        /// <summary>
        /// 最新バージョンがリリースされているかチェックする
        /// </summary>
        /// <returns></returns>
        public static UpdateInfo checkSoftwareUpdate()
        {
            UpdateInfo updateInfo = new UpdateInfo();

            try
            {

                WebResponse res = new WebManager().request("http://www.finalstream.net/dl/download.php?dl=lap-checkupdate");

                updateInfo.NewFileVersion = Path.GetFileNameWithoutExtension(res.ResponseUri.ToString());
                updateInfo.NewFileVersion = updateInfo.NewFileVersion.Substring(updateInfo.NewFileVersion.Length - 5, 5);

                string nowVersion = LinearGlobal.ApplicationVersion.Substring(LinearGlobal.ApplicationVersion.Length - 5, 5);

                if (updateInfo.NewFileVersion.CompareTo(nowVersion) > 0)
                {
                    updateInfo.CheckResultMessage = "新しいバージョン(ver." + updateInfo.NewFileVersion + ")がリリースされています。";
                    updateInfo.CheckResultMessageColor = Color.Crimson;
                    updateInfo.IsReleaseNewVersion = true;
                }
                else if (updateInfo.NewFileVersion.CompareTo(nowVersion) == 0)
                {
                    updateInfo.CheckResultMessage = "最新バージョンのLinear Audio Playerを使用しています。";
                }
                else
                {
                    updateInfo.CheckResultMessage = "開発中バージョンのLinear Audio Playerを使用しています。";
                    updateInfo.CheckResultMessageColor = Color.MediumBlue;
                }



            }
            catch
            {
                updateInfo.CheckResultMessage = "最新バージョンチェックに失敗しました。";
            }


            return updateInfo;

        }

    }
}
