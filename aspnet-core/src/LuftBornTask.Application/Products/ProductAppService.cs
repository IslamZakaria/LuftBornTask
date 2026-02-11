using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using LuftBornTask.Permissions;

namespace LuftBornTask.Products
{
    [Authorize(LuftBornTaskPermissions.Products.Default)]
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
            
            GetPolicyName = LuftBornTaskPermissions.Products.Default;
            GetListPolicyName = LuftBornTaskPermissions.Products.Default;
            CreatePolicyName = LuftBornTaskPermissions.Products.Create;
            UpdatePolicyName = LuftBornTaskPermissions.Products.Edit;
            DeletePolicyName = LuftBornTaskPermissions.Products.Delete;
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return product.ToDto();
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _productRepository.GetQueryableAsync();
            
            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query.OrderBy(p => p.Name) // Default sorting
                         .PageBy(input);

            var products = await AsyncExecuter.ToListAsync(query);

            var productDtos = products.Select(p => p.ToDto()).ToList();

            return new PagedResultDto<ProductDto>(totalCount, productDtos);
        }

        [Authorize(LuftBornTaskPermissions.Products.Create)]
        public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            Logger.LogInformation("Creating a new product: {ProductName} with price {Price}", input.Name, input.Price);

            var product = new Product(
                GuidGenerator.Create(),
                input.Name,
                input.Price,
                input.Description
            );

            await _productRepository.InsertAsync(product);

            return product.ToDto();
        }

        [Authorize(LuftBornTaskPermissions.Products.Edit)]
        public async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            Logger.LogInformation("Updating product {ProductId} with new name {ProductName} and price {Price}", id, input.Name, input.Price);

            var product = await _productRepository.GetAsync(id);

            product.Name = input.Name;
            product.Price = input.Price;
            product.Description = input.Description;

            await _productRepository.UpdateAsync(product);

            return product.ToDto();
        }

        [Authorize(LuftBornTaskPermissions.Products.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            Logger.LogInformation("Deleting product {ProductId}", id);
            await _productRepository.DeleteAsync(id);
        }
    }
}
