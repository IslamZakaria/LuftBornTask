using System;
using System.Threading.Tasks;
using LuftBornTask.Products;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace LuftBornTask.Application.Tests.Products
{
    public class ProductAppService_Tests : LuftBornTaskApplicationTestBase<LuftBornTaskApplicationTestModule>
    {
        private readonly IProductAppService _productAppService;

        public ProductAppService_Tests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
        }

        [Fact]
        public async Task Should_Create_A_Valid_Product()
        {
            // Act
            var output = await _productAppService.CreateAsync(
                new CreateUpdateProductDto
                {
                    Name = "Test Product 1",
                    Price = 100,
                    Description = "Description for Test Product 1"
                }
            );

            // Assert
            output.Id.ShouldNotBe(Guid.Empty);
            output.Name.ShouldBe("Test Product 1");
            output.Price.ShouldBe(100);
        }

        [Fact]
        public async Task Should_Get_List_Of_Products()
        {
            // Arrange
            await _productAppService.CreateAsync(
                new CreateUpdateProductDto
                {
                    Name = "Product A",
                    Price = 50,
                    Description = "Product A Description"
                }
            );

            await _productAppService.CreateAsync(
                new CreateUpdateProductDto
                {
                    Name = "Product B",
                    Price = 150,
                    Description = "Product B Description"
                }
            );

            // Act
            var result = await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Assert
            result.Items.ShouldContain(p => p.Name == "Product B");
        }

        [Fact]
        public async Task Should_Update_Product()
        {
            // Arrange
            var product = await _productAppService.CreateAsync(
                new CreateUpdateProductDto
                {
                    Name = "Product to Update",
                    Price = 100,
                    Description = "Initial Description"
                }
            );

            // Act
            var updatedProduct = await _productAppService.UpdateAsync(
                product.Id,
                new CreateUpdateProductDto
                {
                    Name = "Updated Product Name",
                    Price = 150,
                    Description = "Updated Description"
                }
            );

            // Assert
            updatedProduct.Id.ShouldBe(product.Id);
            updatedProduct.Name.ShouldBe("Updated Product Name");
            updatedProduct.Price.ShouldBe(150);
            updatedProduct.Description.ShouldBe("Updated Description");
        }

        [Fact]
        public async Task Should_Delete_Product()
        {
            // Arrange
            var product = await _productAppService.CreateAsync(
                new CreateUpdateProductDto
                {
                    Name = "Product to Delete",
                    Price = 100,
                    Description = "Will be deleted"
                }
            );

            // Act
            await _productAppService.DeleteAsync(product.Id);

            // Assert
            var exception = await Assert.ThrowsAsync<Volo.Abp.Domain.Entities.EntityNotFoundException>(async () =>
            {
                await _productAppService.GetAsync(product.Id);
            });

            exception.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Fail_To_Create_Invalid_Product()
        {
            // Act & Assert
            await Assert.ThrowsAsync<Volo.Abp.Validation.AbpValidationException>(async () =>
            {
                await _productAppService.CreateAsync(
                    new CreateUpdateProductDto
                    {
                        Name = "", // Invalid: Required
                        Price = 100
                    }
                );
            });
        }
    }
}
