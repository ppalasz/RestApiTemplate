using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        
        public double ProductPrice { get; set; }
    }
}
