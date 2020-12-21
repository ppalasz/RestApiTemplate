using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Database.Models;

namespace WebApi.Database.Data
{
    public class ProductsDbContext: DbContext
    {
        public DbSet<Product> Products {get;set;}
        public DbSet<ProductType> ProductTypes { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {
            //Database.SetInitializer(new SchoolDBInitializer());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(s => s.CreatedOn)
                .HasDefaultValueSql("Getdate()");

            modelBuilder.Entity<Product>()
                .HasMany<Category>(s => s.Categories)
                .WithMany(c => c.Products);

            base.OnModelCreating(modelBuilder);
        }

        //protected void Seed(ProductsDbContext context)
        //{
        //    IList<Standard> defaultStandards = new List<Type>();

        //    defaultStandards.Add(new Standard() { StandardName = "Standard 1", Description = "First Standard" });
        //    defaultStandards.Add(new Standard() { StandardName = "Standard 2", Description = "Second Standard" });
        //    defaultStandards.Add(new Standard() { StandardName = "Standard 3", Description = "Third Standard" });

        //    context.Standards.AddRange(defaultStandards);

        //    base.Seed(context);
        //}

        /*
         
        Microsoft.EntityFrameworkCore.EntityState in EF Core with the following values:
            Added
            Modified
            Deleted
            Unchanged
            Detached
         
         */

        //https://www.entityframeworktutorial.net/basics/entity-states.aspx

        //https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entitystate?view=efcore-5.0
    }
}
