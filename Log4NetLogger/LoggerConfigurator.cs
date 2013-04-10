using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Log4NetLogger
{
    public class LoggerConfigurator
    {
        public static bool Configure()
        {
            FileInfo configFile = new FileInfo("log4net.xml");
            if (configFile.Exists)
            {
                log4net.Config.XmlConfigurator.Configure(configFile);
                return true;
            }
            else
            {
                log4net.Config.XmlConfigurator.Configure(GetDefaultConfiguration());
                return false;
            }
        }

        public static XmlElement GetDefaultConfiguration()
        {
            XmlDocument configDocument = new XmlDocument();

            XmlElement log4netRoot = configDocument.CreateElement("log4net");

                XmlElement appenderLogFile = configDocument.CreateElement("appender");
                appenderLogFile.SetAttribute("name", "LogFile");
                appenderLogFile.SetAttribute("type", "log4net.Appender.RollingFileAppender");

                    XmlElement logFileFile = configDocument.CreateElement("file");
                    logFileFile.SetAttribute("value", "log4net.log");
                    appenderLogFile.AppendChild(logFileFile);
                    
                    XmlElement logFileAppendToFile = configDocument.CreateElement("appendToFile");
                    logFileAppendToFile.SetAttribute("value", "true");
                    appenderLogFile.AppendChild(logFileAppendToFile);

                    XmlElement logFileRollingStyle = configDocument.CreateElement("rollingStyle");
                    logFileRollingStyle.SetAttribute("value", "Size");
                    appenderLogFile.AppendChild(logFileRollingStyle);

                    XmlElement logFileMaxSizeRollBackups = configDocument.CreateElement("maxSizeRollBackups");
                    logFileMaxSizeRollBackups.SetAttribute("value", "10");
                    appenderLogFile.AppendChild(logFileMaxSizeRollBackups);

                    XmlElement logFileMaximumFileSize = configDocument.CreateElement("maximumFileSize");
                    logFileMaximumFileSize.SetAttribute("value", "10MB");
                    appenderLogFile.AppendChild(logFileMaximumFileSize);

                    XmlElement logFileStaticLogFileName = configDocument.CreateElement("staticLogFileName");
                    logFileStaticLogFileName.SetAttribute("value", "true");
                    appenderLogFile.AppendChild(logFileStaticLogFileName);

                    XmlElement logFileLayout = configDocument.CreateElement("layout");
                    logFileLayout.SetAttribute("type", "log4net.Layout.PatternLayout");

                        XmlElement logFileConversionPattern = configDocument.CreateElement("conversionPattern");
                        logFileConversionPattern.SetAttribute("value", "%date  %property{componentID} [%thread] %-5level %logger - %message%newline");
                        logFileLayout.AppendChild(logFileConversionPattern);
                    
                    appenderLogFile.AppendChild(logFileLayout);
                    
                    XmlElement logFileFilter = configDocument.CreateElement("filter");
                    logFileFilter.SetAttribute("type", "log4net.Filter.LevelRangeFilter");

                        XmlElement logFileLevelMin = configDocument.CreateElement("levelMin");
                        logFileLevelMin.SetAttribute("value", "DEBUG");
                        logFileFilter.AppendChild(logFileLevelMin);

                        XmlElement logFileLevelMax = configDocument.CreateElement("levelMax");
                        logFileLevelMax.SetAttribute("value", "INFO");
                        logFileFilter.AppendChild(logFileLevelMax);
                    
                    appenderLogFile.AppendChild(logFileFilter);

                log4netRoot.AppendChild(appenderLogFile);

                XmlElement appenderEventLog = configDocument.CreateElement("appender");
                appenderEventLog.SetAttribute("name", "EventLogFile");
                appenderEventLog.SetAttribute("type", "log4net.Appender.EventLogAppender");
                
                    XmlElement eventLogLayout = configDocument.CreateElement("layout");
                    eventLogLayout.SetAttribute("type", "log4net.Layout.PatternLayout");
                    
                        XmlElement eventLogConversionPattern = configDocument.CreateElement("conversionPattern");
                        eventLogConversionPattern.SetAttribute("value", "%date  %property{componentID} [%thread] %-5level %logger - %message%newline");
                        eventLogLayout.AppendChild(eventLogConversionPattern);
                    
                    appenderEventLog.AppendChild(eventLogLayout);

                    XmlElement eventLogFilter = configDocument.CreateElement("filter");
                    eventLogFilter.SetAttribute("type", "log4net.Filter.LevelRangeFilter");

                        XmlElement eventLogLevelMin = configDocument.CreateElement("levelMin");
                        eventLogLevelMin.SetAttribute("value", "WARN");
                        eventLogFilter.AppendChild(eventLogLevelMin);

                        XmlElement eventLogLevelMax = configDocument.CreateElement("levelMax");
                        eventLogLevelMax.SetAttribute("value", "FATAL");
                        eventLogFilter.AppendChild(eventLogLevelMax);
                    
                    appenderEventLog.AppendChild(eventLogFilter);
                
                log4netRoot.AppendChild(appenderEventLog);

                XmlElement root = configDocument.CreateElement("root");

                    XmlElement rootLevel = configDocument.CreateElement("level");
                    rootLevel.SetAttribute("value", "INFO");
                    root.AppendChild(rootLevel);

                    XmlElement rootAppenderLogFile = configDocument.CreateElement("appender-ref");
                    rootAppenderLogFile.SetAttribute("ref", "LogFile");
                    root.AppendChild(rootAppenderLogFile);

                    XmlElement rootAppenderEventLog = configDocument.CreateElement("appender-ref");
                    rootAppenderEventLog.SetAttribute("ref", "EventLogFile");
                    root.AppendChild(rootAppenderEventLog);

                log4netRoot.AppendChild(root);

            return log4netRoot;
        }
    }
}
