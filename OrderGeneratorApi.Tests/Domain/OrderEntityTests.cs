using System;
using System.Collections.Generic;
using OrderGeneratorApi.Domain.Entities;
using OrderGeneratorApi.Domain.Enums;
using Xunit;

namespace OrderGeneratorApi.Tests.Domain
{
    public class OrderEntityTests
    {
        [Fact]
        public void IsValid_ShouldReturnTrueIfOrderIsValid()
        {
            // Arrange
            var order = new OrderEntity
            {
                Products = new List<ProductEntity>
                {
                    new ProductEntity { Id = Guid.NewGuid() },
                },
                OrderDate = DateTime.UtcNow,
                TotalAmount = 10.0,
                Status = OrderStatus.Pending
            };

            // Act
            var result = order.IsValid();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldThrowArgumentNullExceptionIfNoProducts()
        {
            // Arrange
            var order = new OrderEntity
            {
                Products = new List<ProductEntity>(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 10.0,
                Status = OrderStatus.Pending
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => order.IsValid());
        }

        [Fact]
        public void IsValid_ShouldThrowArgumentOutOfRangeExceptionIfTotalAmountIsZeroOrNegative()
        {
            // Arrange
            var order = new OrderEntity
            {
                Products = new List<ProductEntity>
                {
                    new ProductEntity { Id = Guid.NewGuid() },
                },
                OrderDate = DateTime.UtcNow,
                TotalAmount = 0.0,
                Status = OrderStatus.Pending
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => order.IsValid());
        }

        [Fact]
        public void IsValid_ShouldValidateEachProduct()
        {
            // Arrange
            var order = new OrderEntity
            {
                Products = new List<ProductEntity>
                {
                    new ProductEntity { Id = Guid.NewGuid() },
                    new ProductEntity { Id = Guid.Empty } // Invalid product
                },
                OrderDate = DateTime.UtcNow,
                TotalAmount = 10.0,
                Status = OrderStatus.Pending
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => order.IsValid());
        }    
    }
}