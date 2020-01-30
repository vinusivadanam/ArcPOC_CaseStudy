//-----------------------------------------------------------------------
// <copyright file="ICustomerNotificationService.cs" company="FictiousInsurance">
// Customer notification contract
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Business
{
    using FictitiousInsurance.Model;

    /// <summary>
    /// Customer notification contract
    /// </summary>
    public interface ICustomerNotificationService
    {
        /// <summary>
        /// Will generate the renewal letters for customers policies are due.
        /// </summary>
        /// <returns>String: Notification file generation response </returns>
        ApiResponse GenerateRenewalNotificationLetter();
    }
}
