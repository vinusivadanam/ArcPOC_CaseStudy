using FictitiousInsurance.Helper;
using FictitiousInsurance.Model;
using System;
using System.Collections.Generic;

namespace FictitiousInsurance.Business
{
    /// <summary>
    /// Service responsible for all payment related calculations
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private float _creditServiceRate = 0.05F;
        public PaymentService()
        { }
        
        public bool CalculatePremiumDetails(PaymentModel paymentDetails)
        {
            if (paymentDetails == null || paymentDetails.AnnualPremium <= 0)
            {
                LogHelper.LogException($"PaymentService.CalculateMonthlyPayAmounts, Invalid Annual Premium.");
                return false;
            }
            paymentDetails.CreditCharge = CalculateCreditCharge(paymentDetails.AnnualPremium);
            paymentDetails.TotalPremium = CalculateTotalPremium(paymentDetails.AnnualPremium, paymentDetails.CreditCharge);
            paymentDetails.AvgMonthlyPremium = CalculateAvgMonthlyPremium(paymentDetails.TotalPremium);
            double mAvg = paymentDetails.TotalPremium / 12;
            //Getting monthly pund
            var mPount = Math.Truncate(mAvg);
            //Getting pence
            var mPence = (mAvg - mPount) * 100;
            var balPence = mPence - Math.Truncate(mPence);
            mPence = mPence - balPence;
            var otherMonthlyPayAmount = mPount + (mPence / 100);
            var initialMonthlyPayAmount = otherMonthlyPayAmount + (balPence / 100 * 12);

            paymentDetails.OtherMonthlyPayAmount = Math.Round(otherMonthlyPayAmount, 2);
            paymentDetails.InitialMonthlyPayAmount = Math.Round(initialMonthlyPayAmount, 2);
            return true;

        }
        private double CalculateAvgMonthlyPremium(double totalPremium)
        {
                double avgMontlyPremium = totalPremium / 12;
                return Math.Round(avgMontlyPremium, 2);
        }
        private double CalculateTotalPremium(double annualPremium, double creditCharge)
        {
                double totalPremium = annualPremium + creditCharge;
                return Math.Round(totalPremium, 2);
        }
        private double CalculateCreditCharge(double annualPremium)
        {
                double creditCharge = annualPremium * _creditServiceRate;
                return Math.Round(creditCharge, 2);
        }
    }
}
