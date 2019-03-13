using Autofac;
using ISApp.Core.Services;
using ISApp.Services;
using ISApp.Web.Infrastructure.References;

namespace ISApp.Web.Infrastructure.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ReferencedAssemblies.Services)
                .Where(_ => _.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();
        }
    }
}