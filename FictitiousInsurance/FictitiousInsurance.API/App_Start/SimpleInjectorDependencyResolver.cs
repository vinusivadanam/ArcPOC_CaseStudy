//-----------------------------------------------------------------------
// <copyright file="SimpleInjectorDependencyResolver.cs" company="FictiousInsurance">
// This will resolve dependency
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using SimpleInjector;

    /// <summary>
    /// Simple injection resolver
    /// </summary>
    public sealed class SimpleInjectorDependencyResolver : IDependencyResolver, IDependencyScope, System.Web.Mvc.IDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorDependencyResolver" /> class
        /// </summary>
        /// <param name="container">Container param</param>
        public SimpleInjectorDependencyResolver(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Container not available.");
            }

            this.Container = container;
        }

        /// <summary>
        /// Gets Container
        /// </summary>
        public Container Container { get; private set; }

        /// <summary>
        /// Dependency resolver
        /// </summary>
        /// <returns>Dependency resolver for web http</returns>
        IDependencyScope System.Web.Http.Dependencies.IDependencyResolver.BeginScope()
        {
            return this;
        }

        /// <summary>
        /// Get Service for web http.
        /// </summary>
        /// <param name="serviceType">The source of the event.</param>
        /// <returns>service object</returns>
        object IDependencyScope.GetService(Type serviceType)
        {
            return ((IServiceProvider)this.Container)
                .GetService(serviceType);
        }

        /// <summary>
        /// Get Services
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service Objects</returns>
        IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
        {
            IServiceProvider provider = Container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var services = (IEnumerable<object>)provider.GetService(collectionType);
            return services ?? Enumerable.Empty<object>();
        }

        /// <summary>
        /// Get Service
        /// </summary>
        /// <param name="serviceType">Service Type</param>
        /// <returns>Service Object</returns>
        public object GetService(Type serviceType)
        {
            if (!serviceType.IsAbstract && typeof(System.Web.Mvc.IController).IsAssignableFrom(serviceType))
            {
                return this.Container.GetInstance(serviceType);
            }

            return ((IServiceProvider)this.Container).GetService(serviceType);
        }

        /// <summary>
        /// Get Services
        /// </summary>
        /// <param name="serviceType">Service Type</param>
        /// <returns>Service object</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Container.GetAllInstances(serviceType);
        }

        /// <summary>
        /// Dispose all
        /// </summary>
        public void Dispose()
        {
        }
    }
}