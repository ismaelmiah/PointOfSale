using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSets.Entity
{
    public class SalesDetails
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProductId ")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        [Column(TypeName = "Date")]
        public DateTime SaleDate { get; set; }
    }
}