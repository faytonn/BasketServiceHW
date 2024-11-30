namespace Allup.Application.UI.ViewModels
{
    public class BasketCookieViewModel
    {
        public int ProductId {  get; set; }
        public int Count {  get; set; }
    }

    public class BasketViewModel
    {
        public List<BasketProductViewModel>? Items { get; set; } = [];
        public int Count => Items.Sum(x => x.Count);
        public decimal TotalAmount => Items.Sum(x => x.Price * x.Count);
    }

    public class BasketProductViewModel
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? CoverImageUrl { get; set; }
        public decimal Price { get; set; }
        public string? FormattedPrice {  get; set; }
        public int Count { get; set; }
    }
}
