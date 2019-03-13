using System.Reflection;

namespace ISApp.Web.Infrastructure.References
{
    public static class ReferencedAssemblies
    {
        public static Assembly Services
        {
            get { return Assembly.Load("ISApp.Services"); }
        }

        public static Assembly Repositories
        {
            get { return Assembly.Load("ISApp.Data"); }
        }

        public static Assembly Dto
        {
            get
            {
                return Assembly.Load("ISApp.Dto");
            }
        }

        public static Assembly Domain
        {
            get
            {
                return Assembly.Load("ISApp.Core");
            }
        }
    }
}