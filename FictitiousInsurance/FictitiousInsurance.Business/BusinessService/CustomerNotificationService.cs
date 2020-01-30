//-----------------------------------------------------------------------
// <copyright file="CustomerNotificationService.cs" company="FictiousInsurance">
// Customer notification service
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FictitiousInsurance.Common;
    using FictitiousInsurance.DataAccess;
    using FictitiousInsurance.Helper;
    using FictitiousInsurance.Model;

    /// <summary>
    /// Service responsible for all kind of customer notifications
    /// </summary>
    public class CustomerNotificationService : ICustomerNotificationService
    {
        /// <summary>
        /// Customer service connect
        /// </summary>
        private ICustomerService custService;

        /// <summary>
        /// Notification repo connect
        /// </summary>
        private ICustomerNotificationRepository custNotifRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotificationService" /> class
        /// </summary>
        /// <param name="custSvc">Customer service</param>
        /// <param name="custNotifRepo">Customer Repository</param>
        public CustomerNotificationService(ICustomerService custSvc, ICustomerNotificationRepository custNotifRepo)
        {
            this.custService = custSvc;
            this.custNotifRepository = custNotifRepo;
        }

        /// <summary>
        /// Will generate the renewal letters for customers policies are due.
        /// </summary>
        /// <returns>String: Notification file generation response</returns>
        public ApiResponse GenerateRenewalNotificationLetter()
        {
            var policyDueCustomers = this.custService.GetPolicyDueCustomerDetails();
            var customerNotificationDetails = this.MapCustomerToRenewalNotification(policyDueCustomers);
            var fileTemplate = this.custNotifRepository.GetRenewalNotificationTemplate();
            ApiResponse response = new ApiResponse();
            customerNotificationDetails.ForEach(x => 
            {
                var fileName = GenerateRenewalNotificationLetterFileName(x);
                var fileContent = CreateNotificationFileContent(fileTemplate, x);
                if (this.custNotifRepository.GenerateRenewalNotificationLetter(fileContent, fileName))
                {
                    response.ProcessedCount += 1;
                }
                else
                {
                    response.ErrorCount += 1;
                }
            });
            response.Success = true;
            response.Message = "GenerateRenewalNotificationLetter: Service Completed Successfully.";
            return response;
        }

        /// <summary>
        /// Generate file name for notification letter based on FirstName and Customer Id
        /// File Name Template config
        /// </summary>
        /// <param name="renewalNotifDetails">Renewal Notification Model</param>
        /// <returns>Notification file name</returns>
        private string GenerateRenewalNotificationLetterFileName(RenewalNotificationModel renewalNotifDetails)
        {
            if (renewalNotifDetails.CustomerId <= 0 || string.IsNullOrEmpty(renewalNotifDetails?.FirstName))
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
        /// Mapper to generate List of RenewalNotificationModel from List of CustomerModel
        /// </summary>
        /// <param name="customers">List of CustomerModel</param>
        /// <returns>List of RenewalNotificationModel</returns>
        private List<RenewalNotificationModel> MapCustomerToRenewalNotification(List<CustomerModel> customers)
        {
            var notificationList = new List<RenewalNotificationModel>();

            if (customers == null)
            {
                throw new TechnicalExceptions("Mapping failed Customer data is null");
            }
            ////ToDo : AutoMapper is a nice to have feature
            customers.ForEach(x =>
                {
                    notificationList.Add(new RenewalNotificationModel()
                    {
                        CustomerId = x.CustomerId,
                        Title = x.Title,
                        FirstName = x.FirstName,
                        SurName = x.SurName,
                        ProductName = x?.ProductDetails?.ProductName,
                        AnnualPremium = x?.PaymentDetails.AnnualPremium == null ? 0 : x.PaymentDetails.AnnualPremium,
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
        /// Prepare the notification file content based on RenewalNotificationModel data using provided file template.
        /// </summary>
        /// <param name="fileTemplate">File template as string</param>
        /// <param name="notificationData">Data to write</param>
        /// <returns>Formatted notification letter text</returns>
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
                fileData = parameters.Aggregate(fileTemplate, (current, parameter) => current.Replace(parameter.Key, parameter.Value.ToString()));
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
