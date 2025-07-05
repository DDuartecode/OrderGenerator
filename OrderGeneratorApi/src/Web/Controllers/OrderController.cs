using Serilog;
using Microsoft.AspNetCore.Mvc;
using OrderGeneratorApi.App.UseCases.Order;
using OrderGeneratorApi.Domain.Entities;
using OrderGeneratorApi.Domain.Interfaces;

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
        try
        {
            _logger.LogInformation($"Request receveid for send the order: {order.Id}");
            await _createOrderUseCase.HandleAsync(order);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError($"Failed to send order {order.Id} for processing - Message: {ex.Message}", ex);
            return BadRequest($"Failed to send order {order.Id} for processing - Message: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unespected error for sending order {order.Id}: {ex.Message}", ex);
            return BadRequest($"Failed to send order {order.Id}: {ex.Message}");
        }


        _logger.LogInformation($"Order {order.Id} sent for processing successfully");
        return Ok($"Order {order.Id} sent for processing successfully");
    }
}