using Autofac;
using DataSets.Interfaces;
using PointOfSale.RepositoryPattern.Repository;
using PointOfSale.Services;

namespace PointOfSale.Modules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<CategoryServices>().AsSelf().SingleInstance();
            builder.RegisterType<ProductServices>().AsSelf().SingleInstance();
            builder.RegisterType<SaleServices>().AsSelf().SingleInstance();
            builder.RegisterType<MonthlyDetailServices>().AsSelf().SingleInstance();
            base.Load(builder);
        }
    }
}
