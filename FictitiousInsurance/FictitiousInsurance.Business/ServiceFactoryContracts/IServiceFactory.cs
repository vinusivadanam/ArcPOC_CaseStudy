//-----------------------------------------------------------------------
// <copyright file="IServiceFactory.cs" company="FictiousInsurance">
// Customer service
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Business.Factories
{
    /// <summary>
    /// Service factory contract
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Create payment service based on product name
        /// </summary>
        /// <param name="productName">Product name</param>
        /// <returns>payment service</returns>
        IPaymentService GetPaymentService(string productName);
    }
}
