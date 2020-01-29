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
        public void CalculatePremiumDetails_CustomerLisIsNull_ResultFalse()
        {
            //Arrange
            List<CustomerModel> lstCustomerModel = null;

            //Act
            var result = _paymentSvc.CalculatePremiumDetails(lstCustomerModel);

            //Asset
            Assert.IsTrue(result == false);
        }

        [TestMethod()]
        public void CalculatePremiumDetails_CustomerPaymentDetailsNull_ResultCalculationSkip()
        {
            //Arrange
            List<CustomerModel> lstCustomerModel = GetCustomerList();
            lstCustomerModel[0].PaymentDetails = null;

            //Act
            var result = _paymentSvc.CalculatePremiumDetails(lstCustomerModel);

            //Asset
            Assert.IsTrue(result == true);
        }

        [TestMethod()]
        public void CalculatePremiumDetails_CustomerPaymentDetailsWithPaymentDetails_ResultCalculatedValues()
        {
            //Arrange
            List<CustomerModel> lstCustomerModel = GetCustomerList();

            //Act
            var result = _paymentSvc.CalculatePremiumDetails(lstCustomerModel);

            //Asset
            Assert.IsTrue(result == true);
            Assert.IsTrue(lstCustomerModel[0].PaymentDetails.AvgMonthlyPremium == 4.38);
            Assert.IsTrue(lstCustomerModel[0].PaymentDetails.CreditCharge == 2.5);
            Assert.IsTrue(lstCustomerModel[0].PaymentDetails.InitialMonthlyPayAmount == 4.43);
            Assert.IsTrue(lstCustomerModel[0].PaymentDetails.OtherMonthlyPayAmount == 4.37);
            Assert.IsTrue(lstCustomerModel[0].PaymentDetails.TotalPremium == 52.5);
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


    }
}