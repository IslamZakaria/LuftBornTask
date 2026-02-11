using System;
using Volo.Abp.Application.Dtos;

namespace LuftBornTask.Products
{
    public class ProductDto : AuditedEntityDto<Guid>
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
