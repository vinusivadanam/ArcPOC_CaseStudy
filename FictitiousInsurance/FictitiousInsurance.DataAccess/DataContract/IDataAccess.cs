using FictitiousInsurance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
