namespace Converter_1._0
{
    partial class InfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.labelInfo = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.radioButtonWAV = new System.Windows.Forms.RadioButton();
            this.radioButtonAIFF = new System.Windows.Forms.RadioButton();
            this.pictureBoxFileStructure = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFileStructure)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(104, 128);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(106, 13);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "by Alex Kulagin 2020";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::Converter_1._0.Properties.Resources.Converter_WAV_AIFF_1_0;
            this.pictureBoxLogo.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(284, 112);
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            // 
            // radioButtonWAV
            // 
            this.radioButtonWAV.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonWAV.AutoSize = true;
            this.radioButtonWAV.Location = new System.Drawing.Point(57, 151);
            this.radioButtonWAV.Name = "radioButtonWAV";
            this.radioButtonWAV.Size = new System.Drawing.Size(201, 23);
            this.radioButtonWAV.TabIndex = 2;
            this.radioButtonWAV.TabStop = true;
            this.radioButtonWAV.Text = "Information about the structure of WAV";
            this.radioButtonWAV.UseVisualStyleBackColor = true;
            this.radioButtonWAV.CheckedChanged += new System.EventHandler(this.radioButtonWAV_CheckedChanged);
            // 
            // radioButtonAIFF
            // 
            this.radioButtonAIFF.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAIFF.AutoSize = true;
            this.radioButtonAIFF.Location = new System.Drawing.Point(59, 180);
            this.radioButtonAIFF.Name = "radioButtonAIFF";
            this.radioButtonAIFF.Size = new System.Drawing.Size(198, 23);
            this.radioButtonAIFF.TabIndex = 3;
            this.radioButtonAIFF.TabStop = true;
            this.radioButtonAIFF.Text = "Information about the structure of AIFF";
            this.radioButtonAIFF.UseVisualStyleBackColor = true;
            this.radioButtonAIFF.CheckedChanged += new System.EventHandler(this.radioButtonAIFF_CheckedChanged);
            // 
            // pictureBoxFileStructure
            // 
            this.pictureBoxFileStructure.Location = new System.Drawing.Point(304, 13);
            this.pictureBoxFileStructure.Name = "pictureBoxFileStructure";
            this.pictureBoxFileStructure.Size = new System.Drawing.Size(475, 527);
            this.pictureBoxFileStructure.TabIndex = 4;
            this.pictureBoxFileStructure.TabStop = false;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 552);
            this.Controls.Add(this.pictureBoxFileStructure);
            this.Controls.Add(this.radioButtonAIFF);
            this.Controls.Add(this.radioButtonWAV);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.labelInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InfoForm";
            this.Text = "Info";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFileStructure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.RadioButton radioButtonWAV;
        private System.Windows.Forms.RadioButton radioButtonAIFF;
        private System.Windows.Forms.PictureBox pictureBoxFileStructure;
    }
}