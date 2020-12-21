using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using WebApi.Database.Data;
using WebApi.Database.Dto;
using WebApi.Database.Models;

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

        public IQueryable<ProductSelectDto> GetAll(string search)
        {
            var products = _productContext
                .Products
                .Where(x =>
                    x.ProductName.Contains(search) 
                    || String.IsNullOrWhiteSpace(search))
                .Select(x => _mapper.Map<ProductSelectDto>(x));

            return products;
        }

        public ProductDetailsDto Get(int id)
        {
            var product = _productContext
                .Products
                .SingleOrDefault(x => x.ProductId == id);

            return _mapper.Map<ProductDetailsDto>(product);
        }

        public int Add(ProductInsertDto product)
        {
            var newProduct = _mapper.Map<Product>(product);

            var productAdded = _productContext.Products.Add(newProduct);

            _productContext.SaveChanges();

            return productAdded.Entity.ProductId;
        }

        public void Update(int id, ProductUpdateDto product)
        {
            var productFound = _productContext
                .Products
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductId == id);

            if (productFound == null || product.ProductId != id)
                throw new KeyNotFoundException();
            
            _mapper.Map(product, productFound);

            _productContext.Products.Update(productFound);
            
            _productContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var productFound = _productContext
                .Products
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductId == id);

            if (productFound == null)
                throw new KeyNotFoundException();
            
            _productContext.Products.Remove(productFound);

            _productContext.SaveChanges();
        }
    }
}