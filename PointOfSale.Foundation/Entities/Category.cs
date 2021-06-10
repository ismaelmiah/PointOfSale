using System;
using System.Collections.Generic;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation
{
    public class Category : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Invest { get; set; }
        public int NoOfProduct { get; set; }
        public int StockProduct { get; set; }
        public double Sales { get; set; }
        public List<Product> Products { get; set; }
        public virtual MonthDetail MonthDetail { get; set; }
    }
}