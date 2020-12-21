using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Database.Models
{
    [Table("Product", Schema = "Shop")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        [Column("Name")]
        public string ProductName { get; set; }

        [ForeignKey("ProductType")]
        //[Index("INDEX_REGNUM", IsClustered = true, IsUnique = true)]
        public int TypeId { get; set; }

        public ProductType ProductType { get; set; }

        [Required]
        [Column("Price")]
        public double ProductPrice { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        //[DefaultValue("Getdate()")]
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
        
        [NotMapped]
        public int SomeStuff { get; set; }
        
        public ICollection<Category> Categories { get; set; }
    }
}
