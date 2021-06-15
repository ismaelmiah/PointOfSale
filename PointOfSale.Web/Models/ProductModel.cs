using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PointOfSale.Web.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
    }
}