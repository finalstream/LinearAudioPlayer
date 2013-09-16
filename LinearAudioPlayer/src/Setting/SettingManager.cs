using System;
using System.Collections.Generic;
using System.Text;
using FINALSTREAM.Commons.Controls;
using FINALSTREAM.Commons.Parser;
using FINALSTREAM.Commons.Utils;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using FINALSTREAM.LinearAudioPlayer.Info;
using FINALSTREAM.LinearAudioPlayer.Core;

namespace FINALSTREAM.LinearAudioPlayer.Setting
{
    class SettingManager
    {

        /*
            パブリックメソッド
        */
        #region Public Method

        /// <summary>
        /// 設定をXMLに保存する
        /// </summary>
        /// <returns></returns>
        public bool SaveSetting()
        {

            LinearConfig linearConfig = getLinearConfigValue();

            new XmlSerializer().save(linearConfig, typeof(LinearConfig), Application.StartupPath + LinearConst.SETTING_FILE);

            // TODO: カラーテスト
            //ColorConfigXml cc = new ColorConfigXml();
            //XmlUtils.save(cc, typeof(ColorConfigXml), Application.StartupPath + "\\color/test.xml");

            return true;
        }

        /// <summary>
        /// XMLから設定をロードする
        /// </summary>
        /// <returns>成功したか</returns>
        public bool LoadSetting()
        {
            bool result = false;

            // アプリケーションの設定を読み込む
            if (File.Exists(Application.StartupPath + LinearConst.SETTING_FILE))
            {
                object obj = new XmlSerializer().load(typeof(LinearConfig), Application.StartupPath + LinearConst.SETTING_FILE);
                
                if (obj != null) {
                    LinearGlobal.LinearConfig = (LinearConfig) obj;
                } else {
                    LinearGlobal.LinearConfig = new LinearConfig();
                }

            }
            else
            {
                LinearGlobal.LinearConfig = new LinearConfig();
            }

            // カラー設定を読み込む
            LinearGlobal.ColorConfig = LoadColorConfig(LinearGlobal.LinearConfig.ViewConfig.ColorProfile);

            // スタイル設定を読み込む
            if (!Directory.Exists(Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                      LinearGlobal.LinearConfig.ViewConfig.StyleName))
            {
                LinearGlobal.LinearConfig.ViewConfig.StyleName = LinearConst.DEFAULT_STYLE;
            }
            LinearGlobal.StyleConfig = LoadStyleConfig(LinearGlobal.LinearConfig.ViewConfig.StyleName);

            result = true;

            return result;
        }


        public ColorConfig LoadColorConfig(string colorProfileName)
        {
            string colorprofilePath = Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME +
                                      colorProfileName;

            if (!File.Exists(colorprofilePath))
            {
                colorProfileName = LinearConst.DEFAULT_STYLE + ".xml";
                colorprofilePath = Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME +
                                      colorProfileName;
                LinearGlobal.LinearConfig.ViewConfig.ColorProfile = colorProfileName;
            }

            if (File.Exists(colorprofilePath))
            {
                object obj = new XmlSerializer().load(typeof(ColorConfigXml), colorprofilePath);

                if (obj != null)
                {
                    return restoreColorConfig((ColorConfigXml)obj);
                }
                else
                {
                    return restoreColorConfig(new ColorConfigXml());
                }

            }
            else
            {
                return restoreColorConfig(new ColorConfigXml());
            }
        }

        public StyleConfig LoadStyleConfig(string styleName)
        {
            
            
            string styleInfoPath = Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                      styleName + "\\styleinfo.xml";

            if (!File.Exists(styleInfoPath))
            {
                styleName = LinearConst.DEFAULT_STYLE;
                styleInfoPath = Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                      styleName + "\\styleinfo.xml";
                LinearGlobal.LinearConfig.ViewConfig.StyleName = LinearConst.DEFAULT_STYLE;
            }

            if (File.Exists(styleInfoPath))
            {
                object obj = new XmlSerializer().load(typeof(StyleConfig), styleInfoPath);

                if (obj != null)
                {
                    return (StyleConfig)obj;
                }
                else
                {
                    return new StyleConfig();
                }

            }
            else
            {
                return new StyleConfig();
            }
        }

