//-----------------------------------------------------------------------
// <copyright file="CustomerService.cs" company="FictiousInsurance">
// Customer service
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Business
{
    using System.Collections.Generic;
    using FictitiousInsurance.DataAccess;
    using FictitiousInsurance.Helper;
    using FictitiousInsurance.Model;

    /// <summary>
    /// Service responsible for all customer related activities
    /// </summary>
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Customer repository connect
        /// </summary>
        private ICustomerNotificationRepository custNotifRepository;

        /// <summary>
        /// Payment service connect
        /// </summary>
        private IPaymentService paymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService" /> class
        /// </summary>
        /// <param name="custNotifRepo">Customer repository</param>
        /// <param name="paymentSvc">Payment Service</param>
        public CustomerService(ICustomerNotificationRepository custNotifRepo, IPaymentService paymentSvc)
        {
            this.custNotifRepository = custNotifRepo;
            this.paymentService = paymentSvc;
        }

        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>List of Customer Model</returns>
        public List<CustomerModel> GetPolicyDueCustomerDetails()
        {
            var policyDueCustomers = this.custNotifRepository.GetPolicyDueCustomers();

            if (policyDueCustomers == null)
            {
                LogHelper.LogException($"CustomerService.GetPolicyDueCustomerDetails, No Customer details available.");
                return policyDueCustomers;
            }

            foreach (var customer in policyDueCustomers)
            {
                if (!this.CalculateCustomerPremiumDetails(customer.PaymentDetails))
                {
                    LogHelper.LogException($"CustomerService.GetPolicyDueCustomerDetails: Payment calculation failed for customer:{customer.CustomerId}");
                }
            }

            return policyDueCustomers;
        }

        /// <summary>
        /// Calculate premium details for given payment
        /// </summary>
        /// <param name="paymentDetails">Payment Model</param>
        /// <returns>bool: Success</returns>
        private bool CalculateCustomerPremiumDetails(PaymentModel paymentDetails)
        {
            if (paymentDetails == null)
            {
                LogHelper.LogException($"CustomerService.CalculateCustomerPremiumDetails: Payment Deails not available");
                return false;
            }

            this.paymentService.CalculatePremiumDetails(paymentDetails);

            return true;
        }
    }
}
