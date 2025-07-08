using OrderGeneratorApi.Domain.Enums;

namespace OrderGeneratorApi.Domain.Entities;

public class OrderEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public List<ProductOrderEntity> Products { get; set; } = new List<ProductOrderEntity>();
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public double TotalAmount { get; set; } = 0.0;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public bool IsValid()
    {
        if (Products.Count <= 0) {
            throw new ArgumentNullException(nameof(Products), "Order must have at least one product.");
        }

        Products.ForEach(product => product.IsValid());

        if (TotalAmount <= 0) {
            throw new ArgumentOutOfRangeException(nameof(TotalAmount), "Total amount must be greater than zero.");
        }

        return true;
    }
}