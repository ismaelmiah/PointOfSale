using System;

namespace PointOfSale.DataAccessLayer
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
