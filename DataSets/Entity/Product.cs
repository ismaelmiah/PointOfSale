using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSets.Entity
{
    public class Product : BaseEntity
    {
        [DisplayName("Buying Price")]
        [Required(ErrorMessage = "Buying Price is required!")]
        public double BuyPrice { get; set; }
        [DisplayName("Sale Price")]
        [Required(ErrorMessage = "Sale Price is required!")]
        public double SalePrice { get; set; }
        public double Quantity { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
