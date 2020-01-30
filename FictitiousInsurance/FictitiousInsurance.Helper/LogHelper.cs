//-----------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="FictiousInsurance">
// Configuration helper
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Helper
{
    using System;

    /// <summary>
    /// Log helper
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// Information logger
        /// </summary>
        /// <param name="message">log message</param>
        public static void LogInfo(string message)
        {
            LogInfo(message, null);
        }

        /// <summary>
        /// Information logger with data
        /// </summary>
        /// <param name="message">log message</param>
        /// <param name="arg">data object</param>
        public static void LogInfo(string message, object arg)
        { 
        }

        /// <summary>
        /// Exception logger
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="e">Exception object</param>
        public static void LogException(string message, Exception e)
        {
            LogException(message, e, null);
        }

        /// <summary>
        /// Exception logger without details
        /// </summary>
        /// <param name="message">custom message</param>
        public static void LogException(string message)
        {
            LogException(message, null, null);
        }

        /// <summary>
        /// Exception logger with data
        /// </summary>
        /// <param name="message">custom message</param>
        /// <param name="e">exception object</param>
        /// <param name="arg">data object</param>
        public static void LogException(string message, Exception e, object arg)
        { 
        }
    }
}
