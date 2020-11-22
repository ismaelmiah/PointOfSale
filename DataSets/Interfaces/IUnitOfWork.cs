using System;

namespace DataSets.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategory Category { get; }
        IProduct Product { get; }
        IOrderDetails OrderDetails { get; }
        IMonthDetails MonthDetails { get; }
        void Save();
    }
}