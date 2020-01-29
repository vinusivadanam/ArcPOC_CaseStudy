using FictitiousInsurance.Business;
using FictitiousInsurance.DataAccess;
using FictitiousInsurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FictitiousInsurance.Test
{
    [TestClass()]
    public class CustomerServiceTest
    {
        ICustomerService _customerService;

        [TestMethod()]
        public void GetPolicyDueCustomerDetails_NoCustomersAvailable()
        {
            //Arrange
            List<CustomerModel> customerList = null;
            var custNotifRepoMock = new Mock<ICustomerNotificationRepository>();
            custNotifRepoMock.Setup(x => x.GetPolicyDueCustomers()).Returns(customerList);

            var paymentSvcMock = new Mock<IPaymentService>();
            paymentSvcMock.Setup(x => x.CalculatePremiumDetails(customerList)).Returns(true);

            _customerService = new CustomerService(custNotifRepoMock.Object, paymentSvcMock.Object);

            //Act
            var result = _customerService.GetPolicyDueCustomerDetails();

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetPolicyDueCustomerDetails_CustomersAvailable()
        {
            //Arrange
            List<CustomerModel> customerList = GetCustomerList();
            var custNotifRepoMock = new Mock<ICustomerNotificationRepository>();
            custNotifRepoMock.Setup(x => x.GetPolicyDueCustomers()).Returns(customerList);

            var paymentSvcMock = new Mock<IPaymentService>();
            paymentSvcMock.Setup(x => x.CalculatePremiumDetails(customerList)).Returns(true);

            _customerService = new CustomerService(custNotifRepoMock.Object, paymentSvcMock.Object);

            //Act
            var result = _customerService.GetPolicyDueCustomerDetails();

            //Assert
            Assert.IsTrue(result!=null);
            Assert.IsTrue(result.Any() == true);
            Assert.IsTrue(result[0].PaymentDetails != null);
            Assert.IsTrue(result[0].ProductDetails != null);
        }

        private List<CustomerModel> GetCustomerList()
        {
            return new List<CustomerModel> { new CustomerModel() {
            CustomerId = 123,
            FirstName = "TestName",
            SurName = "Surname",
            Title = "Mr",
            ProductDetails = new ProductModel{
                    ProductId = 1,
                    ProductName = "Enhanced Cover"
                },
            PaymentDetails = new PaymentModel{
                PayoutAmount = 83205.5,
                AnnualPremium = 50
            }
            } };
        }
    }
}
