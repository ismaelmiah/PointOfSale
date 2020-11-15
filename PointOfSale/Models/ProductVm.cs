using System.Collections.Generic;
using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PointOfSale.Models
{
    public class ProductVm
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
