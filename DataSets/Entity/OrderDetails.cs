using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSets.Entity
{
    public class OrderDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId ")]
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}