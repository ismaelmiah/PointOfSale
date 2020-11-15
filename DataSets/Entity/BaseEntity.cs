using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DataSets.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}