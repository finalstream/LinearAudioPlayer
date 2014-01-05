namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    partial class ArtworkViewForm
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
            this.SuspendLayout();
            // 
            // ArtworkViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ArtworkViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ArtworkView";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.ArtworkViewForm_Activated);
            this.Deactivate += new System.EventHandler(this.ArtworkViewForm_Deactivate);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ArtworkViewForm_Paint);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ArtworkViewForm_MouseWheel);
            this.ResumeLayout(false);

        }

        #endregion
    }
}