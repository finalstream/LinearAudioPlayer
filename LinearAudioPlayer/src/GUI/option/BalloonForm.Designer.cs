namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    partial class BalloonForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.numFilteringCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numFilteringCount)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(75, 42);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(44, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // numFilteringCount
            // 
            this.numFilteringCount.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numFilteringCount.Location = new System.Drawing.Point(14, 45);
            this.numFilteringCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFilteringCount.Name = "numFilteringCount";
            this.numFilteringCount.Size = new System.Drawing.Size(55, 19);
            this.numFilteringCount.TabIndex = 8;
            this.numFilteringCount.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Limit件数(0:無限)";
            // 
            // BalloonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(142, 72);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numFilteringCount);
            this.Controls.Add(this.buttonOK);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "BalloonForm";
            this.ShowIcon = false;
            this.Text = "BalloonForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BalloonForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numFilteringCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.NumericUpDown numFilteringCount;
        private System.Windows.Forms.Label label1;
    }
}