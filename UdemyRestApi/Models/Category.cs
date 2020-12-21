using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Database.Models
{
    [Table("Category", Schema = "Shop")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        [Column("Category")]
        public string Type { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}
