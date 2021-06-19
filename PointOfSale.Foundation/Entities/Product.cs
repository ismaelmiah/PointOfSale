using System;
using System.Collections.Generic;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}