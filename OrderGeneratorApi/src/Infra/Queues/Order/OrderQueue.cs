using OrderGeneratorApi.Domain.Entities;
using OrderGeneratorApi.Domain.Interfaces;

namespace OrderGeneratorApi.Infra.Queues.Order;

public class OrderQueue : IOrder
{
    public async Task SendOrderAsync(OrderEntity order)
    {
        await Task.Run(() => 
        {
            Console.WriteLine($"Order {order.Id} sent to queue.");
        });
    }
}