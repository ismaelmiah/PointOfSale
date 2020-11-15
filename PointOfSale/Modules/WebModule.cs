using Autofac;
using DataSets.Interfaces;
using PointOfSale.Models;
using PointOfSale.RepositoryPattern.Repository;

namespace PointOfSale.Modules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<ProductVm>().AsSelf().SingleInstance();
            base.Load(builder);
        }
    }
}
