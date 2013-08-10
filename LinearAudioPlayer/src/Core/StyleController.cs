
using FINALSTREAM.LinearAudioPlayer.Setting;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    public class StyleController
    {

        /// <summary>
        /// カラープロファイルを設定する。
        /// </summary>
        public void setColorProfile(ColorConfig colorConfig)
        {
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
            LinearGlobal.MainForm.loadStyle(styleConfig);
            LinearGlobal.MainForm.ListForm.loadStyle(styleConfig);
            //LinearGlobal.MainForm.ListForm.InterruptForm.loadStyle(styleConfig);
            LinearAudioPlayer.GridController.loadStyle();
            LinearAudioPlayer.FilteringGridController.loadStyle();
            LinearAudioPlayer.GroupGridController.loadStyle();
            LinearAudioPlayer.LinkGridController.loadStyle();
        }

    }
}
