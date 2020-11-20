using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSets.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfEntry { get; set; }
    }
}