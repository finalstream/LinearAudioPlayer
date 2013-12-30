using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Database;

namespace FINALSTREAM.LinearAudioPlayer.Utils
{
    public static class LinearUtils
    {

        /// <summary>
        /// データベースに接続する。
        /// </summary>
        /// <param name="databaseName"></param>
        public static void connectDatabase(string databaseName)
        {
            if (!File.Exists(LinearGlobal.DatabaseDirectory + databaseName + ".db"))
            {
                // 存在しなかったらデフォルト
                databaseName = "linear";
            }

            // データベースに接続
            SQLiteManager.Instance.connectDatabase(
                LinearGlobal.DatabaseDirectory + databaseName + ".db");

            SQLiteManager.Instance.registFunction(typeof(GetDirectoryPathSQLiteFunction));
            SQLiteManager.Instance.registFunction(typeof(IsMatchMigemoSQLiteFunction));
            SQLiteManager.Instance.registFunction(typeof(IsMatchTagSQLiteFunction));
            //SQLiteManager.Instance.registFunction(typeof(GetFileHashSQLiteFunction));
            SQLiteManager.Instance.registFunction(typeof(GetFileSizeSQLiteFunction));
            SQLiteManager.Instance.registFunction(typeof(GetDirNameSQLiteFunction));
            // DBアップデート
            //UpdateUtils.updateDatabaseBeta5();

        }

        /* 2010/02/11 del 配列でチェックするので廃止
        /// <summary>
        /// ファイル拡張子で再生対象か判定する
        /// </summary>
        /// <param name="strFileExt">ファイル拡張子</param>
        /// <returns>対象であるか</returns>
        public static bool CheckFileExt(string strFileExt)
        {
            // 拡張子を小文字に変換
            strFileExt = strFileExt.ToLower();
            // 拡張子チェック
            if (strFileExt == ".mp3"
                        || strFileExt == ".ogg"
                        || strFileExt == ".wma"
                        || strFileExt == ".flac"
                        || strFileExt == ".wav")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
    }
}
