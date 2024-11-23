using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allup.Application.UI.ViewModels
{
    public class BasketCookieViewModel
    {
        public int ProductId {  get; set; }
        public int Count {  get; set; }
    }

    public class BasketViewModel
    {
        public List<BasketItemViewModel>? Items { get; set; } = [];
        public int Count => Items.Sum(x => x.Count);
        public decimal TotalAmount => Items.Sum(x => x.Price * x.Count);
    }

    public class BasketItemViewModel
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? CoverImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
