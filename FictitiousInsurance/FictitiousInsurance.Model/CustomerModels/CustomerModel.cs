using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictitiousInsurance.Model
{
    public class CustomerModel
    {
        public long CustomerId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }

        public ProductModel ProductDetails { get; set; }
        public PaymentModel PaymentDetails { get; set; }
    }
}
