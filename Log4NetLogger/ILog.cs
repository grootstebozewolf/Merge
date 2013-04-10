using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4NetLogger
{
    public interface ILog
    {
        void Debug(Type callerStackBoundaryDeclaringType, object message);
        void Debug(Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void DebugFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args);
        void DebugIf(bool condition, Type callerStackBoundaryDeclaringType, object message);
        void DebugIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void DebugFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args);

        void Error(Type callerStackBoundaryDeclaringType, object message);
        void Error(Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void ErrorFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args);
        void ErrorIf(bool condition, Type callerStackBoundaryDeclaringType, object message);
        void ErrorIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void ErrorFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args);

        void Fatal(Type callerStackBoundaryDeclaringType, object message);
        void Fatal(Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void FatalFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args);
        void FatalIf(bool condition, Type callerStackBoundaryDeclaringType, object message);
        void FatalIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void FatalFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args);

        void Info(Type callerStackBoundaryDeclaringType, object message);
        void Info(Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void InfoFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args);
        void InfoIf(bool condition, Type callerStackBoundaryDeclaringType, object message);
        void InfoIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void InfoFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args);

        void Warn(Type callerStackBoundaryDeclaringType, object message);
        void Warn(Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void WarnFormat(Type callerStackBoundaryDeclaringType, string format, params object[] args);
        void WarnIf(bool condition, Type callerStackBoundaryDeclaringType, object message);
        void WarnIf(bool condition, Type callerStackBoundaryDeclaringType, object message, Exception exception);
        void WarnFormatIf(bool condition, Type callerStackBoundaryDeclaringType, string format, params object[] args);
    }
}
