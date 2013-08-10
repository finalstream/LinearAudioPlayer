namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    partial class StyleEditDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StyleEditDialog));
            this.stylePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.colorPreviewBox = new System.Windows.Forms.PictureBox();
            this.txtStyleName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.colorDialogEx = new FINALSTREAM.Commons.Controls.ColorPicker.ColorManagement.ColorDialogEx();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.colorPreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // stylePropertyGrid
            // 
            this.stylePropertyGrid.Location = new System.Drawing.Point(12, 37);
            this.stylePropertyGrid.Name = "stylePropertyGrid";
            this.stylePropertyGrid.Size = new System.Drawing.Size(407, 296);
            this.stylePropertyGrid.TabIndex = 0;
            this.stylePropertyGrid.ToolbarVisible = false;
            this.stylePropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.stylePropertyGrid_PropertyValueChanged);
            this.stylePropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.stylePropertyGrid_SelectedGridItemChanged);
            // 
            // colorPreviewBox
            // 
            this.colorPreviewBox.BackColor = System.Drawing.Color.White;
            this.colorPreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPreviewBox.Location = new System.Drawing.Point(392, 8);
            this.colorPreviewBox.Name = "colorPreviewBox";
            this.colorPreviewBox.Size = new System.Drawing.Size(27, 24);
            this.colorPreviewBox.TabIndex = 2;
            this.colorPreviewBox.TabStop = false;
            this.colorPreviewBox.Click += new System.EventHandler(this.colorPreviewBox_Click);
            // 
            // txtStyleName
            // 
            this.txtStyleName.Location = new System.Drawing.Point(71, 10);
            this.txtStyleName.Name = "txtStyleName";
            this.txtStyleName.Size = new System.Drawing.Size(119, 19);
            this.txtStyleName.TabIndex = 10;
            this.txtStyleName.TextChanged += new System.EventHandler(this.txtStyleName_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(333, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(47, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "スタイル名";
            // 
            // txtBaseName
            // 
            this.txtBaseName.BackColor = System.Drawing.SystemColors.Control;
            this.txtBaseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBaseName.Location = new System.Drawing.Point(250, 12);
            this.txtBaseName.Name = "txtBaseName";
            this.txtBaseName.ReadOnly = true;
            this.txtBaseName.Size = new System.Drawing.Size(77, 12);
            this.txtBaseName.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "Based by";
            // 
            // StyleEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 345);
            this.Controls.Add(this.txtBaseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtStyleName);
            this.Controls.Add(this.colorPreviewBox);
            this.Controls.Add(this.stylePropertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StyleEditDialog";
            this.Text = "Linear Style Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StyleEditDialog_FormClosing);
            this.Load += new System.EventHandler(this.StyleEditDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.colorPreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid stylePropertyGrid;
        private System.Windows.Forms.PictureBox colorPreviewBox;
        private Commons.Controls.ColorPicker.ColorManagement.ColorDialogEx colorDialogEx;
        private System.Windows.Forms.TextBox txtStyleName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaseName;
        private System.Windows.Forms.Label label2;
    }
}