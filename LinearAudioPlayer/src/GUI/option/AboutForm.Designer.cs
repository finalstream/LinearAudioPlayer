namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblversion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picLinearLogo = new System.Windows.Forms.PictureBox();
            this.picLinearIcon = new System.Windows.Forms.PictureBox();
            this.lblUpdateCheck = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLinearLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLinearIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.White;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMessage.Location = new System.Drawing.Point(12, 109);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(409, 66);
            this.txtMessage.TabIndex = 20;
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblversion.Location = new System.Drawing.Point(274, 27);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(45, 12);
            this.lblversion.TabIndex = 3;
            this.lblversion.Text = "ver.0.0.0";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCopyright.Location = new System.Drawing.Point(12, 60);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(317, 12);
            this.lblCopyright.TabIndex = 4;
            this.lblCopyright.Text = "Copyright © 2003-2014 FINALSTREAM. All Rights Reserved.";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblUrl.Location = new System.Drawing.Point(12, 84);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(146, 12);
            this.lblUrl.TabIndex = 5;
            this.lblUrl.Text = "http://www.finalstream.net/";
            this.lblUrl.Click += new System.EventHandler(this.lblUrl_Click);
            this.lblUrl.MouseLeave += new System.EventHandler(this.lblUrl_MouseLeave);
            this.lblUrl.MouseHover += new System.EventHandler(this.lblUrl_MouseHover);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(326, 185);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 21);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FINALSTREAM.LinearAudioPlayer.Properties.Resources.mediakun;
            this.pictureBox1.Location = new System.Drawing.Point(6, 189);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // picLinearLogo
            // 
            this.picLinearLogo.Image = global::FINALSTREAM.LinearAudioPlayer.Properties.Resources.linearlogo;
            this.picLinearLogo.Location = new System.Drawing.Point(41, 12);
            this.picLinearLogo.Name = "picLinearLogo";
            this.picLinearLogo.Size = new System.Drawing.Size(237, 35);
            this.picLinearLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLinearLogo.TabIndex = 2;
            this.picLinearLogo.TabStop = false;
            // 
            // picLinearIcon
            // 
            this.picLinearIcon.Image = global::FINALSTREAM.LinearAudioPlayer.Properties.Resources.lap;
            this.picLinearIcon.Location = new System.Drawing.Point(12, 12);
            this.picLinearIcon.Name = "picLinearIcon";
            this.picLinearIcon.Size = new System.Drawing.Size(32, 32);
            this.picLinearIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLinearIcon.TabIndex = 0;
            this.picLinearIcon.TabStop = false;
            // 
            // lblUpdateCheck
            // 
            this.lblUpdateCheck.AutoSize = true;
            this.lblUpdateCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUpdateCheck.Location = new System.Drawing.Point(28, 191);
            this.lblUpdateCheck.Name = "lblUpdateCheck";
            this.lblUpdateCheck.Size = new System.Drawing.Size(9, 12);
            this.lblUpdateCheck.TabIndex = 23;
            this.lblUpdateCheck.Text = " ";
            // 
            // AboutForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(433, 212);
            this.Controls.Add(this.lblUpdateCheck);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.picLinearLogo);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.picLinearIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Linear Audio Playerについて";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLinearLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLinearIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLinearIcon;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.PictureBox picLinearLogo;
        private System.Windows.Forms.Label lblversion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUpdateCheck;
    }
}