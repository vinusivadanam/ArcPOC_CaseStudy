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
        private float creditServiceRate = 0.05F;
        public PaymentService()
        { }
        /// <summary>
        /// Calculate premium details for customers
        /// </summary>
        /// <param name="_policyDueCustomers">List<CustomerModel></param>
        /// <returns>bool: Success</returns>
        public bool CalculatePremiumDetails(List<CustomerModel> _policyDueCustomers)
        {
            bool res;
            foreach(var x in _policyDueCustomers)
            {
                if (x.PaymentDetails == null)
                {
                    LogHelper.LogException($"PaymentService.CalculatePremiumDetails failed for customer {x.CustomerId}, No payment information available.");
                }
                else
                {
                    x.PaymentDetails.CreditCharge = CalculateCreditCharge(x);
                    x.PaymentDetails.TotalPremium = CalculateTotalPremium(x);
                    x.PaymentDetails.AvgMonthlyPremium = CalculateAvgMonthlyPremium(x);
                    CalculateMonthlyPayAmounts(x);
                }
            }

            res = true;
            return res;
        }
        private void CalculateMonthlyPayAmounts(CustomerModel x)
        {

            double mAvg = x.PaymentDetails.TotalPremium / 12;
            //Getting monthly pund
            var mPount = Math.Truncate(mAvg);
            //Getting pence
            var mPence = (mAvg - mPount) * 100;
            var balPence = mPence - Math.Truncate(mPence);
            mPence = mPence - balPence;
            var otherMonthlyPayAmount = mPount + (mPence / 100);
            var initialMonthlyPayAmount = otherMonthlyPayAmount + (balPence / 100 * 12);

            x.PaymentDetails.OtherMonthlyPayAmount = Math.Round(otherMonthlyPayAmount, 2);
            x.PaymentDetails.InitialMonthlyPayAmount = Math.Round(initialMonthlyPayAmount, 2);

        }
        private double CalculateAvgMonthlyPremium(CustomerModel x)
        {
                double avgMontlyPremium = x.PaymentDetails.TotalPremium / 12;
                return Math.Round(avgMontlyPremium, 2);
        }
        private double CalculateTotalPremium(CustomerModel x)
        {
                double totalPremium = x.PaymentDetails.AnnualPremium + x.PaymentDetails.CreditCharge;
                return Math.Round(totalPremium, 2);
        }
        private double CalculateCreditCharge(CustomerModel x)
        {
                double creditCharge = x.PaymentDetails.AnnualPremium * creditServiceRate;
                return Math.Round(creditCharge, 2);
        }
    }
}
