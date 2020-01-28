using FictitiousInsurance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
