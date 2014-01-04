using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Network;
using FINALSTREAM.Commons.Utils;

namespace LinearAudioPlayerUpdate
{
    public partial class UpdateForm : Form
    {
        public static string LINEAR_DIR = Path.GetDirectoryName(Application.StartupPath);
        public static string UPDATE_DIR = Application.StartupPath + "\\temp";
        private static string LINEAR_URL = "http://www.finalstream.net/dl/download.php?dl=lap";
        //WebClientフィールド
        private System.Net.WebClient downloadClient = null;

        public UpdateForm()
        {
            InitializeComponent();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            
            

            downloadClient = new WebClient();
            //イベントハンドラの作成
            downloadClient.DownloadProgressChanged +=
                new System.Net.DownloadProgressChangedEventHandler(
                    downloadClient_DownloadProgressChanged);
            downloadClient.DownloadFileCompleted +=
                new System.ComponentModel.AsyncCompletedEventHandler(downloadClient_DownloadFileCompleted);

            if (Program.isForceUpdate)
            {
                update();
                return;
            }

            // アップデートする必要があるか？
            string nowVersion = "";
            if (File.Exists(LINEAR_DIR + "\\LinearAudioPlayer.exe"))
            {
                System.Diagnostics.FileVersionInfo appver = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                    LINEAR_DIR +  "\\LinearAudioPlayer.exe");
                nowVersion = appver.FileMajorPart.ToString() + "."
                + appver.FileMinorPart.ToString() + "."
                + appver.FileBuildPart.ToString();
            }

            WebResponse res = new WebManager().request(LINEAR_URL);
            string newFileVersion = "";
            newFileVersion  = Path.GetFileNameWithoutExtension(res.ResponseUri.ToString());
            res.Close();
            newFileVersion = newFileVersion.Substring(newFileVersion.Length - 5, 5);

            if (newFileVersion.CompareTo(nowVersion) > 0)
            {
                if (MessageUtils.showQuestionMessage("最新バージョンが見つかりました。\nver." + newFileVersion + "にアップデートしますか？\nアップデートするとLinearAudioPlayerは再起動します。") == DialogResult.OK)
                {
                    update();
                }
                else
                {
                    // 終了
                    Application.Exit();
                }
            }
            else
            {
                MessageUtils.showMessage(MessageBoxIcon.Information, "最新バージョンのLinearAudioPlayerを使用しています。");
                // 終了
                Application.Exit();
            }
            
        }

        /// <summary>
        /// アップデート
        /// </summary>
        private void update()
        {
            
            

            downloadfile(LINEAR_URL, UPDATE_DIR);
            
        }


        /// <summary>
        /// WEBからファイルをダウンロードする。
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        private void downloadfile(string url,
            string downloadpath)
        {



            //非同期ダウンロードを開始する
            downloadClient.DownloadFileAsync(new Uri(url), downloadpath + "\\lap.zip");

            return;
        }

        private void downloadClient_DownloadProgressChanged(object sender,
    System.Net.DownloadProgressChangedEventArgs e)
        {
            progressbar.Maximum = (int) e.TotalBytesToReceive;
            progressbar.Value = (int) e.BytesReceived;
            statusMessage.Text = e.ProgressPercentage + "% ダウンロードが完了しました。";
            //Console.WriteLine("{0}% ({1}byte 中 {2}byte) ダウンロードが終了しました。",
            //    e.ProgressPercentage, e.TotalBytesToReceive, e.BytesReceived);
        }

        private void downloadClient_DownloadFileCompleted(object sender,
            System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                Console.WriteLine("エラー:{0}", e.Error.Message);
            else if (e.Cancelled)
                Console.WriteLine("キャンセルされました。");
            else
                Console.WriteLine("ダウンロードが完了しました。");


            // 解凍
            statusMessage.Text = "解凍中です。";
            this.Refresh();
            SevenZipManager.Instance.extract(UPDATE_DIR + "\\lap.zip", null, UPDATE_DIR);

            // インストール
            statusMessage.Text = "インストール中です。";
            this.Refresh();
            try
            {
                FileUtils.allcopy(UPDATE_DIR + "\\LinearAudioPlayer", LINEAR_DIR);
            }
            catch (IOException ioex)
            {
                if (ioex.Message.IndexOf("LinearAudioPlayerUpdate.exe") == -1)
                {
                    MessageUtils.showMessage(MessageBoxIcon.Error, "ファイルが使用中のため失敗しました。\nLinearAudioPlayerを起動している場合は終了してから再度実行してください。\n\n" + ioex.Message);
                }
                
            }


            MessageUtils.showMessage(MessageBoxIcon.Information, "アップデートが完了しました。再起動します。");

            Process p = Process.Start(LINEAR_DIR + "\\LinearAudioPlayer.exe");

            // 終了
            Application.Exit();
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // アップデートフォルダ削除(updaterをアップデートするために残しておく)
            //DirectoryUtils.deleteDir(UPDATE_DIR);
        }

    }
}
