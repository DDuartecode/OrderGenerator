using System;
using System.Collections.Generic;
using OrderGeneratorApi.Domain.Entities;
using Xunit;

namespace OrderGeneratorApi.Tests.Domain
{
    public class ProductOrderEntityTests
    {
        [Fact]
        public void IsValid_ShouldReturnTrueIfProductIsValid()
        {
            // Arrange
            var product = new ProductOrderEntity
            {
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 1,
                Price = 10.0
            };

            // Act
            var result = product.IsValid();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldThrowArgumentNullExceptionIfIdIsNullOrEmpty()
        {
            // Arrange
            var product = new ProductOrderEntity
            {
                OrderId = Guid.Empty,
                ProductId = Guid.NewGuid(),
                Quantity = 1,
                Price = 10.0
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => product.IsValid());
        }
    }
}
