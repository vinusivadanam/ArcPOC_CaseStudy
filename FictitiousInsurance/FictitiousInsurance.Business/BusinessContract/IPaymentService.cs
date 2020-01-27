using FictitiousInsurance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictitiousInsurance.Business
{
    public interface IPaymentService
    {
        /// <summary>
        /// Calculate premium details for customers
        /// </summary>
        /// <param name="_policyDueCustomers">List<CustomerModel></param>
        /// <returns>bool: Success</returns>
        bool CalculatePremiumDetails(List<CustomerModel> _policyDueCustomers);
    }
}
