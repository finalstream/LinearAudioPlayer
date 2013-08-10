using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FINALSTREAM.LinearAudioPlayer.Setting;

namespace FINALSTREAM.LinearAudioPlayer.GUI {
    public partial class InterruptForm : Form
    {
        private ListBox _interruptListBox;

        public InterruptForm()
        {
            InitializeComponent();

            _interruptListBox = this.InterruptList;
        }

        private void InterruptForm_Resize(object sender, EventArgs e)
        {
            //loadStyle();

            int yposition = 5;

            if (this.InterruptList.BorderStyle == BorderStyle.None)
            {
                yposition += 5;
            }

            this.InterruptList.Location = new Point(5, yposition);
            this.InterruptList.Size = new Size(this.Size.Width - 10, this.Size.Height - yposition);
            //this.btnClear.Size = new Size(this.InterruptList.Width, this.btnClear.Size.Height);
            //this.btnClear.Location = new Point(this.InterruptList.Location.X, this.InterruptList.Location.Y + this.InterruptList.Size.Height+4);

        }

        public void loadStyle(StyleConfig styleConfig)
        {
            this.InterruptList.BorderStyle = styleConfig.DisplayBorderStyle;

            this.InterruptForm_Resize(null, null);
            this.Refresh();
        }

        public void setColorProfile(ColorConfig colorConfig)
        {
            InterruptList.ForeColor = LinearGlobal.ColorConfig.FontColor;
            InterruptList.BackColor = LinearGlobal.ColorConfig.DisplayBackgroundColor;

            this.BackColor = LinearGlobal.ColorConfig.FormBackgroundColor;
        }

        public ListBox InterruptListBox
        {
            get
            {
                return _interruptListBox;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.InterruptList.Items.Clear();
        }

        /// <summary>
        /// 削除をクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InterruptList.SelectedIndex > -1) 
            {
                InterruptList.Items.RemoveAt(InterruptList.SelectedIndex);
            }
            
        }

        /// <summary>
        /// クリアをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.InterruptList.Items.Clear();
        }

        private int rowIndexFromMouseDown;
        private Rectangle dragBoxFromMouseDown;
        private void InterruptList_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = InterruptList.IndexFromPoint(new Point(e.X, e.Y));
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred.
                // The DragSize indicates the size that the mouse can move
                // before a drag event should be started.               
                Size dragSize = SystemInformation.DragSize;
                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                e.Y - (dragSize.Height / 2)),
                dragSize);

            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void InterruptList_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != null && dragBoxFromMouseDown != Rectangle.Empty &&
                !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                   
                    DragDropEffects dropEffect = InterruptList.DoDragDrop(
                    InterruptList.Items[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void InterruptList_DragDrop(object sender, DragEventArgs e)
        {
            // グリッド内のD&Dなら並び替えをする
            // The mouse locations are relative to the screen, so they must be
            // converted to client coordinates.
            Point clientPoint = InterruptList.PointToClient(new Point(e.X, e.Y));
            // Get the row index of the item the mouse is below.
            int rowIndexOfItemUnderMouseToDrop =
            InterruptList.IndexFromPoint(new Point(clientPoint.X, clientPoint.Y));
            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move && rowIndexOfItemUnderMouseToDrop != -1)
            {
                int i = 0;
                int k = 0;
                object work;
                if (rowIndexFromMouseDown < rowIndexOfItemUnderMouseToDrop)
                {
                    while (i < InterruptList.Items.Count)
                    {
                        if (InterruptList.GetSelected(i) == true)
                        {
                            work = InterruptList.Items[i - k];
                            InterruptList.Items[i - k] = InterruptList.Items[rowIndexOfItemUnderMouseToDrop];
                            InterruptList.Items[rowIndexOfItemUnderMouseToDrop] = work;
                            k++;
                        }
                        i++;
                    }
                }
                else
                {
                    i = InterruptList.Items.Count - 1;
                    k = 0;
                    while (i > 0)
                    {
                        if (InterruptList.GetSelected(i) == true)
                        {
                            work = InterruptList.Items[i + k];
                            InterruptList.Items[i + k] = InterruptList.Items[rowIndexOfItemUnderMouseToDrop];
                            InterruptList.Items[rowIndexOfItemUnderMouseToDrop] = work;
                            k++;
                        }
                        i--;
                    }
                }
            }
        }

        private void InterruptList_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int penWidth = 1;
            Pen outSideBottomLeftPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.OutSideBottomLeftLineColor), penWidth);
            Pen inSideBottomLeftPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.InSideBottomLeftLineColor), penWidth);
            Pen outSideUnderRightPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.OutSideUnderRightLineColor), penWidth);
            Pen inSideUnderRightPen = new Pen(Color.FromArgb(LinearGlobal.StyleConfig.InSideUnderRightLineColor), penWidth);

            // 立体感を出す

            // 左ライン
            e.Graphics.DrawLine(outSideBottomLeftPen,
               0,
               0,
               0,
               this.Size.Height);
            e.Graphics.DrawLine(inSideBottomLeftPen,
               1,
               0,
               1,
               this.Size.Height);

            // 上ライン
            e.Graphics.DrawLine(outSideBottomLeftPen,
               0,
               0,
               this.Size.Width,
               0);
            e.Graphics.DrawLine(inSideBottomLeftPen,
               0,
               1,
               this.Size.Width,
               1);

            // 右ライン
            e.Graphics.DrawLine(outSideUnderRightPen,
               this.Size.Width - 1,
                0,
                this.Size.Width - 1,
                this.Size.Height);
            e.Graphics.DrawLine(inSideUnderRightPen,
              this.Size.Width - 2,
               0,
               this.Size.Width - 2,
               this.Size.Height);

            // 下ライン
            e.Graphics.DrawLine(outSideUnderRightPen,
                0,
                this.Size.Height - 1,
                this.Size.Width,
                this.Size.Height - 1);
            e.Graphics.DrawLine(inSideUnderRightPen,
                0,
                this.Size.Height - 2,
                this.Size.Width,
                this.Size.Height - 2);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

    }
}
