namespace OrderGeneratorApi.Domain.Entities;

public class OrderEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";

    public bool validate()
    {
        if (Products == null || Products.Count == 0)
        {
            throw new ArgumentNullException(nameof(Products), "Order must have at least one product.");
        }

        if (TotalAmount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(TotalAmount), "Total amount must be greater than zero.");
        }

        return true;
    }
}