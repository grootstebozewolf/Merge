namespace UpdateUI
{
    partial class Settings
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
            this.scSettings = new System.Windows.Forms.SplitContainer();
            this.tvSettings = new System.Windows.Forms.TreeView();
            this.scSettings.Panel1.SuspendLayout();
            this.scSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // scSettings
            // 
            this.scSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSettings.Location = new System.Drawing.Point(0, 0);
            this.scSettings.Name = "scSettings";
            // 
            // scSettings.Panel1
            // 
            this.scSettings.Panel1.Controls.Add(this.tvSettings);
            this.scSettings.Size = new System.Drawing.Size(292, 266);
            this.scSettings.SplitterDistance = 97;
            this.scSettings.TabIndex = 0;
            // 
            // tvSettings
            // 
            this.tvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSettings.Location = new System.Drawing.Point(0, 0);
            this.tvSettings.Name = "tvSettings";
            this.tvSettings.Size = new System.Drawing.Size(94, 266);
            this.tvSettings.TabIndex = 0;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.scSettings);
            this.Name = "Settings";
            this.Text = "Settings";
            this.scSettings.Panel1.ResumeLayout(false);
            this.scSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scSettings;
        private System.Windows.Forms.TreeView tvSettings;
    }
}