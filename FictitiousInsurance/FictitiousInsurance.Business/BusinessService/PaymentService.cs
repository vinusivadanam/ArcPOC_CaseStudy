
using FictitiousInsurance.Common;
using FictitiousInsurance.Helper;
using FictitiousInsurance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictitiousInsurance.Business
{
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
            bool res = false;
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
            var mPount = Math.Truncate(mAvg);
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
                double _avgMontlyPremium = x.PaymentDetails.TotalPremium / 12;
                return Math.Round(_avgMontlyPremium, 2);
        }
        private double CalculateTotalPremium(CustomerModel x)
        {
                double _totalPremium = x.PaymentDetails.AnnualPremium + x.PaymentDetails.CreditCharge;
                return Math.Round(_totalPremium, 2);
        }
        private double CalculateCreditCharge(CustomerModel x)
        {
                double _creditCharge = x.PaymentDetails.AnnualPremium * creditServiceRate;
                return Math.Round(_creditCharge, 2);
        }
    }
}
