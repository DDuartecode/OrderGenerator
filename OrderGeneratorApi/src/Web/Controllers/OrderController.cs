using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderGeneratorApi.App.UseCases.Order;
using OrderGeneratorApi.Domain.Entities;

namespace OrderGeneratorApi.Web.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly CreateOrderUseCase _createOrderUseCase;

    public OrderController(
        ILogger<OrderController> logger,
        CreateOrderUseCase createOrderUseCase
    )
    {
        _logger = logger;
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpPost]
    [ApiVersion("1.0")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderEntity order)
    {
        if (order == null)
        {
            _logger.LogError("Order cannot be null");
            return BadRequest("Order cannot be null");
        }

        await _createOrderUseCase.HandleAsync(order);

        _logger.LogInformation($"Pedido {order.Id} enviado com sucesso");
        return Ok($"Pedido {order.Id} enviado com sucesso");
    }
}