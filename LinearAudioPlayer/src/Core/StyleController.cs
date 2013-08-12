
using System.Drawing;
using FINALSTREAM.LinearAudioPlayer.Setting;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    public class StyleController
    {
        public Image NoPictureImage { get; set; }
        /// <summary>
        /// カラープロファイルを設定する。
        /// </summary>
        public void setColorProfile(ColorConfig colorConfig)
        {
            createNoPictureImage();
            LinearGlobal.MainForm.setColorProfile(colorConfig);
            LinearGlobal.MainForm.ListForm.setColorProfile(colorConfig);
            //LinearGlobal.MainForm.ListForm.InterruptForm.setColorProfile(colorConfig);
            LinearAudioPlayer.GridController.setColorProfile(colorConfig);
            LinearAudioPlayer.FilteringGridController.setColorProfile(colorConfig);
            LinearAudioPlayer.GroupGridController.setColorProfile(colorConfig);
            LinearAudioPlayer.LinkGridController.setColorProfile(colorConfig);

            

            LinearGlobal.ColorConfig = colorConfig;
        }

        public void loadStyle(StyleConfig styleConfig)
        {
            createNoPictureImage();
            LinearGlobal.MainForm.loadStyle(styleConfig);
            LinearGlobal.MainForm.ListForm.loadStyle(styleConfig);
            //LinearGlobal.MainForm.ListForm.InterruptForm.loadStyle(styleConfig);
            LinearAudioPlayer.GridController.loadStyle();
            LinearAudioPlayer.FilteringGridController.loadStyle();
            LinearAudioPlayer.GroupGridController.loadStyle();
            LinearAudioPlayer.LinkGridController.loadStyle();
        }

        /// <summary>
        /// アートワーク(NoPicture)を作成(スタイルの背景色とNoPiciture用イメージを合成)
        /// </summary>
        private void createNoPictureImage()
        {
            Image noPicture = Image.FromFile(LinearGlobal.StyleDirectory + "\\nocover.png");
            NoPictureImage = new Bitmap(noPicture.Width, noPicture.Height);
            using (var g = Graphics.FromImage(NoPictureImage))
            {
                g.Clear(LinearGlobal.ColorConfig.FormBackgroundColor);
                g.DrawImage(noPicture, 0, 0);
            }
        }

    }
}
