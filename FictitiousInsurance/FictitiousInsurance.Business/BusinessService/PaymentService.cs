//-----------------------------------------------------------------------
// <copyright file="PaymentService.cs" company="FictiousInsurance">
// Customer service
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Business
{
    using System;
    using FictitiousInsurance.Helper;
    using FictitiousInsurance.Model;

    /// <summary>
    /// Service responsible for all payment related calculations
    /// </summary>
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// Credit service rate
        /// </summary>
        private float creditServiceRate = 0.05F;
        
        /// <summary>
        /// Calculate Premium details
        /// </summary>
        /// <param name="paymentDetails">Payment Details</param>
        /// <returns>success value</returns>
        public bool CalculatePremiumDetails(PaymentModel paymentDetails)
        {
            if (paymentDetails == null || paymentDetails.AnnualPremium <= 0)
            {
                LogHelper.LogException($"PaymentService.CalculateMonthlyPayAmounts, Invalid Annual Premium.");
                return false;
            }

            paymentDetails.CreditCharge = this.CalculateCreditCharge(paymentDetails.AnnualPremium);
            paymentDetails.TotalPremium = this.CalculateTotalPremium(paymentDetails.AnnualPremium, paymentDetails.CreditCharge);
            paymentDetails.AvgMonthlyPremium = this.CalculateAvgMonthlyPremium(paymentDetails.TotalPremium);
            double monthAvg = paymentDetails.TotalPremium / 12;
            ////Getting monthly pund
            var monthlyPount = Math.Truncate(monthAvg);
            ////Getting pence
            var monthlyPence = (monthAvg - monthlyPount) * 100;
            var balPence = monthlyPence - Math.Truncate(monthlyPence);
            monthlyPence = monthlyPence - balPence;
            var otherMonthlyPayAmount = monthlyPount + (monthlyPence / 100);
            var initialMonthlyPayAmount = otherMonthlyPayAmount + (balPence / 100 * 12);
            paymentDetails.OtherMonthlyPayAmount = Math.Round(otherMonthlyPayAmount, 2);
            paymentDetails.InitialMonthlyPayAmount = Math.Round(initialMonthlyPayAmount, 2);
            return true;
        }

        /// <summary>
        /// Calculate average monthly premium
        /// </summary>
        /// <param name="totalPremium">total premium</param>
        /// <returns>average monthly premium</returns>
        private double CalculateAvgMonthlyPremium(double totalPremium)
        {
                double avgMontlyPremium = totalPremium / 12;
                return Math.Round(avgMontlyPremium, 2);
        }

        /// <summary>
        /// Calculate total premium
        /// </summary>
        /// <param name="annualPremium">annual premium</param>
        /// <param name="creditCharge">credit charge</param>
        /// <returns>total premium</returns>
        private double CalculateTotalPremium(double annualPremium, double creditCharge)
        {
                double totalPremium = annualPremium + creditCharge;
                return Math.Round(totalPremium, 2);
        }

        /// <summary>
        /// Calculate credit charge
        /// </summary>
        /// <param name="annualPremium">annual premium</param>
        /// <returns>credit charge</returns>
        private double CalculateCreditCharge(double annualPremium)
        {
                double creditCharge = annualPremium * this.creditServiceRate;
                return Math.Round(creditCharge, 2);
        }
    }
}
