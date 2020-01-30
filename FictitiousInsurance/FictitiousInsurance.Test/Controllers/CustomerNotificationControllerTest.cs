//-----------------------------------------------------------------------
// <copyright file="CustomerNotificationControllerTest.cs" company="FictiousInsurance">
// Test methods for customer notification controller
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Test.Controllers
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using FictitiousInsurance.API.Controllers;
    using FictitiousInsurance.Business;
    using FictitiousInsurance.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Customer notification controller test
    /// </summary>
    [TestClass]
    public sealed class CustomerNotificationControllerTest : IDisposable
    {
        /// <summary>
        /// Customer notification controller
        /// </summary>
        private CustomerNotificationController custimerNotifCtrl;
        
        /// <summary>
        /// Dispose controller object
        /// </summary>
        public void Dispose()
        {
            this.custimerNotifCtrl.Dispose();
            GC.SuppressFinalize(this.custimerNotifCtrl);
        }

        /// <summary>
        /// GenerateRenewalLetter Check Response Type
        /// </summary>
        [TestMethod]
        public void GenerateRenewalLetter_ResponseType()
        {
            ////Arrange
            var custNotifRepoMock = new Mock<ICustomerNotificationService>();
            ApiResponse apiResponse = this.GetApiResponse();
            custNotifRepoMock.Setup(x => x.GenerateRenewalNotificationLetter()).Returns(apiResponse);
            this.custimerNotifCtrl = new CustomerNotificationController(custNotifRepoMock.Object);
            this.custimerNotifCtrl.Request = new HttpRequestMessage();
            this.custimerNotifCtrl.Configuration = new HttpConfiguration();
            ////Act
            var result = this.custimerNotifCtrl.GenerateRenewalLetter();
            var response = result.Content.ReadAsAsync<ApiResponse>().Result;
            ////Assert
            Assert.IsInstanceOfType(response, typeof(ApiResponse));
        }

        /// <summary>
        /// Get API response
        /// </summary>
        /// <returns>Default response object</returns>
        private ApiResponse GetApiResponse()
        {
            return new ApiResponse()
            {
                Success = true,
                Message = "Creation success",
                ErrorCount = 0,
                ProcessedCount = 0
            };
        }
    }
}
