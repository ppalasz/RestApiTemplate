using System;
using System.Linq;
using AutoMapper;
using WebApi.Database.Dto;
using WebApi.Database.Models;

namespace WebApi.Database.Services
{
    public interface IProductService
    {
        public IQueryable<ProductSelectDto> GetAll(string search);

        public ProductDetailsDto Get(int id);

        public int Add(ProductInsertDto product);

        public void Update(int id, ProductUpdateDto product);

        public void Delete(int id);
    }
}