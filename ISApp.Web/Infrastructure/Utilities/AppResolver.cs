using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISApp.Web.Infrastructure.Utilities
{
    public class AppResolver
    {
        public IContainer CreateContainer()
        {
            DependencyInjection resolver = new DependencyInjection();
            var builder = new ContainerBuilder();
 
            builder = resolver.ConfigureBuilder();
            var container = builder.Build();

            return container;
        }
    }
}