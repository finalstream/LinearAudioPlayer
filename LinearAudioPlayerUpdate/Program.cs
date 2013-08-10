using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FINALSTREAM.Commons.Utils;

namespace LinearAudioPlayerUpdate
{
    static class Program
    {
        /// <summary>
        /// 強制アップデート
        /// </summary>
        public static bool isForceUpdate = false;

        public static string linearProcessId = "";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // ThreadExceptionイベント・ハンドラを登録する
            Application.ThreadException += new
              ThreadExceptionEventHandler(Application_ThreadException);

            // UnhandledExceptionイベント・ハンドラを登録する
            Thread.GetDomain().UnhandledException += new
              UnhandledExceptionEventHandler(Application_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // アップデート用フォルダ作成
            Directory.CreateDirectory(UpdateForm.UPDATE_DIR);

            if (args.Length > 0)
            {
                if ("-u".Equals(args[0]))
                {
                    isForceUpdate = true;
                }
            }

            // 必要なファイルコピー
            File.Copy(UpdateForm.LINEAR_DIR + "\\lib\\finalstream\\Finalstream.Commons.dll", UpdateForm.LINEAR_DIR + "\\update\\Finalstream.Commons.dll", true);
            File.Copy(UpdateForm.LINEAR_DIR + "\\lib\\sevenzip\\7z.dll", UpdateForm.LINEAR_DIR + "\\update\\7z.dll", true);
            File.Copy(UpdateForm.LINEAR_DIR + "\\lib\\sevenzip\\SevenZipSharp.dll", UpdateForm.LINEAR_DIR + "\\update\\SevenZipSharp.dll", true);

            

            Application.Run(new UpdateForm());
        }

        // 未処理例外をキャッチするイベント・ハンドラ
        // （Windowsアプリケーション用）
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //ShowErrorMessage(e.Exception, "Application_ThreadExceptionによる例外通知です。");
            MessageBox.Show(e.Exception.ToString());
        }

        // 未処理例外をキャッチするイベント・ハンドラ
        // （主にコンソール・アプリケーション用）
        public static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
