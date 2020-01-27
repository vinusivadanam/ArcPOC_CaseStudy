using System;

namespace FictitiousInsurance.Helper
{
    public class LogHelper
    {
        public static void LogInfo(string message)
        {
            LogInfo(message, null);
        }
        public static void LogInfo(string message, object arg)
        { 
        }
        public static void LogException(string message, Exception e)
        {
            LogException(message, e, null);
        }
        public static void LogException(string message)
        {
            LogException(message, null, null);
        }
        public static void LogException(string message, Exception e, object arg)
        { 
        }
    }
}
