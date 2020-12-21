using System;

namespace WebApi.Database.Dto
{
    public class ProductDetailsDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public double ProductPrice { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
