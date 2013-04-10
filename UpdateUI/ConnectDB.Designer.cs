namespace UpdateUI
{
    partial class ConnectDB
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblConnectionstring = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(806, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblConnectionstring
            // 
            this.lblConnectionstring.AutoSize = true;
            this.lblConnectionstring.Location = new System.Drawing.Point(12, 16);
            this.lblConnectionstring.Name = "lblConnectionstring";
            this.lblConnectionstring.Size = new System.Drawing.Size(86, 13);
            this.lblConnectionstring.TabIndex = 1;
            this.lblConnectionstring.Text = "Connectionstring";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(106, 13);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(694, 20);
            this.txtConnectionString.TabIndex = 2;
            this.txtConnectionString.Text = "Password=Fieldv!si0n;Persist Security Info=True;User ID=con;Initial Catalog=Field" +
                "Vision_Gamko_test;Data Source=SRV-DB-02\\SRVDB02_2000";
            // 
            // ConnectDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 46);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.lblConnectionstring);
            this.Controls.Add(this.btnConnect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectDB";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Connect Data DB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblConnectionstring;
        private System.Windows.Forms.TextBox txtConnectionString;
    }
}