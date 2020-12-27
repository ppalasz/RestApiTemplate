using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Database.Dto;
using WebApi.Database.Models;

namespace WebApi.Database.Services
{
    public interface IProductService
    {
        public Task<IQueryable<ProductSelectDto>> GetAllAsync(string search, string sortBy, string sortOrder, int pageNr = 1, int pageSize = 50);
        
        public Task<ProductDetailsDto> GetAsync(int id);

        public Task<int> AddAsync(ProductInsertDto product);

        public Task UpdateAsync(int id, ProductUpdateDto product);

        public Task DeleteAsync(int id);
    }
}