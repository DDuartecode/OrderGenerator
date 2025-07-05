namespace OrderGeneratorApi.Domain.Entities;

public class OrderEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
}