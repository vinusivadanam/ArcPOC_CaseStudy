//-----------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="FictiousInsurance">
// All configurations for web api
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.API
{
    using System.Web.Http;

    /// <summary>
    /// WebApi config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Web API registrations
        /// </summary>
        /// <param name="config">Http Config</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
