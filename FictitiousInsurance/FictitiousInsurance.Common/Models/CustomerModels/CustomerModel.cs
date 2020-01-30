//-----------------------------------------------------------------------
// <copyright file="CustomerModel.cs" company="FictiousInsurance">
// Application response
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Model
{
    /// <summary>
    /// Customer details
    /// </summary>
    public class CustomerModel
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
        /// Gets or sets Sur name
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Gets or sets Product details
        /// </summary>
        public ProductModel ProductDetails { get; set; }

        /// <summary>
        /// Gets or sets Payment details
        /// </summary>
        public PaymentModel PaymentDetails { get; set; }
    }
}
