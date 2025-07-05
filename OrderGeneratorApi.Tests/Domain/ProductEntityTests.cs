using System;
using System.Collections.Generic;
using OrderGeneratorApi.Domain.Entities;
using Xunit;

namespace OrderGeneratorApi.Tests.Domain
{
    public class ProductEntityTests
    {
        [Fact]
        public void IsValid_ShouldReturnTrueIfProductIsValid()
        {
            // Arrange
            var product = new ProductEntity
            {
                Id = Guid.NewGuid(),
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
            var product = new ProductEntity
            {
                Id = Guid.Empty,
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => product.IsValid());
        }
    }
}
