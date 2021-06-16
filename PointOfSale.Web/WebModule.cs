using System.Reflection;
using Autofac;

namespace PointOfSale.Web
{
    public class WebModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public WebModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var webExecutingAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(webExecutingAssembly).Where(x => x.Namespace != null && x.Namespace.Contains("PointOfSale.Web.Models")).AsSelf();

            base.Load(builder);
        }
    }
}