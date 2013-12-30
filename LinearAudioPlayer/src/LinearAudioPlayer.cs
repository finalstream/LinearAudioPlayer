using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.LinearAudioPlayer.Grid;
using FINALSTREAM.LinearAudioPlayer.GUI;
using FINALSTREAM.LinearAudioPlayer.Plugin;
using FINALSTREAM.LinearAudioPlayer.Resources;
using FINALSTREAM.LinearAudioPlayer.Setting;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Utils;
using System.IO;
using System.Threading;
using FINALSTREAM.LinearAudioPlayer.Core;
using FINALSTREAM.Commons.Exceptions;



namespace FINALSTREAM.LinearAudioPlayer
{

    /// <summary>
    /// LinearAudioPlayer Class
    /// </summary>
    static class LinearAudioPlayer
    {

        //TraceSourceの作成
        private static System.Diagnostics.TraceSource logger =
            new System.Diagnostics.TraceSource("LinearLogging");

        // プレイヤーコントローラ
        private static PlayerController _playController = null;

        public static Thread AutoAudioFileRegistThread { get; set; }

        public static GridController GridController { get; set; }

        public static FilteringGridController FilteringGridController { get; set; }

        public static LinkGridController LinkGridController { get; set; }

        public static GroupGridController GroupGridController { get; set; }

        public static StyleController StyleController { get; set; }

        public static SettingManager SettingManager { get; set; }

        public static PlayerController PlayController
        {
            set { _playController = value;  }
            get { return _playController; }
        }

        public static TraceSource Logger
        {
            set { logger = value; }
            get { return logger; }
        }

        

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            // Updaterを更新する。
            FileUtils.copy(
                Application.StartupPath + "\\update\\temp\\LinearAudioPlayer\\update\\LinearAudioPlayerUpdate.exe",
                Application.StartupPath + "\\update\\LinearAudioPlayerUpdate.exe",
                true);
            DirectoryUtils.deleteDir(Application.StartupPath + "\\update\\temp");

            // ThreadExceptionイベント・ハンドラを登録する
            Application.ThreadException += new
              ThreadExceptionEventHandler(Application_ThreadException);

            // UnhandledExceptionイベント・ハンドラを登録する
            Thread.GetDomain().UnhandledException += new
              UnhandledExceptionEventHandler(Application_UnhandledException);
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initializer init = new Initializer();
            //init.Initialize();
            //init = null;


            SettingManager = new SettingManager();
            SettingManager.LoadSetting();
            SettingManager.restoreGlobalSetting();

            initLogging();

            initApplication();

            initPlugin();

            // データベースアップデート
            UpdateUtils.updateDatabase();



            

            _playController = new PlayerController();

            LinearUtils.connectDatabase(LinearGlobal.LinearConfig.PlayerConfig.SelectDatabase);
            LinearAudioPlayer.GridController = new GridController();
            // 再生中リスト復元
            _playController.restorePlayingList();

            LinearGlobal.MainForm = new MainForm();

            StyleController = new StyleController();
            StyleController.loadStyle(LinearGlobal.StyleConfig);
            StyleController.setColorProfile(LinearGlobal.ColorConfig);
            

            // TODO: 仮設定
            switch (LinearGlobal.LinearConfig.EngineConfig.PlayEngine)
            {
                case LinearEnum.PlayEngine.FMOD:
                    LinearGlobal.MainForm.setPlayEngineInfo("Powered by FMOD Engine");
                    break;
                case LinearEnum.PlayEngine.BASS:
                    LinearGlobal.MainForm.setPlayEngineInfo("Powered by BASS Engine");
                    break;
            }

            

            /* 設定を復元 */
            // メインフォーム
            //LinearGlobal.MainForm.restoreSetting();
            // リストフォーム
            //LinearGlobal.MainForm.ListForm.restoreSetting();
            // 設定フォーム
            //LinearGlobal.MainForm.ConfigForm.restoreConfig(
            //    LinearGlobal.LinearConfig);

            

            // データベースアップデート(ver.0.3.0用)
            LinearGlobal.isCompleteStartup = true;

            
            Application.Run(LinearGlobal.MainForm);


        }
        

        /// <summary>
        /// プラグインの初期化
        /// </summary>
        private static void initPlugin()
        {
            LinearAudioPlayerPluginInfo[] pluginInfos = LinearAudioPlayerPluginInfo.FindPlugins();

            //すべてのプラグインクラスのインスタンスを作成する
            LinearGlobal.Plugins = new ILinearAudioPlayerPlugin[pluginInfos.Length];
            for (int i = 0; i < pluginInfos.Length; i++)
                LinearGlobal.Plugins[i] = pluginInfos[i].CreateInstance();

            List<string> excludePlugins = new List<string>(LinearGlobal.LinearConfig.PlayerConfig.ExcludePlugins);
            foreach (var plugin in LinearGlobal.Plugins)
            {
                if (!excludePlugins.Contains(plugin.Name))
                {
                    bool result = plugin.Init();
                    if (result)
                    {
                        plugin.Enable = true;
                    }
                    else
                    {
                        // 初期化に失敗した場合、プラグイン除外リストに追加する
                        excludePlugins.Add(plugin.Name);

                    }
                }
            }
            LinearGlobal.LinearConfig.PlayerConfig.ExcludePlugins = excludePlugins.ToArray();

        }

