using OrderGeneratorApi.Domain.Entities;
using OrderGeneratorApi.Domain.Interfaces;

namespace OrderGeneratorApi.App.UseCases.Order;

public class CreateOrderUseCase
{
    private readonly IOrder _orderRepository;

    public CreateOrderUseCase(IOrder orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task HandleAsync(OrderEntity order)
    {
        order.IsValid();

        await _orderRepository.SendOrderAsync(order);
    }
}