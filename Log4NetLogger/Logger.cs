using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace Log4NetLogger
{
    public class Logger : Singleton<Logger>, Log4NetLogger.ILog
    {
        public Logger()
        {
            WarnIf(!LoggerConfigurator.Configure(), typeof(Logger), "Log4Net configuration file not found; Default configuration used");
        }

        #region ILog Members

        #region Debug

        public void Debug(Type callerStackBoundaryDeclaringType, object message )
        {
            GetLogger(callerStackBoundaryDeclaringType).Debug(message);
        }

        public void Debug(Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            GetLogger(callerStackBoundaryDeclaringType).Debug(message, exception);
        }

        public void DebugFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            GetLogger(callerStackBoundaryDeclaringType).DebugFormat(format, args);
        }

        public void DebugIf(bool condition, Type callerStackBoundaryDeclaringType, object message)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Debug(message);
        }

        public void DebugIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Debug(message, exception);
        }

        public void DebugFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).DebugFormat(format, args);
        }

        #endregion

        #region Error

        public void Error(Type callerStackBoundaryDeclaringType, object message)
        {
            GetLogger(callerStackBoundaryDeclaringType).Error(message);
        }

        public void Error(Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            GetLogger(callerStackBoundaryDeclaringType).Error(message, exception);
        }

        public void ErrorFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            GetLogger(callerStackBoundaryDeclaringType).ErrorFormat(format, args);
        }

        public void ErrorIf(bool condition, Type callerStackBoundaryDeclaringType, object message)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Error(message);
        }

        public void ErrorIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Error(message, exception);
        }

        public void ErrorFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).ErrorFormat(format, args);
        }

        #endregion

        #region Fatal

        public void Fatal(Type callerStackBoundaryDeclaringType, object message)
        {
            GetLogger(callerStackBoundaryDeclaringType).Fatal(message);
        }

        public void Fatal(Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            GetLogger(callerStackBoundaryDeclaringType).Fatal(message, exception);
        }

        public void FatalFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            GetLogger(callerStackBoundaryDeclaringType).FatalFormat(format, args);
        }

        public void FatalIf(bool condition, Type callerStackBoundaryDeclaringType, object message)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Fatal(message);
        }

        public void FatalIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Fatal(message, exception);
        }

        public void FatalFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).FatalFormat(format, args);
        }

        #endregion

        #region Info

        public void Info(Type callerStackBoundaryDeclaringType, object message)
        {
            GetLogger(callerStackBoundaryDeclaringType).Info(message);
        }

        public void Info(Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            GetLogger(callerStackBoundaryDeclaringType).Info(message, exception);
        }

        public void InfoFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            GetLogger(callerStackBoundaryDeclaringType).InfoFormat(format, args);
        }

        public void InfoIf(bool condition, Type callerStackBoundaryDeclaringType, object message)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Info(message);
        }

        public void InfoIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Info(message, exception);
        }

        public void InfoFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).InfoFormat(format, args);
        }

        #endregion

        #region Warn

        public void Warn(Type callerStackBoundaryDeclaringType, object message)
        {
            GetLogger(callerStackBoundaryDeclaringType).Warn(message);
        }

        public void Warn(Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            GetLogger(callerStackBoundaryDeclaringType).Warn(message, exception);
        }

        public void WarnFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            GetLogger(callerStackBoundaryDeclaringType).WarnFormat(format, args);
        }

        public void WarnIf(bool condition, Type callerStackBoundaryDeclaringType, object message)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Warn(message);
        }

        public void WarnIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).Warn(message, exception);
        }

        public void WarnFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args)
        {
            if (condition) GetLogger(callerStackBoundaryDeclaringType).WarnFormat(format, args);
        }

        #endregion

        #endregion

        protected log4net.ILog GetLogger(Type type)
        {
            if (type == null)
            {
                type = typeof(Logger);
            }
            return LogManager.GetLogger(type);
        }
    }
}