        /// <summary>
        /// カラー設定をXMLに保存する
        /// </summary>
        /// <returns></returns>
        public bool SaveColorConfig(string colorProfileName)
        {

            ColorConfigXml ccxml = convertColorConfig(LinearGlobal.ColorConfig);
            new XmlSerializer().save(ccxml, typeof(ColorConfigXml), Application.StartupPath + LinearConst.COLOR_DIRECTORY_NAME +
                                      colorProfileName);

            // TODO: カラーテスト
            //ColorConfigXml cc = new ColorConfigXml();
            //XmlUtils.save(cc, typeof(ColorConfigXml), Application.StartupPath + "\\color/test.xml");

            return true;
        }

        /// <summary>
        /// スタイル設定をXMLに保存する
        /// </summary>
        /// <returns></returns>
        public bool SaveStyleConfig(string styleName)
        {
            LinearGlobal.StyleConfig.Name = styleName;
            new XmlSerializer().save(LinearGlobal.StyleConfig, typeof(StyleConfig), Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                      styleName + "\\styleinfo.xml");

            return true;
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 設定を取得する。(保存用)
        /// </summary>
        /// <returns></returns>
        private LinearConfig getLinearConfigValue()
        {
            LinearConfig setting = new LinearConfig();

            setting.Version = LinearGlobal.ApplicationVersion;

            // 表示設定
            setting.ViewConfig.MainLocation = LinearGlobal.MainForm.Location;
            setting.ViewConfig.MainSize = new Size(LinearGlobal.MainForm.Size.Width, LinearGlobal.MainForm.Size.Height);
            setting.ViewConfig.ListLocation = LinearGlobal.MainForm.ListForm.Location;
            setting.ViewConfig.ListSize = LinearGlobal.MainForm.ListForm.Size;
            setting.ViewConfig.ConfigLocation = LinearGlobal.MainForm.ConfigForm.Location;
            setting.ViewConfig.ConfigSize = LinearGlobal.MainForm.ConfigForm.Size;
            setting.ViewConfig.TitleScroll = LinearGlobal.TitleDisplayScroll;
            setting.ViewConfig.TopMost = LinearGlobal.MainForm.TopMost;
            setting.ViewConfig.Opacity = LinearGlobal.MainForm.Opacity;
            setting.ViewConfig.TagEditDialogLocation = LinearGlobal.LinearConfig.ViewConfig.TagEditDialogLocation;
            setting.ViewConfig.ColorProfileEditDialogLocation =
                LinearGlobal.LinearConfig.ViewConfig.ColorProfileEditDialogLocation;
            setting.ViewConfig.StyleEditDialogLocation =
                LinearGlobal.LinearConfig.ViewConfig.StyleEditDialogLocation;
            

            // Gridの幅を取得
            int i = 0;
            while (i < LinearGlobal.MainForm.ListForm.Grid.ColumnsCount)
            {
                setting.ViewConfig.ColumnHeaderWidth[i] =
                    LinearGlobal.MainForm.ListForm.Grid.Columns.GetWidth(i);
                i++;
            }
            setting.ViewConfig.ColorProfile = LinearGlobal.LinearConfig.ViewConfig.ColorProfile;
            setting.ViewConfig.StyleName = LinearGlobal.LinearConfig.ViewConfig.StyleName;

            setting.ViewConfig.TitleTemplete = LinearGlobal.LinearConfig.ViewConfig.TitleTemplete;
            setting.ViewConfig.isTitleCentering = LinearGlobal.LinearConfig.ViewConfig.isTitleCentering;
            setting.ViewConfig.TitleScrollMode = LinearGlobal.LinearConfig.ViewConfig.TitleScrollMode;
            setting.ViewConfig.isNotificationWindow = LinearGlobal.LinearConfig.ViewConfig.isNotificationWindow;
            setting.ViewConfig.isSlidePIP = LinearGlobal.LinearConfig.ViewConfig.isSlidePIP;
            setting.ViewConfig.isGetNetworkArtwork = LinearGlobal.LinearConfig.ViewConfig.isGetNetworkArtwork;

            setting.ViewConfig.MiniVisualizationLevel = LinearGlobal.LinearConfig.ViewConfig.MiniVisualizationLevel;
            setting.ViewConfig.PIPViewDuration = LinearGlobal.LinearConfig.ViewConfig.PIPViewDuration;
            setting.ViewConfig.FontBold = LinearGlobal.LinearConfig.ViewConfig.FontBold;
            setting.ViewConfig.GroupListOrder = LinearGlobal.LinearConfig.ViewConfig.GroupListOrder;
            setting.ViewConfig.AlbumJudgeCount = LinearGlobal.LinearConfig.ViewConfig.AlbumJudgeCount;

            // プレイヤー設定
            setting.PlayerConfig.PlayMode = (int)LinearGlobal.PlayMode;
            setting.PlayerConfig.PlaylistMode = (int)LinearGlobal.PlaylistMode;
            setting.PlayerConfig.FilteringMode = (int)LinearGlobal.FilteringMode;
            setting.PlayerConfig.ShuffleMode = (int)LinearGlobal.ShuffleMode;
            // 選択データベース
            setting.PlayerConfig.SelectDatabase =
                LinearGlobal.MainForm.ListForm.DatabaseList.Text;
            setting.PlayerConfig.SelectFilter =
                LinearGlobal.LinearConfig.PlayerConfig.SelectFilter;
            setting.PlayerConfig.GroupMode =
                LinearGlobal.LinearConfig.PlayerConfig.GroupMode;
            // 対応拡張子
            setting.PlayerConfig.SupportAudioExtension =
                LinearGlobal.LinearConfig.PlayerConfig.SupportAudioExtension;
            setting.PlayerConfig.ResumeId = LinearGlobal.CurrentPlayItemInfo.Id;
            setting.PlayerConfig.ResumePosition = LinearGlobal.LinearConfig.PlayerConfig.ResumePosition;
            setting.PlayerConfig.ResumePlay = LinearGlobal.LinearConfig.PlayerConfig.ResumePlay;
            setting.PlayerConfig.IsAutoUpdate = LinearGlobal.LinearConfig.PlayerConfig.IsAutoUpdate;
            // 除外キーワード
            setting.PlayerConfig.ExclusionKeywords = LinearGlobal.LinearConfig.PlayerConfig.ExclusionKeywords;
            setting.PlayerConfig.TempDirectory = LinearGlobal.LinearConfig.PlayerConfig.TempDirectory;
            setting.PlayerConfig.isRandomStyleSelect = LinearGlobal.LinearConfig.PlayerConfig.isRandomStyleSelect;
            setting.PlayerConfig.isAlbumAutoRename = LinearGlobal.LinearConfig.PlayerConfig.isAlbumAutoRename;
            setting.PlayerConfig.albumAutoRenameTemplete =
                LinearGlobal.LinearConfig.PlayerConfig.albumAutoRenameTemplete;
            setting.PlayerConfig.ExcludePlugins = LinearGlobal.LinearConfig.PlayerConfig.ExcludePlugins;
            setting.PlayerConfig.IsOpenPlaylist = LinearGlobal.LinearConfig.PlayerConfig.IsOpenPlaylist;
            setting.PlayerConfig.PlayCountUpRatio = LinearGlobal.LinearConfig.PlayerConfig.PlayCountUpRatio;
            setting.PlayerConfig.SortMode = (int)LinearGlobal.SortMode;
            setting.PlayerConfig.IsLinkLibrary = LinearGlobal.LinearConfig.PlayerConfig.IsLinkLibrary;
            setting.PlayerConfig.RestCount = LinearGlobal.LinearConfig.PlayerConfig.RestCount;
            setting.PlayerConfig.RestMaxCount = LinearGlobal.LinearConfig.PlayerConfig.RestMaxCount;
            setting.PlayerConfig.AudioFileAutoRegistInfo =
                LinearGlobal.LinearConfig.PlayerConfig.AudioFileAutoRegistInfo;

            // サウンド設定
            // サイレントボリューム前の値を復元する
            if (LinearGlobal.MainForm.ListForm.TmpVolume != -1)
            {
                LinearGlobal.Volume = LinearGlobal.MainForm.ListForm.TmpVolume;
            }
            setting.SoundConfig.Volume = LinearGlobal.Volume;
            setting.SoundConfig.SilentVolume =
                LinearGlobal.LinearConfig.SoundConfig.SilentVolume;
            setting.SoundConfig.FadeEffect =
                LinearGlobal.LinearConfig.SoundConfig.FadeEffect;
            setting.SoundConfig.FadeDuration =
                LinearGlobal.LinearConfig.SoundConfig.FadeDuration;
            setting.SoundConfig.IsVolumeNormalize =
                LinearGlobal.LinearConfig.SoundConfig.IsVolumeNormalize;

            setting.EngineConfig.PlayEngine =
                LinearGlobal.LinearConfig.EngineConfig.PlayEngine;

            setting.DatabaseConfig.LimitCount = LinearGlobal.LinearConfig.DatabaseConfig.LimitCount;

            /* 設定フォーム */
            



            return setting;
        }

        /// <summary>
        /// カラーコンフィグを復元
        /// </summary>
        /// <param name="ccxml">カラーコンフィグXML</param>
        /// <returns></returns>
        private ColorConfig restoreColorConfig(ColorConfigXml ccxml)
        {
            ColorConfig cc = new ColorConfig();

            cc.FormBackgroundColor = Color.FromArgb(ccxml.FormBackgroundColor);
            cc.DisplayBackgroundColor = Color.FromArgb(ccxml.DisplayBackgroundColor);
            cc.DisplayBorderColor = Color.FromArgb(ccxml.DisplayBorderColor);
            cc.FirstRowBackgroundColor = Color.FromArgb(ccxml.FirstRowBackgroundColor);
            cc.SecondRowBackgroundColor = Color.FromArgb(ccxml.SecondRowBackgroundColor);
            cc.FontColor = Color.FromArgb(ccxml.FontColor);
            cc.PlayingColor = Color.FromArgb(ccxml.PlayingColor);
            cc.SelectRowColor = Color.FromArgb(ccxml.SelectRowColor);
            cc.NoPlayColor = Color.FromArgb(ccxml.NoPlayColor);
            //cc.BitRateColor = Color.FromArgb(ccxml.BitRateColor);
            cc.PlayTimeColor = Color.FromArgb(ccxml.PlayTimeColor);
            cc.PlayModeColor = Color.FromArgb(ccxml.PlayModeColor);
            cc.HeaderBackgroundColor = Color.FromArgb(ccxml.HeaderBackgroundColor);
            cc.HeaderFontColor = Color.FromArgb(ccxml.HeaderFontColor);
            cc.PlaylistInfoColor = Color.FromArgb(ccxml.PlaylistInfoColor);
            cc.ProgressSeekBarMainBottomBackgroundColor = Color.FromArgb(ccxml.ProgressSeekBarMainBottomBackgroundColor);
            cc.ProgressSeekBarMainUnderBackgroundColor = Color.FromArgb(ccxml.ProgressSeekBarMainUnderBackgroundColor);
            cc.ProgressSeekBarUpBottomBackgroundColor = Color.FromArgb(ccxml.ProgressSeekBarUpBottomBackgroundColor);
            cc.ProgressSeekBarUpUnderBackgroundColor = Color.FromArgb(ccxml.ProgressSeekBarUpUnderBackgroundColor);
            cc.ProgressSeekBarMainBottomActiveColor = Color.FromArgb(ccxml.ProgressSeekBarMainBottomActiveColor);
            cc.ProgressSeekBarMainUnderActiveColor = Color.FromArgb(ccxml.ProgressSeekBarMainUnderActiveColor);
            cc.ProgressSeekBarUpBottomActiveColor = Color.FromArgb(ccxml.ProgressSeekBarUpBottomActiveColor);
            cc.ProgressSeekBarUpUnderActiveColor = Color.FromArgb(ccxml.ProgressSeekBarUpUnderActiveColor);
            cc.ProgressSeekBarTheme = ccxml.ProgressSeekBarTheme;
            cc.ProgressSeekBarBorderColor = Color.FromArgb(ccxml.ProgressSeekBarBorderColor);
            
            cc.MiniProgressSeekBarMainBottomBackgroundColor = Color.FromArgb(ccxml.MiniProgressSeekBarMainBottomBackgroundColor);
            cc.MiniProgressSeekBarMainUnderBackgroundColor = Color.FromArgb(ccxml.MiniProgressSeekBarMainUnderBackgroundColor);
            cc.MiniProgressSeekBarUpBottomBackgroundColor = Color.FromArgb(ccxml.MiniProgressSeekBarUpBottomBackgroundColor);
            cc.MiniProgressSeekBarUpUnderBackgroundColor = Color.FromArgb(ccxml.MiniProgressSeekBarUpUnderBackgroundColor);
            cc.MiniProgressSeekBarBorderColor = Color.FromArgb(ccxml.MiniProgressSeekBarBorderColor);
            cc.MiniProgressSeekBarMainBottomActiveColor = Color.FromArgb(ccxml.MiniProgressSeekBarMainBottomActiveColor);
            cc.MiniProgressSeekBarMainUnderActiveColor = Color.FromArgb(ccxml.MiniProgressSeekBarMainUnderActiveColor);
            cc.MiniProgressSeekBarUpBottomActiveColor = Color.FromArgb(ccxml.MiniProgressSeekBarUpBottomActiveColor);
            cc.MiniProgressSeekBarUpUnderActiveColor = Color.FromArgb(ccxml.MiniProgressSeekBarUpUnderActiveColor);
            cc.MiniProgressSeekBarTheme = ccxml.MiniProgressSeekBarTheme;

            cc.SpectrumLevelHightLevelColor = Color.FromArgb(ccxml.SpectrumLevelHightLevelColor);
            cc.SpectrumLevelLowLevelColor = Color.FromArgb(ccxml.SpectrumLevelLowLevelColor);

            cc.NotificationHeaderColor = Color.FromArgb(ccxml.NotificationHeaderColor);
            cc.NotficationFontColor = Color.FromArgb(ccxml.NotificationFontColor);
            cc.NotficationBodyFirstColor = Color.FromArgb(ccxml.NotificationBodyFirstColor);
            cc.NotficationBodySecondColor = Color.FromArgb(ccxml.NotificationBodySecondColor);

            return cc;
        }

        /// <summary>
        /// カラーコンフィグを変換
        /// </summary>
        /// <param name="ccxml">カラーコンフィグ</param>
        /// <returns></returns>
        private ColorConfigXml convertColorConfig(ColorConfig cc)
        {
            ColorConfigXml ccxml = new ColorConfigXml();

            ccxml.FormBackgroundColor = cc.FormBackgroundColor.ToArgb();
            ccxml.DisplayBackgroundColor = cc.DisplayBackgroundColor.ToArgb();
            ccxml.DisplayBorderColor = cc.DisplayBorderColor.ToArgb();
            ccxml.FirstRowBackgroundColor = cc.FirstRowBackgroundColor.ToArgb();
            ccxml.SecondRowBackgroundColor = cc.SecondRowBackgroundColor.ToArgb();
            ccxml.FontColor = cc.FontColor.ToArgb();
            ccxml.PlayingColor = cc.PlayingColor.ToArgb();
            ccxml.SelectRowColor = cc.SelectRowColor.ToArgb();
            ccxml.NoPlayColor = cc.NoPlayColor.ToArgb();
            //ccxml.BitRateColor = cc.BitRateColor.ToArgb();
            ccxml.PlayTimeColor = cc.PlayTimeColor.ToArgb();
            ccxml.PlayModeColor = cc.PlayModeColor.ToArgb();
            ccxml.HeaderBackgroundColor = cc.HeaderBackgroundColor.ToArgb();
            ccxml.HeaderFontColor = cc.HeaderFontColor.ToArgb();
            ccxml.PlaylistInfoColor = cc.PlaylistInfoColor.ToArgb();
            ccxml.ProgressSeekBarMainBottomBackgroundColor = cc.ProgressSeekBarMainBottomBackgroundColor.ToArgb();
            ccxml.ProgressSeekBarMainUnderBackgroundColor = cc.ProgressSeekBarMainUnderBackgroundColor.ToArgb();
            ccxml.ProgressSeekBarUpBottomBackgroundColor = cc.ProgressSeekBarUpBottomBackgroundColor.ToArgb();
            ccxml.ProgressSeekBarUpUnderBackgroundColor = cc.ProgressSeekBarUpUnderBackgroundColor.ToArgb();
            ccxml.ProgressSeekBarMainBottomActiveColor = cc.ProgressSeekBarMainBottomActiveColor.ToArgb();
            ccxml.ProgressSeekBarMainUnderActiveColor = cc.ProgressSeekBarMainUnderActiveColor.ToArgb();
            ccxml.ProgressSeekBarUpBottomActiveColor = cc.ProgressSeekBarUpBottomActiveColor.ToArgb();
            ccxml.ProgressSeekBarUpUnderActiveColor = cc.ProgressSeekBarUpUnderActiveColor.ToArgb();
            ccxml.ProgressSeekBarTheme = cc.ProgressSeekBarTheme;
            ccxml.ProgressSeekBarBorderColor = cc.ProgressSeekBarBorderColor.ToArgb();

            ccxml.MiniProgressSeekBarMainBottomBackgroundColor = cc.MiniProgressSeekBarMainBottomBackgroundColor.ToArgb();
            ccxml.MiniProgressSeekBarMainUnderBackgroundColor = cc.MiniProgressSeekBarMainUnderBackgroundColor.ToArgb();
            ccxml.MiniProgressSeekBarUpBottomBackgroundColor = cc.MiniProgressSeekBarUpBottomBackgroundColor.ToArgb();
            ccxml.MiniProgressSeekBarUpUnderBackgroundColor = cc.MiniProgressSeekBarUpUnderBackgroundColor.ToArgb();
            ccxml.MiniProgressSeekBarBorderColor = cc.MiniProgressSeekBarBorderColor.ToArgb();
            ccxml.MiniProgressSeekBarMainBottomActiveColor = cc.MiniProgressSeekBarMainBottomActiveColor.ToArgb();
            ccxml.MiniProgressSeekBarMainUnderActiveColor = cc.MiniProgressSeekBarMainUnderActiveColor.ToArgb();
            ccxml.MiniProgressSeekBarUpBottomActiveColor = cc.MiniProgressSeekBarUpBottomActiveColor.ToArgb();
            ccxml.MiniProgressSeekBarUpUnderActiveColor = cc.MiniProgressSeekBarUpUnderActiveColor.ToArgb();
            ccxml.MiniProgressSeekBarTheme = cc.MiniProgressSeekBarTheme;

            ccxml.SpectrumLevelHightLevelColor = cc.SpectrumLevelHightLevelColor.ToArgb();
            ccxml.SpectrumLevelLowLevelColor = cc.SpectrumLevelLowLevelColor.ToArgb();

            ccxml.NotificationHeaderColor = cc.NotificationHeaderColor.ToArgb();
            ccxml.NotificationFontColor = cc.NotficationFontColor.ToArgb();
            ccxml.NotificationBodyFirstColor = cc.NotficationBodyFirstColor.ToArgb();
            ccxml.NotificationBodySecondColor = cc.NotficationBodySecondColor.ToArgb();

            return ccxml;
        }

        public void restoreGlobalSetting()
        {
            // テンポラリディレクトリを設定する
            if (!Directory.Exists(LinearGlobal.LinearConfig.PlayerConfig.TempDirectory))
            {
                LinearGlobal.LinearConfig.PlayerConfig.TempDirectory = Path.GetTempPath();
            }
            LinearGlobal.TempDirectory =
                Path.Combine(LinearGlobal.LinearConfig.PlayerConfig.TempDirectory,  LinearConst.TEMP_DIRCTORY_NAME);
            DirectoryUtils.createDir(LinearGlobal.TempDirectory);

            // タイトルをスクロールするか
            LinearGlobal.TitleDisplayScroll = 
                LinearGlobal.LinearConfig.ViewConfig.TitleScroll;
            
            // 対応拡張子
            LinearGlobal.SupportAudioExtensionAry =
                LinearGlobal.LinearConfig.PlayerConfig.SupportAudioExtension.Split(',');

        }

        #endregion


    }
}
