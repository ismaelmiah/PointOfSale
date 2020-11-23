using System.Collections.Generic;
using DataSets.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PointOfSale.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public SalesDetails SalesDetails { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
