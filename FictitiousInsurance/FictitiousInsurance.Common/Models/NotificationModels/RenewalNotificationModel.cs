//-----------------------------------------------------------------------
// <copyright file="RenewalNotificationModel.cs" company="FictiousInsurance">
// Application response
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Model
{
    /// <summary>
    /// Renewal notification data
    /// </summary>
    public class RenewalNotificationModel
    {
        /// <summary>
        /// Gets or sets Customer Id
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// Gets or sets title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets sur name
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Gets full name
        /// </summary>
        public string FullName
        {
            get
            {
                return $"{Title}. {FirstName} {SurName}";
            }
        }

        /// <summary>
        /// Gets or sets Product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets payout amount
        /// </summary>
        public double PayoutAmount { get; set; }

        /// <summary>
        /// Gets or sets Annual premium
        /// </summary>
        public double AnnualPremium { get; set; }

        /// <summary>
        /// Gets or sets credit charge
        /// </summary>
        public double CreditCharge { get; set; }

        /// <summary>
        /// Gets or sets total premium
        /// </summary>
        public double TotalPremium { get; set; }

        /// <summary>
        /// Gets or sets Average monthly premium
        /// </summary>
        public double AvgMonthlyPremium { get; set; }

        /// <summary>
        /// Gets or sets initial monthly premium
        /// </summary>
        public double InitialMonthlyPayAmount { get; set; }

        /// <summary>
        /// Gets or sets other monthly pay amount
        /// </summary>
        public double OtherMonthlyPayAmount { get; set; }
    }
}
