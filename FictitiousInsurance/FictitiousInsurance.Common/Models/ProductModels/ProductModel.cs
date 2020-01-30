//-----------------------------------------------------------------------
// <copyright file="ProductModel.cs" company="FictiousInsurance">
// Application response
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Model
{
    /// <summary>
    /// Product model
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Gets or sets Product id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Gets or sets product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets product description
        /// </summary>
        public string ProductDescription { get; set; }
    }
}
