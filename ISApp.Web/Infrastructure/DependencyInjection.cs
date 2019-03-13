using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ISApp.Core.Data;
using ISApp.Core.Logging;
using ISApp.Data;
using ISApp.Logging.Logging;
using ISApp.Web.Infrastructure.Modules;
using System.Web.Http;
using System.Web.Mvc;
using ISApp.Data.UnitOfWorks;
using ISApp.Data.Context;
using ISApp.Core.Data.UnitOfWorks;
using ISApp.Business.Common.Objects;

namespace ISApp.Web.Infrastructure
{
    public class DependencyInjection
    {
        public void Initialise(HttpConfiguration config)
        {
            var builder = ConfigureBuilder();          
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
            
        }

        public ContainerBuilder ConfigureBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();


            builder.RegisterFilterProvider();

            string databaseConnectionString = string.Format("name={0}", Globals.ConnectionStringName);

            builder.Register<IDbContext>(b =>
            {
                var logger = b.ResolveOptional<ILogger>();
                var context = new DatabaseContext(databaseConnectionString, logger);
                return context;
            }).InstancePerLifetimeScope();
           
            builder.Register(b => NLogLogger.Instance).SingleInstance();

            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ControllerModule>();

            return builder;
        }
    }
}