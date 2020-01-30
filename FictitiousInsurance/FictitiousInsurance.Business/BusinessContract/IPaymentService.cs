//-----------------------------------------------------------------------
// <copyright file="IPaymentService.cs" company="FictiousInsurance">
// Payment service
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Business
{
    using FictitiousInsurance.Model;

    /// <summary>
    /// Payment Service Contract
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Calculate premium details for customers
        /// </summary>
        /// <param name="paymentDetails">Payment Model</param>
        /// <returns>bool: Success</returns>
        bool CalculatePremiumDetails(PaymentModel paymentDetails);
    }
}
