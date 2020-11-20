using System.Collections.Generic;

namespace DataSets.Entity
{
    public class Category : BaseEntity
    {
        public double Invest { get; set; }
        public int NoOfProduct { get; set; }
        public int StockProduct { get; set; }
        public double Sales { get; set; }
        public List<Product> Products { get; set; }
    }
}