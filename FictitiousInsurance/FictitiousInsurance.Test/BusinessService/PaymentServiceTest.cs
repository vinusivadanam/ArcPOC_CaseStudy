//-----------------------------------------------------------------------
// <copyright file="PaymentServiceTest.cs" company="FictiousInsurance">
// Test methods for customer service
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Tests
{
    using System.Collections.Generic;
    using FictitiousInsurance.Business;
    using FictitiousInsurance.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Payment service test class
    /// </summary>
    [TestClass]
    public class PaymentServiceTest
    {
        /// <summary>
        /// Payment Service connect
        /// </summary>
        private IPaymentService paymentSvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentServiceTest" /> class
        /// </summary>
        public PaymentServiceTest()
        {
            this.paymentSvc = new PaymentService();
        }

        /// <summary>
        /// CalculatePremiumDetails when pass CustomerPaymentDetails as Null returns Result as False
        /// </summary>
        [TestMethod]
        public void CalculatePremiumDetails_CustomerPaymentDetailsNull_ResultFalse()
        {
            ////Arrange
            PaymentModel paymentDetails = null;
            ////Act
            var result = this.paymentSvc.CalculatePremiumDetails(paymentDetails);
            ////Asset
            Assert.IsTrue(result == false);
        }

        /// <summary>
        /// CalculatePremiumDetails when pass CustomerPaymentDetails With PaymentDetails returns Result including Calculated Values
        /// </summary>
        [TestMethod]
        public void CalculatePremiumDetails_CustomerPaymentDetailsWithPaymentDetails_ResultCalculatedValues()
        {
            ////Arrange
            PaymentModel paymentDetails = this.GetPayment();
            ////Act
            var result = this.paymentSvc.CalculatePremiumDetails(paymentDetails);
            ////Asset
            Assert.IsTrue(result == true);
            Assert.IsTrue(paymentDetails.AvgMonthlyPremium == 4.38);
            Assert.IsTrue(paymentDetails.CreditCharge == 2.5);
            Assert.IsTrue(paymentDetails.InitialMonthlyPayAmount == 4.43);
            Assert.IsTrue(paymentDetails.OtherMonthlyPayAmount == 4.37);
            Assert.IsTrue(paymentDetails.TotalPremium == 52.5);
        }

        /// <summary>
        /// Get Customer list object
        /// </summary>
        /// <returns>List of customers</returns>
        private List<CustomerModel> GetCustomerList()
        {
            return new List<CustomerModel> 
            { 
                new CustomerModel() 
                {
                    CustomerId = 123,
                    FirstName = "TestName",
                    SurName = "Surname",
                    Title = "Mr",
                    ProductDetails = null,
                    PaymentDetails = new PaymentModel
                    { 
                        PayoutAmount = 83205.5,
                        AnnualPremium = 50
                    }
                } 
            };
        }

        /// <summary>
        /// Get payment details
        /// </summary>
        /// <returns>Payment model</returns>
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