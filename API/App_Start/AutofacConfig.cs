using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DataAccessLayer;
using DataAccessLayer.ApiKey;
using DataLogicLayer.ApiKey;

namespace API.App_Start
{
    /// <summary>
    /// Autofac config
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Initial setup for Autofac
        /// </summary>
        /// <returns></returns>
        public static IContainer SetupAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<Context>().AsImplementedInterfaces();
            builder.RegisterType<CustomerApiKeyFacade>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}