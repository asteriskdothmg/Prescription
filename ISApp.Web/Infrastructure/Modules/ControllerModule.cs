using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace ISApp.Web.Infrastructure.Modules
{
    public class ControllerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterControllers(Assembly.Load("ISApp.Web"));
            builder.RegisterApiControllers(Assembly.Load("ISApp.Web"));
            builder.RegisterControllers(Assembly.Load("ISApp.Web"));
        }
    }
}