using FictitiousInsurance.DataAccess;
using FictitiousInsurance.Helper;
using FictitiousInsurance.Model;
using System;
using System.Collections.Generic;

namespace FictitiousInsurance.Business
{
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
            var _policyDueCustomers = _custNotifRepo.GetPolicyDueCustomers();
            _paymentSvc.CalculatePremiumDetails(_policyDueCustomers);
            return _policyDueCustomers;
        }
    }
}
