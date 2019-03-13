using Autofac;
using ISApp.Core.Data.Repositories;
using ISApp.Data.Repositories;
using ISApp.Web.Infrastructure.References;

namespace ISApp.Web.Infrastructure.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
        
            builder.RegisterAssemblyTypes(ReferencedAssemblies.Repositories)
                .Where(_ => _.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}