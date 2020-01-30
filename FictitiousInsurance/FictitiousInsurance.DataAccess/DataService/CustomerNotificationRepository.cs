//-----------------------------------------------------------------------
// <copyright file="CustomerNotificationRepository.cs" company="FictiousInsurance">
// Customer notification repository
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.DataAccess
{
    using System.Collections.Generic;
    using FictitiousInsurance.Helper;
    using FictitiousInsurance.Model;

    /// <summary>
    /// The responsible to do all the notification things
    /// </summary>
    public class CustomerNotificationRepository : ICustomerNotificationRepository
    {
        /// <summary>
        /// Data access connect
        /// </summary>
        private IDataAccess dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotificationRepository" /> class
        /// </summary>
        /// <param name="dataAccess">data access connect</param>
        public CustomerNotificationRepository(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>List of customers</returns>
        public List<CustomerModel> GetPolicyDueCustomers()
        {
            return this.dataAccess.GetPolicyDueCustomers();
        }

        /// <summary>
        /// Get Renewal notification template from config path
        /// config Key : Renewal Notification FileTemplate
        /// </summary>
        /// <returns>string: file template</returns>
        public string GetRenewalNotificationTemplate()
        {
            var filePath = ConfigHelper.GetConfigValue("RenewalNotifFileTemplate");
            return FileHelper.ReadTextFile(filePath);
        }

        /// <summary>
        /// Generate Renewal notification letter
        /// Generated file will placed in configuration : OutputFileLocation
        /// </summary>
        /// <param name="fileContent"> Content to put in file</param>
        /// <param name="fileName">Name for generated file</param>
        /// <returns>success response</returns>
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
