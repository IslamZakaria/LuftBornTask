using System;
using Shouldly;
using Xunit;

namespace LuftBornTask.Products;

public class Product_Tests : LuftBornTaskDomainTestBase<LuftBornTaskDomainTestModule>
{
    [Fact]
    public void Should_Set_Name()
    {
        // Arrange
        var product = new Product(Guid.NewGuid(), "Test Product", 10, "Description");

        // Act
        product.SetName("New Name");

        // Assert
        product.Name.ShouldBe("New Name");
    }

    [Fact]
    public void Should_Set_Price()
    {
        // Arrange
        var product = new Product(Guid.NewGuid(), "Test Product", 10, "Description");

        // Act
        product.SetPrice(20);

        // Assert
        product.Price.ShouldBe(20);
    }

    [Fact]
    public void Should_Set_Description()
    {
        // Arrange
        var product = new Product(Guid.NewGuid(), "Test Product", 10, "Description");

        // Act
        product.SetDescription("New Description");

        // Assert
        product.Description.ShouldBe("New Description");
    }

    [Fact]
    public void Should_Create_Product()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Test Product";
        var price = 10.5m;
        var description = "Test Description";

        // Act
        var product = new Product(id, name, price, description);

        // Assert
        product.Id.ShouldBe(id);
        product.Name.ShouldBe(name);
        product.Price.ShouldBe(price);
        product.Description.ShouldBe(description);
    }
}
