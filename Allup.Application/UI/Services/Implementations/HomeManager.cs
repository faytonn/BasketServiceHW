using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Allup.Application.UI.Services.Implementations;

public class HomeManager : IHomeService
{
    private readonly ICategoryService _categoryService;

    public HomeManager(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<HomeViewModel> GetHomeViewModelAsync(int languageId)
    {
        var categories = await _categoryService.GetAllAsync(include: x => x.Include(y => y.CategoryTranslations!.Where(ct => ct.LanguageId == languageId)));

        var homeViewModel = new HomeViewModel
        {
            Categories = categories
        };

        return homeViewModel;
    }
}
