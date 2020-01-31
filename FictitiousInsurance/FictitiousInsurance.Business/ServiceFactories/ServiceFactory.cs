//-----------------------------------------------------------------------
// <copyright file="ServiceFactory.cs" company="FictiousInsurance">
// Customer service
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Business.Factories
{
    /// <summary>
    /// Service factory class
    /// </summary>
    public sealed class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// Create payment service based on product name
        /// </summary>
        /// <param name="productName">Product name</param>
        /// <returns>payment service</returns>
        public IPaymentService GetPaymentService(string productName)
        {
            switch (productName)
            {
                case "Standard Cover":
                    return new PaymentService();
                default:
                    return new PaymentService();
            }
        }
    }
}
