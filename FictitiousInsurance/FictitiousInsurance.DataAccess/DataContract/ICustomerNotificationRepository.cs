//-----------------------------------------------------------------------
// <copyright file="ICustomerNotificationRepository.cs" company="FictiousInsurance">
// Application response
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.DataAccess
{
    using System.Collections.Generic;
    using FictitiousInsurance.Model;

    /// <summary>
    /// Customer notification repository
    /// </summary>
    public interface ICustomerNotificationRepository 
    {
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>List of customers</returns>
        List<CustomerModel> GetPolicyDueCustomers();

        /// <summary>
        /// Get Renewal notification template from config path
        /// config Key : Renewal Notification File Template
        /// </summary>
        /// <returns>string: file template</returns>
        string GetRenewalNotificationTemplate();

        /// <summary>
        /// Generate Renewal notification letter
        /// Generated file will placed in config location : OutputFileLocation
        /// </summary>
        /// <param name="fileContent"> Content to put in file</param>
        /// <param name="fileName">Name for generated file</param>
        /// <returns>success indicator</returns>
        bool GenerateRenewalNotificationLetter(string fileContent, string fileName);
    }
}
