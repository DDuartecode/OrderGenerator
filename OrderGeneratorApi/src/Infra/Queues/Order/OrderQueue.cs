using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using OrderGeneratorApi.Domain.Entities;
using OrderGeneratorApi.Domain.Interfaces;
using OrderGeneratorApi.Infra.Settings;

namespace OrderGeneratorApi.Infra.Queues.Order;

public class OrderQueue : IOrder
{
    private readonly RabbitMqSettings _settings;

    public OrderQueue(IOptions<RabbitMqSettings> settings)
    {
        if (!settings.Value.IsValid())
        {
            throw new Exception("Invalid RabbitMQ settings.");
        }
        
        _settings = settings.Value;
    }

    public async Task SendOrderAsync(OrderEntity order)
    {

        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };
        
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "orders",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        // tentativa para manter as mensagens mesmo se o container morrer ou for reconstruído
        // var properties = new BasicProperties
        // {
        //     Persistent = true
        // };

        var message = JsonSerializer.Serialize(order);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: "orders",
            // mandatory: false,
            // basicProperties: properties,
            body: body
        );

        await Task.CompletedTask;
    }
}