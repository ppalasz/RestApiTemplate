using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Database.Dto
{
    public class ProductInsertDto
    {
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string ProductName { get; set; }

        [Required]
        public double ProductPrice { get; set; }
        
        [Required]
        public int TypeId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required] public DateTime CreatedOn { get; } = DateTime.Now;
    }
    
}
