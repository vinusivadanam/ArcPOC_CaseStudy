using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictitiousInsurance.Model
{
    public class RenewalNotificationModel
    {
        public long CustomerId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string FullName
        {
            get
            {
                return $"{Title}. {FirstName} {SurName}";
            }
        }
        public string ProductName { get; set; }
        public double PayoutAmount { get; set; }
        public double AnnualPremium { get; set; }
        public double CreditCharge { get; set; }
        public double TotalPremium { get; set; }
        public double AvgMonthlyPremium { get; set; }
        public double InitialMonthlyPayAmount { get; set; }
        public double OtherMonthlyPayAmount { get; set; }
    }
}
