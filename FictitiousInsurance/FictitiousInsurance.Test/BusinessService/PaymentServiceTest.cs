using FictitiousInsurance.Business;
using FictitiousInsurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FictitiousInsurance.Tests
{
    [TestClass()]
    public class PaymentServiceTest
    {
        IPaymentService _paymentSvc;

        public PaymentServiceTest()
        {
            _paymentSvc = new PaymentService();
        }

        [TestMethod()]
        public void CalculatePremiumDetails_CustomerPaymentDetailsNull_ResultFalse()
        {
            //Arrange
            PaymentModel paymentDetails = null;

            //Act
            var result = _paymentSvc.CalculatePremiumDetails(paymentDetails);

            //Asset
            Assert.IsTrue(result == false);
        }

        [TestMethod()]
        public void CalculatePremiumDetails_CustomerPaymentDetailsWithPaymentDetails_ResultCalculatedValues()
        {
            //Arrange
            PaymentModel paymentDetails = GetPayment();

            //Act
            var result = _paymentSvc.CalculatePremiumDetails(paymentDetails);

            //Asset
            Assert.IsTrue(result == true);
            Assert.IsTrue(paymentDetails.AvgMonthlyPremium == 4.38);
            Assert.IsTrue(paymentDetails.CreditCharge == 2.5);
            Assert.IsTrue(paymentDetails.InitialMonthlyPayAmount == 4.43);
            Assert.IsTrue(paymentDetails.OtherMonthlyPayAmount == 4.37);
            Assert.IsTrue(paymentDetails.TotalPremium == 52.5);
        }

        private List<CustomerModel> GetCustomerList()
        {
            return new List<CustomerModel> { new CustomerModel() {
            CustomerId = 123,
            FirstName = "TestName",
            SurName = "Surname",
            Title = "Mr",
            ProductDetails = null,
            PaymentDetails = new PaymentModel{ 
                PayoutAmount = 83205.5,
                AnnualPremium = 50
            }
            } };
        }

        private PaymentModel GetPayment()
        {
            return new PaymentModel
            {
                PayoutAmount = 83205.5,
                AnnualPremium = 50
            };
        }
    }
}