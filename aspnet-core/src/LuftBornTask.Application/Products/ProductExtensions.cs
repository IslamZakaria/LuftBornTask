using System;
using LuftBornTask.Products;

namespace LuftBornTask.Products
{
    public static class ProductExtensions
    {
        public static ProductDto ToDto(this Product entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                CreationTime = entity.CreationTime,
                CreatorId = entity.CreatorId,
                LastModificationTime = entity.LastModificationTime,
                LastModifierId = entity.LastModifierId
            };
        }
        
        // Removed ToEntity as it was raising implemented exception anyway and not strictly needed for this pattern
        // if the AppService creates the entity via constructor.
    }
}