        static public bool initApplication()
        {
            bool result = true;

            // グローバル情報設定
            LinearGlobal.DatabaseDirectory =
                Application.StartupPath + LinearConst.DATABASE_DIRECTORY_NAME;

           LinearGlobal.StyleDirectory =
                Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME + LinearGlobal.LinearConfig.ViewConfig.StyleName + "\\";

            Environment.SetEnvironmentVariable("PATH", Application.StartupPath + "\\lib\\migemo");


            // データべースファイル存在確認
            if (FileUtils.getFilePathListWithExtFilter(
                new string[]{LinearGlobal.DatabaseDirectory}, 
                SearchOption.AllDirectories,
                new string[]{".db"}).Count == 0)
            {
                // dbファイルが一つもない場合linear.dbを作成
                FileUtils.copy(Application.StartupPath + "\\" + LinearConst.BLANK_DBFILE,
                    Application.StartupPath + LinearConst.DATABASE_DIRECTORY_NAME + LinearConst.LINEAR_DBFILE,
                    false);
            }

            // バージョン情報設定
            setVersionInfo();

            SevenZipManager.Instance.init(Application.StartupPath + "\\lib\\sevenzip\\7z.dll");

            return result;

        }

        /// <summary>
        /// ロギングを初期化する
        /// </summary>
        static private void initLogging()
        {

            //Error以上のイベントが通過されるようにする
            logger.Switch.Level = System.Diagnostics.SourceLevels.Warning;

            //DefaultTraceListenerが必要なければ削除する
            logger.Listeners.Remove("Default");

            //TextWriterTraceListenerオブジェクトを作成
            System.Diagnostics.TextWriterTraceListener twtl =
                new System.Diagnostics.TextWriterTraceListener(
                    Application.StartupPath + "\\log\\linearerror.log", "LogFile");

            twtl.TraceOutputOptions = System.Diagnostics.TraceOptions.DateTime;

            //リスナコレクションに追加する
            logger.Listeners.Add(twtl);

        }

        static public void endApplication()
        {
            if (AutoAudioFileRegistThread != null)
            {
                AutoAudioFileRegistThread.Join();
            }

            // 再生中リスト保存
            _playController.savePlayingList();

            //Initializer init = new Initializer();
            SettingManager sm = new SettingManager();

            SQLiteManager.Instance.closeDatabase();
            sm.SaveSetting();

            // テンポラリディレクトリ削除
            DirectoryUtils.deleteDir(LinearGlobal.TempDirectory);

            //init.dispose();
            Dispose();

            if (LinearGlobal.IsUpdateNewVersion)
            {
                System.Threading.Thread.Sleep(1000);
                Process p = new Process();
                // 管理者として実行
                p.StartInfo.FileName = Application.StartupPath + "\\update\\LinearAudioPlayerUpdate.exe";
                p.StartInfo.Arguments = "-u";
                p.Start();
            }
        }

        static public void setVersionInfo()
        {
            //自分自身のバージョン情報を取得する
            System.Diagnostics.FileVersionInfo appver =
                System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            LinearGlobal.ApplicationVersion = "ver." 
                + appver.FileMajorPart.ToString() + "." 
                + appver.FileMinorPart.ToString() + "." 
                + appver.FileBuildPart.ToString();
        }

        
        // 未処理例外をキャッチするイベント・ハンドラ
        // （Windowsアプリケーション用）
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //ShowErrorMessage(e.Exception, "Application_ThreadExceptionによる例外通知です。");
            if (e.Exception is FinalstreamException)
            {
                FinalstreamException finalex = (FinalstreamException)e.Exception;

                switch (finalex.ErrorLevel)
                {
                    case FinalstreamException.ERROR_LEVEL.Critical:
                        ShowErrorMessage(e.Exception);
                        break;
                    case FinalstreamException.ERROR_LEVEL.Warn:
                        writeErrorMessage(e.Exception);
                        break;
                }
            }
            else
            {
                ShowErrorMessage(e.Exception);
            }

        }

        // 未処理例外をキャッチするイベント・ハンドラ
        // （主にコンソール・アプリケーション用）
        public static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                ShowErrorMessage(ex);
            }
        }

        // ユーザー・フレンドリなダイアログを表示するメソッド
        public static void ShowErrorMessage(Exception ex)
        {
            logger.TraceEvent(System.Diagnostics.TraceEventType.Critical,
                0, ex.ToString());

            logger.Flush();
            logger.Close();

            MessageBox.Show("エラーが発生したため、エラーログ(log\\linearerror.log)を作成しました。\n" +
              " お手数ですが問題解決のため、エラーログをFINALSTREAMにメールでお知らせください。\n" +
              " ご協力お願いいたします。","クリティカルエラー",MessageBoxButtons.OK, MessageBoxIcon.Error);

            //Application.Exit();

        }

        // ユーザー・フレンドリなログを出力するメソッド
        public static void writeErrorMessage(Exception ex)
        {
            logger.TraceEvent(System.Diagnostics.TraceEventType.Warning,
                0, ex.ToString());

            logger.Flush();
            logger.Close();

        }

        public static void Dispose()
        {
            if (PlayController != null)
            {
                PlayController.Dispose();
                PlayController = null;
            }
            
        }

    }
}
