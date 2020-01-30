using FictitiousInsurance.DataAccess;
using FictitiousInsurance.Helper;
using FictitiousInsurance.Model;
using System.Collections.Generic;

namespace FictitiousInsurance.Business
{
    /// <summary>
    /// Service responsible for all customer related activities
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private ICustomerNotificationRepository _custNotifRepo;
        IPaymentService _paymentSvc;
        public CustomerService(ICustomerNotificationRepository custNotifRepo, IPaymentService paymentSvc)
        {
            _custNotifRepo = custNotifRepo;
            _paymentSvc = paymentSvc;
        }
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>List<CustomerModel></returns>
        public List<CustomerModel> GetPolicyDueCustomerDetails()
        {
            var policyDueCustomers = _custNotifRepo.GetPolicyDueCustomers();

            if (policyDueCustomers == null)
            {
                LogHelper.LogException($"CustomerService.GetPolicyDueCustomerDetails, No Customer details available.");
                return policyDueCustomers;
            }
            foreach (var customer in policyDueCustomers)
            {
                if (!CalculateCustomerPremiumDetails(customer.PaymentDetails))
                {
                    LogHelper.LogException($"CustomerService.GetPolicyDueCustomerDetails: Payment calculation failed for customer:{customer.CustomerId}");
                }
            }

            return policyDueCustomers;
        }

        /// <summary>
        /// Calculate premium details for given payment
        /// </summary>
        /// <param name="paymentDetails">PaymentModel</param>
        /// <returns>bool: Success</returns>
        private bool CalculateCustomerPremiumDetails(PaymentModel paymentDetails)
        {
            if (paymentDetails == null)
            {
                LogHelper.LogException($"CustomerService.CalculateCustomerPremiumDetails: Payment Deails not available");
                return false;
            }
            _paymentSvc.CalculatePremiumDetails(paymentDetails);

            return true;
        }
    }
}
