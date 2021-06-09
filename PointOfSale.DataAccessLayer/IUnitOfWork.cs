using System;

namespace PointOfSale.DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}