using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Setting;

namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    public partial class StyleEditDialog : Form
    {

        public StyleEditDialog(string basename)
        {
            InitializeComponent();
            txtBaseName.Text = basename;
            txtStyleName.Text = basename + "Customize";
        }

        private void StyleEditDialog_Load(object sender, EventArgs e)
        {
            this.Location = LinearGlobal.LinearConfig.ViewConfig.StyleEditDialogLocation;
            LinearGlobal.StyleConfig = LinearAudioPlayer.SettingManager.LoadStyleConfig(LinearGlobal.LinearConfig.ViewConfig.StyleName);
            stylePropertyGrid.SelectedObject = LinearGlobal.StyleConfig;
        }

        private void stylePropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            LinearAudioPlayer.StyleController.setColorProfile(LinearGlobal.ColorConfig);
            LinearGlobal.MainForm.ListForm.ReloadAllGrid();
            LinearAudioPlayer.StyleController.loadStyle(LinearGlobal.StyleConfig);
        }

        private void colorPreviewBox_Click(object sender, EventArgs e)
        {
            colorDialogEx.Color = colorPreviewBox.BackColor;
            if (colorDialogEx.ShowDialog() == DialogResult.OK)
            {
                colorPreviewBox.BackColor = colorDialogEx.Color;

                setColor(colorPreviewBox.BackColor);
                
            }
        }

        private void setColor(Color color)
        {
            PropertyInfo pi = typeof(StyleConfig).GetProperty(stylePropertyGrid.SelectedGridItem.Label);
            pi.SetValue(LinearGlobal.StyleConfig, color, null);
            stylePropertyGrid.Refresh();
            stylePropertyGrid_PropertyValueChanged(null, null);
        }

        private void stylePropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {

            if (stylePropertyGrid.SelectedGridItem.Value is Color)
            {
                colorPreviewBox.Enabled = true;
                Color color = (Color)stylePropertyGrid.SelectedGridItem.Value;

                
                colorPreviewBox.BackColor = color;

            }
            else
            {
                colorPreviewBox.Enabled = false;
            }
        }


        private void StyleEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.StyleEditDialogLocation = this.Location;
            LinearGlobal.LinearConfig.ViewConfig.StyleName = "AutoSaveStyle";

            // Styleコピー
            string baseDir = Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                txtBaseName.Text + "\\";
            string newDir = Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                LinearGlobal.LinearConfig.ViewConfig.StyleName + "\\";
            if (File.Exists(newDir + "background_miniface.png"))
            {
                File.Delete(newDir + "background_miniface.png");
            }
            if (!txtBaseName.Text.Equals(LinearGlobal.LinearConfig.ViewConfig.StyleName))
            {
                FileUtils.allcopy(baseDir, newDir);
            }

            SettingManager sm = new SettingManager();
            sm.SaveStyleConfig(LinearGlobal.LinearConfig.ViewConfig.StyleName);
            ((ConfigForm) this.Owner).setEditStyle();
        }

        private void txtStyleName_TextChanged(object sender, EventArgs e)
        {
            if (txtStyleName.Text.Length == 0)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            // Styleコピー

            string baseDir = Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                txtBaseName.Text + "\\";
            string newDir = Application.StartupPath + LinearConst.STYLE_DIRECTORY_NAME +
                                txtStyleName.Text + "\\";
            if (!txtBaseName.Text.Equals(txtStyleName.Text))
            {
                FileUtils.allcopy(baseDir, newDir);
            }
            SettingManager sm = new SettingManager();
            sm.SaveStyleConfig(txtStyleName.Text);
            LinearGlobal.LinearConfig.ViewConfig.StyleName = txtStyleName.Text;
            ((ConfigForm)this.Owner).setEditStyle();
        }

    }
}
