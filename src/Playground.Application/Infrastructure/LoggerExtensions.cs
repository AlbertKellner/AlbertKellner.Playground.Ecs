using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Runtime.CompilerServices;

//public static class LoggerExtensions
//{
//    //public static void LogInformationExtended(this ILogger logger, string message,
//    //    [CallerMemberName] string methodName = "",
//    //    [CallerFilePath] string filePath = "",
//    //    [CallerLineNumber] int lineNumber = 0, params object[] propertyValues)
//    //{
//    //    var fileInfo = new FileInfo(filePath);
//    //    var className = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

//    //    LogContext.PushProperty("ClassName", className);
//    //    LogContext.PushProperty("MethodName", methodName);
//    //    LogContext.PushProperty("LineNumber", lineNumber);

//    //    logger.LogInformation(message, propertyValues);
//    //}

//    public static void LogInformationExtended(this ILogger logger, string message, params object[] propertyValues)
//    {
//        string methodName = "";
//        //string filePath = "";
//        int lineNumber = 0;

//        var stackTrace = new System.Diagnostics.StackTrace(1);
//        var stackFrame = stackTrace.GetFrame(0);
//        if (stackFrame != null)
//        {
//            var method = stackFrame.GetMethod();
//            if (method != null)
//            {
//                methodName = method.Name;
//            }

//            //filePath = stackFrame.GetFileName() ?? "Unknow";
//            lineNumber = stackFrame.GetFileLineNumber();
//        }

//        //var fileInfo = new FileInfo(filePath);
//        //var className = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

//        //LogContext.PushProperty("ClassName", className);
//        LogContext.PushProperty("MethodName", methodName);
//        LogContext.PushProperty("LineNumber", lineNumber);

//        logger.LogInformation(message, propertyValues);
//    }
//}


//public static class LoggerExtensions
//{
//    public static void LogInformation(this ILogger logger, string message,
//        bool extended = false,
//        [CallerMemberName] string methodName = "",
//        [CallerFilePath] string filePath = "",
//        [CallerLineNumber] int lineNumber = 0)
//    {
//        if (extended)
//        {
//            var fileInfo = new FileInfo(filePath);
//            var className = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

//            LogContext.PushProperty("ClassName", className);
//            LogContext.PushProperty("MethodName", methodName);
//            LogContext.PushProperty("LineNumber", lineNumber);
//        }

//        logger.LogInformation(message);
//    }
//}


//public interface IExtendedLogger : ILogger
//{
//}

//public static class LoggerExtensions
//{
//    public static void LogInformation(this IExtendedLogger logger, string message,
//        [CallerMemberName] string methodName = "",
//        [CallerFilePath] string filePath = "",
//        [CallerLineNumber] int lineNumber = 0)
//    {
//        var fileInfo = new FileInfo(filePath);
//        var className = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

//        LogContext.PushProperty("ClassName", className);
//        LogContext.PushProperty("MethodName", methodName);
//        LogContext.PushProperty("LineNumber", lineNumber);

//        ((ILogger)logger).LogInformation(message);
//    }
//}