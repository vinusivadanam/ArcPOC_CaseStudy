//-----------------------------------------------------------------------
// <copyright file="CustomerServiceTest.cs" company="FictiousInsurance">
// Test methods for customer service
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using FictitiousInsurance.Business;
    using FictitiousInsurance.DataAccess;
    using FictitiousInsurance.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Customer service test class
    /// </summary>
    [TestClass]
    public class CustomerServiceTest
    {
        /// <summary>
        /// Customer service connect
        /// </summary>
        private ICustomerService customerService;

        /// <summary>
        /// Payment service connect
        /// </summary>
        private IPaymentService paymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerServiceTest" /> class
        /// </summary>
        public CustomerServiceTest()
        {
            this.paymentService = new PaymentService();
        }

        /// <summary>
        /// GetPolicyDueCustomerDetails if No CustomersAvailable
        /// </summary>
        [TestMethod]
        public void GetPolicyDueCustomerDetails_NoCustomersAvailable()
        {
            ////Arrange
            List<CustomerModel> customerList = null;
            PaymentModel payment = null;
            var custNotifRepoMock = new Mock<ICustomerNotificationRepository>();
            custNotifRepoMock.Setup(x => x.GetPolicyDueCustomers()).Returns(customerList);
            var paymentSvcMock = new Mock<IPaymentService>();
            paymentSvcMock.Setup(x => x.CalculatePremiumDetails(payment)).Returns(true);
            this.customerService = new CustomerService(custNotifRepoMock.Object, paymentSvcMock.Object);
            ////Act
            var result = this.customerService.GetPolicyDueCustomerDetails();
            ////Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// GetPolicyDueCustomerDetails if Customers Available
        /// </summary>
        [TestMethod]
        public void GetPolicyDueCustomerDetails_CustomersAvailable()
        {
            ////Arrange
            List<CustomerModel> customerList = this.GetCustomerList();
            var custNotifRepoMock = new Mock<ICustomerNotificationRepository>();
            custNotifRepoMock.Setup(x => x.GetPolicyDueCustomers()).Returns(customerList);
            this.customerService = new CustomerService(custNotifRepoMock.Object, this.paymentService);
            ////Act
            var result = this.customerService.GetPolicyDueCustomerDetails();
            ////Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Any() == true);
            Assert.IsTrue(result[0].PaymentDetails != null);
            Assert.IsTrue(result[0].ProductDetails != null);
        }

        /// <summary>
        /// Get Customer List
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
                    ProductDetails = new ProductModel
                    {
                            ProductId = 1,
                            ProductName = "Enhanced Cover"
                    },
                    PaymentDetails = new PaymentModel
                    {
                        PayoutAmount = 83205.5,
                        AnnualPremium = 50
                    }
                }
            };
        }
    }
}
