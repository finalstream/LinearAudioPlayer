namespace ColorProfileGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.colorPreviewBox = new System.Windows.Forms.PictureBox();
            this.colorValue = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.randomButton = new System.Windows.Forms.Button();
            this.checkHoldAlpha = new System.Windows.Forms.CheckBox();
            this.checkHoldColor = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.colorPreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(12, 12);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(308, 152);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // colorPreviewBox
            // 
            this.colorPreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPreviewBox.Location = new System.Drawing.Point(12, 193);
            this.colorPreviewBox.Name = "colorPreviewBox";
            this.colorPreviewBox.Size = new System.Drawing.Size(25, 24);
            this.colorPreviewBox.TabIndex = 1;
            this.colorPreviewBox.TabStop = false;
            // 
            // colorValue
            // 
            this.colorValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorValue.Location = new System.Drawing.Point(52, 195);
            this.colorValue.Name = "colorValue";
            this.colorValue.Size = new System.Drawing.Size(124, 19);
            this.colorValue.TabIndex = 2;
            this.colorValue.TextChanged += new System.EventHandler(this.colorValue_TextChanged);
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(261, 193);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(59, 23);
            this.copyButton.TabIndex = 3;
            this.copyButton.Text = "copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(196, 194);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(59, 23);
            this.randomButton.TabIndex = 4;
            this.randomButton.Text = "random";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // checkHoldAlpha
            // 
            this.checkHoldAlpha.AutoSize = true;
            this.checkHoldAlpha.Location = new System.Drawing.Point(12, 170);
            this.checkHoldAlpha.Name = "checkHoldAlpha";
            this.checkHoldAlpha.Size = new System.Drawing.Size(86, 16);
            this.checkHoldAlpha.TabIndex = 5;
            this.checkHoldAlpha.Text = "Alphaを固定";
            this.checkHoldAlpha.UseVisualStyleBackColor = true;
            // 
            // checkHoldColor
            // 
            this.checkHoldColor.AutoSize = true;
            this.checkHoldColor.Location = new System.Drawing.Point(104, 170);
            this.checkHoldColor.Name = "checkHoldColor";
            this.checkHoldColor.Size = new System.Drawing.Size(84, 16);
            this.checkHoldColor.TabIndex = 6;
            this.checkHoldColor.Text = "Colorを固定";
            this.checkHoldColor.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 229);
            this.Controls.Add(this.checkHoldColor);
            this.Controls.Add(this.checkHoldAlpha);
            this.Controls.Add(this.randomButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.colorValue);
            this.Controls.Add(this.colorPreviewBox);
            this.Controls.Add(this.propertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Linear ColorProfile Generator ver.0.0.2";
            ((System.ComponentModel.ISupportInitialize)(this.colorPreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.PictureBox colorPreviewBox;
        private System.Windows.Forms.TextBox colorValue;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.CheckBox checkHoldAlpha;
        private System.Windows.Forms.CheckBox checkHoldColor;

    }
}

