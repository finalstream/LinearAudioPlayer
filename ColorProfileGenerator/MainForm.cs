using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorProfileGenerator
{
    public partial class MainForm : Form
    {

        public ColorInfo ColorInfo { set; get; }

        public MainForm()
        {
            InitializeComponent();

            ColorInfo = new ColorInfo();

            this.propertyGrid.SelectedObject = ColorInfo;

        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            colorPreviewBox.BackColor = Color.FromArgb(ColorInfo.Alpha, ColorInfo.Color);

            colorValue.Text = colorPreviewBox.BackColor.ToArgb().ToString();
        }

        private void randomButton_Click(object sender, System.EventArgs e)
        {
            Random random = new Random();

            if (!checkHoldAlpha.Checked)
            {
                ColorInfo.Alpha = (byte)random.Next(0, 255);
            }

            if (!checkHoldColor.Checked)
            {
                ColorInfo.Color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            }
            
            propertyGrid.Refresh();
            propertyGrid_PropertyValueChanged(null,null);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(colorValue.Text))
            {
                Clipboard.SetText(colorValue.Text);
            }
            
        }

        private void colorValue_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(colorValue.Text))
            {
                int colval;
                try
                {
                    colval = int.Parse(colorValue.Text);
                }
                catch (Exception)
                {
                    return;
                }
                Color color = Color.FromArgb(colval);

                ColorInfo.Alpha = color.A;
                ColorInfo.Color = color;

                propertyGrid.Refresh();
                propertyGrid_PropertyValueChanged(null, null);
            }
        }

    }
}
