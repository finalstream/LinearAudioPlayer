﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FINALSTREAM.Commons.Archive;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Resources;
using FINALSTREAM.LinearAudioPlayer.Setting;

namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            setStyleList();

            setColorProfileList();

            restoreConfig(LinearGlobal.LinearConfig);

            
        }

        /// <summary>
        /// カラープロファイルリストを設定
        /// </summary>
        private void setColorProfileList()
        {
            colorProfileList.Items.Clear();
            IList<string> resultList = FileUtils.getFilePathListWithExtFilter(
                new string[] { Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME }, 
                SearchOption.AllDirectories,
                new string[]{ ".xml" });

            colorProfileList.Items.Add(LinearConst.DEFAULT_STYLE + ".xml");
            foreach (string colorProfilePath in resultList)
            {
                string colorProfileName = Path.GetFileName(colorProfilePath);
                if (!colorProfileList.Items.Contains(colorProfileName) && !"AutoSaveColor.xml".Equals(colorProfileName))
                {
                    colorProfileList.Items.Add(
                        Path.GetFileName(colorProfileName));
                }
            }
            if (File.Exists(Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME + "AutoSaveColor.xml"))
            {
                colorProfileList.Items.Add("AutoSaveColor.xml");
            }

        }

        /// <summary>
        /// スタイルリストを設定
        /// </summary>
        private void setStyleList()
        {
            styleList.Items.Clear();
            IList<string> resultList = FileUtils.getFilePathListWithExtFilter(
                new string[] { Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME },
                SearchOption.AllDirectories,
                new string[] { ".xml" });


            styleList.Items.Add(LinearConst.DEFAULT_STYLE);
            foreach (string styleDir in Directory.GetDirectories(Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME))
            {
                string stylename = new DirectoryInfo(styleDir).Name;
                if (!styleList.Items.Contains(stylename) && !"AutoSaveStyle".Equals(stylename))
                {
                    styleList.Items.Add(stylename);
                }
            }
            if (Directory.Exists(Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME + "AutoSaveStyle") )
            {
                styleList.Items.Add("AutoSaveStyle");
            }

        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// 設定を復元
        /// </summary>
        public void restoreConfig(LinearConfig linearConfig)
        {
            // 設定画面の位置およびサイズ復元
            this.Location = linearConfig.ViewConfig.ConfigLocation;
            //this.Size = linearConfig.ViewConfig.ConfigSize;
            this.Size = new Size(408, 405);

            this.checkResumePlay.Checked = linearConfig.PlayerConfig.ResumePlay;
            this.checkAutoUpdate.Checked = linearConfig.PlayerConfig.IsAutoUpdate;
            this.txtExclusionKeywords.Text = String.Join(",", linearConfig.PlayerConfig.ExclusionKeywords);

            this.txtTitleTemplete.Text = linearConfig.ViewConfig.TitleTemplete;
            this.checkTitleCentering.Checked = linearConfig.ViewConfig.isTitleCentering;
            this.checkNotificationWindow.Checked = linearConfig.ViewConfig.isNotificationWindow;
            this.checkSlidePIP.Checked = linearConfig.ViewConfig.isSlidePIP;
            this.checkRandomStyle.Checked = linearConfig.PlayerConfig.isRandomStyleSelect;
            this.checkGetNetworkArtwork.Checked = linearConfig.ViewConfig.isGetNetworkArtwork;
            this.checkAlbumAutoRename.Checked = linearConfig.PlayerConfig.isAlbumAutoRename;
            this.checkSoundNormalize.Checked = linearConfig.SoundConfig.IsVolumeNormalize;
            this.txtAlbumAutoRenameTemplete.Text = linearConfig.PlayerConfig.albumAutoRenameTemplete;
            this.txtAlbumAutoRenameTemplete.Enabled = this.checkAlbumAutoRename.Checked;

            this.numudPIPViewDuration.Value = (decimal) linearConfig.ViewConfig.PIPViewDuration;
            this.numudPlayCountUpRatio.Value = linearConfig.PlayerConfig.PlayCountUpRatio;

            // WEBUI設定
            this.checkWEBUIEnable.Checked = linearConfig.ViewConfig.UseWebInterface;
            this.numWEBUIPort.Value = linearConfig.ViewConfig.WebInterfaceListenPort;
            numWEBUIPort.Enabled = checkWEBUIEnable.Checked;
            labelOpenWEBUI.Enabled = checkWEBUIEnable.Checked;
            comboWebUITheme.Enabled = checkWEBUIEnable.Checked;

            comboWebUITheme.Items.Clear();
            var filePaths = FileUtils.getFilePathList(
                Application.StartupPath + LinearConst.WEB_DIRECTORY_NAME + "ui", SearchOption.TopDirectoryOnly);
            foreach (string path in filePaths)
            {
                comboWebUITheme.Items.Add(
                Path.GetFileNameWithoutExtension(path));
            }
            comboWebUITheme.SelectedIndex =
                comboWebUITheme.FindStringExact(linearConfig.ViewConfig.WebInterfaceTheme);

            // フェード持続時間
            this.numudFadeDuration.Value =
                (decimal) linearConfig.SoundConfig.FadeDuration;

            this.tbMiniVisualLevel.Value = linearConfig.ViewConfig.MiniVisualizationLevel;
            

            // 再生方式
            if (linearConfig.EngineConfig.PlayEngine == LinearEnum.PlayEngine.FMOD)
            {
                this.radioFMOD.Checked = true;
            }
            else
            {
                this.radioBASS.Checked = true;
            }

            this.checkFontBold.Checked = linearConfig.ViewConfig.FontBold;

            // タイトルスクロールモード
            if (linearConfig.ViewConfig.TitleScrollMode == LinearEnum.TitleScrollMode.LOOP)
            {
                this.radioTitleScrollLoop.Checked = true;
            }
            else if (linearConfig.ViewConfig.TitleScrollMode == LinearEnum.TitleScrollMode.REFLECT)
            {
                this.radioTitleScrollReflect.Checked = true;
            }
            else if (linearConfig.ViewConfig.TitleScrollMode == LinearEnum.TitleScrollMode.ROLL)
            {
                this.radioTitleScrollRoll.Checked = true;
            }
            else
            {
                this.radioTitleScrollNone.Checked = true;
            }

            // カラープロファイル選択
            colorProfileList.SelectedIndex =
                colorProfileList.FindStringExact(
                LinearGlobal.LinearConfig.ViewConfig.ColorProfile);

            // スタイル選択
            if (checkRandomStyle.Checked)
            {
                styleList.SelectedIndex = new Random().Next(0, styleList.Items.Count - 1);
            }
            else
            {
                styleList.SelectedIndex = styleList.FindStringExact(LinearGlobal.LinearConfig.ViewConfig.StyleName);
            }

            this.txtTempDirectory.Text = LinearGlobal.LinearConfig.PlayerConfig.TempDirectory;

            // プラグインリスト表示
            List<string> excludePlugins = new List<string>(linearConfig.PlayerConfig.ExcludePlugins);
            foreach (var plugin in LinearGlobal.Plugins)
            {
                ListViewItem lvi = new ListViewItem(plugin.Name);
                lvi.SubItems.Add(plugin.Version);
                lvi.SubItems.Add(plugin.Author);
                if (!excludePlugins.Contains(plugin.Name))
                {
                    lvi.Checked = true;
                }
                listPlugin.Items.Add(lvi);
            }
            
            // オーディオファイル自動登録設定
            this.textMoniterDir.Text = linearConfig.PlayerConfig.AudioFileAutoRegistInfo.MonitoringDirectory;
            this.textStorageDir.Text = linearConfig.PlayerConfig.AudioFileAutoRegistInfo.StorageDirectory;

            IList<string> dbList =
                FileUtils.getFilePathListWithExtFilter(
                new string[] { LinearGlobal.DatabaseDirectory },
                SearchOption.AllDirectories,
                new string[] { ".db" });

            comboTargetDatabase.Items.Clear();
            comboTargetDatabase.Items.Add("");
            foreach (string dbfile in dbList)
            {
                    comboTargetDatabase.Items.Add(
                    Path.GetFileNameWithoutExtension(dbfile));
            }
            comboTargetDatabase.SelectedIndex =
                comboTargetDatabase.FindStringExact(linearConfig.PlayerConfig.AudioFileAutoRegistInfo.TargetDatabase);

            this.checkAutoRegist.Checked = linearConfig.PlayerConfig.AudioFileAutoRegistInfo.IsEnable;

            this.numMiniVisualLineCount.Value = linearConfig.ViewConfig.MiniVisualizationLineCount;
        }

        public void backupConfig(LinearConfig linearConfig)
        {
            ConfigForm configForm = LinearGlobal.MainForm.ConfigForm;

            // フェード持続時間
            linearConfig.SoundConfig.FadeDuration =
                (float) configForm.numudFadeDuration.Value;

        }


        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // フォーカスをリセット
            tabOption.Focus();
        }

        private void numudFadeDuration_ValueChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.SoundConfig.FadeDuration =
                (float) this.numudFadeDuration.Value;
        }




        private void radioFMOD_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.EngineConfig.PlayEngine = LinearEnum.PlayEngine.FMOD;
            // 説明文を表示
            lblPlayEngineComment.Text = "Firelight Technologiesのオーディオ再生エンジンです。高音質かつ標準で対応フォーマットの数が多いです。";
        }

        private void radioBASS_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.EngineConfig.PlayEngine = LinearEnum.PlayEngine.BASS;
            // 説明文を表示
            lblPlayEngineComment.Text = "Un4seen Developmentsのオーディオ再生エンジンです。高音質かつm4a(aac)再生可能です。プラグインで拡張できます。";
        }

        private void colorProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeColorProfile(colorProfileList.Text);
            colorProfileList.Focus();
        }

        private void changeColorProfile(string colorProfile)
        {
            if (!String.IsNullOrEmpty(colorProfile))
            {
                LinearGlobal.LinearConfig.ViewConfig.ColorProfile = colorProfile;
                if (LinearGlobal.isCompleteStartup)
                {

                    ColorConfig cc =
                        LinearAudioPlayer.SettingManager.LoadColorConfig(
                            LinearGlobal.LinearConfig.ViewConfig.ColorProfile);
                    LinearAudioPlayer.StyleController.setColorProfile(cc);
                    //LinearGlobal.MainForm.ListForm.reloadDatabase(true);
                    LinearGlobal.MainForm.ListForm.ReloadAllGrid();

                    LinearAudioPlayer.StyleController.loadStyle(LinearGlobal.StyleConfig);
                }
            }
        }

        private void checkResumePlay_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.ResumePlay = checkResumePlay.Checked;
        }

        private void txtExclusionKeywords_Leave(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.ExclusionKeywords = txtExclusionKeywords.Text.Split(',');
        }

        private void btnReStart_Click(object sender, EventArgs e)
        {
            Application.Exit();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

        private void txtTitleTemplete_Leave(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.TitleTemplete = txtTitleTemplete.Text;
        }

        private void checkTitleCentering_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.isTitleCentering = checkTitleCentering.Checked;
            if (LinearGlobal.MainForm != null)
            {
                LinearGlobal.MainForm.setTitleCentering();
            }
        }

        private void radioTitleScrollLoop_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.TitleDisplayScroll = true;
            LinearGlobal.LinearConfig.ViewConfig.TitleScrollMode = LinearEnum.TitleScrollMode.LOOP;
        }

        private void radioTitleScrollReflect_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.TitleDisplayScroll = true;
            LinearGlobal.LinearConfig.ViewConfig.TitleScrollMode = LinearEnum.TitleScrollMode.REFLECT;
        }

        private void radioTitleScrollRoll_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.TitleDisplayScroll = true;
            LinearGlobal.LinearConfig.ViewConfig.TitleScrollMode = LinearEnum.TitleScrollMode.ROLL;
            if (LinearGlobal.MainForm != null)
            {
                LinearGlobal.MainForm.setTitleCentering();
            }
        }


        private void radioTitleScrollNone_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.TitleDisplayScroll = false;
            LinearGlobal.LinearConfig.ViewConfig.TitleScrollMode = LinearEnum.TitleScrollMode.NONE;
            if (LinearGlobal.MainForm != null)
            {
                LinearGlobal.MainForm.setTitleCentering();
            }
        }

        private void checkNotificationWindow_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.isNotificationWindow = checkNotificationWindow.Checked;
        }

        private void numudPIPViewDuration_ValueChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.PIPViewDuration = (float) numudPIPViewDuration.Value;
        }

        private void checkSlidePIP_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.isSlidePIP = checkSlidePIP.Checked;
        }

        private void tbMiniVisualLevel_ValueChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.MiniVisualizationLevel =
                this.tbMiniVisualLevel.Value;
        }

        private void btnSelectTempDir_Click(object sender, EventArgs e)
        {
            txtTempDirectory.Text = DirectoryUtils.showFolderDialog();
            LinearGlobal.LinearConfig.PlayerConfig.TempDirectory = txtTempDirectory.Text;
        }

        private void txtTempDirectory_Leave(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.TempDirectory = txtTempDirectory.Text;
        }

        private void styleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeStyle(styleList.Text);
            styleList.Focus();
        }

        private void changeStyle(string style)
        {
            LinearGlobal.LinearConfig.ViewConfig.StyleName = style;
            StyleConfig styleConfig =
                        LinearAudioPlayer.SettingManager.LoadStyleConfig(
                             LinearGlobal.LinearConfig.ViewConfig.StyleName);

            labelStyleName.Text = styleConfig.Name;
            labelStyleInfo.Text = styleConfig.Version + "\n"
                                          + styleConfig.Comment + "\n"
                                          + "Design by " + styleConfig.Designer;


            if (LinearGlobal.isCompleteStartup)
            {
                LinearGlobal.StyleDirectory =
                Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME + LinearGlobal.LinearConfig.ViewConfig.StyleName + "\\";
                StyleConfig sc =
                    LinearAudioPlayer.SettingManager.LoadStyleConfig(LinearGlobal.LinearConfig.ViewConfig.StyleName);
                LinearGlobal.StyleConfig = sc;
                colorProfileList.SelectedIndex = colorProfileList.FindStringExact(styleConfig.DefaultColorProfile);
                LinearAudioPlayer.StyleController.loadStyle(sc);
                //return;
            }

            colorProfileList.SelectedIndex = -1;
            if (checkRandomStyle.Checked)
            {
                colorProfileList.SelectedIndex = colorProfileList.FindStringExact(styleConfig.DefaultColorProfile);
            }
            else
            {
                colorProfileList.SelectedIndex =
                    colorProfileList.FindStringExact(LinearGlobal.LinearConfig.ViewConfig.ColorProfile);
            }
        }

        private void btnImportStyle_Click(object sender, EventArgs e)
        {
            string filePath = FileUtils.showFileDialog("Linearスタイルアーカイブファイル(zip)を選択してください。",
                                                       "Linearスタイルアーカイブファイル(*.zip)|*.zip");
　　　　
            if (filePath != "" && !Path.GetFileName(filePath).Substring(0,8).Equals("LINEARS-") )
            {
                MessageUtils.showMessage(MessageBoxIcon.Exclamation, MessageResource.W0003);
                return;
            }

            if (filePath != "")
            {
                //スタイルをインストール
                installStyle(filePath);
                
                setStyleList();
                setColorProfileList();

                // カラープロファイル選択
                colorProfileList.SelectedIndex =
                    colorProfileList.FindStringExact(
                    LinearGlobal.LinearConfig.ViewConfig.ColorProfile);

                // スタイル選択
                styleList.SelectedIndex = styleList.FindStringExact(LinearGlobal.LinearConfig.ViewConfig.StyleName);
            }
            
        }

        public void setEditColorProfile()
        {
            setColorProfileList();

            // カラープロファイル選択
            colorProfileList.SelectedIndex =
                colorProfileList.FindStringExact(
                LinearGlobal.LinearConfig.ViewConfig.ColorProfile);
        }

        public void setEditStyle()
        {
            setStyleList();

            // スタイル選択
            styleList.SelectedIndex = styleList.FindStringExact(LinearGlobal.LinearConfig.ViewConfig.StyleName);
        }

        /// <summary>
        /// インポートスタイル
        /// </summary>
        /// <param name="filePath"></param>
        private void installStyle(string filePath)
        {
            string tempDir = Application.StartupPath + "\\temp";
            // アップデート用フォルダ作成
            Directory.CreateDirectory(tempDir);

            // 解凍
            SevenZipManager.Instance.extract(filePath, null, tempDir);

            // インストール
            if (
                Directory.Exists(tempDir + "\\" + Path.GetFileNameWithoutExtension(filePath) +
                                 LinearConst.STYLE_DIRECTORY_NAME))
            {
                FileUtils.allcopy(
                    tempDir + "\\" + Path.GetFileNameWithoutExtension(filePath) + LinearConst.STYLE_DIRECTORY_NAME,
                    Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME);
            }
            if (
                Directory.Exists(tempDir + "\\" + Path.GetFileNameWithoutExtension(filePath) + LinearConst.COLOR_DIRECTORY_NAME))
            {
                FileUtils.allcopy(
                    tempDir + "\\" + Path.GetFileNameWithoutExtension(filePath) + LinearConst.COLOR_DIRECTORY_NAME,
                    Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME);
            }
            

            // テンポラリ削除
            DirectoryUtils.deleteDir(tempDir);

        }

        private void ConfigForm_Activated(object sender, EventArgs e)
        {
            if (LinearAudioPlayer.PlayController != null &&  LinearAudioPlayer.PlayController.isEnableWasapi())
            {
                labelWasapiStatus.Text = "Enabled";
                labelWasapiStatus.ForeColor = Color.Black;
            }
            else
            {
                labelWasapiStatus.Text = "Disabled";
                labelWasapiStatus.ForeColor = Color.Gray;
            }
        }

        private void checkFontBold_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.FontBold = this.checkFontBold.Checked;
        }

        private void styleList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isStyleFocus = true;
                styleList.SelectedItem = styleList.Items[styleList.IndexFromPoint(e.Location)];
            }
        }

        private void 削除ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (styleList.Focused && !String.IsNullOrEmpty(styleList.Text))
                {
                    if (LinearConst.DEFAULT_STYLE.Equals(styleList.Text))
                    {
                        MessageBox.Show("デフォルトスタイルは削除できません");
                        contextMenuStrip1.Close();
                        return;
                    }

                    if (MessageUtils.showQuestionMessage(String.Format(MessageResource.Q0006, styleList.Text)) == DialogResult.OK)
                    {
                        changeStyle(LinearConst.DEFAULT_STYLE);
                        DirectoryUtils.moveRecycleBin(
                            Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME + styleList.Text);
                        setStyleList();
                    }
                    
                }
                else if (colorProfileList.Focused && !String.IsNullOrEmpty(colorProfileList.Text))
                {
                    if ((LinearConst.DEFAULT_STYLE+".xml").Equals(colorProfileList.Text))
                    {
                        MessageBox.Show("デフォルトカラープロファイルは削除できません");
                        contextMenuStrip1.Close();
                        return;
                    }

                    if (MessageUtils.showQuestionMessage(String.Format(MessageResource.Q0007, colorProfileList.Text)) == DialogResult.OK)
                    {
                        changeColorProfile(LinearConst.DEFAULT_STYLE + ".xml");
                        FileUtils.moveRecycleBin(
                            Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME + colorProfileList.Text);
                        setColorProfileList();
                    }
                }
            }
            contextMenuStrip1.Close();
        }

        private bool isStyleFocus = false;
        private void colorProfileList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isStyleFocus = false;
                colorProfileList.SelectedItem = colorProfileList.Items[colorProfileList.IndexFromPoint(e.Location)];
            }
        }

        private void checkRandomStyle_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.isRandomStyleSelect = checkRandomStyle.Checked;
        }

        private void checkGetNetworkArtwork_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.isGetNetworkArtwork = checkGetNetworkArtwork.Checked;
        }

        private void checkAlbumAutoRename_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.isAlbumAutoRename = checkAlbumAutoRename.Checked;
            txtAlbumAutoRenameTemplete.Enabled = checkAlbumAutoRename.Checked;
        }

        private void txtAlbumAutoRenameTemplete_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAlbumAutoRenameTemplete.Text))
            {
                txtAlbumAutoRenameTemplete.Text = LinearConst.DEFAULT_ALBUM_RENAME_TEMPLETE;
            }
            LinearGlobal.LinearConfig.PlayerConfig.albumAutoRenameTemplete = txtAlbumAutoRenameTemplete.Text;
        }

        private void listPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPlugin.SelectedIndices.Count > 0)
            {
                lblPluginDescription.Text = LinearGlobal.Plugins[listPlugin.SelectedIndices[0]].Description;
            }
        }

        private void listPlugin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked == listPlugin.Items[e.Item.Index].Checked)
            {
                
            }
        }

        private void listPlugin_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newBool= false;
            if (e.NewValue == CheckState.Checked)
            {
                newBool = true;
            }

            LinearGlobal.Plugins[e.Index].Enable = newBool;
            if (newBool)
            {
                bool result = LinearGlobal.Plugins[e.Index].Init();
                if (!result)
                {
                    e.NewValue = CheckState.Unchecked;
                }
                else
                {
                    // 除外から削除する
                    List<string> el = new List<string>(LinearGlobal.LinearConfig.PlayerConfig.ExcludePlugins);
                    el.Remove(LinearGlobal.Plugins[e.Index].Name);
                    LinearGlobal.LinearConfig.PlayerConfig.ExcludePlugins = el.ToArray();
                }
            } else
            {
                LinearGlobal.Plugins[e.Index].Disabled();
                // 除外に追加する
                List<string> el = new List<string>(LinearGlobal.LinearConfig.PlayerConfig.ExcludePlugins);
                if (!el.Contains(LinearGlobal.Plugins[e.Index].Name))
                {
                    el.Add(LinearGlobal.Plugins[e.Index].Name);
                }
                LinearGlobal.LinearConfig.PlayerConfig.ExcludePlugins = el.ToArray();
            }
        }

        private void numudPlayCountUpRatio_ValueChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.PlayCountUpRatio = (int)numudPlayCountUpRatio.Value;
        }

        private void checkAutoRegist_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAutoRegist.Checked 
                && (String.IsNullOrEmpty(textMoniterDir.Text) || String.IsNullOrEmpty(textStorageDir.Text) || String.IsNullOrEmpty(comboTargetDatabase.Text)))
            {
                // 両方とも入力されていなければエラー
                MessageUtils.showMessage(MessageBoxIcon.Warning, MessageResource.W0004);
                checkAutoRegist.Checked = false;
            }

            LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.IsEnable = checkAutoRegist.Checked;

        }

        private void textMoniterDir_Leave(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.MonitoringDirectory = textMoniterDir.Text;
        }

        private void textStorageDir_Leave(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.StorageDirectory = textStorageDir.Text;
        }

        private void btnMoniterDir_Click(object sender, EventArgs e)
        {
            textMoniterDir.Text = DirectoryUtils.showFolderDialog();
            LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.MonitoringDirectory = textMoniterDir.Text;
        }

        private void btnStorageDir_Click(object sender, EventArgs e)
        {
            textStorageDir.Text = DirectoryUtils.showFolderDialog();
            LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.StorageDirectory= textStorageDir.Text;
        }

        private void comboTargetDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo.TargetDatabase = comboTargetDatabase.Text;
        }

        private void checkSoundNormalize_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.SoundConfig.IsVolumeNormalize = checkSoundNormalize.Checked;
            if (LinearAudioPlayer.PlayController != null)
            {
                LinearAudioPlayer.PlayController.applyNormalize(checkSoundNormalize.Checked);
            }
        }

        private void checkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.PlayerConfig.IsAutoUpdate = checkAutoUpdate.Checked;
        }

        private void btnExecAudioFileRegist_Click(object sender, EventArgs e)
        {

            if (!LinearAudioPlayer.PlayController.executeAutoAudioFileRegist())
            {
                LinearGlobal.MainForm.ListForm.showToastMessage(MessageResource.I0009);
            }

        }

        private void editToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editStyle(e, false);
            }
        }

        private void copyToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editStyle(e, true);
            }
        }

        private void editStyle(MouseEventArgs e,bool isCustomize)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (isStyleFocus && !String.IsNullOrEmpty(styleList.Text))
                {
                    StyleEditDialog sed
                        = new StyleEditDialog(styleList.Text, isCustomize);
                    sed.Show(this);
                }
                else if (!isStyleFocus && !String.IsNullOrEmpty(colorProfileList.Text))
                {
                    ColorProfileEditDialog ced
                        = new ColorProfileEditDialog(Path.GetFileNameWithoutExtension(colorProfileList.Text), isCustomize);
                    ced.Show(this);
                }
            }
            contextMenuStrip1.Close();

        }

        private void buttonPackStyle_Click(object sender, EventArgs e)
        {
            StyleConfig styleConfig =
                        LinearAudioPlayer.SettingManager.LoadStyleConfig(
                             LinearGlobal.LinearConfig.ViewConfig.StyleName);

            string packFileName = "LINEARS-" + styleList.Text + styleConfig.Version;
            string packDir = Application.StartupPath + "\\stylepackage";
            DirectoryUtils.createDir(packDir);
            string packFilePath = packDir + "\\" + packFileName + ".zip";

            Dictionary<string, string> archiveDict = new Dictionary<string, string>();

            string archiveIncludePath = packFileName;
            // style 
            archiveIncludePath += "\\style\\" + styleList.Text + "\\";

            string styleDir = Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                              styleList.Text;

            foreach (string filePath in Directory.GetFiles(styleDir))
            {
                archiveDict.Add(archiveIncludePath + Path.GetFileName(filePath), filePath);
            }

            // color

            archiveIncludePath = packFileName;
            archiveIncludePath += "\\color\\";

            string colorProfileFile = Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME +
                                      colorProfileList.Text;

            archiveDict.Add(archiveIncludePath + Path.GetFileName(colorProfileFile), colorProfileFile);

            SevenZipManager.Instance.compress(packFilePath, archiveDict);

            MessageUtils.showMessage(MessageBoxIcon.Information, "スタイルのパッケージを作成しました。\n" + packFilePath);

        }

        private void checkWEBUIEnable_CheckedChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.UseWebInterface = checkWEBUIEnable.Checked;
            numWEBUIPort.Enabled = checkWEBUIEnable.Checked;
            labelOpenWEBUI.Enabled = checkWEBUIEnable.Checked;
            comboWebUITheme.Enabled = checkWEBUIEnable.Checked;
        }

        private void numWEBUIPort_ValueChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.WebInterfaceListenPort =
                (int) this.numWEBUIPort.Value;
        }

        private void labelOpenWEBUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                System.Diagnostics.Process.Start("http://localhost:" + numWEBUIPort.Value);
            }
        }

        private void comboWebUITheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.WebInterfaceTheme = comboWebUITheme.Text;
        }

        private void linkLabelStyleDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.finalstream.net/linearsg/");
        }

        private void numMiniVisualLineCount_ValueChanged(object sender, EventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.MiniVisualizationLineCount = (int)numMiniVisualLineCount.Value;
            changeStyle(styleList.Text);
        }


    }
}
