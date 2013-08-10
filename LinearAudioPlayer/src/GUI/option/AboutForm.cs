﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FINALSTREAM.Commons.Network;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Core;
using System.Net;
using System.IO;

namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    public partial class AboutForm : Form
    {
        private bool _isReleaseNewVersion;
        private string newFileVersion;

        #region delegate

        //  デリゲートを定義。

        delegate void UpdateTextDelegate(string message, Color color);

        delegate void ShowMessageDelegate();

        #endregion

        public AboutForm()
        {
            InitializeComponent();

            System.Diagnostics.FileVersionInfo fmodver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\fmod\\fmodex.dll");
            System.Diagnostics.FileVersionInfo fcver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\finalstream\\Finalstream.Commons.dll");
            System.Diagnostics.FileVersionInfo sqlitever = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\sqlite\\System.Data.SQLite.DLL");
            System.Diagnostics.FileVersionInfo sgver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\sourcegrid\\SourceGrid.dll");
            System.Diagnostics.FileVersionInfo taglibsver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\taglib\\taglib-sharp.dll");
            System.Diagnostics.FileVersionInfo szsver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\sevenzip\\SevenZipSharp.dll");
            System.Diagnostics.FileVersionInfo szver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\sevenzip\\7z.dll");
            System.Diagnostics.FileVersionInfo bassver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\bass\\bass.dll");
            System.Diagnostics.FileVersionInfo bassnetver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\bass\\Bass.Net.dll");
            System.Diagnostics.FileVersionInfo gapiver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\google\\GAPI.dll");
            System.Diagnostics.FileVersionInfo migemover = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\migemo\\migemo.dll");
            System.Diagnostics.FileVersionInfo restsharpver = System.Diagnostics.FileVersionInfo.GetVersionInfo("lib\\rest\\RestSharp.dll");

            string mes = "\r\nCommon Library:\r\nFinalstream Commons Library ver." + fcver.FileVersion + "\r\nCopyright © 2008-2013 FINALSTREAM.\r\nhttp://www.finalstream.net/";
            mes += "\r\n\r\nPlay Engine:\r\nPowered by FMOD Sound System ver." + fmodver.FileVersion + "\r\nCopyright © Firelight Technologies Pty, Ltd., 1994-2013.\r\nhttp://www.fmod.org/";
            mes += "               \r\n\r\nPowered by BASS Audio Library ver." + bassver.FileVersion + "\r\nCopyright © 1999-2013 Un4seen Developments Ltd.\r\nhttp://www.un4seen.com/";
            mes += "               \r\nPowered by BASS.NET ver." + bassnetver.FileVersion + "\r\nCopyright © 2005-2013 by radio42, Hamburg, Germany\r\nhttp://www.bass.radio42.com/";
            mes += "\r\n\r\nDatabase Engine:\r\nPowered by System.Data.SQLite ver." + sqlitever.FileVersion + "\r\nhttp://system.data.sqlite.org/";
            mes += "\r\n\r\nGrid Engine:\r\nPowered by SourceGrid ver." + sgver.FileVersion + "\r\nCopyright © 2009-2010 Davide Icardi, Darius Damalakas\r\nhttp://sourcegrid.codeplex.com/\rhttp://bitbucket.org/dariusdamalakas/sourcegrid";
            mes += "\r\n\r\nTagEdit Engine:\r\nPowered by TagLib# ver." + taglibsver.FileVersion + "\r\nCopyright © 2006-2013 Brian Nickel\r\nhttp://download.banshee-project.org/taglib-sharp/";
            mes += "\r\n\r\nArchive Engine:\r\nPowered by SevenZipSharp ver." + szsver.FileVersion + "\r\nCopyright © markhor\r\nhttp://sevenzipsharp.codeplex.com/";
            mes += "               \r\nPowered by 7-Zip ver." + szver.FileVersion + "\r\nCopyright © 1999-2010 Igor Pavlov.\r\nhttp://www.7-zip.org/";
            mes += "\r\n\r\nGoogle API:\r\nPowered by Gapi.NET ver." + gapiver.FileVersion + "\r\nhttp://gapidotnet.codeplex.com/";
            mes += "\r\n\r\nIcon Product by Copyright © 2004 SHIN-ICHI.\r\nhttp://surviveplus.net/";
            mes += "\r\nIcon Product by Copyright © Mark James\r\nhttp://www.famfamfam.com/";
            mes += "\r\nIcon Product by Copyright © 2010 Prax08. Some rights reserved.\r\nhttp://prax-08.deviantart.com/";
            mes += "\r\nIcon Product by Copyright © chrfb\r\nhttp://chrfb.deviantart.com";
            mes += "\r\nIcon Product by Copyright © Yusuke Kamiyamane\r\nhttp://p.yusukekamiyamane.com/";
            mes += "\r\nIcon Product by Copyright © 19eighty7\r\nhttp://www.19eighty7.com";
            mes += "\r\nIcon Product by Copyright © Jonas Rask\r\nhttp://jonasraskdesign.com/";
            mes += "\r\nIcon Product by Copyright © Laurent Baumann\r\nhttp://lbaumann.com/";
            mes += "\r\nIcon Product by Copyright © acidrums4\r\nhttp://acidrums4.deviantart.com/";
            mes += "\r\n\r\nWebService:\r\nPowered by Google\r\nhttp://www.google.com/\r\nPowered by Amazon Japan\r\nhttp://www.amazon.co.jp/";
            mes += "\r\n\r\nIncremental Search Engine by C/Migemo ver." + migemover.FileVersion +"\r\nCopyright © 2003-2007 MURAOKA Taro (KoRoN).\r\nhttp://code.google.com/p/cmigemo/";
            mes += "\r\n\r\nREST API by RestSharp ver." + restsharpver.FileVersion + "\r\nCopyright © RestSharp Project 2009-2012\r\nhttp://restsharp.org/";
            mes += "\r\n\r\n\r\nThank you All Developers & Users.";

            lblversion.Text = LinearGlobal.ApplicationVersion;
            txtMessage.Text = mes;
        }

        private void lblUrl_MouseHover(object sender, EventArgs e)
        {
            lblUrl.ForeColor = Color.DarkOrange;
            lblUrl.Font = new Font(this.Font, FontStyle.Underline);
        }

        private void lblUrl_MouseLeave(object sender, EventArgs e)
        {
            lblUrl.ForeColor = Color.OrangeRed;
            lblUrl.Font = new Font(this.Font, FontStyle.Regular);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblUrl_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(lblUrl.Text);
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblUpdateCheck.Text = "新しいバージョンがあるかチェックしています...";

            Thread t = new Thread(new ThreadStart(checkUpdate));
            t.IsBackground = true;
            t.Start();
            
        }

        void checkUpdate()
        {

            String message = "";
            Color messageColor = Color.FromArgb(0, 64, 64, 64);
            _isReleaseNewVersion = false;
            newFileVersion = "";
            try
            {

                WebResponse res = new WebManager().request("http://www.finalstream.net/dl/download.php?dl=lap");

                newFileVersion = Path.GetFileNameWithoutExtension(res.ResponseUri.ToString());
                newFileVersion = newFileVersion.Substring(newFileVersion.Length - 5, 5);

                string nowVersion = LinearGlobal.ApplicationVersion.Substring(LinearGlobal.ApplicationVersion.Length - 5, 5);

                if (newFileVersion.CompareTo(nowVersion) > 0)
                {
                    message = "新しいバージョン(ver." + newFileVersion + ")がリリースされています。";
                    messageColor = Color.Crimson;
                    _isReleaseNewVersion = true;
                }
                else if (newFileVersion.CompareTo(nowVersion) == 0)
                {
                    message = "最新バージョンのLinear Audio Playerを使用しています。";
                }
                else
                {
                    message = "開発中バージョンのLinear Audio Playerを使用しています。";
                    messageColor = Color.MediumBlue;
                }



            }
            catch
            {
                message = "最新バージョンチェックに失敗しました。";
            }

            this.BeginInvoke(new UpdateTextDelegate(updateText), new object[]
                                                                     {
                                                                         message, messageColor
                                                                     }
                );

            this.BeginInvoke(new ShowMessageDelegate(showMessage), null);


        }

        private void updateText(string message, Color color)
        {
            lblUpdateCheck.Text = message;
            lblUpdateCheck.ForeColor = color;
        }

        private void showMessage()
        {
            if (_isReleaseNewVersion)
            {
                if (MessageUtils.showQuestionMessage(this, "最新バージョンが見つかりました。\nいますぐver." + newFileVersion + "にアップデートしますか？\nアップデート後、再起動します。") == DialogResult.OK)
                {
                    LinearGlobal.IsUpdateNewVersion = true;
                    //アプリケーションを終了する
                    Application.Exit();
                }
            }
        }
    }
}