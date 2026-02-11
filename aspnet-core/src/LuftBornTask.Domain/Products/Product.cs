using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace LuftBornTask.Products
{
    public class Product : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        protected Product()
        {
        }

        public Product(Guid id, string name, decimal price, string description) : base(id)
        {
            SetName(name);
            SetPrice(price);
            SetDescription(description);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be empty", nameof(name));
            }

            if (name.Length > 128)
            {
                throw new ArgumentException("Product name cannot be longer than 128 characters", nameof(name));
            }

            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(price));
            }

            Price = price;
        }

        public void SetDescription(string description)
        {
            if (description != null && description.Length > 500)
            {
                throw new ArgumentException("Description cannot be longer than 500 characters", nameof(description));
            }

            Description = description;
        }
    }
}
