using System;
using System.Collections.Generic;
using System.Text;
using FINALSTREAM.LinearAudioPlayer.GUI;
using FINALSTREAM.LinearAudioPlayer.Plugin;
using FINALSTREAM.LinearAudioPlayer.Setting;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.GUI.option;

namespace FINALSTREAM.LinearAudioPlayer
{
    static class LinearGlobal
    {

        /*
            静的プライベートメンバ
        */
        #region Static Private Member

        /* Application */
        // スタートアップフラグ
        public static bool isCompleteStartup = false;
        private static string _styleDirectory;
        private static string _databaseDirectory;
        private static string _tempDirectory;

        // 再生回数カウントアップストップウオッチ
        public static System.Diagnostics.Stopwatch PlayCountUpStopwatch = new System.Diagnostics.Stopwatch();
        
        private static string[] _supportAudioExtensionAry;

        private static string _applicationVersion = "beta";

       
        /* Setting */
        private static LinearConfig _linearConfig;

        public static ColorConfig ColorConfig { get; set; }

        public static StyleConfig StyleConfig { get; set; }

        public static bool IsUpdateNewVersion = false;

        public static ILinearAudioPlayerPlugin[] Plugins { get; set; }

        /* FORM */
        private static MainForm _mainForm;
        //private static ConfigForm _configForm;

        public static TagEditDialog TagEditDialog { get; set; }


        /* Player */
        private static string _timeDisplayMode = LinearConst.DISPLAYTIMEMODE_NORMAL;
        private static LinearEnum.PlayMode _playMode = LinearEnum.PlayMode.NORMAL;
        public static PlayItemInfo CurrentPlayItemInfo = new PlayItemInfo();
        private static bool _titleDisplayScroll = false;
        private static int _volume = 100;
        private static double _totalSec = 0;
        private static LinearEnum.PlaylistMode _playlistMode = LinearEnum.PlaylistMode.NORMAL;
        private static LinearEnum.SilentMode _silentMode = LinearEnum.SilentMode.OFF;
        private static LinearEnum.FilteringMode _filteringMode = LinearEnum.FilteringMode.DEFAULT;
        private static LinearEnum.DatabaseMode _databaseMode = LinearEnum.DatabaseMode.MUSIC;

        public static LinearEnum.ShuffleMode ShuffleMode { get; set; }

        public static LinearEnum.SortMode SortMode { get; set; }

        public static long PreviousBaseId { get; set; } 
        public static string PreviousBaseDateTime { get; set; }

        /// <summary>
        /// クリップボード
        /// </summary>
        public static Stack<PlayItemInfo> ClipboardStack = new Stack<PlayItemInfo>();

        /// <summary>
        /// ストックタグ情報
        /// </summary>
        public static List<GridItemInfo> StockTagEditList = new List<GridItemInfo>(); 

        /// <summary>
        /// 一時無効IDテーブル
        /// </summary>
        public static HashSet<long> invalidIdTable = new HashSet<long>();

        #endregion


        /*
         * プロパティ
         */
        #region Property

        /// <summary>
        /// アプリケーションバージョン
        /// </summary>
        public static string ApplicationVersion
        {
            get { return LinearGlobal._applicationVersion; }
            set { LinearGlobal._applicationVersion = value; }
        }

        /// <summary>
        /// 設定情報
        /// </summary>
        public static LinearConfig LinearConfig
        {
            get { return LinearGlobal._linearConfig; }
            set { LinearGlobal._linearConfig = value; }
        }

        /// <summary>
        /// MainFormのインスタンス
        /// </summary>
        public static MainForm MainForm
        {
            get { return LinearGlobal._mainForm; }
            set { LinearGlobal._mainForm = value; }
        }

        /// <summary>
        /// プレイモード
        /// </summary>
        public static LinearEnum.PlayMode PlayMode
        {
            get { return _playMode; }
            set { _playMode = value; }
        }

        /// <summary>
        /// 時間表示モード
        /// </summary>
        public static string TimeDisplayMode
        {
            get { return _timeDisplayMode; }
            set { _timeDisplayMode = value; }
        }

        /// <summary>
        /// タイトルスクロール
        /// </summary>
        public static bool TitleDisplayScroll
        {
            get { return LinearGlobal._titleDisplayScroll; }
            set { LinearGlobal._titleDisplayScroll = value; }
        }

        /// <summary>
        /// ボリューム
        /// </summary>
        public static int Volume
        {
            get { return LinearGlobal._volume; }
            set { LinearGlobal._volume = value; }
        }

        /// <summary>
        /// トータル時間(sec)
        /// </summary>
        public static double TotalSec
        {
            get { return LinearGlobal._totalSec; }
            set { LinearGlobal._totalSec = value; }
        }

        /// <summary>
        /// プレイリストモード
        /// </summary>
        public static LinearEnum.PlaylistMode PlaylistMode
        {
            get { return LinearGlobal._playlistMode; }
            set { LinearGlobal._playlistMode = value; }
        }

        /// <summary>
        /// アイコンディレクトリ
        /// </summary>
        public static string StyleDirectory
        {
            get { return LinearGlobal._styleDirectory; }
            set { LinearGlobal._styleDirectory = value; }
        }

        public static string DefaultStyleDirectory { get; set; }

        /// <summary>
        /// データベースディレクトリ
        /// </summary>
        public static string DatabaseDirectory
        {
            get { return LinearGlobal._databaseDirectory; }
            set { LinearGlobal._databaseDirectory = value; }
        }

        /// <summary>
        /// テンポラリディレクトリ
        /// </summary>
        public static string TempDirectory
        {
            get { return LinearGlobal._tempDirectory; }
            set { LinearGlobal._tempDirectory = value; }
        }

        /// <summary>
        /// サイレントモード
        /// </summary>
        public static LinearEnum.SilentMode SilentMode
        {
            get { return LinearGlobal._silentMode; }
            set { LinearGlobal._silentMode = value; }
        }

        /// <summary>
        /// コンディションモード
        /// </summary>
        public static LinearEnum.FilteringMode FilteringMode
        {
            get { return LinearGlobal._filteringMode; }
            set { LinearGlobal._filteringMode = value; }
        }

        /// <summary>
        /// 対応オーディオファイル拡張子
        /// </summary>
        public static string[] SupportAudioExtensionAry
        {
            get { return LinearGlobal._supportAudioExtensionAry; }
            set { LinearGlobal._supportAudioExtensionAry = value; }
        }

        /// <summary>
        /// データベースモード
        /// </summary>
        public static LinearEnum.DatabaseMode DatabaseMode
        {
            get { return _databaseMode; }
            set { _databaseMode = value; }
        }

        #endregion


    }
}
