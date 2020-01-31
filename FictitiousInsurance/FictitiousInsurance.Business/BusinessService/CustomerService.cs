//-----------------------------------------------------------------------
// <copyright file="CustomerService.cs" company="FictiousInsurance">
// Customer service
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Business
{
    using System.Collections.Generic;
    using FictitiousInsurance.Business.Factories;
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
        /// Service factory object
        /// </summary>
        private IServiceFactory serviceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService" /> class
        /// </summary>
        /// <param name="custNotifRepo">Customer repository</param>
        /// <param name="serviceFactory">Service Factory</param>
        public CustomerService(ICustomerNotificationRepository custNotifRepo, IServiceFactory serviceFactory)
        {
            this.custNotifRepository = custNotifRepo;
            this.serviceFactory = serviceFactory;
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
                this.SetPaymentSystemForProduct(customer.ProductDetails.ProductName);
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

        /// <summary>
        /// Set the payment service type based on product
        /// </summary>
        /// <param name="productName">Product name</param>
        private void SetPaymentSystemForProduct(string productName)
        {
            this.paymentService = this.serviceFactory.GetPaymentService(productName);
        }
    }
}
