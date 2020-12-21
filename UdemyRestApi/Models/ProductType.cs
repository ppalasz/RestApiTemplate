using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Database.Models
{
    [Table("Type", Schema = "Shop")]
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        [Column("Type")]
        public string Type { get; set; }

        //[InverseProperty("Products")]
        public ICollection<Product> Products { get; set; }
    }
}
