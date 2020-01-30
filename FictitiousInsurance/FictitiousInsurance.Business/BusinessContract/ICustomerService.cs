//-----------------------------------------------------------------------
// <copyright file="ICustomerService.cs" company="FictiousInsurance">
// Customer service
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Business
{
    using System.Collections.Generic;
    using FictitiousInsurance.Model;

    /// <summary>
    /// Customer service contract
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>Customer list</returns>
        List<CustomerModel> GetPolicyDueCustomerDetails();
    }
}
