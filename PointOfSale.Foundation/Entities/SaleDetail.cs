using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation
{
    public class SaleDetail : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime SaleDate { get; set; }
    }
}