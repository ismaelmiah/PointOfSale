using System.Collections.Generic;

namespace DataSets.Entity
{
    public class Category : BaseEntity
    {
        public List<Product> Products { get; set; }
    }
}