using OrderGeneratorApi.Domain.Entities;

namespace OrderGeneratorApi.Domain.Interfaces;

public interface IOrder
{
    Task SendOrderAsync(OrderEntity order);
}