using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSets.Entity
{
    public class Account
    {

        [Key]
        public Guid Id { get; set; }
        public double Profit { get; set; }
        public double Loss { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public double Balance { get; set; }
    }
}