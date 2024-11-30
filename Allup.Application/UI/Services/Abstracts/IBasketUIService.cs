using Allup.Application.UI.ViewModels;

namespace Allup.Application.UI.Services.Abstracts
{
    public interface IBasketUIService
    {
        Task<BasketViewModel> GetBasketViewModelUIAsync();
        Task<BasketViewModel> AddBasketItemUIAsync(int productId);
        Task<BasketViewModel> RemoveFromBasketUIAsync(int id);
    }
}
