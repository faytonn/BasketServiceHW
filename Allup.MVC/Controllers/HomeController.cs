using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Allup.MVC.Controllers
{
    public class HomeController : LocalizerController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService, ILanguageService languageService) : base(languageService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
           var languageId = await GetLanguageAsync();

            var homeViewModel = await _homeService.GetHomeViewModelAsync(languageId);

            return View(homeViewModel);
        }
    }
}
