using Core.Persistence.Repositories;

namespace Allup.Domain.Entities;

public class BasketItem : Entity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string ClientId { get; set; }
    public int Count { get; set; }
    public decimal TotalPrice => Product.Price * Count;
}
