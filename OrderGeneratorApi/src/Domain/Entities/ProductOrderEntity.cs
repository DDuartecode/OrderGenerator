namespace OrderGeneratorApi.Domain.Entities;

public class ProductOrderEntity
{
    public Guid OrderId { get; set; } = Guid.Empty;
    public Guid ProductId { get; set; } = Guid.Empty;
    public int Quantity { get; set; } = 0;
    public double Price { get; set; } = 0.0;


    public bool IsValid()
    {
        if (OrderId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(OrderId), "ProductOrder OrderId cannot be empty.");
        }

        if (ProductId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(ProductId), "ProductOrder ProductId cannot be empty.");
        }

        if (Quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Quantity), "ProductOrder Quantity must be greater than zero.");
        }

        if (Price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Price), "ProductOrder Price cannot be negative.");
        }

        return true;
    }
}