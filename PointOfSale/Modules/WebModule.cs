using Autofac;
using DataSets.Interfaces;
using PointOfSale.RepositoryPattern.Repository;

namespace PointOfSale.Modules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            base.Load(builder);
        }
    }
}
