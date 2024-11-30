namespace Allup.Application.ViewModels;

public class BasketItemViewModel 
{
    public int Id { get; set; }
    public string ClientId { get; set; } = null!;
    public ProductViewModel? Product { get; set; }
    public required int ProductId { get; set; }
    public int Count { get; set; }

}

public class CreateBasketItemViewModel
{
    public string ClientId { get; set; } = null!;
    public required int ProductId { get; set; }
    public int Count { get; set; }
}
public class UpdateBasketItemViewModel
{
    public int Id { get; set; }
    public string ClientId { get; set; } = null!;
    public required int ProductId { get; set; }
    public int Count { get; set; }
}

