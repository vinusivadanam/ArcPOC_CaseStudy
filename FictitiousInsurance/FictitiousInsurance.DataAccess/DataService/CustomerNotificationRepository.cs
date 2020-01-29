using FictitiousInsurance.Helper;
using FictitiousInsurance.Model;
using System.Collections.Generic;

namespace FictitiousInsurance.DataAccess
{
    /// <summary>
    /// The responsible to do all the notification things
    /// </summary>
    public class CustomerNotificationRepository : ICustomerNotificationRepository
    {
        IDataAccess _dataAccess;
        public CustomerNotificationRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returrn>List<CustomerModel></returrn>
        public List<CustomerModel> GetPolicyDueCustomers()
        {
            return  _dataAccess.GetPolicyDueCustomers();
        }

        /// <summary>
        /// Get Renewal notification template from config path
        /// config Key : RenewalNotifFileTemplate
        /// </summary>
        /// <returns>string: file template</returns>
        public string GetRenewalNotificationTemplate()
        {
            var filePath = ConfigHelper.GetConfigValue("RenewalNotifFileTemplate");
            return FileHelper.ReadTextFile(filePath);
        }

        /// <summary>
        /// Generate Renewal notification letter
        /// Generated file will placed in config location : OutputFileLocation
        /// </summary>
        /// <param name="fileContent"> Content to put in file</param>
        /// <param name="fileName">Name for generated file</param>
        /// <returns>success</returns>
        public bool GenerateRenewalNotificationLetter(string fileContent, string fileName)
        {
            var filePath = ConfigHelper.GetConfigValue("OutputFileLocation");
            var fullFilePath = $"{filePath}\\{fileName}";
            if (!FileHelper.FileExists(fullFilePath))
            {
                return FileHelper.WriteTextFile(fullFilePath, fileContent, false);
            }
            else
            {
                LogHelper.LogInfo("CustomerNotificationRepository.GenerateRenewalNotificationLetter: File Already in location.");
                return false;
            }
        }
    }
}
