using System;

namespace DataSets.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategory Category { get; }
        IProduct Product { get; }
        ISaleDetails SaleDetails { get; }
        IMonthDetails MonthDetails { get; }
        void Save();
    }
}