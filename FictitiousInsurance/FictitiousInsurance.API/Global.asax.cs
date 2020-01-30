//-----------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="FictiousInsurance">
// Global settings
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.API
{
    using System.Web.Http;

    /// <summary>
    /// Web Application settings
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// App Start method
        /// </summary>
        protected void Application_Start()
        { 
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DependencyConfig.Register();
        }
    }
}
