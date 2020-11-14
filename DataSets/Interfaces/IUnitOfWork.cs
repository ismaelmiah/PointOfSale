using System;

namespace DataSets.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategory Category { get; }
        IProduct Product { get; }
        void Save();
    }
}