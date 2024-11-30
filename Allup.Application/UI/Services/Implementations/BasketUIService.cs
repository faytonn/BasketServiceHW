using Allup.Application.Services.Abstracts;
using Allup.Application.Services.Implementations;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.ViewModels;
using Allup.Application.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Allup.Application.UI.Services.Implementations
{
    public class BasketUIService : IBasketUIService
    {
        private readonly ICookieService _cookieService;
        private readonly IBasketItemService _basketItemService;
        private readonly IProductService _productService;

        public BasketUIService(ICookieService cookieService, IBasketItemService basketItemService, IProductService productService)
        {
            _cookieService = cookieService;
            _basketItemService = basketItemService;
            _productService = productService;
        }

        public async Task<BasketViewModel> AddBasketItemUIAsync(int productId)
        {
            var existProduct = await _productService.GetAsync(productId);

            if (existProduct == null) throw new Exception("not found");

            string clientId;
            if (_cookieService.IsAuthorized())
            {
                clientId = _cookieService.GetUserId();
            }
            else
            {
                clientId = _cookieService.GetBrowserId();
            }


            var createViewModel = new CreateBasketItemViewModel
            {
                ProductId = productId,
                ClientId = clientId
            };
            await _basketItemService.CreateAsync(createViewModel);

            var language = _cookieService.GetLanguageAsync();

            var items = await _basketItemService.GetAllAsync(x => x.ClientId == clientId);

            var basketItemVMs = new List<BasketItemViewModel>();

            foreach (var item in items)
            {
                var existBasketItem = await _productService.GetAsync(x => x.Id == item.ProductId,
                     x => x.Include(y => y.ProductTranslations!.Where(z => z.LanguageId == language.Id)));

                if (existBasketItem == null) continue;

                basketItemVMs.Add(new BasketItemViewModel
                {
                    ProductId = existBasketItem.Id,
                    Count = existBasketItem.Count
                });
            }

            BasketViewModel basket = new BasketViewModel();

            return basket;
        }

        public async Task<BasketViewModel> GetBasketViewModelUIAsync()
        {
            var language = await _cookieService.GetLanguageAsync();

            string clientId;
            if (_cookieService.IsAuthorized())
            {
                clientId = _cookieService.GetUserId();
            }
            else
            {
                clientId = _cookieService.GetBrowserId();
            }

            var items = await _basketItemService.GetAllAsync(x => x.ClientId == clientId);
            var basketItemVMs = new List<BasketItemViewModel>();

            foreach (var item in items)
            {
                var basketItemExists = await _productService.GetAsync(x => x.Id == item.ProductId, x => x.Include(y => y.ProductTranslations!.Where(k => k.LanguageId == language.Id)));

                if (basketItemExists == null) continue;

                basketItemVMs.Add(new BasketItemViewModel()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Product = item.Product,
                    Count = item.Count
                });
            }



            BasketViewModel basket = new()
            {
                //Items = basketItemVMs
            };
            return basket;

        }

        public async Task<BasketViewModel> RemoveFromBasketUIAsync(int id)
        {
            string clientId;
            if (_cookieService.IsAuthorized())
            {
                clientId = _cookieService.GetUserId();
            }
            else
            {
                clientId = _cookieService.GetBrowserId();
            }

            await _basketItemService.Remove(id);

            return await GetBasketViewModelUIAsync();
        }
    }
}
