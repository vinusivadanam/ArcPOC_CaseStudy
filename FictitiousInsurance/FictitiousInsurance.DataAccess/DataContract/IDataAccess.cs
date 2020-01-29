using FictitiousInsurance.Model;
using System.Collections.Generic;

namespace FictitiousInsurance.DataAccess
{
    public interface IDataAccess
    {
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returrn>List<CustomerModel></returrn>
        List<CustomerModel> GetPolicyDueCustomers();
    }
}
