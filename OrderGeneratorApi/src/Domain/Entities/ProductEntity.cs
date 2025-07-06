namespace OrderGeneratorApi.Domain.Entities;

public class ProductEntity
{
    public Guid Id { get; set; } = Guid.Empty; //TODO:Guid informado inválido continua retornando erro não tratado

    public bool IsValid()
    {
        if (Id == Guid.Empty) {
            throw new ArgumentNullException(nameof(Id), "Product ID cannot be empty.");
        }

        return true;
    }
}