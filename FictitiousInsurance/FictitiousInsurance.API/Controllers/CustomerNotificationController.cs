//-----------------------------------------------------------------------
// <copyright file="CustomerNotificationController.cs" company="FictiousInsurance">
// Customer Notification Controller
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.API.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using FictitiousInsurance.Business;
    using FictitiousInsurance.Common;
    using FictitiousInsurance.Model;

    /// <summary>
    /// Controller will contain all the Notification related actions
    /// </summary>
    public class CustomerNotificationController : ApiController
    {
        /// <summary>
        /// Customer notification service reference
        /// </summary>
        private ICustomerNotificationService custNotificationSvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotificationController" /> class
        /// </summary>
        /// <param name="custNotificationSvc">CustomerNotificationService object</param>
        public CustomerNotificationController(ICustomerNotificationService custNotificationSvc)
        {
            this.custNotificationSvc = custNotificationSvc;
        }

        /// <summary>
        /// Action will generate Renewal Letters for due customers
        /// </summary>
        /// <returns>Http Response</returns>
        [HttpPost]
        [Route("GenerateRenewalLetter")]
        public HttpResponseMessage GenerateRenewalLetter()
        {
            try
            {
                var resp = this.custNotificationSvc.GenerateRenewalNotificationLetter();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (TechnicalExceptions ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiResponse { Success = false, Message = ex.Message });
            }
        }
    }
}
