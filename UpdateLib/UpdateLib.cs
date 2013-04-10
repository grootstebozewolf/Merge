using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace UpdateLib
{
    public class UpdateLib
    {
        public enum EnumMobileVersion
        {
            MvPPC2003,
            MvWM50,
            MvWM61
        }
        public static void UpdateStartUpXML(string strOldStartUp, string strOutputFile, string strNewStartUp, bool blnHasServiceNavigation, bool blnHasTimeSync, string strTimeSyncTimeout, string strTimeServer, string strStorageCardRootName, bool blnHasScripting, string strScriptingDLL, bool blnHasCommunication, string strCommunicationDLL)
        {
            const string strExcludeTagRegEx = @"\b(xmlversion|log-lines|log-files|dll|type|Service|timeout|params|server)";
            Hashtable objOldStartUpKeys = new Hashtable();
            ReadOldKeys(strOldStartUp, strExcludeTagRegEx, objOldStartUpKeys);
            using (XmlTextWriter objWriter = new XmlTextWriter(strOutputFile, Encoding.UTF8))
            {
                using (XmlTextReader objReader = new XmlTextReader(new StreamReader(strNewStartUp)))
                {
                    bool blnUseOldKey = false;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.None:
                                break;
                            case XmlNodeType.Element:
                                objWriter.WriteStartElement(objReader.Name);

                                if (objReader.HasAttributes)
                                {
                                    while (objReader.MoveToNextAttribute())
                                    {
                                        objWriter.WriteAttributeString(objReader.Name, objReader.Value);
                                    }
                                    // Move the reader back to the element node.
                                    objReader.MoveToElement();
                                }
                                if (!objReader.IsEmptyElement)
                                {
                                    if (objOldStartUpKeys.Contains(objReader.Name))
                                    {
                                        blnUseOldKey = true;
                                        string strValue = objOldStartUpKeys[objReader.Name].ToString();
                                        const string strValuesWithStorageCardRoot = @"\b(dbbackuppath|UpdateExePath|UpdateIniPath)";
                                        if (Regex.IsMatch(objReader.Name,strValuesWithStorageCardRoot))
                                        {
                                            strValue = SetStorageCardRootName(strValue, strStorageCardRootName);
                                        }
                                        objWriter.WriteString(strValue);
                                    }
                                    else
                                    {
                                        blnUseOldKey = false;
                                    }
                                }
                                if(objReader.Name == "Services")
                                {
                                    if (blnHasServiceNavigation)
                                    {
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteStartElement("Service");
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("type");
                                        objWriter.WriteString("Navigation");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("params");
                                        objWriter.WriteWhitespace(@"
            ");
                                        objWriter.WriteStartElement("wrapperdll");
                                        string strWrapperDLL = objOldStartUpKeys.Contains("wrapperdll") 
                                                                   ? objOldStartUpKeys["wrapperdll"].ToString() 
                                                                   : "Tensing.Navigation.Wrappers.TomTom5.dll";
                                        objWriter.WriteString(
                                            strWrapperDLL.Replace("Tensing.Navigation.Wrappers.TomTom6.dll", "Tensing.Navigation.Wrappers.TomTom.dll"));
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteEndElement();
                                    }
                                    if (blnHasTimeSync)
                                    {
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteStartElement("Service");
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("type");
                                        objWriter.WriteString("TimeSync");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("params");
                                        objWriter.WriteWhitespace(@"
            ");
                                        objWriter.WriteStartElement("timeout");
                                        objWriter.WriteString(
                                            strTimeSyncTimeout);
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
            ");
                                        objWriter.WriteStartElement("server");
                                        objWriter.WriteString(
                                            strTimeServer);
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteEndElement();
                                    }
                                    if (blnHasScripting)
                                    {
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteStartElement("Service");
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("type");
                                        objWriter.WriteString("Scripting");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("params");
                                        objWriter.WriteWhitespace(@"
            ");
                                        objWriter.WriteStartElement("scriptingDll");
                                        objWriter.WriteString(strScriptingDLL);
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteEndElement();
                                    }
                                    if (blnHasCommunication)
                                    {
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteStartElement("Service");
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("type");
                                        objWriter.WriteString("CommReceive");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteStartElement("params");
                                        objWriter.WriteWhitespace(@"
            ");
                                        objWriter.WriteStartElement("commReceiveDll");
                                        objWriter.WriteString(
                                            objOldStartUpKeys.Contains("commReceiveDll")
                                                ? objOldStartUpKeys["commReceiveDll"].ToString()
                                                : strCommunicationDLL);
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
         ");
                                        objWriter.WriteEndElement();
                                        objWriter.WriteWhitespace(@"
      ");
                                        objWriter.WriteEndElement();
                                    }
                                    while (!((objReader.NodeType == XmlNodeType.EndElement) && (objReader.Name == "Services")))
                                    {
                                        objReader.Read();
                                    }
                                    objWriter.WriteWhitespace(@"
   ");
                                    objWriter.WriteEndElement();
                                }

                                if (objReader.IsEmptyElement)
                                {
                                    objWriter.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Attribute:
                                break;
                            case XmlNodeType.Text:
                                if (!blnUseOldKey)
                                {
                                    objWriter.WriteString(objReader.Value);
                                }
                                break;
                            case XmlNodeType.CDATA:
                                break;
                            case XmlNodeType.EntityReference:
                                break;
                            case XmlNodeType.Entity:
                                break;
                            case XmlNodeType.ProcessingInstruction:
                                break;
                            case XmlNodeType.Comment:
                                objWriter.WriteComment(objReader.Value);
                                break;
                            case XmlNodeType.Document:
                                break;
                            case XmlNodeType.DocumentType:
                                break;
                            case XmlNodeType.DocumentFragment:
                                break;
                            case XmlNodeType.Notation:
                                break;
                            case XmlNodeType.Whitespace:
                                objWriter.WriteWhitespace(objReader.Value);
                                break;
                            case XmlNodeType.SignificantWhitespace:
                                break;
                            case XmlNodeType.EndElement:
                                objWriter.WriteEndElement();
                                break;
                            case XmlNodeType.EndEntity:
                                break;
                            case XmlNodeType.XmlDeclaration:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }

        private static string SetStorageCardRootName(string strValue, string strStorageCardRootName)
        {
            strValue = strValue.Replace("Storage Card", strStorageCardRootName);
            strValue = strValue.Replace("storage card", strStorageCardRootName);
            strValue = strValue.Replace("MMC Card", strStorageCardRootName);
            strValue = strValue.Replace("mmc card", strStorageCardRootName);
            return strValue;
        }

        private static void ReadOldKeys(string strOldXmlFile, IDictionary objOldXmlKeys)
        {
            const string strDoNotExclude = @"!";
            ReadOldKeys(strOldXmlFile, strDoNotExclude, objOldXmlKeys);
        }

        private static void ReadOldKeys(string strOldXmlFile, string strExcludeTagRegEx, IDictionary objOldXmlKeys)
        {
            using (XmlTextReader objReader = new XmlTextReader(new StreamReader(strOldXmlFile)))
            {
                string strName = objReader.Name;
                bool blnIsKeyRead = false;
                while (objReader.Read())
                {
                    if (objReader.IsStartElement())
                    {
                        //Negeer lege elememten
                        if (!objReader.IsEmptyElement)
                        {
                            //Voeg alle keys aan hashtable toe behalve deze in excludeTagRegEx
                            if (!Regex.IsMatch(objReader.Name, strExcludeTagRegEx))
                            {
                                strName = objReader.Name;
                                blnIsKeyRead = true;
                            }
                            else
                            {
                                blnIsKeyRead = false;
                            }
                        }
                    }
                    if (objReader.NodeType == XmlNodeType.Text)
                    {
                        if (blnIsKeyRead)
                        {
                            if(strName == "log-dir" && objOldXmlKeys.Contains("log-dir"))
                            {} else objOldXmlKeys.Add(strName, objReader.Value);
                            blnIsKeyRead = false;
                        }
                    }
                }
            }
        }

        public static void UpdateShortCut(string strFilename)
        {
            string strShortCut;
            using (StreamReader objReader = new StreamReader(strFilename))
            {
                strShortCut = objReader.ReadLine();
            }
            using (StreamWriter objWriter = new StreamWriter(strFilename, false, Encoding.UTF8))
            {
                objWriter.WriteLine(strShortCut.Replace("FieldVisionDNet", "MobileClient"));
            }
        }

        // How much deep to scan. (of course you can also pass it to the method)
        const int HowDeepToScan = 4;

        public static void ProcessDirUpdateShortCut(string sourceDir, int recursionLvl)
        {
            if (recursionLvl <= HowDeepToScan)
            {
                // Process the list of files found in the directory. 
                string[] fileEntries = Directory.GetFiles(sourceDir);
                foreach (string fileName in fileEntries)
                {
                    // do something with fileName
                    if(fileName.EndsWith(".scc")) continue;
                    if (fileName.EndsWith(".dll")) continue;
                    UpdateShortCut(fileName);
                }


                // Recurse into subdirectories of this directory.
                string[] subdirEntries = Directory.GetDirectories(sourceDir);
                foreach (string subdir in subdirEntries)
                    // Do not iterate through reparse points
                    if ((File.GetAttributes(subdir) &
                         FileAttributes.ReparsePoint) !=
                        FileAttributes.ReparsePoint)

                        ProcessDirUpdateShortCut(subdir, recursionLvl + 1);
            }
        }

        public static void UpdateFVInstaller(string strOldFVInstaller, string strOutputFile, EnumMobileVersion enumMobileVersion, bool blnFramework35)
        {
            if (!File.Exists(strOldFVInstaller)) return;
            using (StreamWriter objWriter = new StreamWriter(strOutputFile, false, Encoding.UTF8))
            {
                using (StreamReader objReader = new StreamReader(strOldFVInstaller)) 
                {
                    while(objReader.Peek() > -1)
                    {
                        string strConfigLine = objReader.ReadLine();
                        //Ignore comments
                        if(strConfigLine.StartsWith("#"))
                        {
                            objWriter.WriteLine(strConfigLine);
                            continue;
                        }
                        if (Regex.IsMatch(strConfigLine.ToLower(), blnFramework35?@"(sqlce20[.]ppc[.]wce[45][.]armv4i?[.]cab|sqlce[.]ppc3[.]arm[.]cab|sqlce20[.]ppc[.]wce[4-5][.]armv4i?[.]cab)":@"(sqlce[.]ppc3[.]arm[.]cab|sqlce20[.]ppc[.]wce[4-5][.]armv4i?[.]cab)"))
                        {
                            if (blnFramework35)
                            {
                                switch (enumMobileVersion)
                                {
                                    case EnumMobileVersion.MvPPC2003:
                                        strConfigLine = @"SQL Server CE      = \sqlce35.ppc.wce4.armv4.CAB";
                                        break;
                                    case EnumMobileVersion.MvWM50:
                                        strConfigLine = @"SQL Server CE      = \sqlce35.ppc.wce5.armv4i.CAB";
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException("enumMobileVersion");
                                }
                            } else {
                                switch (enumMobileVersion)
                                {
                                    case EnumMobileVersion.MvPPC2003:
                                        strConfigLine = @"SQL Server CE      = \sqlce20.ppc.wce4.armv4.CAB";
                                        break;
                                    case EnumMobileVersion.MvWM50:
                                        strConfigLine = @"SQL Server CE      = \sqlce20.ppc.wce5.armv4i.cab";
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException("enumMobileVersion");
                                }
                            }
                        }
                        if (Regex.IsMatch(strConfigLine.ToLower(), Regex.Escape(@"[servicepack]")))
                        {
                            objWriter.WriteLine(strConfigLine);
                            strConfigLine = objReader.ReadLine();
                            //If netcf.all.wce4.ARMV4.cab or netcf.all.wce4.ARMV4I.cab is not installed 
                            //make sure correct version of compact framework is installed.
                            if (!Regex.IsMatch(strConfigLine.ToLower(), blnFramework35 ? @"netcfv35[.](ppc|wm)[.]arm4i?[.]cab":@"netcf[.]all[.]wce4[.]arm4i?[.]cab"  ))
                            {
                                if (blnFramework35)
                                {
                                    switch (enumMobileVersion)
                                    {
                                        case EnumMobileVersion.MvPPC2003:
                                            strConfigLine = @"dotNet CF 3.5      = \NETCFv35.ppc.armv4.cab";
                                            objWriter.WriteLine(strConfigLine);
                                            strConfigLine = "";
                                            break;
                                        case EnumMobileVersion.MvWM50:
                                            strConfigLine = @"dotNet CF 3.5      = \NETCFv35.wm.armv4i.cab";
                                            objWriter.WriteLine(strConfigLine);
                                            strConfigLine = "";
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException("enumMobileVersion");
                                    }
                                }
                                else
                                {
                                    switch (enumMobileVersion)
                                    {
                                        case EnumMobileVersion.MvPPC2003:
                                            strConfigLine = @"dotNet CF sp3      = \netcf.all.wce4.ARMV4.cab";
                                            objWriter.WriteLine(strConfigLine);
                                            strConfigLine = "";
                                            break;
                                        case EnumMobileVersion.MvWM50:
                                            strConfigLine = @"dotNet CF sp3      = \netcf.all.wce4.ARMV4I.cab";
                                            objWriter.WriteLine(strConfigLine);
                                            strConfigLine = "";
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException("enumMobileVersion");
                                    }
                                }
                                //Else if SP2 is installed replace it with the correct version.
                            } else if (Regex.IsMatch(strConfigLine.ToLower(), @"sp2[.]netcf[.]all[.]wce4[.]arm4i?[.]cab"))
                            {

                                if (blnFramework35)
                                {
                                    switch (enumMobileVersion)
                                    {
                                        case EnumMobileVersion.MvPPC2003:

                                            strConfigLine = @"dotNet CF 3.5      = \NETCFv35.ppc.armv4.cab";
                                            break;
                                        case EnumMobileVersion.MvWM50:

                                            strConfigLine = @"dotNet CF 3.5      = \NETCFv35.wm.armv4i.cab";
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException("enumMobileVersion");
                                    }
                                
                                }
                                else
                                {
                                    switch (enumMobileVersion)
                                    {
                                        case EnumMobileVersion.MvPPC2003:

                                            strConfigLine = @"dotNet CF sp3      = \netcf.all.wce4.ARMV4.cab";
                                            break;
                                        case EnumMobileVersion.MvWM50:

                                            strConfigLine = @"dotNet CF sp3      = \netcf.all.wce4.ARMV4I.cab";
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException("enumMobileVersion");
                                    }
                                }
                            }
                        }
                        objWriter.WriteLine(strConfigLine);
                    
                    }
                }
            }
        }

        public static void RemoveRedundantCab(string strOutputDir, EnumMobileVersion version)
        {
            if (File.Exists(strOutputDir + "sqlce.ppc3.arm.CAB"))
            {
                File.Delete(strOutputDir + "sqlce.ppc3.arm.CAB");
            }
            switch(version)
            {
                case EnumMobileVersion.MvPPC2003:
                    if (File.Exists(strOutputDir + "sqlce20.ppc.wce5.armv4i.cab"))
                    {
                        File.Delete(strOutputDir + "sqlce20.ppc.wce5.armv4i.cab");
                    }
                    break;
                case EnumMobileVersion.MvWM50:
                    if (File.Exists(strOutputDir + "sqlce20.ppc.wce4.armv4.CAB"))
                    {
                        File.Delete(strOutputDir + "sqlce20.ppc.wce4.armv4.CAB");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("version");
            }
        }

        public static void UpdateTransporterXML(string strOldTransporterXML, string strOutputFile, string strNewTransporterXML)
        {
            //const string strExcludeTagRegEx = @"\b(xmlversion|log-lines|log-files)";
            Hashtable objOldTransporterKeys = new Hashtable();
            try
            {
                ReadOldKeys(strOldTransporterXML, objOldTransporterKeys);
            }
            catch (System.IO.FileNotFoundException)
            {
                ReadOldKeys(strOldTransporterXML.Replace("TransporterClient", "FVcomm"), objOldTransporterKeys);
            }

            using (XmlTextWriter objWriter = new XmlTextWriter(strOutputFile, Encoding.UTF8))
            {
                using (XmlTextReader objReader = new XmlTextReader(new StreamReader(strNewTransporterXML)))
                {
                    bool blnUseOldKey = false;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.None:
                                break;
                            case XmlNodeType.Element:
                                //Als het een leeg element is schrijf een leeg element
                                objWriter.WriteStartElement(objReader.Name);

                                if (objReader.HasAttributes)
                                {
                                    while (objReader.MoveToNextAttribute())
                                    {
                                        objWriter.WriteAttributeString(objReader.Name, objReader.Value);
                                    }
                                    // Move the reader back to the element node.
                                    objReader.MoveToElement();
                                }
                                if (!objReader.IsEmptyElement)
                                {
                                    if (objOldTransporterKeys.Contains(objReader.Name))
                                    {
                                        blnUseOldKey = true;
                                        objWriter.WriteString(objOldTransporterKeys[objReader.Name].ToString());
                                    }
                                    else
                                    {
                                        blnUseOldKey = false;
                                    }
                                }
                                if (objReader.IsEmptyElement)
                                {
                                    objWriter.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Attribute:
                                break;
                            case XmlNodeType.Text:
                                if (!blnUseOldKey)
                                {
                                    objWriter.WriteString(objReader.Value);
                                }
                                break;
                            case XmlNodeType.CDATA:
                                break;
                            case XmlNodeType.EntityReference:
                                break;
                            case XmlNodeType.Entity:
                                break;
                            case XmlNodeType.ProcessingInstruction:
                                break;
                            case XmlNodeType.Comment:
                                objWriter.WriteComment(objReader.Value);
                                break;
                            case XmlNodeType.Document:
                                break;
                            case XmlNodeType.DocumentType:
                                break;
                            case XmlNodeType.DocumentFragment:
                                break;
                            case XmlNodeType.Notation:
                                break;
                            case XmlNodeType.Whitespace:
                                objWriter.WriteWhitespace(objReader.Value);
                                break;
                            case XmlNodeType.SignificantWhitespace:
                                break;
                            case XmlNodeType.EndElement:
                                objWriter.WriteEndElement();
                                break;
                            case XmlNodeType.EndEntity:
                                break;
                            case XmlNodeType.XmlDeclaration:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }



        public static void UpdateAutorunXML(string strOldStartUp, string strOutputFile, string strNewStartUp, string strStorageCardRootName)
        {
            if (!File.Exists(strOldStartUp)) return;
            if (!File.Exists(strNewStartUp)) return;
            using (XmlTextWriter objWriter = new XmlTextWriter(strOutputFile, Encoding.UTF8))
            {
                using (XmlTextReader objReader = new XmlTextReader(new StreamReader(strOldStartUp)))
                {
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.None:
                                break;
                            case XmlNodeType.Element:
                                //Als het een leeg element is schrijf een leeg element
                                objWriter.WriteStartElement(objReader.Name);

                                if (objReader.HasAttributes)
                                {
                                    while (objReader.MoveToNextAttribute())
                                    {
                                        objWriter.WriteAttributeString(objReader.Name, objReader.Value);
                                    }
                                    // Move the reader back to the element node.
                                    objReader.MoveToElement();
                                }
                                if (objReader.IsEmptyElement)
                                {
                                    objWriter.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Attribute:
                                break;
                            case XmlNodeType.Text:
                                string strValue = objReader.Value;
                                strValue = SetStorageCardRootName(strValue, strStorageCardRootName);
                                objWriter.WriteString(strValue);
                                break;
                            case XmlNodeType.CDATA:
                                break;
                            case XmlNodeType.EntityReference:
                                break;
                            case XmlNodeType.Entity:
                                break;
                            case XmlNodeType.ProcessingInstruction:
                                break;
                            case XmlNodeType.Comment:
                                objWriter.WriteComment(objReader.Value);
                                break;
                            case XmlNodeType.Document:
                                break;
                            case XmlNodeType.DocumentType:
                                break;
                            case XmlNodeType.DocumentFragment:
                                break;
                            case XmlNodeType.Notation:
                                break;
                            case XmlNodeType.Whitespace:
                                objWriter.WriteWhitespace(objReader.Value);
                                break;
                            case XmlNodeType.SignificantWhitespace:
                                break;
                            case XmlNodeType.EndElement:
                                objWriter.WriteEndElement();
                                break;
                            case XmlNodeType.EndEntity:
                                break;
                            case XmlNodeType.XmlDeclaration:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }

        public static void UpdateLanguageXML(string strOldStartUp, string strOutputFile, string strNewStartUp)
        {
            Hashtable objOldLanguageKeys = new Hashtable();
            ReadOldKeys(strOldStartUp, objOldLanguageKeys);
            using (XmlTextWriter objWriter = new XmlTextWriter(strOutputFile, Encoding.UTF8))
            {
                using (XmlTextReader objReader = new XmlTextReader(new StreamReader(strNewStartUp)))
                {
                    bool blnUseOldKey = false;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.None:
                                break;
                            case XmlNodeType.Element:
                                //Als het een leeg element is schrijf een leeg element
                                objWriter.WriteStartElement(objReader.Name);

                                if (objReader.HasAttributes)
                                {
                                    while (objReader.MoveToNextAttribute())
                                    {
                                        objWriter.WriteAttributeString(objReader.Name, objReader.Value);
                                    }
                                    // Move the reader back to the element node.
                                    objReader.MoveToElement();
                                }
                                if (!objReader.IsEmptyElement)
                                {
                                    if (objOldLanguageKeys.Contains(objReader.Name))
                                    {
                                        blnUseOldKey = true;
                                        objWriter.WriteString(objOldLanguageKeys[objReader.Name].ToString());
                                    }
                                    else
                                    {
                                        blnUseOldKey = false;
                                    }
                                }
                                if (objReader.IsEmptyElement)
                                {
                                    objWriter.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Attribute:
                                break;
                            case XmlNodeType.Text:
                                if (!blnUseOldKey)
                                {
                                    objWriter.WriteString(objReader.Value);
                                }
                                break;
                            case XmlNodeType.CDATA:
                                break;
                            case XmlNodeType.EntityReference:
                                break;
                            case XmlNodeType.Entity:
                                break;
                            case XmlNodeType.ProcessingInstruction:
                                break;
                            case XmlNodeType.Comment:
                                objWriter.WriteComment(objReader.Value);
                                break;
                            case XmlNodeType.Document:
                                break;
                            case XmlNodeType.DocumentType:
                                break;
                            case XmlNodeType.DocumentFragment:
                                break;
                            case XmlNodeType.Notation:
                                break;
                            case XmlNodeType.Whitespace:
                                objWriter.WriteWhitespace(objReader.Value);
                                break;
                            case XmlNodeType.SignificantWhitespace:
                                break;
                            case XmlNodeType.EndElement:
                                objWriter.WriteEndElement();
                                break;
                            case XmlNodeType.EndEntity:
                                break;
                            case XmlNodeType.XmlDeclaration:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }

        public static void UpdateColorXML(string strOldColor, string strOutputFile, string strNewColor)
        {
            if (!File.Exists(strOldColor)) return;
            if (!File.Exists(strNewColor)) return;
            using (XmlTextWriter objWriter = new XmlTextWriter(strOutputFile, Encoding.UTF8))
            {
                using (XmlTextReader objReader = new XmlTextReader(new StreamReader(strOldColor)))
                {
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.None:
                                break;
                            case XmlNodeType.Element:
                                //Als het een leeg element is schrijf een leeg element
                                objWriter.WriteStartElement(objReader.Name);

                                if (objReader.HasAttributes)
                                {
                                    while (objReader.MoveToNextAttribute())
                                    {
                                        objWriter.WriteAttributeString(objReader.Name, objReader.Value);
                                    }
                                    // Move the reader back to the element node.
                                    objReader.MoveToElement();
                                }
                                if (objReader.IsEmptyElement)
                                {
                                    objWriter.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Attribute:
                                break;
                            case XmlNodeType.Text:
                                objWriter.WriteString(objReader.Value);
                                break;
                            case XmlNodeType.CDATA:
                                break;
                            case XmlNodeType.EntityReference:
                                break;
                            case XmlNodeType.Entity:
                                break;
                            case XmlNodeType.ProcessingInstruction:
                                break;
                            case XmlNodeType.Comment:
                                objWriter.WriteComment(objReader.Value);
                                break;
                            case XmlNodeType.Document:
                                break;
                            case XmlNodeType.DocumentType:
                                break;
                            case XmlNodeType.DocumentFragment:
                                break;
                            case XmlNodeType.Notation:
                                break;
                            case XmlNodeType.Whitespace:
                                objWriter.WriteWhitespace(objReader.Value);
                                break;
                            case XmlNodeType.SignificantWhitespace:
                                break;
                            case XmlNodeType.EndElement:
                                objWriter.WriteEndElement();
                                break;
                            case XmlNodeType.EndEntity:
                                break;
                            case XmlNodeType.XmlDeclaration:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }

        public static string GetSolutionFromScreen(string strSelectedPath, string strPathFvdNet)
        {
            string strSolution;
            string strDate;
            string strDatabase;
            string strServer;
            GetSupportInfoScreen(strSelectedPath, strPathFvdNet, out strDate, out strDatabase, out strSolution, out strServer);
            return strSolution;
        }

        public static void GetSupportInfoScreen(string strSelectedPath, string strPathFVDNet, out string strDate, out string strDatabase, out string strSolution, out string strServer)
        {
            XmlReader xrScreen = XmlReader.Create(strSelectedPath + strPathFVDNet + "Screen.xml");
            xrScreen.ReadToFollowing("Support");
            strDate = null;
            strDatabase = null;
            strServer = null;
            strSolution = null;
            if (!xrScreen.IsEmptyElement)
            {
                xrScreen.ReadToFollowing("date");
                xrScreen.Read();
                strDate = xrScreen.Value;

                xrScreen.ReadToFollowing("database");
                xrScreen.Read();
                strDatabase = xrScreen.Value;

                xrScreen.ReadToFollowing("server");
                xrScreen.Read();
                strServer = xrScreen.Value;

                xrScreen.ReadToFollowing("solution");
                xrScreen.Read();
                strSolution = xrScreen.Value;
            }
            xrScreen.Close();
        }

        public static string GetStrPathFVDNet(bool blnIsForDesktop)
        {
            return GetStrPathFVDNet(blnIsForDesktop, "");
        }

        public static string GetStrPathFVDNet(bool blnIsForDesktop, string strFileName)
        {
            return GetStrPathFV(blnIsForDesktop, true, strFileName);
        }

        public static string GetStrPathTransporter(bool blnIsForDesktop, string strFileName)
        {
            return GetStrPathFV(blnIsForDesktop, false, strFileName);
        }

        public static string GetStrPathFV(bool blnIsForDesktop, bool blnIsFVDNet, string strFileName)
        {
            string strPathFVDNet;
            if(blnIsForDesktop)
            {
                const string cstrDecktopPathFVDnet = @"\FVI\COPY\Program files\FVDnet\";
                const string cstrDecktopPathTransporter = @"\FVI\COPY\Program files\Transporter\";
                strPathFVDNet = (blnIsFVDNet ? cstrDecktopPathFVDnet : cstrDecktopPathTransporter) + strFileName;
            }
            else
            {
                const string cstrWCEPatchFVDnet = @"\FVI\COPY\Program files\FVDnet\";
                const string cstrWCEPathTransporter = @"\FVI\COPY\Program files\Transporter\";
                strPathFVDNet = (blnIsFVDNet ? cstrWCEPatchFVDnet : cstrWCEPathTransporter) + strFileName;
            }
            return strPathFVDNet;
        }

        public static void UpdateScreenXML(string strOldScreen, string strOutputFile)
        {
            if (!File.Exists(strOldScreen)) return;
            using (XmlTextWriter objWriter = new XmlTextWriter(strOutputFile, Encoding.UTF8))
            {
                using (XmlTextReader objReader = new XmlTextReader(new StreamReader(strOldScreen)))
                {
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.None:
                                break;
                            case XmlNodeType.Element:
                                //Als het een leeg element is schrijf een leeg element
                                objWriter.WriteStartElement(objReader.Name);

                                if (objReader.HasAttributes)
                                {
                                    while (objReader.MoveToNextAttribute())
                                    {
                                        objWriter.WriteAttributeString(objReader.Name, objReader.Value);
                                    }
                                    // Move the reader back to the element node.
                                    objReader.MoveToElement();
                                }
                                if (objReader.IsEmptyElement)
                                {
                                    objWriter.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Attribute:
                                break;
                            case XmlNodeType.Text:
                                if (Regex.IsMatch(objReader.Value,@"""InstantMessaging""\)\.StartIMTimer\(\);"))
                                {
                                    objWriter.WriteString(objReader.Value.Replace("VanDenAnkerIM","FieldVisionIM"));  
                                }
                                else
                                {
                                    objWriter.WriteString(objReader.Value);    
                                }
                                break;
                            case XmlNodeType.CDATA:
                                break;
                            case XmlNodeType.EntityReference:
                                break;
                            case XmlNodeType.Entity:
                                break;
                            case XmlNodeType.ProcessingInstruction:
                                break;
                            case XmlNodeType.Comment:
                                objWriter.WriteComment(objReader.Value);
                                break;
                            case XmlNodeType.Document:
                                break;
                            case XmlNodeType.DocumentType:
                                break;
                            case XmlNodeType.DocumentFragment:
                                break;
                            case XmlNodeType.Notation:
                                break;
                            case XmlNodeType.Whitespace:
                                objWriter.WriteWhitespace(objReader.Value);
                                break;
                            case XmlNodeType.SignificantWhitespace:
                                break;
                            case XmlNodeType.EndElement:
                                objWriter.WriteEndElement();
                                break;
                            case XmlNodeType.EndEntity:
                                break;
                            case XmlNodeType.XmlDeclaration:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }

        public static void UpgradeFVInstaller(string strOldFVInstaller, string strOutputFile, EnumMobileVersion enumMobileVersion, bool blnFramework35)
        {
            if (!File.Exists(strOldFVInstaller)) return;
            config objConfig = new config();
            configRun[] objSequences = new configRun[7];
            objSequences[0] = new configRun { section = ".Net CF 3.5" };
            objSequences[1] = new configRun { section = "SQLCE35" };
            objSequences[2] = new configRun { section = "OpenNetCF" };
            objSequences[3] = new configRun { section = "GPRS" };
            objSequences[4] = new configRun { section = "Files" };
            objSequences[5] = new configRun { section = "Registry" };
            objSequences[6] = new configRun { section = "Reboot" };
            objConfig.sequence = objSequences;
            configSection[] objSections = new configSection[7];
            objSections[0] = new configSection {
                Items = new object[]
                            {
                                new installType
                                    {
                                        type = installTypeType.cab, 
                                        run = installTypeRun.now, 
                                        waitforprocess = installTypeWaitforprocess.yes, 
                                        waitforprocessSpecified = true, 
                                        timeout = 600,
                                        mode = installTypeMode.normal, 
                                        modeSpecified = true,  
                                        timeoutSpecified = true, 
                                        Value = "NETCFv35.Messages.EN.wm.cab"
                                    }
                            },
                name = ".Net CF 3.5",
                idletimeout = 30,
                idletimeoutSpecified = true
            };
            objSections[1] = new configSection
            {
                Items = new object[]
                            {
                                new installType
                                    {
                                        type = installTypeType.cab, 
                                        run = installTypeRun.now, 
                                        waitforprocess = installTypeWaitforprocess.yes, 
                                        waitforprocessSpecified = true, 
                                        timeout = 600, 
                                        mode = installTypeMode.normal, 
                                        modeSpecified = true, 
                                        timeoutSpecified = true, 
                                        Value = "sqlce35.ppc.wce5.armv4i.cab"
                                    },
                                new installType
                                    {
                                        type = installTypeType.cab, 
                                        run = installTypeRun.now, 
                                        waitforprocess = installTypeWaitforprocess.yes, 
                                        waitforprocessSpecified = true, 
                                        mode = installTypeMode.normal, 
                                        modeSpecified = true,  
                                        timeoutSpecified = true, 
                                        Value = "sqlce35.repl.ppc.wce5.armv4i.CAB"
                                    }
                            },
                name = "SQLCE35"
            };
            objSections[2] = new configSection
            {
                Items = new object[]
                            {
                                new installType
                                    {
                                        type = installTypeType.cab, 
                                        run = installTypeRun.now, 
                                        waitforprocess = installTypeWaitforprocess.yes, 
                                        waitforprocessSpecified = true, 
                                        timeout = 600, 
                                        mode = installTypeMode.normal, 
                                        modeSpecified = true, 
                                        timeoutSpecified = true, 
                                        Value = "OpenNETCF.SDF.2.2.cab"
                                    }
                            },
                name = "GPRS",
                idletimeout = 30
            };
            objSections[3] = new configSection
            {
                Items = new object[]
                            {
                                new installType
                                    {
                                        type = installTypeType.cab, 
                                        run = installTypeRun.now, 
                                        waitforprocess = installTypeWaitforprocess.yes, 
                                        waitforprocessSpecified = true, 
                                        timeout = 600, 
                                        mode = installTypeMode.normal, 
                                        modeSpecified = true, 
                                        timeoutSpecified = true, 
                                        Value = "VodafoneGPRS.cab"
                                    }
                            },
                name = "GPRS",
                idletimeout = 30
            };
            //objSections[0] = new configSection {idletimeout = 10, idletimeoutSpecified = true, Items = new object[]
            //                                                                                              {
            //                                                                                                  new waitType{type = waitTypeType.seconds,Value="60"},
            //                                                                                                  new killType{mode = killTypeMode.killemall, modeSpecified = true, Value = "MobileClient.exe"}
                                                                                                              
            //                                          }, name = "kill"};
            //objSections[1] = new configSection
            //{
            //    Items = new object[]
            //                {
            //                    new installType
            //                        {
            //                            type = installTypeType.cab, 
            //                            run = installTypeRun.now, 
            //                            waitforprocess = installTypeWaitforprocess.yes, 
            //                            waitforprocessSpecified = true, 
            //                            timeout = 600, 
            //                            mode = installTypeMode.normal, 
            //                            modeSpecified = true, 
            //                            timeoutSpecified = true, 
            //                            Value = "NETCFv35.wm.armv4i.cab"
            //                        },
            //                    new installType
            //                        {
            //                            type = installTypeType.cab, 
            //                            run = installTypeRun.now, 
            //                            waitforprocess = installTypeWaitforprocess.yes, 
            //                            waitforprocessSpecified = true, 
            //                            timeout = 600,
            //                            mode = installTypeMode.normal, 
            //                            modeSpecified = true,  
            //                            timeoutSpecified = true, 
            //                            Value = "NETCFv35.Messages.EN.wm.cab"
            //                        }
            //                },
            //    name = ".Net CF 3.5",
            //    idletimeout = 30,
            //    idletimeoutSpecified = true
            //};
            //objSections[2] = new configSection
            //{
            //    Items = new object[]
            //                {
            //                    new installType
            //                        {
            //                            type = installTypeType.cab, 
            //                            run = installTypeRun.now, 
            //                            waitforprocess = installTypeWaitforprocess.yes, 
            //                            waitforprocessSpecified = true, 
            //                            timeout = 600, 
            //                            mode = installTypeMode.normal, 
            //                            modeSpecified = true, 
            //                            timeoutSpecified = true, 
            //                            Value = "sqlce35.ppc.wce5.armv4i.cab"
            //                        },
            //                    new installType
            //                        {
            //                            type = installTypeType.cab, 
            //                            run = installTypeRun.now, 
            //                            waitforprocess = installTypeWaitforprocess.yes, 
            //                            waitforprocessSpecified = true, 
            //                            mode = installTypeMode.normal, 
            //                            modeSpecified = true,  
            //                            timeoutSpecified = true, 
            //                            Value = "sqlce35.repl.ppc.wce5.armv4i.CAB"
            //                        }
            //                },
            //    name = "SQLCE35"
            //};
            Hashtable tblSections = new Hashtable();
            string strSection = "[ini]";
            Collection<string> colLines = new Collection<string>();
            using (StreamReader objReader = new StreamReader(strOldFVInstaller))
            {
                while (objReader.Peek() > -1)
                {
                    string strConfigLine = objReader.ReadLine();
                    //Ignore comments
                    if (strConfigLine.StartsWith("#"))
                    {
                        continue;
                    }
                    if(strConfigLine.StartsWith("[") && strConfigLine.EndsWith("]"))
                    {
                        tblSections.Add(strSection,colLines);
                        colLines = new Collection<string>();
                        strSection = strConfigLine.ToLower();
                    } else{
                        colLines.Add(strConfigLine);
                    }
                }
                tblSections.Add(strSection.ToLower(), colLines);
            }
            Collection<ioType> colIoTypes = new Collection<ioType>();
            colIoTypes.Add(new ioType
                               {
                                   type = ioTypeType.copy, 
                                   attributesSpecified = true, 
                                   attributes = ioTypeAttributes.preserve ,
                                   Value = "Program Files"
                               });
            if(tblSections["[copy]"] != null)
            {
                colLines = (Collection<string>)tblSections["[copy]"];
                foreach (var line in colLines)
                {
                    if(line.Contains("="))
                    {
                        string value = line.Split('=')[1].Trim();
                        if(value.StartsWith("\\") || value.StartsWith("/"))
                        {
                            value = value.Substring(1);
                        }
                        if (value.ToLower() == "program files") continue;
                        colIoTypes.Add(new ioType { type = ioTypeType.copy, Value = value});
                    }
                }
            }
            if (tblSections["[delete]"] != null)
            {
                colLines = (Collection<string>)tblSections["[delete]"];
                foreach (var line in colLines)
                {
                    if (line.Contains("="))
                    {
                        string value = line.Split('=')[1].Trim();
                        if (value.StartsWith("\\") || value.StartsWith("/"))
                        {
                            value = value.Substring(1);
                        }
                        colIoTypes.Add(new ioType { type = ioTypeType.delete, Value = value });
                    }
                }
            }
            Collection<object> colRegistries = new Collection<object>();
            if (tblSections["[registry]"] != null)
            {
                colLines = (Collection<string>)tblSections["[registry]"];
                foreach (var line in colLines)
                {
                    if (line.Contains("="))
                    {
                        string value = line.Split('=')[1].Trim();
                        if (value.StartsWith("\\") || value.StartsWith("/"))
                        {
                            value = value.Substring(1);
                        }
                        colRegistries.Add(new registryType() { type = registryTypeType.create, Value = value });
                    }
                }
            }
            //colRegistries.Add(new rebootType { installationfinished = rebootTypeInstallationfinished.yes, installationfinishedSpecified = true });
            ioType[] objIoTypeItems = new ioType[colIoTypes.Count];
            colIoTypes.CopyTo(objIoTypeItems,0);
            objSections[4] = new configSection
            {
                name = "Files",
                Items = objIoTypeItems
            };
            object[] objRegistryItems = new object[colRegistries.Count];
            colRegistries.CopyTo(objRegistryItems,0);
            objSections[5] = new configSection { name = "Registry",
                                                 Items = objRegistryItems
            };
            objSections[6] = new configSection
            {
                name = "Reboot",
                Items = new object[] { new rebootType { installationfinishedSpecified = true, installationfinished = rebootTypeInstallationfinished.yes } }
            };
            objConfig.section = objSections;
            using (XmlTextWriter objWriter = new XmlTextWriter(strOutputFile, Encoding.UTF8))
            {
                objWriter.Formatting = Formatting.Indented;
                objWriter.Indentation = 2;
                new XmlSerializer(objConfig.GetType()).Serialize(objWriter, objConfig);
            }


        }
    }
}