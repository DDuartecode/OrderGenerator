namespace OrderGeneratorApi.Domain.Entities;

public class OrderEntity
{
    public Guid Id { get; set; } = Guid.NewGuid(); //TODO:Guid informado inválido continua retornando erro não tratado
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public double TotalAmount { get; set; } = 0.0;
    public string Status { get; set; } = "Pending";

    public bool IsValid()
    {
        if (Products.Count <= 0)
        {
            throw new ArgumentNullException(nameof(Products), "Order must have at least one product.");
        }

        Products.ForEach(product => product.IsValid());

        if (TotalAmount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(TotalAmount), "Total amount must be greater than zero.");
        }

        return true;
    }
}