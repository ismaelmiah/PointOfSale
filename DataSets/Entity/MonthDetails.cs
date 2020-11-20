using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSets.Entity
{
    public class MonthDetails
    {

        [Key]
        public Guid Id { get; set; }
        public double Profit { get; set; }
        public double Loss { get; set; }
        public double Invest { get; set; }
        public double Balance { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfDetails { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}