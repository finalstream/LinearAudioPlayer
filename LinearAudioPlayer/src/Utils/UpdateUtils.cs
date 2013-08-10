using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Forms;
using FINALSTREAM.Commons.Utils;
using System.Data.SQLite;
using System.Diagnostics;

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

    }
}
