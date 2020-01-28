using FictitiousInsurance.Business;
using FictitiousInsurance.Common;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FictitiousInsurance.API.Controllers
{
    public class CustomerNotificationController : ApiController
    {
        private ICustomerNotificationService _custNotificationSvc;

        public CustomerNotificationController(ICustomerNotificationService custNotificationSvc)
        {
            _custNotificationSvc = custNotificationSvc;
        }

        [HttpPost]
        [Route("GenerateRenewalLetter")]
        public HttpResponseMessage GenerateRenewalLetter()
        {
            try
            {
                var resp = _custNotificationSvc.GenerateRenewalNotificationLetter();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (TechnicalExceptions ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiResponse { Success = false, Message = ex.Message });
            }
        }
        
    }
}
