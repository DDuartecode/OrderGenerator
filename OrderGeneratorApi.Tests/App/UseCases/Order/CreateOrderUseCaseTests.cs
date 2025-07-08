using System;
using Moq;
using Xunit;
using System.Threading.Tasks;
using OrderGeneratorApi.Domain.Entities;
using OrderGeneratorApi.Domain.Enums;
using OrderGeneratorApi.Domain.Interfaces;
using OrderGeneratorApi.App.UseCases.Order;

namespace OrderGeneratorApi.Tests.App.UseCases.Order
{
    public class CreateOrderUseCaseTests
    {
        private readonly Mock<IOrder> _orderRepositoryMock;
        private readonly CreateOrderUseCase _createOrderUseCase;

        public CreateOrderUseCaseTests()
        {
            _orderRepositoryMock = new Mock<IOrder>();
            _createOrderUseCase = new CreateOrderUseCase(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateOrder_WhenOrderIsValid()
        {
            // Arrange
            var order = new OrderEntity
            {
                Products = new List<ProductOrderEntity>
                {
                    new ProductOrderEntity { 
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Price = 10.0
                    }
                },
                OrderDate = DateTime.UtcNow,
                TotalAmount = 100.0,
                Status = OrderStatus.Pending
            };

            _orderRepositoryMock.Setup(repo => repo.SendOrderAsync(order)).Returns(Task.CompletedTask);

            // Act
            await _createOrderUseCase.HandleAsync(order);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.SendOrderAsync(It.Is<OrderEntity>(o => o.IsValid())), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenOrderIsInvalid()
        {
            // Arrange
            var order = new OrderEntity
            {
                Products = new List<ProductOrderEntity>(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 100.0,
                Status = OrderStatus.Pending
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _createOrderUseCase.HandleAsync(order));
        }
    }
}