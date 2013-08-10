namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    partial class ColorProfileEditDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorProfileEditDialog));
            this.colorPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.colorPreviewBox = new System.Windows.Forms.PictureBox();
            this.randomButton = new System.Windows.Forms.Button();
            this.checkHoldColor = new System.Windows.Forms.CheckBox();
            this.checkHoldAlpha = new System.Windows.Forms.CheckBox();
            this.nudAlpha = new System.Windows.Forms.NumericUpDown();
            this.txtColorProfileName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnKeep = new System.Windows.Forms.Button();
            this.keepColorBox = new System.Windows.Forms.PictureBox();
            this.colorDialogEx = new FINALSTREAM.Commons.Controls.ColorPicker.ColorManagement.ColorDialogEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBaseName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.colorPreviewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keepColorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // colorPropertyGrid
            // 
            this.colorPropertyGrid.Location = new System.Drawing.Point(12, 37);
            this.colorPropertyGrid.Name = "colorPropertyGrid";
            this.colorPropertyGrid.Size = new System.Drawing.Size(407, 254);
            this.colorPropertyGrid.TabIndex = 0;
            this.colorPropertyGrid.ToolbarVisible = false;
            this.colorPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.colorPropertyGrid_PropertyValueChanged);
            this.colorPropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.colorPropertyGrid_SelectedGridItemChanged);
            // 
            // colorPreviewBox
            // 
            this.colorPreviewBox.BackColor = System.Drawing.Color.White;
            this.colorPreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPreviewBox.Location = new System.Drawing.Point(12, 309);
            this.colorPreviewBox.Name = "colorPreviewBox";
            this.colorPreviewBox.Size = new System.Drawing.Size(27, 24);
            this.colorPreviewBox.TabIndex = 2;
            this.colorPreviewBox.TabStop = false;
            this.colorPreviewBox.Click += new System.EventHandler(this.colorPreviewBox_Click);
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(269, 310);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(59, 23);
            this.randomButton.TabIndex = 5;
            this.randomButton.Text = "random";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // checkHoldColor
            // 
            this.checkHoldColor.AutoSize = true;
            this.checkHoldColor.Location = new System.Drawing.Point(45, 314);
            this.checkHoldColor.Name = "checkHoldColor";
            this.checkHoldColor.Size = new System.Drawing.Size(75, 16);
            this.checkHoldColor.TabIndex = 8;
            this.checkHoldColor.Text = "Color固定";
            this.checkHoldColor.UseVisualStyleBackColor = true;
            // 
            // checkHoldAlpha
            // 
            this.checkHoldAlpha.AutoSize = true;
            this.checkHoldAlpha.Location = new System.Drawing.Point(126, 314);
            this.checkHoldAlpha.Name = "checkHoldAlpha";
            this.checkHoldAlpha.Size = new System.Drawing.Size(77, 16);
            this.checkHoldAlpha.TabIndex = 7;
            this.checkHoldAlpha.Text = "Alpha固定";
            this.checkHoldAlpha.UseVisualStyleBackColor = true;
            // 
            // nudAlpha
            // 
            this.nudAlpha.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAlpha.Location = new System.Drawing.Point(209, 313);
            this.nudAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAlpha.Name = "nudAlpha";
            this.nudAlpha.Size = new System.Drawing.Size(48, 19);
            this.nudAlpha.TabIndex = 9;
            this.nudAlpha.ValueChanged += new System.EventHandler(this.nudAlpha_ValueChanged);
            // 
            // txtColorProfileName
            // 
            this.txtColorProfileName.Location = new System.Drawing.Point(114, 10);
            this.txtColorProfileName.Name = "txtColorProfileName";
            this.txtColorProfileName.Size = new System.Drawing.Size(107, 19);
            this.txtColorProfileName.TabIndex = 10;
            this.txtColorProfileName.TextChanged += new System.EventHandler(this.txtColorProfileName_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(369, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(47, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnKeep
            // 
            this.btnKeep.Location = new System.Drawing.Point(340, 310);
            this.btnKeep.Name = "btnKeep";
            this.btnKeep.Size = new System.Drawing.Size(46, 23);
            this.btnKeep.TabIndex = 12;
            this.btnKeep.Text = "keep";
            this.btnKeep.UseVisualStyleBackColor = true;
            this.btnKeep.Click += new System.EventHandler(this.btnKeep_Click);
            // 
            // keepColorBox
            // 
            this.keepColorBox.BackColor = System.Drawing.Color.White;
            this.keepColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.keepColorBox.Location = new System.Drawing.Point(392, 309);
            this.keepColorBox.Name = "keepColorBox";
            this.keepColorBox.Size = new System.Drawing.Size(27, 24);
            this.keepColorBox.TabIndex = 13;
            this.keepColorBox.TabStop = false;
            this.keepColorBox.Click += new System.EventHandler(this.keepColorBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "カラープロファイル名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "Based by";
            // 
            // txtBaseName
            // 
            this.txtBaseName.BackColor = System.Drawing.SystemColors.Control;
            this.txtBaseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBaseName.Location = new System.Drawing.Point(282, 13);
            this.txtBaseName.Name = "txtBaseName";
            this.txtBaseName.ReadOnly = true;
            this.txtBaseName.Size = new System.Drawing.Size(77, 12);
            this.txtBaseName.TabIndex = 16;
            // 
            // ColorProfileEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 345);
            this.Controls.Add(this.txtBaseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keepColorBox);
            this.Controls.Add(this.btnKeep);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtColorProfileName);
            this.Controls.Add(this.nudAlpha);
            this.Controls.Add(this.checkHoldColor);
            this.Controls.Add(this.checkHoldAlpha);
            this.Controls.Add(this.randomButton);
            this.Controls.Add(this.colorPreviewBox);
            this.Controls.Add(this.colorPropertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColorProfileEditDialog";
            this.Text = "Linear ColorProfile Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorProfileEditDialog_FormClosing);
            this.Load += new System.EventHandler(this.ColorProfileEditDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.colorPreviewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keepColorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid colorPropertyGrid;
        private System.Windows.Forms.PictureBox colorPreviewBox;
        private Commons.Controls.ColorPicker.ColorManagement.ColorDialogEx colorDialogEx;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.CheckBox checkHoldColor;
        private System.Windows.Forms.CheckBox checkHoldAlpha;
        private System.Windows.Forms.NumericUpDown nudAlpha;
        private System.Windows.Forms.TextBox txtColorProfileName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnKeep;
        private System.Windows.Forms.PictureBox keepColorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBaseName;
    }
}