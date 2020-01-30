//-----------------------------------------------------------------------
// <copyright file="ConfigHelper.cs" company="FictiousInsurance">
// Configuration helper
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Helper
{
    using System.Configuration;
    using FictitiousInsurance.Common;

    /// <summary>
    /// Configuration helper
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// Get value from configuration
        /// </summary>
        /// <param name="key">config key</param>
        /// <returns>value associated with the key</returns>
        public static string GetConfigValue(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                LogHelper.LogException($"ConfigHelper.GetConfigValue: Unable to find value for key: {key}");
                throw new TechnicalExceptions($"Unable to find value for key: {key}");
            }

            return value;
        }
    }
}
