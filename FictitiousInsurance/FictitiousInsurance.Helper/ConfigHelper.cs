using FictitiousInsurance.Common;
using System;
using System.Configuration;

namespace FictitiousInsurance.Helper
{
    public class ConfigHelper
    {
        public static string GetConfigValue(string key)
        {
            try
            {
                var value = ConfigurationManager.AppSettings.Get(key);
                return value;
            }
            catch (Exception ex)
            {
                LogHelper.LogException($"ConfigHelper.GetConfigValue: Unable to find value for key: {key}", ex);
                throw new TechnicalExceptions($"Unable to find value for key: {key}");
            }
            
        }
    }
}
