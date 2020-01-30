//-----------------------------------------------------------------------
// <copyright file="IDataAccess.cs" company="FictiousInsurance">
// Data source access interface
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.DataAccess
{
    using System.Collections.Generic;
    using FictitiousInsurance.Model;

    /// <summary>
    /// Data access interface
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>List of customers</returns>
        List<CustomerModel> GetPolicyDueCustomers();
    }
}
