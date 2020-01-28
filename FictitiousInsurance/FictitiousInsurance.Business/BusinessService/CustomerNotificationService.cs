using FictitiousInsurance.Common;
using FictitiousInsurance.DataAccess;
using FictitiousInsurance.Helper;
using FictitiousInsurance.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FictitiousInsurance.Business
{
    /// <summary>
    /// Service responsible for all kind of customer notifications
    /// </summary>
    public class CustomerNotificationService : ICustomerNotificationService
    {
        private ICustomerService _custSvc;
        private ICustomerNotificationRepository _custNotifRepo;
        public CustomerNotificationService(ICustomerService custSvc, ICustomerNotificationRepository custNotifRepo)
        {
            _custSvc = custSvc;
            _custNotifRepo = custNotifRepo;
        }

        /// <summary>
        /// Will generate the renewal letters for customers whos policies are due.
        /// </summary>
        /// <returns>String: Notification file generation response </returns>
        public ApiResponse GenerateRenewalNotificationLetter()
        {
            var policyDueCustomers = _custSvc.GetPolicyDueCustomerDetails();
            var customerNotificationDetails = MapCustomerToRenewalNotification(policyDueCustomers);
            var fileTemplate = _custNotifRepo.GetRenewalNotificationTemplate();
            ApiResponse response = new ApiResponse();
            customerNotificationDetails.ForEach(x => {
                var fileName = GenerateRenewalNotificationLetterFileName(x);
                var fileContent = CreateNotificationFileContent(fileTemplate, x);
                if (_custNotifRepo.GenerateRenewalNotificationLetter(fileContent, fileName))
                    response.ProcessedCount += 1;
                else
                    response.ErrorCount += 1;
            });
            response.Success = true;
            response.Message = "GenerateRenewalNotificationLetter: Service Completed Successfully.";
            return response;
        }
        /// <summary>
        /// Generate file name for notification letter based on FirstName and Customer Id
        /// File Name Template config : NotifFileNameTemplate
        /// </summary>
        /// <param name="renewalNotifDetails">RenewalNotificationModel</param>
        /// <returns>Nitification file name</returns>
        private string GenerateRenewalNotificationLetterFileName(RenewalNotificationModel renewalNotifDetails)
        {
            if (renewalNotifDetails.CustomerId<=0 ||string.IsNullOrEmpty(renewalNotifDetails?.FirstName))
            {
                LogHelper.LogException("CustomerNotificationService.GenerateRenewalNotificationLetterFileName: CustomerId and FirstName required to generate file name.");
                throw new TechnicalExceptions("CustomerNotificationService.GenerateRenewalNotificationLetterFileName: CustomerId and FirstName required to generate file name.");
            }
            else
            {
                var fileNameTemp = ConfigHelper.GetConfigValue("NotifFileNameTemplate");
                var fileName = string.Format(fileNameTemp, renewalNotifDetails.FirstName, renewalNotifDetails.CustomerId);
                return fileName;
            }
        }

        /// <summary>
        /// Mapper to generate List<RenewalNotificationModel> from List<CustomerModel>
        /// </summary>
        /// <param name="customerProducts">List<CustomerModel></param>
        /// <returns>List<RenewalNotificationModel></returns>
        private List<RenewalNotificationModel> MapCustomerToRenewalNotification(List<CustomerModel> customer)
        {
            var notificationList = new List<RenewalNotificationModel>();

            if (customer == null)
            {
                throw new TechnicalExceptions("Mapping failed Customer data is null");
            }
            //ToDo : AutoMapper is a nice to have feature
            customer.ForEach(x =>
                {
                    notificationList.Add(new RenewalNotificationModel()
                    {
                        CustomerId = x.CustomerId,
                        Title = x.Title,
                        FirstName = x.FirstName,
                        SurName = x.SurName,
                        ProductName = x?.ProductDetails?.ProductName,
                        AnnualPremium = x?.PaymentDetails.AnnualPremium==null?0: x.PaymentDetails.AnnualPremium,
                        PayoutAmount = x?.PaymentDetails.PayoutAmount == null ? 0 : x.PaymentDetails.PayoutAmount,
                        CreditCharge = x?.PaymentDetails.CreditCharge == null ? 0 : x.PaymentDetails.CreditCharge,
                        TotalPremium = x?.PaymentDetails.TotalPremium == null ? 0 : x.PaymentDetails.TotalPremium,
                        AvgMonthlyPremium = x?.PaymentDetails.AvgMonthlyPremium == null ? 0 : x.PaymentDetails.AvgMonthlyPremium,
                        InitialMonthlyPayAmount = x?.PaymentDetails.InitialMonthlyPayAmount == null ? 0 : x.PaymentDetails.InitialMonthlyPayAmount,
                        OtherMonthlyPayAmount = x?.PaymentDetails.OtherMonthlyPayAmount == null ? 0 : x.PaymentDetails.OtherMonthlyPayAmount
                    });
                });
           
            return notificationList;
        }
        /// <summary>
        /// Prepare the nitification file content based on RenewalNotificationModel data using provided file template.
        /// </summary>
        /// <param name="fileTemplate"></param>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        private string CreateNotificationFileContent(string fileTemplate, RenewalNotificationModel notificationData)
        {
            string fileData = string.Empty;
            var parameters = new Dictionary<string, object>();
            parameters.Add("@DateVal", DateTime.Now.ToString("dd/MM/yyyy"));
            parameters.Add("@FullName", notificationData.FullName);
            parameters.Add("@Title", notificationData.Title);
            parameters.Add("@Surname", notificationData.SurName);
            parameters.Add("@ProductName", notificationData.ProductName);
            parameters.Add("@PayoutAmount", notificationData.PayoutAmount);
            parameters.Add("@AnnualPremium", notificationData.AnnualPremium);
            parameters.Add("@CreditCharge", notificationData.CreditCharge);
            parameters.Add("@TotCreditCharge", notificationData.TotalPremium);
            parameters.Add("@InitMonthlyPayAmt", notificationData.InitialMonthlyPayAmount);
            parameters.Add("@OthMonthlyPayAmt", notificationData.OtherMonthlyPayAmount);
            try
            {
                fileData = parameters.Aggregate(fileTemplate, (current, parameter) =>
                    current.Replace(parameter.Key, parameter.Value.ToString())
                    );
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CustomerNotificationService.CreateNotificationFileContent: File Content generation failed", ex);
                throw new TechnicalExceptions("Customer notification file Content Creation failed.");
            }
            return fileData;
        }
    }
}
