using System.Collections.Generic;

namespace DataSets.Entity
{
    public class Category : BaseEntity
    {
        public double Invest { get; set; }
        public int NoOfProduct { get; set; }
        public List<Product> Products { get; set; }
    }
}