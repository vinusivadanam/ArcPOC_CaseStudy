using FictitiousInsurance.Model;
using System.Collections.Generic;

namespace FictitiousInsurance.DataAccess
{
    public interface ICustomerNotificationRepository 
    {
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returrn>List<CustomerModel></returrn>
        List<CustomerModel> GetPolicyDueCustomers();
        /// <summary>
        /// Get Renewal notification template from config path
        /// config Key : RenewalNotifFileTemplate
        /// </summary>
        /// <returns>string: file template</returns>
        string GetRenewalNotificationTemplate();
        /// <summary>
        /// Generate Renewal notification letter
        /// Generated file will placed in config location : OutputFileLocation
        /// </summary>
        /// <param name="fileContent"> Content to put in file</param>
        /// <param name="fileName">Name for generated file</param>
        /// <returns>success</returns>
        bool GenerateRenewalNotificationLetter(string fileContent, string fileName);
    }
}
