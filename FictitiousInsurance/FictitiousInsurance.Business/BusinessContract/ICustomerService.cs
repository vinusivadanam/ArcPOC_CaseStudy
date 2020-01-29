using FictitiousInsurance.Model;
using System.Collections.Generic;

namespace FictitiousInsurance.Business
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>List<CustomerModel></returns>
        List<CustomerModel> GetPolicyDueCustomerDetails();
    }
}
