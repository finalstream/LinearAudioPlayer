using System.Windows.Forms;
namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    partial class InterruptForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.InterruptList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // InterruptList
            // 
            this.InterruptList.AllowDrop = true;
            this.InterruptList.BackColor = System.Drawing.Color.Black;
            this.InterruptList.ContextMenuStrip = this.contextMenuStrip;
            this.InterruptList.ForeColor = System.Drawing.Color.DarkGray;
            this.InterruptList.FormattingEnabled = true;
            this.InterruptList.ItemHeight = 12;
            this.InterruptList.Location = new System.Drawing.Point(4, 12);
            this.InterruptList.Name = "InterruptList";
            this.InterruptList.Size = new System.Drawing.Size(190, 160);
            this.InterruptList.TabIndex = 0;
            this.InterruptList.DragDrop += new System.Windows.Forms.DragEventHandler(this.InterruptList_DragDrop);
            this.InterruptList.DragOver += new System.Windows.Forms.DragEventHandler(this.InterruptList_DragOver);
            this.InterruptList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InterruptList_MouseDown);
            this.InterruptList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InterruptList_MouseMove);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 54);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "削除";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearToolStripMenuItem.Text = "クリア";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // InterruptForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(201, 168);
            this.Controls.Add(this.InterruptList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InterruptForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "InterruptForm";
            this.Resize += new System.EventHandler(this.InterruptForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private ListBox InterruptList;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem clearToolStripMenuItem;

        #endregion
    }
}