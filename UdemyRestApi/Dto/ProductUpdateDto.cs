using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Database.Dto
{
    public class ProductUpdateDto
    {
        [Required]
        [Key]
        public int ProductId { get; set; }
        
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string ProductName { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedOn { get; } = DateTime.Now;
    }
}
