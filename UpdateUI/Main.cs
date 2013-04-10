using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Timer=System.Threading.Timer;
using log4net;

/*using System.ComponentModel;*/
/*using System.Reflection;*/

namespace UpdateUI
{
    public partial class Main
    {
        private readonly BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
        private readonly FormInitLib initLib;

        private readonly string strConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"Configuration.xml";
        private string strCustomConfigFile;
        private readonly string strCreateScreenURI = Properties.Settings.Default.CreateScreenURI;

        private readonly string strDeviceSettingsPath = AppDomain.CurrentDomain.BaseDirectory + @"DeviceSettings.xml";


        private Assembly assemblyCreateScreen;
        private DeviceSettings colDeviceSettings;
        private Form frmCreateScreen;
        private ScreenLib.ScreenConfiguration objScreenConfiguration;
        private Collection<int> colListboxWarn = new Collection<int>();
        private Timer timCheckReady;
        private DateTime dtmStartThread;
        private static string strCurrentDomainBasePath;

        public event EventHandler OnScreenCreated;

        public Main()
        {
            InitializeComponent();
            initLib = new FormInitLib(Controls);
            SDILReader.Globals.LoadOpCodes();
            OnScreenCreated += new EventHandler(Main_OnScreenCreated);
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.xml"));
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyUnhandledExceptionEventHandler);
        }
        static void MyUnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            LogManager.GetLogger("LogFile").Error("MyUnhandledExceptionEventHandler caught : " + e.Message);
            Exception innerException = e.InnerException;
            while (innerException != null)
            {
                LogManager.GetLogger("LogFile").Error("MyUnhandledExceptionEventHandler caught : " + innerException.Message);
                innerException = innerException.InnerException;
            }
        }

        private void Main_OnScreenCreated(object sender, EventArgs e)
        {
            if (!rbtnDesktop.Checked)
            {
                Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0);
                Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0);
                Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0);
                string strOutputDir = txtOutputDir.Text;
                string strCreateSDF = chkCreateSDF.Checked.ToString();
                var proc5 = new Process { StartInfo = { FileName = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "CreateScreenPocketPC.bat"), Arguments = string.Format("{0} {1}", FilesystemLib.GetShortPathName(ref strOutputDir), strCreateSDF) } };
                proc5.Start();
                proc5.WaitForExit(1000);
            } else
            {
                Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0);
                Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0);
                Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0); Thread.Sleep(0);
                string strOutputDir = txtOutputDir.Text;
                string strCreateSDF = chkCreateSDF.Checked.ToString();
                var proc5 = new Process { StartInfo = { FileName = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "CreateScreenDesktop.bat"), Arguments = string.Format("{0} {1}", FilesystemLib.GetShortPathName(ref strOutputDir), strCreateSDF) } };
                proc5.Start();
                proc5.WaitForExit(1000);
            }

        }


        public DeviceSettings ColDeviceSettings
        {
            get { return colDeviceSettings; }
        }

        private static void RunApp(object state) // Need this function to pass into 
            // WaitCallBack().
        {
            Application.Run((Form) state);
        }

        private static void InvokeMethod(ISynchronizeInvoke form, string methodName, params object[] parms)
        {
            var eh = (EventHandler) Delegate.CreateDelegate(typeof (EventHandler), form, methodName);
            if (eh != null)
            {
                form.Invoke(eh, parms);
            }
        }

        private object GetField(object obj, string fieldName)
        {
            Type t = obj.GetType();
            FieldInfo fi = t.GetField(fieldName, flags);
            return fi.GetValue(obj);
        }

        private object GetProperty(object obj, string propertyName)
        {
            Type t = obj.GetType();
            PropertyInfo pi = t.GetProperty(propertyName, flags);
            return pi.GetValue(obj, new object[0]);
        }

        private void cmdSelectOldDir_Click(object sender, EventArgs e)
        {
            if (dlgFolderOld != null)
            {
                if (Directory.Exists(txtOldDir.Text))
                {
                    dlgFolderOld.SelectedPath = txtOldDir.Text;
                }
                if (dlgFolderOld.ShowDialog() == DialogResult.OK)
                {
                    txtOldDir.Text = dlgFolderOld.SelectedPath;
                    ExtractAndCheckSettingsFromOldDir(dlgFolderOld.SelectedPath);
                }
            }
        }


        private void ExtractAndCheckSettingsFromOldDir(string strSelectedPath)
        {
            string strScriptingDll = null;
            lstMessages.Items.Clear();

            #region ExtractSettings

            if (Directory.Exists(strSelectedPath + @"\4 FVI\COPY\Program files\FVDnet\"))
            {
                rbtnDesktop.Checked = true;
            }
            else if (Directory.Exists(strSelectedPath + @"\FVI\COPY\Program files\FVDnet\"))
            {
                if (File.Exists(strSelectedPath + @"\FVI\COPY\Program files\FVDnet\MsqClientPC.xml") ||
                    File.Exists(strSelectedPath + @"\FVI\COPY\Program files\FVDnet\FVmsqPC.xml"))
                {
                    rbtnDesktop.Checked = true;
                }
                else
                {
                    rbtnPocketPC.Checked = true;
                }
                
            }
            else
            {
                return;
            }
            chkTimeSync.Checked = false;
            chkScripting.Checked = false;
            chkNavigatie.Checked = false;
            if(rbtnPocketPC.Checked && File.Exists(strSelectedPath + @"\FVI\COPY\Program files\FVDnet\Tensing.Positioning.Gps.dll"))
            {
                chkNavigatie.Checked = true;
                rbtnGPS.Checked = true;
            }
            XmlReader xrStartup = XmlReader.Create(strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, "StartUp.xml"));
            xrStartup.ReadToFollowing("Services");
            if (!xrStartup.IsEmptyElement)
            {
                while (xrStartup.ReadToFollowing("Service"))
                {
                    xrStartup.ReadToFollowing("type");
                    xrStartup.Read();
                    switch (xrStartup.Value)
                    {
                        case "TimeSync":
                            chkTimeSync.Checked = true;
                            xrStartup.ReadToFollowing("timeout");
                            if (!xrStartup.EOF)
                            {
                                xrStartup.Read();
                                txtTimeout.Text = xrStartup.Value;
                            }
                            else
                            {
                                xrStartup.Close();
                                xrStartup = XmlReader.Create(strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, "StartUp.xml"));
                                xrStartup.ReadToFollowing("Services");
                                xrStartup.ReadToFollowing("Service");
                                xrStartup.ReadToFollowing("type");
                                txtTimeout.Text = "4000";
                            }
                            xrStartup.ReadToFollowing("server");
                            xrStartup.Read();
                            txtTimeServer.Text = xrStartup.Value;
                            break;
                        case "Scripting":
                            chkScripting.Checked = true;
                            xrStartup.ReadToFollowing("scriptingDll");
                            xrStartup.Read();
                            txtScriptingDLL.Text = xrStartup.Value;
                            strScriptingDll = xrStartup.Value;
                            break;
                        case "CommReceive":
                            chkScripting.Checked = true;
                            xrStartup.ReadToFollowing("commReceiveDll");
                            xrStartup.Read();
                            txtCommunicationDLL.Text = xrStartup.Value;
                            break;
                        case "Navigation":
                            chkNavigatie.Checked = true;
                            xrStartup.ReadToFollowing("wrapperdll");
                            xrStartup.Read();
                            switch (xrStartup.Value)
                            {
                                case "Tensing.Navigation.Wrappers.TomTom6.dll":
                                case "Tensing.Navigation.Wrappers.TomTom.dll":
                                    rbtnTomTom6.Checked = true;
                                    break;
                                case "Tensing.Navigation.Wrappers.TomTom5.dll":
                                    rbtnTomTom5.Checked = true;
                                    break;
                                case "Tensing.Navigation.Wrappers.CoPilot7.dll":
                                    rbtnCoPilot7.Checked = true;
                                    break;
                                case "Tensing.Navigation.Wrappers.CoPilot8.dll":
                                    rbtnCoPilot8.Checked = true;
                                    break;
                                case "Tensing.Navigation.Wrappers.Gps.dll":
                                    rbtnGPS.Checked = true;
                                    break;
                            }
                            break;
                    }
                }
            }

            #endregion

            #region CheckSettings

            // Get all screen version info
            string strDate;
            string strDatabase;
            string strSolution;
            string strServer;
            UpdateLib.UpdateLib.GetSupportInfoScreen(strSelectedPath, UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked), out strDate, out strDatabase, out strSolution, out strServer);

            lstMessages.Items.Add("screen version info");
            if (strDate != null) lstMessages.Items.Add(string.Format("{0}", strDate));
            if (strDatabase != null) lstMessages.Items.Add(string.Format("{0}", strDatabase));
            if (strSolution != null) lstMessages.Items.Add(string.Format("Solution: {0}", strSolution));
            if (strServer != null) lstMessages.Items.Add(string.Format("{0}", strServer));
            strCurrentDomainBasePath = strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked);
            XmlReader xrScreen = XmlReader.Create(strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, "Screen.xml"));

            // Check Start and end screen from agenda.xml
            XmlReader xrAgenda = XmlReader.Create(strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, "Agenda.xml"));
            xrAgenda.ReadToFollowing("Screens");
            if (!xrAgenda.IsEmptyElement)
            {
                xrAgenda.ReadToFollowing("start");
                xrAgenda.Read();
                string strStartScreen = xrAgenda.Value;
                xrAgenda.ReadToFollowing("end");
                xrAgenda.Read();
                string strEndScreen = xrAgenda.Value;
                if (!(strStartScreen == ""))
                {
                    // Check all screen id's from screen
                    xrScreen.ReadToFollowing("Screens");
                    if (!xrScreen.IsEmptyElement)
                    {
                        bool blnDoesStartScreenExist = false;
                        bool blnDoesEndScreenExist = false;
                        while (xrScreen.ReadToFollowing("Screen"))
                        {
                            xrScreen.ReadToFollowing("id");
                            xrScreen.Read();
                            if (!blnDoesStartScreenExist)
                            {
                                blnDoesStartScreenExist = strStartScreen == xrScreen.Value;
                            }
                            if (!blnDoesEndScreenExist)
                            {
                                blnDoesEndScreenExist = strEndScreen == xrScreen.Value;
                            }
                        }
                        if (!blnDoesEndScreenExist || !blnDoesStartScreenExist)
                        {
                            lstMessages.Items.Add(string.Format("End screen or start screen does not exist. Start screen {0} is {1} and end screen {2} is {3}.", strStartScreen, blnDoesStartScreenExist ? "present" : "missing", strEndScreen, blnDoesEndScreenExist ? "present" : "missing"));
                        }
                    }
                }
            }
            xrScreen.Close();
            xrScreen = XmlReader.Create(strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, "Screen.xml"));
            lstMessages.Items.Add("");
            chkPrint.Checked = false;
            chkMessaging.Checked = false;
            while (xrScreen.Read())
            {
                if (Regex.IsMatch(xrScreen.Value, @"Print\([0-9][0-9]?\);"))
                {
                    chkPrint.Checked = true;
                }
                if (Regex.IsMatch(xrScreen.Value, @"""InstantMessaging""\)\.StartIMTimer\(\);"))
                {
                    chkMessaging.Checked = true;
                }
            }
            xrScreen.Close();
            xrScreen = XmlReader.Create(strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, "Screen.xml"));
            if (strScriptingDll != null)
            {
                try
                {
                    Assembly assemblyScripting = Assembly.LoadFrom(strSelectedPath + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, strScriptingDll));
                    objScreenConfiguration = new ScreenLib.ScreenConfiguration(xrScreen, assemblyScripting);
                }
                catch (Exception e)
                {
                    lstMessages.Items.Add(strScriptingDll + " failed to load.");
                    lstMessages.Items.Add("Exception: " + e.Message);
                    return;
                }
            }
            else
            {
                objScreenConfiguration = new ScreenLib.ScreenConfiguration(xrScreen);
            }
            xrAgenda.Close();
            xrStartup.Close();
            DateTime.Now.ToString();
            #endregion
        }

        private void cmdSelectNewDir_Click(object sender, EventArgs e)
        {
            if (dlgFolderNew != null)
            {
                if (Directory.Exists(txtNewDir.Text))
                {
                    dlgFolderNew.SelectedPath = txtNewDir.Text;
                }
                if (dlgFolderNew.ShowDialog() == DialogResult.OK)
                {
                    txtNewDir.Text = dlgFolderNew.SelectedPath;
                }
            }
        }

        private void cmdSelectOutputDir_Click(object sender, EventArgs e)
        {
            if (dlgFolderOutput != null)
            {
                if (Directory.Exists(txtOutputDir.Text))
                {
                    dlgFolderOutput.SelectedPath = txtOutputDir.Text;
                }
                if (dlgFolderOutput.ShowDialog() == DialogResult.OK)
                {
                    txtOutputDir.Text = dlgFolderOutput.SelectedPath;
                }
            }
        }

        private void cmdGo_Click(object sender, EventArgs e)
        {
            LogManager.GetLogger("LogFile").Info("cmdGo_Click");
            initLib.Save(strConfigPath);
            bool blnUpgradeInstaller;
            
            blnUpgradeInstaller = (File.Exists(txtNewDir.Text + @"\SD\Mobile\Client PocketPC\Mobile 5 and 6\FVI\TensingInstall.xml"));
            bool blnIsForDesktop = rbtnDesktop.Checked;
            string strOutputDir = txtOutputDir.Text;
            bool blnHasNavigationService = chkNavigatie.Checked;
            bool blnIsPpc2003Device = rbtnPPC2003.Checked;
            string strOldDir = txtOldDir.Text;
            string strStorageCardRootName = ((Device)cbDevice.SelectedItem).StorageCardRootName;
            bool blnFramework35 = rbtnFramework35.Checked;
            string strPathOldTransporter = UpdateLib.UpdateLib.GetStrPathTransporter(blnIsForDesktop, blnIsForDesktop ? @"FVcommPC.xml" : @"FVcommCE.xml");
            string strPathNewTransporter = UpdateLib.UpdateLib.GetStrPathTransporter(blnIsForDesktop, blnIsForDesktop ? @"TransporterClientPC.xml" : @"TransporterClientPda.xml");
            string strPathColor = UpdateLib.UpdateLib.GetStrPathFVDNet(blnIsForDesktop, @"Color.xml");
            string strPathLanguage = UpdateLib.UpdateLib.GetStrPathFVDNet(blnIsForDesktop, @"Language.xml");
            string strPathFVDNet = UpdateLib.UpdateLib.GetStrPathFVDNet(blnIsForDesktop, @"StartUp.xml");

            Directory.CreateDirectory(strOutputDir);
            var proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + (blnIsForDesktop ? @"UpdateFVPC.bat" : @"UpdateFV.bat");
            string strNewDir = FilesystemLib.GetStrNewDir(blnIsForDesktop, rbtnWindowsCE.Checked, rbtnPocketPC.Checked, blnIsPpc2003Device, rbtnWM50WM60.Checked, txtNewDir.Text);
            proc.StartInfo.Arguments = string.Format("{0} {1} {2} {3} {4}", FilesystemLib.GetShortPathName(ref strOldDir), FilesystemLib.GetShortPathName(ref strNewDir), FilesystemLib.GetShortPathName(ref strOutputDir), DateTime.Now.ToString("yyyyMMddhhmm"), blnUpgradeInstaller);
            proc.Start();
            proc.WaitForExit(750000);
            string strNewDirRoot = txtNewDir.Text;
            if (blnHasNavigationService && !blnIsForDesktop)
            {
                var proc2 = new Process();
                proc2.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "UpdateNavigationFV.bat";
                string strArgument = "unknown";
                if (rbtnTomTom5.Checked) strArgument = "TomTom5";
                if (rbtnTomTom6.Checked) strArgument = "TomTom6";
                if (rbtnCoPilot7.Checked) strArgument = "CoPilot7";
                if (rbtnCoPilot8.Checked) strArgument = "CoPilot8";
                if (rbtnGPS.Checked) strArgument = "GPS";
                proc2.StartInfo.Arguments = string.Format("{0} {1} {2}", strArgument, FilesystemLib.GetShortPathName(ref strNewDirRoot), FilesystemLib.GetShortPathName(ref strOutputDir), FilesystemLib.GetShortPathName(ref strOldDir));
                proc2.Start();
                proc2.WaitForExit(1000);
            }
            UpdateLib.UpdateLib.UpdateStartUpXML(strOldDir + strPathFVDNet, strOutputDir + strPathFVDNet, strNewDir + strPathFVDNet, blnHasNavigationService, chkTimeSync.Checked, txtTimeout.Text, txtTimeServer.Text, strStorageCardRootName, chkScripting.Checked, txtScriptingDLL.Text, chkCommunication.Checked, txtCommunicationDLL.Text);
            const string strPathAutorunXML = @"\2577\autorun.xml";
            UpdateLib.UpdateLib.UpdateAutorunXML(strOldDir + strPathAutorunXML, strOutputDir + strPathAutorunXML, strNewDir + strPathAutorunXML, strStorageCardRootName);
            if (rbtnTransporter.Checked)
            {
                string oldTransporter = strOldDir + strPathOldTransporter;
                string outputFile = strOutputDir + strPathOldTransporter;
                string newTransporter = strNewDir + strPathOldTransporter;
                if (!File.Exists(oldTransporter))
                {
                    oldTransporter = strOldDir + strPathNewTransporter;
                }
                if (!File.Exists(newTransporter))
                {
                    outputFile = strOutputDir + strPathNewTransporter;
                    newTransporter = strNewDir + strPathNewTransporter;
                }
                UpdateLib.UpdateLib.UpdateTransporterXML(oldTransporter, outputFile, newTransporter);
            }
            UpdateLib.UpdateLib.UpdateLanguageXML(strOldDir + strPathLanguage, strOutputDir + strPathLanguage, strNewDir + strPathLanguage);

            UpdateLib.UpdateLib.UpdateColorXML(strOldDir + strPathColor, strOutputDir + strPathColor, strNewDir + strPathColor);
            UpdateLib.UpdateLib.UpdateScreenXML(strOldDir + UpdateLib.UpdateLib.GetStrPathFVDNet(blnIsForDesktop, @"Screen.xml"), strOutputDir + UpdateLib.UpdateLib.GetStrPathFVDNet(blnIsForDesktop, @"Screen.xml"));
            UpdateLib.UpdateLib.EnumMobileVersion enumMobileVersion = UpdateLib.UpdateLib.EnumMobileVersion.MvWM50;
            if (chkPrint.Checked && !blnIsForDesktop)
            {
                var proc6 = new Process {StartInfo = {FileName = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "UpdateFVPrint.bat"), Arguments = string.Format("{0} {1}", FilesystemLib.GetShortPathName(ref strNewDirRoot), FilesystemLib.GetShortPathName(ref strOutputDir))}};
                proc6.Start();
                proc6.WaitForExit(1000);
            }
            if (chkMessaging.Checked && !blnIsForDesktop)
            {
                var proc4 = new Process {StartInfo = {FileName = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "UpdateFVMessaging.bat"), Arguments = string.Format("{0} {1}", FilesystemLib.GetShortPathName(ref strNewDirRoot), FilesystemLib.GetShortPathName(ref strOutputDir))}};
                proc4.Start();
                proc4.WaitForExit(1000);
            }
            if (!blnIsForDesktop)
            {
                // Iterate through all directories and update shortcuts
                const string strWindowsPart = @"\FVI\COPY\Windows";
                UpdateLib.UpdateLib.ProcessDirUpdateShortCut(strOutputDir + strWindowsPart, 0);
                if (blnIsPpc2003Device)
                {
                    enumMobileVersion = UpdateLib.UpdateLib.EnumMobileVersion.MvPPC2003;
                }
                if (blnUpgradeInstaller)
                {
                    UpdateLib.UpdateLib.UpgradeFVInstaller(strOldDir + @"\FVI\FVInstallerCE.ini", strOutputDir + @"\FVI\TensingInstall.xml", enumMobileVersion, blnFramework35);
                }
                else
                {
                    UpdateLib.UpdateLib.UpdateFVInstaller(strOldDir + @"\FVI\FVInstallerCE.ini", strOutputDir + @"\FVI\FVInstallerCE.ini", enumMobileVersion, blnFramework35);
                }
                UpdateLib.UpdateLib.RemoveRedundantCab(strOutputDir + @"\FVI\CAB\", enumMobileVersion);
            }
            if (chkComm2Serv.Checked && !blnIsForDesktop)
            {
                var proc5 = new Process {StartInfo = {FileName = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "UpdateFVComm2Serv.bat"), Arguments = string.Format("{0} {1}", FilesystemLib.GetShortPathName(ref strNewDirRoot), FilesystemLib.GetShortPathName(ref strOutputDir))}};
                proc5.Start();
                proc5.WaitForExit(1000);
            }
            if (chkCreateScreen.Checked)
            {
                LogManager.GetLogger("LogFile").Info("cmdGo_Click::Create new screen and remove old settings");
                //Create new screen and remove old settings
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\settings.txt"))
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\settings.txt");
                var objStreamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\fcconfig.txt", false);
                string path = txtOutputDir.Text;
                string strDate;
                string strDatabase;
                string strSolution;
                string strServer;
                UpdateLib.UpdateLib.GetSupportInfoScreen(txtOldDir.Text, UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked), out strDate, out strDatabase, out strSolution, out strServer);
                objStreamWriter.WriteLine(strServer.ToLower().Replace("on ", ""));
                objStreamWriter.WriteLine(strServer.ToLower().Replace("MultipleActiveResultSetsyes", "") == "on srv-db-02\\srvdb02_screen" ? "con" : "SSPI");
                objStreamWriter.WriteLine(strServer.ToLower().Replace("MultipleActiveResultSetsyes", "") == "on srv-db-02\\srvdb02_screen" ? "Fieldv!si0n" : "");
                objStreamWriter.WriteLine(strDatabase.Replace("db ", ""));
                if (chkCreateSDF.Checked)
                {
                    objStreamWriter.WriteLine(txtSDFServerName.Text);
                    objStreamWriter.WriteLine(txtSDFUsername.Text);
                    objStreamWriter.WriteLine(txtSDFPassword.Text);
                    objStreamWriter.WriteLine(txtSDFDatabase.Text);
                }
                objStreamWriter.Close();

                var proc3 = new Process();
                proc3.StartInfo.FileName = strCreateScreenURI;
                proc3.StartInfo.Arguments =
                    string.Format(@" -P -s ""Password=Fieldv!si0n;Persist Security Info=True;User ID=con;Initial Catalog={1};Data Source=srv-db-02\srvdb02_screen;MultipleActiveResultSets=yes"" -d ""Password={3};Persist Security Info=True;User ID={5};Initial Catalog={2};Data Source={4}"" -c Planon -p ""{0}", txtOutputDir.Text + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked), strDatabase.Replace("db ", ""),txtSDFDatabase.Text,txtSDFPassword.Text,txtSDFServerName.Text,txtSDFUsername.Text);
                proc3.Start();
                proc3.WaitForExit();

                //LogManager.GetLogger("LogFile").Info("strCreateScreenURI:" + strCreateScreenURI);
                //assemblyCreateScreen = Assembly.LoadFrom(strCreateScreenURI);
                //Type typeMain = assemblyCreateScreen.GetType("Tensing.FieldVision.CreateScreen.frmMain");
                //frmCreateScreen = (Form) assemblyCreateScreen.CreateInstance(typeMain.FullName);
                //dtmStartThread = DateTime.Now;
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::QueueUserWorkItem:frmCreateScreen");
                //ThreadPool.QueueUserWorkItem(RunApp, frmCreateScreen);
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::StartProcess:InitCreateScreenPocketPC.bat");
                //var proc = new Process {StartInfo = {FileName = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "InitCreateScreenPocketPC.bat"), Arguments = string.Format("{0}", FilesystemLib.GetShortPathName(ref path))}};
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::proc.Start()");
                //proc.Start();
                //proc.WaitForExit(1000);
                //bool blnInvokeRequired = frmCreateScreen.InvokeRequired;
                //InvokeMethod(frmCreateScreen, "buttonSelectSolutionScreen_Click", new object[] {this, new EventArgs()});
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::CheckForIllegalCrossThreadCalls");
                //CheckForIllegalCrossThreadCalls = false;
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::Set txtOutputDir - GetStrPathFVDNet");
                //var txtFolder = ((TextBox) GetField(frmCreateScreen, "txtFolder"));
                //txtFolder.Text = txtOutputDir.Text + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked);
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::Set Solution - GetSolutionFromScreen");
                //var cSolution = (ComboBox) GetField(frmCreateScreen, "cSolution");
                //strSolution = UpdateLib.UpdateLib.GetSolutionFromScreen(txtOldDir.Text, UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked));
                //int i = 0;
                ////((FieldInfo)GetField(frmCreateScreen, "_connectionStringScreen")).SetValue(frmCreateScreen,@"Password=Fieldv!si0n;Persist Security Info=True;User ID=con;Initial Catalog=MobileWorkflow_FSS-dev_screen;Data Source=srv-db-02\srvdb02_screen;MultipleActiveResultSets=yes");
                ////((FieldInfo)GetField(frmCreateScreen, "_connectionStringSdf")).SetValue(frmCreateScreen, @"Password=Fieldv!si0n;Persist Security Info=True;User ID=con;Initial Catalog=MobilityPlatform_FSS_ontw;Data Source=srv-db-02\srvdb02_2008;MultipleActiveResultSets=yes");
                ////((FieldInfo)GetField(frmCreateScreen, "_connectionStringCommDLL")).SetValue(frmCreateScreen, @"Password=Fieldv!si0n;Persist Security Info=True;User ID=con;Initial Catalog=MobilityPlatform_FSS_ontw;Data Source=srv-db-02\srvdb02_2008;MultipleActiveResultSets=yes");
                ////((ComboBox) GetField(frmCreateScreen, "comboBoxData")).SelectedIndex = 1;
                //foreach (object item in cSolution.Items)
                //{
                //    LogManager.GetLogger("LogFile").Info("cmdGo_Click::Set Solution - Item is in the form '{index} {solution name}'. Where index starts at 1 instead of 0");
                //    //Item is in the form '{index} {solution name}'. Where index starts at 1 instead of 0
                //    if (item.ToString().EndsWith(strSolution))
                //    {
                //        cSolution.SelectedIndex = i;
                //        break;
                //    }
                //    i++;
                //}
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::Check Scripting");
                //if (chkScripting.Checked)
                //{
                //    var txtFileName = ((TextBox) GetField(frmCreateScreen, "txtFileName"));
                //    txtFileName.Text = txtScriptingDLL.Text;
                //    ((CheckBox) GetField(frmCreateScreen, "cbxNewScripting")).Checked = true;
                //}
                //else
                //{
                //    ((CheckBox) GetField(frmCreateScreen, "cbxNewScripting")).Checked = false;
                //}
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::Set  checkBoxDesktop");
                //((CheckBox) GetField(frmCreateScreen, "checkBoxDesktop")).Checked = rbtnDesktop.Checked;
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::Set  checkBoxDesktop");
                //((CheckBox) GetField(frmCreateScreen, "checkBoxPocketPC")).Checked = !rbtnDesktop.Checked;
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::Set  checkBoxDesktop");
                //((CheckBox) GetField(frmCreateScreen, "checkBoxCreateSDF")).Checked = chkCreateSDF.Checked;
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //Thread.Sleep(0);
                //LogManager.GetLogger("LogFile").Info("cmdGo_Click::Create SDF");
                //if (chkCreateSDF.Checked)
                //{
                //    InvokeMethod(frmCreateScreen, "buttonSelectData_Click", new object[] { this, new EventArgs() });
                //}
                //InvokeMethod(frmCreateScreen, "btnStart_Click", new object[] {this, new EventArgs()});
                //CheckForIllegalCrossThreadCalls = true;
                //timCheckReady = new Timer(Callback, frmCreateScreen, 0, 100);
            }
        }

        private void Callback(object obj)
        {
            ProgressBar pComponents = ((ProgressBar)GetField(obj, "pComponents"));
            ProgressBar pForms = ((ProgressBar)GetField(obj, "pForms"));
            ProgressBar pFrames = ((ProgressBar)GetField(obj, "pFrames"));
            ProgressBar progressBarSdf = ((ProgressBar)GetField(obj, "progressBarSdf"));
            if((pForms.Value == pForms.Maximum) &&
                (pComponents.Value == pComponents.Maximum) &&
                (pFrames.Value == pFrames.Maximum))
            {
                if(chkCreateSDF.Checked && (progressBarSdf.Value != progressBarSdf.Maximum))
                {
                    return;
                }
                else
                {
                    if (((Button)GetField(obj, "btnStart")).Enabled)
                    {
                        timCheckReady.Dispose();
                        this.lstMessages.Invoke(new AddMessageDelegate(AddMessage), new object[] { "Screen created." });
                        this.OnScreenCreated(this, new EventArgs());
                    }
                }
            }
        }

        private delegate void AddMessageDelegate(string text); 
        private void AddMessage(string text)
        {
            lstMessages.Items.Add(text);
        }

        private void rbtnDesktop_CheckedChanged(object sender, EventArgs e)
        {
           // pnlMobileSettings.Visible = false;
        }

        private void rbtnPocketPC_CheckedChanged(object sender, EventArgs e)
        {
          //  pnlMobileSettings.Visible = true;
        }

        private void rbtnWindowsCE_CheckedChanged(object sender, EventArgs e)
        {
            pnlMobileSettings.Visible = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(strDeviceSettingsPath))
            {
                colDeviceSettings = new DeviceSettings(strDeviceSettingsPath);
                cbDevice.DataSource = colDeviceSettings;
            }
            if (File.Exists(strConfigPath))
            {
                initLib.Load(strConfigPath);
            }
        }

        private void chkNavigatie_CheckedChanged(object sender, EventArgs e)
        {
            pnlNavigation.Visible = ((CheckBox) sender).Checked;
        }

        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            initLib.Load(((Device) cbDevice.SelectedItem));
        }

        private void chkScripting_CheckedChanged(object sender, EventArgs e)
        {
            pnlScripting.Visible = ((CheckBox) sender).Checked;
        }

        private void txtOldDir_TextChanged(object sender, EventArgs e)
        {
            string strOldDir = ((TextBox) sender).Text;
            ExtractAndCheckSettingsFromOldDir(strOldDir);
            if (strOldDir.EndsWith("\\Old") || strOldDir.EndsWith("\\Old\\"))
            {
                txtOutputDir.Text = strOldDir.Replace("\\Old", "\\Out" + DateTime.Now.ToString("hhmm"));
            }
        }

        private void chkTimeSync_CheckedChanged(object sender, EventArgs e)
        {
            pnlTimeSync.Visible = ((CheckBox) sender).Checked;
        }


        private void btnSetCreateScreen_Click(object sender, EventArgs e)
        {
            if (!rbtnDesktop.Checked)
            {
                string strOutputDir = txtOutputDir.Text;
                var proc5 = new Process {StartInfo = {FileName = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "") + "CreateScreenPocketPC.bat"), Arguments = string.Format("{0}", FilesystemLib.GetShortPathName(ref strOutputDir))}};
                proc5.Start();
                proc5.WaitForExit(1000);
            }
        }


        private void btnCompare_Click(object sender, EventArgs e)
        {
            var proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = @"C:\Program Files (x86)\IDM Computer Solutions\UltraCompare\UC.exe";
            string strOldDir = txtOldDir.Text;
            string strOutputdir = txtOutputDir.Text;
            proc.StartInfo.Arguments = string.Format(@"-d {0} {1}", FilesystemLib.GetShortPathName(ref strOldDir), FilesystemLib.GetShortPathName(ref strOutputdir));
            proc.Start();
        }

        private void chkCommunication_CheckedChanged(object sender, EventArgs e)
        {
            pnlCommunication.Visible = ((CheckBox) sender).Checked;
        }

        private void btnCopyScreen_Click(object sender, EventArgs e)
        {
            var colColumnInfos = new Collection<ScreenLib.ColumnInfo>();
            try
            {
                var objConnectDB = new ConnectDB();
                if(this.txtSDFDatabase.Text != "")
                {
                    objConnectDB.ConnectionString =
                        String.Format(
                            @"Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3};MultipleActiveResultSets=yes",
                            txtSDFPassword.Text, txtSDFUsername.Text, txtSDFDatabase.Text, txtSDFServerName.Text);
                }
                if (objConnectDB.ShowDialog() == DialogResult.OK)
                {
                    using (var objConnection = new SqlConnection(objConnectDB.ConnectionString))
                    {
                        objConnection.Open();
                        var objCommand = new SqlCommand("SELECT ColumnName, TableName, 'TN' + TypeName AS TypeName, TypeLength FROM fv_datadictionary", objConnection);
                        using (SqlDataReader objReader = objCommand.ExecuteReader())
                        {
                            if (objReader != null)
                                while (objReader.Read())
                                {
                                    colColumnInfos.Add(new ScreenLib.ColumnInfo {ColumnName = objReader["ColumnName"].ToString(), TableName = objReader["TableName"].ToString(), TypeName = (ScreenLib.enumTypeName) Enum.Parse(typeof (ScreenLib.enumTypeName), objReader["TypeName"].ToString()), TypeLength = int.Parse(objReader["TypeLength"].ToString())});
                                }
                            if (objReader != null) objReader.Close();
                        }
                        objConnection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                LogManager.GetLogger("LogFile").Error("btnCopyScreen_Click caught : " + exception.Message);
                btnCopyScreen_Click(sender, e);
            }
            if (colColumnInfos.Count > 0)
            {
                IEnumerable<ScreenLib.VarcharColumn> varcharItems = from a in colColumnInfos where a.TypeName == ScreenLib.enumTypeName.TNvarchar || a.TypeName == ScreenLib.enumTypeName.TNnvarchar select new ScreenLib.VarcharColumn(a.ColumnName, a.TableName, a.TypeLength);
                objScreenConfiguration.OnTextboxChecked += objScreenConfiguration_OnTextboxChecked;
                objScreenConfiguration.OnSqlChecked += objScreenConfiguration_OnSqlChecked;
                objScreenConfiguration.CheckTextBoxLengths(varcharItems);
                objScreenConfiguration.CheckSql(colColumnInfos);
            }
        }

        private void objScreenConfiguration_OnSqlChecked(object sender, ScreenLib.ScreenConfiguration.EventArgsSqlChecked e)
        {
            lstMessages.Items.Add(string.Format("SQL query uit Screen {0} met tekst {1} bevat een alias {2} die overeenkomt met een colom naam {3}.", e.Screen, e.Query, e.AliasName, e.ColumnName));
        }

        private void objScreenConfiguration_OnTextboxChecked(object sender, ScreenLib.ScreenConfiguration.EventArgsTextboxChecked e)
        {
            Debug.Print(string.Format("Textbox component uit Screen {0} Frame {1} met id {2}", e.Screen, e.Frame, e.Component));
            Debug.Print("Wordt gebruikt in de volgende query:");
            Debug.Print(e.Query);
            Debug.Print("En heet de volgende lengte:");
            Debug.Print(e.ComponentLength.ToString());
            Debug.Print("Met de volgende varcharvelden:");
            Debug.Print(e.ColumnName);
            Debug.Print(e.ColumnLength.ToString());
            lstMessages.Items.Add(string.Format("Textbox component uit Screen {0} Frame {1} met id {2}", e.Screen, e.Frame, e.Component));
            lstMessages.Items.Add("Wordt gebruikt in de volgende query:");
            lstMessages.Items.Add(e.Query);
            lstMessages.Items.Add("En heet de volgende lengte:");
            lstMessages.Items.Add(e.ComponentLength.ToString());
            lstMessages.Items.Add("Met de volgende varcharvelden:");
            lstMessages.Items.Add(e.ColumnName);
            lstMessages.Items.Add(e.ColumnLength.ToString());

            var matchInsertStatement = new Regex(@"insert[^i]*into(?<table>[^\(]*)\((?<fields>[^\)]*)\)(?<values>.*)", RegexOptions.IgnoreCase);
            Match objMatchInsertStatement = matchInsertStatement.Match(e.Query);
            if (objMatchInsertStatement.Success)
            {
                Debug.Print(objMatchInsertStatement.Groups["table"].Value);
                Debug.Print(objMatchInsertStatement.Groups["fields"].Value);
            }
            var matchUpdateStatement = new Regex(@"update\s*(?<table>[^\s]*)\s*set\s*(?<keyvalues>.*)", RegexOptions.IgnoreCase);
            Match objMatchUpdateStatement = matchUpdateStatement.Match(e.Query);
            if (objMatchUpdateStatement.Success)
            {
                Debug.Print(objMatchInsertStatement.Groups["table"].Value);
                Debug.Print(objMatchInsertStatement.Groups["keyvalues"].Value);
            }
        }

        private void txtNewDir_TextChanged(object sender, EventArgs e)
        {
            string strNewDir = ((TextBox) sender).Text;
            ExtractAndCheckSettingsNewDir(strNewDir);
        }

        private void ExtractAndCheckSettingsNewDir(string strNewDir)
        {
            strNewDir = FilesystemLib.GetStrNewDir(rbtnDesktop.Checked, rbtnWindowsCE.Checked, rbtnPocketPC.Checked, rbtnPPC2003.Checked, rbtnWM50WM60.Checked, strNewDir);
            string strMobileCore = strNewDir + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, "mobilecore.dll");
            if (File.Exists(strMobileCore))
            {
                Assembly objMobileCore = Assembly.LoadFile(strMobileCore);
                if (objMobileCore.ImageRuntimeVersion == "v2.0.50727")
                {
                    rbtnFramework35.Checked = true;
                }
                else
                {
                    rbtnFramework11.Checked = true;
                }
            }
        }

        private void btnQC_Click(object sender, EventArgs e)
        {
            if (txtScriptingDLL.Text != "")
            {
                GetVersion(txtOutputDir.Text + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, txtScriptingDLL.Text));
            }
        }

        private void GetVersion(string strFilename)
        {
            GetVersion(strFilename, "");
        }

        private void GetVersion(string strFilename, string reference)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(strFilename);
            }
            catch (Exception)
            {
                return;
            }
            if (assembly != null)
            {
                Debug.WriteLine("VER:" + assembly.GetName().ToString());
                lstMessages.Items.Add(assembly.GetName().ToString());
                if((reference != "") && (reference != assembly.GetName().ToString()))
                {
                    colListboxWarn.Add(lstMessages.Items.Count-1);
                }
            }
            GetReferenceAssembly(assembly);
            assembly = null;
        }


        private void GetReferenceAssembly(Assembly assembly)
        {
            try
            {
                AssemblyName[] list = assembly.GetReferencedAssemblies();
                foreach (AssemblyName item in list)
                {
                    if (item.Name != "mscorlib")// && !item.Name.StartsWith("System"))
                    {
                        string strFilename = txtOutputDir.Text + UpdateLib.UpdateLib.GetStrPathFVDNet(rbtnDesktop.Checked, item.Name + ".dll");
                        if (File.Exists(strFilename))
                        {
                            Debug.WriteLine("REF:" + item.ToString());
                            lstMessages.Items.Add("REF:" + item.ToString());
                            GetVersion(strFilename, item.ToString().Replace(", Retargetable=Yes", ""));
                        }
                        else
                        {
                            if (!item.Name.StartsWith("System"))
                            {
                                Debug.WriteLine(item.ToString() + " is missing.");
                                lstMessages.Items.Add(item.Name + " is missing.");
                                colListboxWarn.Add(lstMessages.Items.Count - 1);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void lstMessages_DrawItem(object sender, DrawItemEventArgs e)
        {
            //
            // Draw the background of the ListBox control for each item.
            // Create a new Brush and initialize to a Black colored brush
            // by default.
            //
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            //
            // Determine the color of the brush to draw each item based on 
            // the index of the item to draw.
            //
            if(colListboxWarn.Contains(e.Index))
            {
                myBrush = Brushes.Red;
            }
            //
            // Draw the current item text based on the current 
            // Font and the custom brush settings.
            //
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            //
            // If the ListBox has focus, draw a focus rectangle 
            // around the selected item.
            //
            e.DrawFocusRectangle();
        }
        private static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < currentAssemblies.Length; i++)
            {
                if (currentAssemblies[i].FullName == args.Name)
                {
                    return currentAssemblies[i];
                }
            }

            return FindAssembliesInDirectory(args.Name, strCurrentDomainBasePath);
        }

        private static Assembly FindAssembliesInDirectory(string assemblyName, string directory)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                Assembly assm;

                if (TryLoadAssemblyFromFile(file, assemblyName, out assm))
                    return assm;
            }

            return null;
        }

        private static bool TryLoadAssemblyFromFile(string file, string assemblyName, out Assembly assm)
        {
            try
            {
                // Convert the filename into an absolute file name for 
                // use with LoadFile. 
                file = new FileInfo(file).FullName;

                if (AssemblyName.GetAssemblyName(file).FullName == assemblyName)
                {
                    assm = Assembly.LoadFile(file);
                    return true;
                }
            }
            catch
            {
                /* Do Nothing */
            }
            assm = null;
            return false;
        }

        private void pnlCommunication_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkCreateSDF_CheckedChanged(object sender, EventArgs e)
        {
            pnlCreateSDF.Visible = chkCreateSDF.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog != null)
            {
                if (Directory.Exists(strCustomConfigFile))
                {
                    saveFileDialog.InitialDirectory = strCustomConfigFile;
                }
                if (File.Exists(strCustomConfigFile))
                {
                    saveFileDialog.FileName = strCustomConfigFile;
                } else
                {
                    saveFileDialog.FileName = strConfigPath;
                }
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    strCustomConfigFile = saveFileDialog.FileName;
                    initLib.Save(strCustomConfigFile);
                }
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initLib.Save(strConfigPath);
            if (File.Exists(strCustomConfigFile))
            {
               initLib.Save(strCustomConfigFile);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(strCustomConfigFile))
            {
                openFileDialog.FileName = strCustomConfigFile;
            }
            else
            {
                openFileDialog.FileName = strConfigPath;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                initLib.Load(openFileDialog.FileName);
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void generateMSTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(@"generateMSTestToolStripMenuItem_Click");
            objScreenConfiguration.GenerateMSTest(@"C:\My Test\Generated test\");
        }

    }
}