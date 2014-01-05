using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    public partial class ArtworkViewForm : Form
    {

        private Image artworkImage;
        private Size artworkSize;
        public ArtworkViewForm(Image img)
        {
            InitializeComponent();
            setImage(img);
        }

        private void ArtworkViewForm_Paint(object sender, PaintEventArgs e)
        {
                //DrawImageメソッドで画像を座標(0, 0)の位置に表示する
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            
            e.Graphics.DrawImage(artworkImage,
                    0, 0, artworkSize.Width, artworkSize.Height);
        }

        private void ArtworkViewForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ArtworkViewForm_MouseWheel(object sender, MouseEventArgs e)
        {
            float delta = (e.Delta/120);
            float add = delta/10;

            artworkSize.Width = (int) (artworkSize.Width * (1.0f + add));
            artworkSize.Height = (int)(artworkSize.Height * (1.0f + add));

            this.Width = artworkSize.Width;
            this.Height = artworkSize.Height;
            this.Refresh();

        }

        public void setImage(Image img)
        {
            artworkImage = img;
            artworkSize.Width = img.Width;
            artworkSize.Height = img.Height;
            this.CenterToScreen();
        }

        [SecurityPermission(SecurityAction.Demand,
    Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 1;
            const int HTCAPTION = 2;

            //マウスポインタがクライアント領域内にあるか
            if ((m.Msg == WM_NCHITTEST) &&
                (m.Result.ToInt32() == HTCLIENT))
            {
                //マウスがタイトルバーにあるふりをする
                m.Result = (IntPtr)HTCAPTION;
            }
        }

        private void ArtworkViewForm_Activated(object sender, EventArgs e)
        {
            this.Width = artworkSize.Width;
            this.Height = artworkSize.Height;
        }
    }
}
