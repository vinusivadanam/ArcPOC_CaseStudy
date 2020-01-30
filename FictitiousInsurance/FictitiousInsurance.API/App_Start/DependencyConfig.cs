//-----------------------------------------------------------------------
// <copyright file="DependencyConfig.cs" company="FictiousInsurance">
// Create all your dependency here.
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.API
{
    using System.Web.Http;
    using System.Web.Mvc;
    using FictitiousInsurance.Business;
    using FictitiousInsurance.DataAccess;
    using SimpleInjector;

    /// <summary>
    /// Mention all your dependencies here
    /// </summary>
    public class DependencyConfig
    {
        /// <summary>
        /// Method to register the dependencies
        /// </summary>
        public static void Register()
        {
            var container = new Container();
            container.Register<ICustomerNotificationService, CustomerNotificationService>();
            container.Register<ICustomerService, CustomerService>();
            container.Register<ICustomerNotificationRepository, CustomerNotificationRepository>();
            container.Register<IDataAccess, CSVDataAccess>();
            container.Register<IPaymentService, PaymentService>();
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
        }
    }
}