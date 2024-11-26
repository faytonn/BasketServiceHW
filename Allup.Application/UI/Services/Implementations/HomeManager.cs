using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Allup.Application.UI.Services.Implementations;

public class HomeManager : IHomeService
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public HomeManager(ICategoryService categoryService, IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    public async Task<HomeViewModel> GetHomeViewModelAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        var products = await _productService.GetAllAsync();

        var homeViewModel = new HomeViewModel
        {
            Categories = categories,
            Products = products
        };

        return homeViewModel;
    }
}
