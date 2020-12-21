
using WebApi.Database.Models;

namespace WebApi.Database.Dto
{
    public class ProductSelectDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        
        public double ProductPrice { get; set; }
    }
}
