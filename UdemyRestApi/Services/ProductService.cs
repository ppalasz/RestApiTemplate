using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using WebApi.Database.Data;
using WebApi.Database.Dto;
using WebApi.Database.Models;
using WebApi.Database.Helpers;

namespace WebApi.Database.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsDbContext _productContext;
        private readonly IMapper _mapper;

        public ProductService(ProductsDbContext productContext, IMapper mapper)
        {
            _productContext = productContext;
            _mapper = mapper;
        }

        public async Task<IQueryable<ProductSelectDto>> GetAllAsync(
            string search,
            string sortBy, 
            string sortOrder, 
            int pageNr=1, 
            int pageSize=50)
        {
            var products = _productContext
                .Products
                .Where(x =>
                    x.ProductName.Contains(search)
                    || string.IsNullOrWhiteSpace(search));

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortOrder == "desc")
                {
                    products = products
                        .OrderByDescending(sortBy);
                }
                else
                {
                    products = products
                        .OrderBy(sortBy);
                }
            }

            if (pageNr > 1)
            {
                products = products
                    .Skip(pageSize * (pageNr-1))
                    .Take(pageSize);
            }    
            
            if(pageSize > 0)
            {
                products = products
                    .Take(pageSize);
            }

            var results = (await products.ToListAsync()).AsQueryable();

            return results.Select(x => _mapper.Map<ProductSelectDto>(x));
        }

        public async Task<ProductDetailsDto> GetAsync(int id)
        {
            var product = await _productContext
                .Products
                .SingleOrDefaultAsync(x => x.ProductId == id);

            return _mapper.Map<ProductDetailsDto>(product);
        }

        public async Task<int> AddAsync(ProductInsertDto product)
        {
            var newProduct = _mapper.Map<Product>(product);

            var productAdded = await _productContext
                .Products
                .AddAsync(newProduct);

            await _productContext.SaveChangesAsync();

            return productAdded.Entity.ProductId;
        }

        public async void UpdateAsync(int id, ProductUpdateDto product)
        {
            var productFound = await _productContext
                .Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProductId == id);

            if (productFound == null || product.ProductId != id)
                throw new KeyNotFoundException();
            
            _mapper.Map(product, productFound);

            _productContext.Products.Update(productFound);
            
            await _productContext.SaveChangesAsync ();
        }

        public async void DeleteAsync(int id)
        {
            var productFound = await _productContext
                .Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProductId == id);

            if (productFound == null)
                throw new KeyNotFoundException();
            
            _productContext.Products.Remove(productFound);

            await _productContext.SaveChangesAsync();
        }
    }
}