using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSets.Entity
{
    public class Product : BaseEntity
    {
        [DisplayName("Price")]
        [Required(ErrorMessage = "Price is required!")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Quantity is required!")]
        public int Quantity { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
