using FictitiousInsurance.Common;

namespace FictitiousInsurance.Business
{
    public interface ICustomerNotificationService
    {
        /// <summary>
        /// Will generate the renewal letters for customers whos policies are due.
        /// </summary>
        /// <returns>String: Notification file generation response </returns>
        ApiResponse GenerateRenewalNotificationLetter();
    }
}
