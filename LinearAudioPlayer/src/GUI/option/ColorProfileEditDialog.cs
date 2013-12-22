using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using FINALSTREAM.Commons.Info;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Setting;

namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    public partial class ColorProfileEditDialog : Form
    {
        public bool isCustomize { get; set; }
        private bool isSupportAlpha = false;
        private ColorInfo colorInfo = new ColorInfo();
        private int keepColor;

        public ColorProfileEditDialog(string basename)
        {
            InitializeComponent();
            txtBaseName.Text = basename;
            txtColorProfileName.Text = basename + "Customize";
        }

        private void ColorProfileEditDialog_Load(object sender, EventArgs e)
        {
            this.Location = LinearGlobal.LinearConfig.ViewConfig.ColorProfileEditDialogLocation;
            LinearGlobal.ColorConfig = LinearAudioPlayer.SettingManager.LoadColorConfig(LinearGlobal.LinearConfig.ViewConfig.ColorProfile);
            colorPropertyGrid.SelectedObject = LinearGlobal.ColorConfig;
        }

        private void colorPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
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
                if (isSupportAlpha)
                {
                    setColor(Color.FromArgb((int)nudAlpha.Value, colorPreviewBox.BackColor.R, colorPreviewBox.BackColor.G, colorPreviewBox.BackColor.B));
                }
                else
                {
                    setColor(colorPreviewBox.BackColor);
                }
                
            }
        }

        private void setColor(Color color)
        {
            PropertyInfo pi = typeof(ColorConfig).GetProperty(colorPropertyGrid.SelectedGridItem.Label);
            pi.SetValue(LinearGlobal.ColorConfig, color, null);
            colorPropertyGrid.Refresh();
            colorPropertyGrid_PropertyValueChanged(null, null);
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            if (isSupportAlpha)
            {

                if (!checkHoldAlpha.Checked)
                {
                    nudAlpha.Value = (byte)random.Next(0, 255);
                }

                if (!checkHoldColor.Checked)
                {
                    colorInfo.Color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                }

                colorPreviewBox.BackColor = Color.FromArgb((int) nudAlpha.Value, colorInfo.Color.R, colorInfo.Color.G,
                                                           colorInfo.Color.B);
            }
            else
            {
                colorPreviewBox.BackColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            }

            setColor(colorPreviewBox.BackColor);
        }

        private void colorPropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            

            if ("SelectRowColor".Equals(colorPropertyGrid.SelectedGridItem.Label))
            {
                isSupportAlpha = true;
                checkHoldAlpha.Enabled = true;
                nudAlpha.Enabled = true;
                checkHoldColor.Enabled = true;
            }
            else
            {
                isSupportAlpha = false;
                checkHoldAlpha.Enabled = false;
                nudAlpha.Enabled = false;
                checkHoldColor.Enabled = false;
            }

            if (colorPropertyGrid.SelectedGridItem.Value is Color)
            {
                colorPreviewBox.Enabled = true;
                Color color = (Color)colorPropertyGrid.SelectedGridItem.Value;

                
                colorPreviewBox.BackColor = color;
                if (isSupportAlpha)
                {
                    nudAlpha.Value = color.A;
                }
            }
            else
            {
                colorPreviewBox.Enabled = false;
            }
        }

        private void nudAlpha_ValueChanged(object sender, EventArgs e)
        {
            setColor(Color.FromArgb((int)nudAlpha.Value, colorPreviewBox.BackColor.R, colorPreviewBox.BackColor.G, colorPreviewBox.BackColor.B));
        }

        private void ColorProfileEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            LinearGlobal.LinearConfig.ViewConfig.ColorProfileEditDialogLocation = this.Location;
            LinearGlobal.LinearConfig.ViewConfig.ColorProfile = "AutoSaveColor.xml";
            SettingManager sm = new SettingManager();
            sm.SaveColorConfig(LinearGlobal.LinearConfig.ViewConfig.ColorProfile);
            ((ConfigForm) this.Owner).setEditColorProfile();
        }

        private void txtColorProfileName_TextChanged(object sender, EventArgs e)
        {
            if (txtColorProfileName.Text.Length == 0)
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
            SettingManager sm = new SettingManager();
            sm.SaveColorConfig(txtColorProfileName.Text + ".xml");
            LinearGlobal.LinearConfig.ViewConfig.ColorProfile = txtColorProfileName.Text + ".xml";
            ((ConfigForm)this.Owner).setEditColorProfile();
        }

        private void btnKeep_Click(object sender, EventArgs e)
        {
            keepColor = colorPreviewBox.BackColor.ToArgb();
            keepColorBox.BackColor = Color.FromArgb(keepColor);

        }

        private void keepColorBox_Click(object sender, EventArgs e)
        {
            colorPreviewBox.BackColor = Color.FromArgb(keepColor);
            if (isSupportAlpha)
            {
                setColor(Color.FromArgb((int)nudAlpha.Value, colorPreviewBox.BackColor.R, colorPreviewBox.BackColor.G, colorPreviewBox.BackColor.B));
            }
            else
            {
                setColor(colorPreviewBox.BackColor);
            }
        }


    }
}
