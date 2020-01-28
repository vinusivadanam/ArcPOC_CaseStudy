using FictitiousInsurance.Common;
using System;
using System.Configuration;

namespace FictitiousInsurance.Helper
{
    public class ConfigHelper
    {
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
