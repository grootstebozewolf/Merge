using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace UpdateUI
{
    partial class Main: Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnPocketPC = new System.Windows.Forms.RadioButton();
            this.rbtnDesktop = new System.Windows.Forms.RadioButton();
            this.pnlCreateSDF = new System.Windows.Forms.Panel();
            this.txtSDFDatabase = new System.Windows.Forms.TextBox();
            this.txtSDFPassword = new System.Windows.Forms.TextBox();
            this.txtSDFUsername = new System.Windows.Forms.TextBox();
            this.txtSDFServerName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.pnlTransporter = new System.Windows.Forms.Panel();
            this.rbtnBroadbeam = new System.Windows.Forms.RadioButton();
            this.rbtnTransporter = new System.Windows.Forms.RadioButton();
            this.btnCheckScreen = new System.Windows.Forms.Button();
            this.pnlCommunication = new System.Windows.Forms.Panel();
            this.txtCommunicationDLL = new System.Windows.Forms.TextBox();
            this.lblCommunication = new System.Windows.Forms.Label();
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.btnQC = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.cmdSelectOutputDir = new System.Windows.Forms.Button();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.lblOutputDir = new System.Windows.Forms.Label();
            this.btnSetCreateScreen = new System.Windows.Forms.Button();
            this.pnlTimeSync = new System.Windows.Forms.Panel();
            this.txtTimeServer = new System.Windows.Forms.TextBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.lblTimeServer = new System.Windows.Forms.Label();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.pnlScripting = new System.Windows.Forms.Panel();
            this.txtScriptingDLL = new System.Windows.Forms.TextBox();
            this.lblScriptingDll = new System.Windows.Forms.Label();
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.rbtnCoPilot8 = new System.Windows.Forms.RadioButton();
            this.rbtnCoPilot7 = new System.Windows.Forms.RadioButton();
            this.rbtnGPS = new System.Windows.Forms.RadioButton();
            this.rbtnTomTom6 = new System.Windows.Forms.RadioButton();
            this.rbtnTomTom5 = new System.Windows.Forms.RadioButton();
            this.pnlMobileSettings = new System.Windows.Forms.Panel();
            this.chkCreateScreen = new System.Windows.Forms.CheckBox();
            this.chkCreateSDF = new System.Windows.Forms.CheckBox();
            this.chkComm2Serv = new System.Windows.Forms.CheckBox();
            this.chkMessaging = new System.Windows.Forms.CheckBox();
            this.chkCommunication = new System.Windows.Forms.CheckBox();
            this.chkScripting = new System.Windows.Forms.CheckBox();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.chkTimeSync = new System.Windows.Forms.CheckBox();
            this.lblServices = new System.Windows.Forms.Label();
            this.chkNavigatie = new System.Windows.Forms.CheckBox();
            this.rbtnPPC2003 = new System.Windows.Forms.RadioButton();
            this.rbtnWM50WM60 = new System.Windows.Forms.RadioButton();
            this.cmdGo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtnFramework35 = new System.Windows.Forms.RadioButton();
            this.rbtnFramework11 = new System.Windows.Forms.RadioButton();
            this.rbtnWindowsCE = new System.Windows.Forms.RadioButton();
            this.lblNewDir = new System.Windows.Forms.Label();
            this.txtNewDir = new System.Windows.Forms.TextBox();
            this.cmdSelectNewDir = new System.Windows.Forms.Button();
            this.dlgFolderOld = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgFolderNew = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgFolderOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.cmdSelectOldDir = new System.Windows.Forms.Button();
            this.txtOldDir = new System.Windows.Forms.TextBox();
            this.lblOldDir = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMSTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.rbtnWM61AND652 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.pnlCreateSDF.SuspendLayout();
            this.pnlTransporter.SuspendLayout();
            this.pnlCommunication.SuspendLayout();
            this.pnlOutput.SuspendLayout();
            this.pnlTimeSync.SuspendLayout();
            this.pnlScripting.SuspendLayout();
            this.pnlNavigation.SuspendLayout();
            this.pnlMobileSettings.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rbtnPocketPC);
            this.panel1.Controls.Add(this.rbtnDesktop);
            this.panel1.Controls.Add(this.pnlCreateSDF);
            this.panel1.Controls.Add(this.pnlTransporter);
            this.panel1.Controls.Add(this.btnCheckScreen);
            this.panel1.Controls.Add(this.pnlCommunication);
            this.panel1.Controls.Add(this.pnlOutput);
            this.panel1.Controls.Add(this.btnSetCreateScreen);
            this.panel1.Controls.Add(this.pnlTimeSync);
            this.panel1.Controls.Add(this.pnlScripting);
            this.panel1.Controls.Add(this.lstMessages);
            this.panel1.Controls.Add(this.pnlNavigation);
            this.panel1.Controls.Add(this.pnlMobileSettings);
            this.panel1.Controls.Add(this.cmdGo);
            this.panel1.Location = new System.Drawing.Point(12, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 814);
            this.panel1.TabIndex = 0;
            // 
            // rbtnPocketPC
            // 
            this.rbtnPocketPC.AutoSize = true;
            this.rbtnPocketPC.Checked = true;
            this.rbtnPocketPC.Location = new System.Drawing.Point(48, 182);
            this.rbtnPocketPC.Name = "rbtnPocketPC";
            this.rbtnPocketPC.Size = new System.Drawing.Size(45, 17);
            this.rbtnPocketPC.TabIndex = 28;
            this.rbtnPocketPC.TabStop = true;
            this.rbtnPocketPC.Text = "WM";
            this.rbtnPocketPC.UseVisualStyleBackColor = true;
            // 
            // rbtnDesktop
            // 
            this.rbtnDesktop.AutoSize = true;
            this.rbtnDesktop.Location = new System.Drawing.Point(3, 182);
            this.rbtnDesktop.Name = "rbtnDesktop";
            this.rbtnDesktop.Size = new System.Drawing.Size(39, 17);
            this.rbtnDesktop.TabIndex = 27;
            this.rbtnDesktop.Text = "PC";
            this.rbtnDesktop.UseVisualStyleBackColor = true;
            // 
            // pnlCreateSDF
            // 
            this.pnlCreateSDF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCreateSDF.Controls.Add(this.txtSDFDatabase);
            this.pnlCreateSDF.Controls.Add(this.txtSDFPassword);
            this.pnlCreateSDF.Controls.Add(this.txtSDFUsername);
            this.pnlCreateSDF.Controls.Add(this.txtSDFServerName);
            this.pnlCreateSDF.Controls.Add(this.lblPassword);
            this.pnlCreateSDF.Controls.Add(this.lblUsername);
            this.pnlCreateSDF.Controls.Add(this.lblDatabase);
            this.pnlCreateSDF.Controls.Add(this.lblServer);
            this.pnlCreateSDF.Location = new System.Drawing.Point(3, 594);
            this.pnlCreateSDF.Name = "pnlCreateSDF";
            this.pnlCreateSDF.Size = new System.Drawing.Size(410, 112);
            this.pnlCreateSDF.TabIndex = 26;
            this.pnlCreateSDF.Visible = false;
            // 
            // txtSDFDatabase
            // 
            this.txtSDFDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDFDatabase.Location = new System.Drawing.Point(85, 82);
            this.txtSDFDatabase.Name = "txtSDFDatabase";
            this.txtSDFDatabase.Size = new System.Drawing.Size(322, 20);
            this.txtSDFDatabase.TabIndex = 8;
            // 
            // txtSDFPassword
            // 
            this.txtSDFPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDFPassword.Location = new System.Drawing.Point(85, 56);
            this.txtSDFPassword.Name = "txtSDFPassword";
            this.txtSDFPassword.Size = new System.Drawing.Size(322, 20);
            this.txtSDFPassword.TabIndex = 7;
            this.txtSDFPassword.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtSDFUsername
            // 
            this.txtSDFUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDFUsername.Location = new System.Drawing.Point(85, 30);
            this.txtSDFUsername.Name = "txtSDFUsername";
            this.txtSDFUsername.Size = new System.Drawing.Size(322, 20);
            this.txtSDFUsername.TabIndex = 6;
            // 
            // txtSDFServerName
            // 
            this.txtSDFServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDFServerName.Location = new System.Drawing.Point(83, 4);
            this.txtSDFServerName.Name = "txtSDFServerName";
            this.txtSDFServerName.Size = new System.Drawing.Size(324, 20);
            this.txtSDFServerName.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(3, 59);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(3, 33);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(3, 85);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Database";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(3, 7);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(38, 13);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "Server";
            // 
            // pnlTransporter
            // 
            this.pnlTransporter.Controls.Add(this.rbtnBroadbeam);
            this.pnlTransporter.Controls.Add(this.rbtnTransporter);
            this.pnlTransporter.Location = new System.Drawing.Point(3, 337);
            this.pnlTransporter.Name = "pnlTransporter";
            this.pnlTransporter.Size = new System.Drawing.Size(412, 30);
            this.pnlTransporter.TabIndex = 25;
            // 
            // rbtnBroadbeam
            // 
            this.rbtnBroadbeam.AutoSize = true;
            this.rbtnBroadbeam.Location = new System.Drawing.Point(83, 7);
            this.rbtnBroadbeam.Name = "rbtnBroadbeam";
            this.rbtnBroadbeam.Size = new System.Drawing.Size(79, 17);
            this.rbtnBroadbeam.TabIndex = 2;
            this.rbtnBroadbeam.TabStop = true;
            this.rbtnBroadbeam.Text = "Broadbeam";
            this.rbtnBroadbeam.UseVisualStyleBackColor = true;
            // 
            // rbtnTransporter
            // 
            this.rbtnTransporter.AutoSize = true;
            this.rbtnTransporter.Location = new System.Drawing.Point(3, 7);
            this.rbtnTransporter.Name = "rbtnTransporter";
            this.rbtnTransporter.Size = new System.Drawing.Size(79, 17);
            this.rbtnTransporter.TabIndex = 1;
            this.rbtnTransporter.TabStop = true;
            this.rbtnTransporter.Text = "Transporter";
            this.rbtnTransporter.UseVisualStyleBackColor = true;
            // 
            // btnCheckScreen
            // 
            this.btnCheckScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckScreen.Location = new System.Drawing.Point(87, 784);
            this.btnCheckScreen.Name = "btnCheckScreen";
            this.btnCheckScreen.Size = new System.Drawing.Size(100, 23);
            this.btnCheckScreen.TabIndex = 24;
            this.btnCheckScreen.Text = "Check Screen";
            this.btnCheckScreen.UseVisualStyleBackColor = true;
            this.btnCheckScreen.Click += new System.EventHandler(this.btnCopyScreen_Click);
            // 
            // pnlCommunication
            // 
            this.pnlCommunication.Controls.Add(this.txtCommunicationDLL);
            this.pnlCommunication.Controls.Add(this.lblCommunication);
            this.pnlCommunication.Location = new System.Drawing.Point(3, 489);
            this.pnlCommunication.Name = "pnlCommunication";
            this.pnlCommunication.Size = new System.Drawing.Size(338, 45);
            this.pnlCommunication.TabIndex = 23;
            this.pnlCommunication.Visible = false;
            this.pnlCommunication.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCommunication_Paint);
            // 
            // txtCommunicationDLL
            // 
            this.txtCommunicationDLL.Location = new System.Drawing.Point(6, 19);
            this.txtCommunicationDLL.Name = "txtCommunicationDLL";
            this.txtCommunicationDLL.Size = new System.Drawing.Size(329, 20);
            this.txtCommunicationDLL.TabIndex = 3;
            // 
            // lblCommunication
            // 
            this.lblCommunication.AutoSize = true;
            this.lblCommunication.Location = new System.Drawing.Point(3, 3);
            this.lblCommunication.Name = "lblCommunication";
            this.lblCommunication.Size = new System.Drawing.Size(91, 13);
            this.lblCommunication.TabIndex = 0;
            this.lblCommunication.Text = "CommunicationDll";
            // 
            // pnlOutput
            // 
            this.pnlOutput.Controls.Add(this.btnQC);
            this.pnlOutput.Controls.Add(this.btnCompare);
            this.pnlOutput.Controls.Add(this.cmdSelectOutputDir);
            this.pnlOutput.Controls.Add(this.txtOutputDir);
            this.pnlOutput.Controls.Add(this.lblOutputDir);
            this.pnlOutput.Location = new System.Drawing.Point(1, 93);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Size = new System.Drawing.Size(415, 84);
            this.pnlOutput.TabIndex = 22;
            // 
            // btnQC
            // 
            this.btnQC.Location = new System.Drawing.Point(125, 51);
            this.btnQC.Name = "btnQC";
            this.btnQC.Size = new System.Drawing.Size(114, 23);
            this.btnQC.TabIndex = 26;
            this.btnQC.Text = "QC";
            this.btnQC.UseVisualStyleBackColor = true;
            this.btnQC.Click += new System.EventHandler(this.btnQC_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(5, 51);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(114, 23);
            this.btnCompare.TabIndex = 25;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // cmdSelectOutputDir
            // 
            this.cmdSelectOutputDir.Location = new System.Drawing.Point(308, 24);
            this.cmdSelectOutputDir.Name = "cmdSelectOutputDir";
            this.cmdSelectOutputDir.Size = new System.Drawing.Size(24, 20);
            this.cmdSelectOutputDir.TabIndex = 24;
            this.cmdSelectOutputDir.Text = "...";
            this.cmdSelectOutputDir.UseVisualStyleBackColor = true;
            this.cmdSelectOutputDir.Click += new System.EventHandler(this.cmdSelectOutputDir_Click);
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Location = new System.Drawing.Point(5, 25);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(299, 20);
            this.txtOutputDir.TabIndex = 23;
            this.txtOutputDir.Text = "C:\\UpdateFV\\OttoOoms\\Out";
            // 
            // lblOutputDir
            // 
            this.lblOutputDir.AutoSize = true;
            this.lblOutputDir.Location = new System.Drawing.Point(2, 9);
            this.lblOutputDir.Name = "lblOutputDir";
            this.lblOutputDir.Size = new System.Drawing.Size(39, 13);
            this.lblOutputDir.TabIndex = 22;
            this.lblOutputDir.Text = "Output";
            // 
            // btnSetCreateScreen
            // 
            this.btnSetCreateScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetCreateScreen.Enabled = false;
            this.btnSetCreateScreen.Location = new System.Drawing.Point(221, 784);
            this.btnSetCreateScreen.Name = "btnSetCreateScreen";
            this.btnSetCreateScreen.Size = new System.Drawing.Size(114, 23);
            this.btnSetCreateScreen.TabIndex = 20;
            this.btnSetCreateScreen.Text = "Copy Screen";
            this.btnSetCreateScreen.UseVisualStyleBackColor = true;
            this.btnSetCreateScreen.Visible = false;
            this.btnSetCreateScreen.Click += new System.EventHandler(this.btnSetCreateScreen_Click);
            // 
            // pnlTimeSync
            // 
            this.pnlTimeSync.Controls.Add(this.txtTimeServer);
            this.pnlTimeSync.Controls.Add(this.txtTimeout);
            this.pnlTimeSync.Controls.Add(this.lblTimeServer);
            this.pnlTimeSync.Controls.Add(this.lblTimeout);
            this.pnlTimeSync.Location = new System.Drawing.Point(3, 373);
            this.pnlTimeSync.Name = "pnlTimeSync";
            this.pnlTimeSync.Size = new System.Drawing.Size(338, 83);
            this.pnlTimeSync.TabIndex = 17;
            this.pnlTimeSync.Visible = false;
            // 
            // txtTimeServer
            // 
            this.txtTimeServer.Location = new System.Drawing.Point(6, 58);
            this.txtTimeServer.Name = "txtTimeServer";
            this.txtTimeServer.Size = new System.Drawing.Size(329, 20);
            this.txtTimeServer.TabIndex = 3;
            this.txtTimeServer.Text = "time.nist.gov";
            // 
            // txtTimeout
            // 
            this.txtTimeout.Location = new System.Drawing.Point(6, 19);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(329, 20);
            this.txtTimeout.TabIndex = 2;
            this.txtTimeout.Text = "4000";
            // 
            // lblTimeServer
            // 
            this.lblTimeServer.AutoSize = true;
            this.lblTimeServer.Location = new System.Drawing.Point(3, 42);
            this.lblTimeServer.Name = "lblTimeServer";
            this.lblTimeServer.Size = new System.Drawing.Size(62, 13);
            this.lblTimeServer.TabIndex = 1;
            this.lblTimeServer.Text = "Time server";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(3, 3);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(45, 13);
            this.lblTimeout.TabIndex = 0;
            this.lblTimeout.Text = "Timeout";
            // 
            // pnlScripting
            // 
            this.pnlScripting.Controls.Add(this.txtScriptingDLL);
            this.pnlScripting.Controls.Add(this.lblScriptingDll);
            this.pnlScripting.Location = new System.Drawing.Point(3, 452);
            this.pnlScripting.Name = "pnlScripting";
            this.pnlScripting.Size = new System.Drawing.Size(338, 47);
            this.pnlScripting.TabIndex = 2;
            this.pnlScripting.Visible = false;
            // 
            // txtScriptingDLL
            // 
            this.txtScriptingDLL.Location = new System.Drawing.Point(6, 19);
            this.txtScriptingDLL.Name = "txtScriptingDLL";
            this.txtScriptingDLL.Size = new System.Drawing.Size(329, 20);
            this.txtScriptingDLL.TabIndex = 3;
            // 
            // lblScriptingDll
            // 
            this.lblScriptingDll.AutoSize = true;
            this.lblScriptingDll.Location = new System.Drawing.Point(3, 3);
            this.lblScriptingDll.Name = "lblScriptingDll";
            this.lblScriptingDll.Size = new System.Drawing.Size(60, 13);
            this.lblScriptingDll.TabIndex = 0;
            this.lblScriptingDll.Text = "ScriptingDll";
            // 
            // lstMessages
            // 
            this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMessages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.Location = new System.Drawing.Point(6, 705);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(407, 56);
            this.lstMessages.TabIndex = 18;
            this.lstMessages.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstMessages_DrawItem);
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.Controls.Add(this.rbtnCoPilot8);
            this.pnlNavigation.Controls.Add(this.rbtnCoPilot7);
            this.pnlNavigation.Controls.Add(this.rbtnGPS);
            this.pnlNavigation.Controls.Add(this.rbtnTomTom6);
            this.pnlNavigation.Controls.Add(this.rbtnTomTom5);
            this.pnlNavigation.Location = new System.Drawing.Point(3, 534);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(338, 54);
            this.pnlNavigation.TabIndex = 16;
            this.pnlNavigation.Visible = false;
            // 
            // rbtnCoPilot8
            // 
            this.rbtnCoPilot8.AutoSize = true;
            this.rbtnCoPilot8.Location = new System.Drawing.Point(85, 28);
            this.rbtnCoPilot8.Name = "rbtnCoPilot8";
            this.rbtnCoPilot8.Size = new System.Drawing.Size(64, 17);
            this.rbtnCoPilot8.TabIndex = 6;
            this.rbtnCoPilot8.Text = "CoPilot8";
            this.rbtnCoPilot8.UseVisualStyleBackColor = true;
            // 
            // rbtnCoPilot7
            // 
            this.rbtnCoPilot7.AutoSize = true;
            this.rbtnCoPilot7.Location = new System.Drawing.Point(85, 6);
            this.rbtnCoPilot7.Name = "rbtnCoPilot7";
            this.rbtnCoPilot7.Size = new System.Drawing.Size(64, 17);
            this.rbtnCoPilot7.TabIndex = 5;
            this.rbtnCoPilot7.Text = "CoPilot7";
            this.rbtnCoPilot7.UseVisualStyleBackColor = true;
            // 
            // rbtnGPS
            // 
            this.rbtnGPS.AutoSize = true;
            this.rbtnGPS.Location = new System.Drawing.Point(153, 28);
            this.rbtnGPS.Name = "rbtnGPS";
            this.rbtnGPS.Size = new System.Drawing.Size(47, 17);
            this.rbtnGPS.TabIndex = 3;
            this.rbtnGPS.Text = "GPS";
            this.rbtnGPS.UseVisualStyleBackColor = true;
            // 
            // rbtnTomTom6
            // 
            this.rbtnTomTom6.AutoSize = true;
            this.rbtnTomTom6.Location = new System.Drawing.Point(6, 28);
            this.rbtnTomTom6.Name = "rbtnTomTom6";
            this.rbtnTomTom6.Size = new System.Drawing.Size(73, 17);
            this.rbtnTomTom6.TabIndex = 1;
            this.rbtnTomTom6.Text = "TomTom6";
            this.rbtnTomTom6.UseVisualStyleBackColor = true;
            // 
            // rbtnTomTom5
            // 
            this.rbtnTomTom5.AutoSize = true;
            this.rbtnTomTom5.Location = new System.Drawing.Point(6, 5);
            this.rbtnTomTom5.Name = "rbtnTomTom5";
            this.rbtnTomTom5.Size = new System.Drawing.Size(73, 17);
            this.rbtnTomTom5.TabIndex = 0;
            this.rbtnTomTom5.Text = "TomTom5";
            this.rbtnTomTom5.UseVisualStyleBackColor = true;
            // 
            // pnlMobileSettings
            // 
            this.pnlMobileSettings.Controls.Add(this.rbtnWM61AND652);
            this.pnlMobileSettings.Controls.Add(this.chkCreateScreen);
            this.pnlMobileSettings.Controls.Add(this.chkCreateSDF);
            this.pnlMobileSettings.Controls.Add(this.chkComm2Serv);
            this.pnlMobileSettings.Controls.Add(this.chkMessaging);
            this.pnlMobileSettings.Controls.Add(this.chkCommunication);
            this.pnlMobileSettings.Controls.Add(this.chkScripting);
            this.pnlMobileSettings.Controls.Add(this.cbDevice);
            this.pnlMobileSettings.Controls.Add(this.lblDevice);
            this.pnlMobileSettings.Controls.Add(this.chkPrint);
            this.pnlMobileSettings.Controls.Add(this.chkTimeSync);
            this.pnlMobileSettings.Controls.Add(this.lblServices);
            this.pnlMobileSettings.Controls.Add(this.chkNavigatie);
            this.pnlMobileSettings.Controls.Add(this.rbtnPPC2003);
            this.pnlMobileSettings.Controls.Add(this.rbtnWM50WM60);
            this.pnlMobileSettings.Location = new System.Drawing.Point(1, 205);
            this.pnlMobileSettings.Name = "pnlMobileSettings";
            this.pnlMobileSettings.Size = new System.Drawing.Size(415, 129);
            this.pnlMobileSettings.TabIndex = 15;
            // 
            // chkCreateScreen
            // 
            this.chkCreateScreen.AutoSize = true;
            this.chkCreateScreen.Checked = true;
            this.chkCreateScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateScreen.Location = new System.Drawing.Point(309, 4);
            this.chkCreateScreen.Name = "chkCreateScreen";
            this.chkCreateScreen.Size = new System.Drawing.Size(94, 17);
            this.chkCreateScreen.TabIndex = 25;
            this.chkCreateScreen.Text = "Create Screen";
            this.chkCreateScreen.UseVisualStyleBackColor = true;
            // 
            // chkCreateSDF
            // 
            this.chkCreateSDF.AutoSize = true;
            this.chkCreateSDF.Location = new System.Drawing.Point(261, 62);
            this.chkCreateSDF.Name = "chkCreateSDF";
            this.chkCreateSDF.Size = new System.Drawing.Size(78, 17);
            this.chkCreateSDF.TabIndex = 24;
            this.chkCreateSDF.Text = "CreateSDF";
            this.chkCreateSDF.UseVisualStyleBackColor = true;
            this.chkCreateSDF.CheckedChanged += new System.EventHandler(this.chkCreateSDF_CheckedChanged);
            // 
            // chkComm2Serv
            // 
            this.chkComm2Serv.AutoSize = true;
            this.chkComm2Serv.Location = new System.Drawing.Point(245, 39);
            this.chkComm2Serv.Name = "chkComm2Serv";
            this.chkComm2Serv.Size = new System.Drawing.Size(83, 17);
            this.chkComm2Serv.TabIndex = 23;
            this.chkComm2Serv.Text = "Comm2Serv";
            this.chkComm2Serv.UseVisualStyleBackColor = true;
            // 
            // chkMessaging
            // 
            this.chkMessaging.AutoSize = true;
            this.chkMessaging.Location = new System.Drawing.Point(109, 62);
            this.chkMessaging.Name = "chkMessaging";
            this.chkMessaging.Size = new System.Drawing.Size(77, 17);
            this.chkMessaging.TabIndex = 22;
            this.chkMessaging.Text = "Messaging";
            this.chkMessaging.UseVisualStyleBackColor = true;
            // 
            // chkCommunication
            // 
            this.chkCommunication.AutoSize = true;
            this.chkCommunication.Location = new System.Drawing.Point(5, 62);
            this.chkCommunication.Name = "chkCommunication";
            this.chkCommunication.Size = new System.Drawing.Size(98, 17);
            this.chkCommunication.TabIndex = 21;
            this.chkCommunication.Text = "Communication";
            this.chkCommunication.UseVisualStyleBackColor = true;
            this.chkCommunication.CheckedChanged += new System.EventHandler(this.chkCommunication_CheckedChanged);
            // 
            // chkScripting
            // 
            this.chkScripting.AutoSize = true;
            this.chkScripting.Location = new System.Drawing.Point(192, 62);
            this.chkScripting.Name = "chkScripting";
            this.chkScripting.Size = new System.Drawing.Size(67, 17);
            this.chkScripting.TabIndex = 20;
            this.chkScripting.Text = "Scripting";
            this.chkScripting.UseVisualStyleBackColor = true;
            this.chkScripting.CheckedChanged += new System.EventHandler(this.chkScripting_CheckedChanged);
            // 
            // cbDevice
            // 
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(5, 98);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(329, 21);
            this.cbDevice.TabIndex = 19;
            this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.cbDevice_SelectedIndexChanged);
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(2, 82);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 18;
            this.lblDevice.Text = "Device";
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Location = new System.Drawing.Point(192, 39);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(47, 17);
            this.chkPrint.TabIndex = 17;
            this.chkPrint.Text = "Print";
            this.chkPrint.UseVisualStyleBackColor = true;
            // 
            // chkTimeSync
            // 
            this.chkTimeSync.AutoSize = true;
            this.chkTimeSync.Checked = true;
            this.chkTimeSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimeSync.Location = new System.Drawing.Point(5, 39);
            this.chkTimeSync.Name = "chkTimeSync";
            this.chkTimeSync.Size = new System.Drawing.Size(73, 17);
            this.chkTimeSync.TabIndex = 16;
            this.chkTimeSync.Text = "TimeSync";
            this.chkTimeSync.UseVisualStyleBackColor = true;
            this.chkTimeSync.CheckedChanged += new System.EventHandler(this.chkTimeSync_CheckedChanged);
            // 
            // lblServices
            // 
            this.lblServices.AutoSize = true;
            this.lblServices.Location = new System.Drawing.Point(2, 23);
            this.lblServices.Name = "lblServices";
            this.lblServices.Size = new System.Drawing.Size(48, 13);
            this.lblServices.TabIndex = 15;
            this.lblServices.Text = "Services";
            // 
            // chkNavigatie
            // 
            this.chkNavigatie.AutoSize = true;
            this.chkNavigatie.Location = new System.Drawing.Point(109, 39);
            this.chkNavigatie.Name = "chkNavigatie";
            this.chkNavigatie.Size = new System.Drawing.Size(77, 17);
            this.chkNavigatie.TabIndex = 14;
            this.chkNavigatie.Text = "Navigation";
            this.chkNavigatie.UseVisualStyleBackColor = true;
            this.chkNavigatie.CheckedChanged += new System.EventHandler(this.chkNavigatie_CheckedChanged);
            // 
            // rbtnPPC2003
            // 
            this.rbtnPPC2003.AutoSize = true;
            this.rbtnPPC2003.Checked = true;
            this.rbtnPPC2003.Location = new System.Drawing.Point(5, 3);
            this.rbtnPPC2003.Name = "rbtnPPC2003";
            this.rbtnPPC2003.Size = new System.Drawing.Size(73, 17);
            this.rbtnPPC2003.TabIndex = 12;
            this.rbtnPPC2003.TabStop = true;
            this.rbtnPPC2003.Text = "PPC 2003";
            this.rbtnPPC2003.UseVisualStyleBackColor = true;
            // 
            // rbtnWM50WM60
            // 
            this.rbtnWM50WM60.AutoSize = true;
            this.rbtnWM50WM60.Location = new System.Drawing.Point(84, 3);
            this.rbtnWM50WM60.Name = "rbtnWM50WM60";
            this.rbtnWM50WM60.Size = new System.Drawing.Size(102, 17);
            this.rbtnWM50WM60.TabIndex = 13;
            this.rbtnWM50WM60.Text = "WM 5.0 and 6.0";
            this.rbtnWM50WM60.UseVisualStyleBackColor = true;
            // 
            // cmdGo
            // 
            this.cmdGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGo.Location = new System.Drawing.Point(6, 784);
            this.cmdGo.Name = "cmdGo";
            this.cmdGo.Size = new System.Drawing.Size(75, 23);
            this.cmdGo.TabIndex = 11;
            this.cmdGo.Text = "Go";
            this.cmdGo.UseVisualStyleBackColor = true;
            this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.rbtnWindowsCE);
            this.panel2.Controls.Add(this.lblNewDir);
            this.panel2.Controls.Add(this.txtNewDir);
            this.panel2.Controls.Add(this.cmdSelectNewDir);
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(416, 62);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbtnFramework35);
            this.panel3.Controls.Add(this.rbtnFramework11);
            this.panel3.Location = new System.Drawing.Point(147, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(266, 22);
            this.panel3.TabIndex = 26;
            // 
            // rbtnFramework35
            // 
            this.rbtnFramework35.AutoSize = true;
            this.rbtnFramework35.Location = new System.Drawing.Point(105, 2);
            this.rbtnFramework35.Name = "rbtnFramework35";
            this.rbtnFramework35.Size = new System.Drawing.Size(95, 17);
            this.rbtnFramework35.TabIndex = 1;
            this.rbtnFramework35.Text = "Framework 3.5";
            this.rbtnFramework35.UseVisualStyleBackColor = true;
            // 
            // rbtnFramework11
            // 
            this.rbtnFramework11.AutoSize = true;
            this.rbtnFramework11.Checked = true;
            this.rbtnFramework11.Location = new System.Drawing.Point(4, 3);
            this.rbtnFramework11.Name = "rbtnFramework11";
            this.rbtnFramework11.Size = new System.Drawing.Size(95, 17);
            this.rbtnFramework11.TabIndex = 0;
            this.rbtnFramework11.TabStop = true;
            this.rbtnFramework11.Text = "Framework 1.1";
            this.rbtnFramework11.UseVisualStyleBackColor = true;
            // 
            // rbtnWindowsCE
            // 
            this.rbtnWindowsCE.AutoSize = true;
            this.rbtnWindowsCE.Location = new System.Drawing.Point(6, 42);
            this.rbtnWindowsCE.Name = "rbtnWindowsCE";
            this.rbtnWindowsCE.Size = new System.Drawing.Size(39, 17);
            this.rbtnWindowsCE.TabIndex = 9;
            this.rbtnWindowsCE.Text = "CE";
            this.rbtnWindowsCE.UseVisualStyleBackColor = true;
            this.rbtnWindowsCE.CheckedChanged += new System.EventHandler(this.rbtnWindowsCE_CheckedChanged);
            // 
            // lblNewDir
            // 
            this.lblNewDir.AutoSize = true;
            this.lblNewDir.Location = new System.Drawing.Point(3, 0);
            this.lblNewDir.Name = "lblNewDir";
            this.lblNewDir.Size = new System.Drawing.Size(59, 13);
            this.lblNewDir.TabIndex = 1;
            this.lblNewDir.Text = "Nieuwe FV";
            // 
            // txtNewDir
            // 
            this.txtNewDir.Location = new System.Drawing.Point(6, 16);
            this.txtNewDir.Name = "txtNewDir";
            this.txtNewDir.Size = new System.Drawing.Size(299, 20);
            this.txtNewDir.TabIndex = 7;
            this.txtNewDir.Text = "C:\\UpdateFV\\New";
            this.txtNewDir.TextChanged += new System.EventHandler(this.txtNewDir_TextChanged);
            // 
            // cmdSelectNewDir
            // 
            this.cmdSelectNewDir.Location = new System.Drawing.Point(308, 16);
            this.cmdSelectNewDir.Name = "cmdSelectNewDir";
            this.cmdSelectNewDir.Size = new System.Drawing.Size(24, 20);
            this.cmdSelectNewDir.TabIndex = 8;
            this.cmdSelectNewDir.Text = "...";
            this.cmdSelectNewDir.UseVisualStyleBackColor = true;
            this.cmdSelectNewDir.Click += new System.EventHandler(this.cmdSelectNewDir_Click);
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.cmdSelectOldDir);
            this.pnlInput.Controls.Add(this.txtOldDir);
            this.pnlInput.Controls.Add(this.panel2);
            this.pnlInput.Controls.Add(this.lblOldDir);
            this.pnlInput.Location = new System.Drawing.Point(12, 46);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(419, 87);
            this.pnlInput.TabIndex = 24;
            // 
            // cmdSelectOldDir
            // 
            this.cmdSelectOldDir.Location = new System.Drawing.Point(308, 21);
            this.cmdSelectOldDir.Name = "cmdSelectOldDir";
            this.cmdSelectOldDir.Size = new System.Drawing.Size(24, 20);
            this.cmdSelectOldDir.TabIndex = 26;
            this.cmdSelectOldDir.Text = "...";
            this.cmdSelectOldDir.UseVisualStyleBackColor = true;
            this.cmdSelectOldDir.Click += new System.EventHandler(this.cmdSelectOldDir_Click);
            // 
            // txtOldDir
            // 
            this.txtOldDir.Location = new System.Drawing.Point(6, 21);
            this.txtOldDir.Name = "txtOldDir";
            this.txtOldDir.Size = new System.Drawing.Size(299, 20);
            this.txtOldDir.TabIndex = 25;
            this.txtOldDir.Text = "C:\\UpdateFV\\OttoOoms\\Old";
            this.txtOldDir.TextChanged += new System.EventHandler(this.txtOldDir_TextChanged);
            // 
            // lblOldDir
            // 
            this.lblOldDir.AutoSize = true;
            this.lblOldDir.Location = new System.Drawing.Point(3, 5);
            this.lblOldDir.Name = "lblOldDir";
            this.lblOldDir.Size = new System.Drawing.Size(78, 13);
            this.lblOldDir.TabIndex = 24;
            this.lblOldDir.Text = "Te updaten FV";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(439, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.generateMSTestToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(176, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // generateMSTestToolStripMenuItem
            // 
            this.generateMSTestToolStripMenuItem.Name = "generateMSTestToolStripMenuItem";
            this.generateMSTestToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.generateMSTestToolStripMenuItem.Text = "Generate MStest file";
            this.generateMSTestToolStripMenuItem.Click += new System.EventHandler(this.generateMSTestToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(176, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "Update Configuration File|*.xml";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Update Configuration File|*.xml";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // rbtnWM61AND652
            // 
            this.rbtnWM61AND652.AutoSize = true;
            this.rbtnWM61AND652.Location = new System.Drawing.Point(192, 3);
            this.rbtnWM61AND652.Name = "rbtnWM61AND652";
            this.rbtnWM61AND652.Size = new System.Drawing.Size(111, 17);
            this.rbtnWM61AND652.TabIndex = 26;
            this.rbtnWM61AND652.Text = "WM 6.1 and 6.5.2";
            this.rbtnWM61AND652.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 872);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Update";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlCreateSDF.ResumeLayout(false);
            this.pnlCreateSDF.PerformLayout();
            this.pnlTransporter.ResumeLayout(false);
            this.pnlTransporter.PerformLayout();
            this.pnlCommunication.ResumeLayout(false);
            this.pnlCommunication.PerformLayout();
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutput.PerformLayout();
            this.pnlTimeSync.ResumeLayout(false);
            this.pnlTimeSync.PerformLayout();
            this.pnlScripting.ResumeLayout(false);
            this.pnlScripting.PerformLayout();
            this.pnlNavigation.ResumeLayout(false);
            this.pnlNavigation.PerformLayout();
            this.pnlMobileSettings.ResumeLayout(false);
            this.pnlMobileSettings.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNewDir;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderOld;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderNew;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderOutput;
        private System.Windows.Forms.TextBox txtNewDir;
        private System.Windows.Forms.Button cmdSelectNewDir;
        private System.Windows.Forms.Button cmdGo;
        private System.Windows.Forms.RadioButton rbtnWM50WM60;
        private System.Windows.Forms.RadioButton rbtnPPC2003;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnWindowsCE;
        private System.Windows.Forms.Panel pnlMobileSettings;
        private System.Windows.Forms.CheckBox chkNavigatie;
        private System.Windows.Forms.Label lblServices;
        private System.Windows.Forms.CheckBox chkTimeSync;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.RadioButton rbtnTomTom6;
        private System.Windows.Forms.RadioButton rbtnTomTom5;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.CheckBox chkScripting;
        private System.Windows.Forms.Panel pnlScripting;
        private System.Windows.Forms.Label lblScriptingDll;
        private System.Windows.Forms.Panel pnlTimeSync;
        private System.Windows.Forms.Label lblTimeServer;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.TextBox txtTimeServer;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.TextBox txtScriptingDLL;
        private System.Windows.Forms.ListBox lstMessages;
        private System.Windows.Forms.Button btnSetCreateScreen;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Button cmdSelectOldDir;
        private System.Windows.Forms.TextBox txtOldDir;
        private System.Windows.Forms.Label lblOldDir;
        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button cmdSelectOutputDir;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Label lblOutputDir;
        private System.Windows.Forms.Panel pnlCommunication;
        private System.Windows.Forms.TextBox txtCommunicationDLL;
        private System.Windows.Forms.Label lblCommunication;
        private System.Windows.Forms.CheckBox chkCommunication;
        private System.Windows.Forms.CheckBox chkMessaging;
        private System.Windows.Forms.Button btnCheckScreen;
        private System.Windows.Forms.Panel pnlTransporter;
        private System.Windows.Forms.RadioButton rbtnBroadbeam;
        private System.Windows.Forms.RadioButton rbtnTransporter;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbtnFramework35;
        private System.Windows.Forms.RadioButton rbtnFramework11;
        private System.Windows.Forms.CheckBox chkComm2Serv;
        private Button btnQC;
        private RadioButton rbtnGPS;
        private CheckBox chkCreateSDF;
        private Panel pnlCreateSDF;
        private TextBox txtSDFDatabase;
        private TextBox txtSDFPassword;
        private TextBox txtSDFUsername;
        private TextBox txtSDFServerName;
        private Label lblPassword;
        private Label lblUsername;
        private Label lblDatabase;
        private Label lblServer;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
        private CheckBox chkCreateScreen;
        private RadioButton rbtnDesktop;
        private RadioButton rbtnPocketPC;
        private RadioButton rbtnCoPilot8;
        private RadioButton rbtnCoPilot7;
        private ToolStripMenuItem generateMSTestToolStripMenuItem;
        private RadioButton rbtnWM61AND652;
    }
}

