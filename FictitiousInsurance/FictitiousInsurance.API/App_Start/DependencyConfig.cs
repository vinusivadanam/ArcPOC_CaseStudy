using FictitiousInsurance.Business;
using FictitiousInsurance.DataAccess;
using SimpleInjector;
using System.Web.Mvc;
using System.Web.Http;

namespace FictitiousInsurance.API
{
    public class DependencyConfig
    {
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