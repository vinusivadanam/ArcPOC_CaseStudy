using FictitiousInsurance.Model;
using System.Collections.Generic;

namespace FictitiousInsurance.Business
{
    public interface IPaymentService
    {
        /// <summary>
        /// Calculate premium details for customers
        /// </summary>
        /// <param name="paymentDetails">PaymentModel</param>
        /// <returns>bool: Success</returns>
        bool CalculatePremiumDetails(PaymentModel paymentDetails);
    }
}
