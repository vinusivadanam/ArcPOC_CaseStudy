//-----------------------------------------------------------------------
// <copyright file="PaymentModel.cs" company="FictiousInsurance">
// Application response
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Model
{
    /// <summary>
    /// Payment model
    /// </summary>
    public class PaymentModel
    {
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
        /// Gets or sets average monthly premium
        /// </summary>
        public double AvgMonthlyPremium { get; set; }

        /// <summary>
        /// Gets or sets initial monthly payout amount
        /// </summary>
        public double InitialMonthlyPayAmount { get; set; }

        /// <summary>
        /// Gets or sets other monthly pay amount
        /// </summary>
        public double OtherMonthlyPayAmount { get; set; }
    }
}
