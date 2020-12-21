using AutoMapper;
using WebApi.Database.Dto;
using WebApi.Database.Models;

namespace WebApi.Startup
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Product, ProductSelectDto>();

            CreateMap<Product, ProductDetailsDto>();

            CreateMap<ProductInsertDto, Product>();

            CreateMap<ProductUpdateDto, Product>();
        }
    }
}