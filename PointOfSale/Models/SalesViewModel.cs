using System.Collections.Generic;
using DataSets.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PointOfSale.Models
{
    public class SalesViewModel
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public OrderDetails Details { get; set; }
    }
}