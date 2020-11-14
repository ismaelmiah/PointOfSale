using System.Collections.Generic;
using DataSets.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PointOfSale.Models
{
    public class ProductVm
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
