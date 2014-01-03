using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FINALSTREAM.LinearAudioPlayer
{
    static class LinearConst
    {

        public const string APPLICATION_TITLE = "Linear Audio Player";

        public const string TEMP_DIRCTORY_NAME = "lap";

        public const string SETTING_FILE = "\\linearconfig.xml";
        public const string COLOR_DIRECTORY_NAME = "\\color\\";

        public const string DATEFORMAT_ADDDATE = "yyyyMMddHHmmssffffff";

        public const string DEFAULT_STYLE = "Lyrids";

        public const string DEFAULT_ALBUM_RENAME_TEMPLETE = "%R - %A %Y";

        // radioモードタイトル
        public const string RADIO_MODE_TITLE = "radio";

        public const int FLG_OFF = 0;
        public const int FLG_ON = 1;

        /// <summary>
        /// スタイルフォルダ名
        /// </summary>
        public const string STYLE_DIRECTORY_NAME = "\\style\\";

        /// <summary>
        /// データベースフォルダ名
        /// </summary>
        public const string DATABASE_DIRECTORY_NAME = "\\db\\";

        /// <summary>
        /// SQLフォルダ名
        /// </summary>
        public const string SQL_DIRECTORY_NAME = "\\sql\\";

        /// <summary>
        /// WEBサーバドディレクトリ
        /// </summary>
        public const string WEB_DIRECTORY_NAME = "\\web\\";

        /// <summary>
        /// BASSプラグインフォルダ名
        /// </summary>
        public const string BASS_PLUGIN_DIRECTORY_NAME = "\\lib\\bass\\plugin";

        public const string MIGEMO_DICTIONARY_NAME = "\\lib\\migemo\\dict\\migemo-dict";
        public const string MIGEMO_USERDICTIONARY_NAME = "\\lib\\migemo\\dict\\user-dict";

        public const string TITLE_VOLUME_VIEW_FORMAT = " (volume: {0}%)";

        /// <summary>
        /// LINEARデータベースファイル名
        /// </summary>
        public const string LINEAR_DBFILE = "linear.db";

        /// <summary>
        /// RADIOデータベースファイル名
        /// </summary>
        public const string RADIO_DBFILE = "radio.db";

        /// <summary>
        /// ブランクファイル名
        /// </summary>
        public const string BLANK_DBFILE = "db\\blank.dbbase";

        /// <summary>
        /// 評価アイコン：お気に入り
        /// </summary>
        public const string STAR_FAVORITE_FILE = "rating_favorite.png";

        /// <summary>
        /// 評価アイコン：通常
        /// </summary>
        public const string STAR_NORMAL_FILE = "rating_normal.png";

        /*
         * ディスプレイ表示モード
         */
        /// <summary>
        /// 再生時間表示
        /// </summary>
        public const string DISPLAYTIMEMODE_NORMAL = "0";
        /// <summary>
        /// 残り時間表示
        /// </summary>
        public const string DISPLAYTIMEMODE_REVERSE = "1";  

        

        public const string DISPLAYTIME_DEFAULT = "00:00";

        /// <summary>
        /// サポート書庫拡張子
        /// </summary>
        public static string[] ARCHIVE_EXTENSION_ARY = {".zip", ".rar", ".lzh", ".7z" };

        /// <summary>
        /// 割り込みフォームのサイズ
        /// </summary>
        public static int INTERRUPTFORM_WIDTH = 200;

        /// <summary>
        /// 次のプレイリスト最大数
        /// </summary>
        public static int MAX_NEXTPLAYLIST_NUM = 1;
    }
}
