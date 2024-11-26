using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Allup.MVC.ViewComponenets
{
    public class TopHeaderViewComponent : ViewComponent
    {
        private readonly ILanguageService _languageService;
        private readonly ICurrencyService _currencyService;
        private readonly ICompareService _compareService;
        private readonly ICookieService _cookieService;

        public TopHeaderViewComponent(ILanguageService languageService, ICompareService compareService, ICurrencyService currencyService, ICookieService cookieService)
        {
            _languageService = languageService;
            _compareService = compareService;
            _currencyService = currencyService;
            _cookieService = cookieService;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var languages = await _languageService.GetAllAsync();
            var currencies = await _currencyService.GetAllAsync();
            var compareItemCount = _compareService.GetCount();


            var topHeaderViewModel = new TopHeaderViewModel
            {
                Languages = languages,
                SelectedLanguage = await _cookieService.GetLanguageAsync(),
                CompareItemCount = compareItemCount,
                Currencies = currencies,
                SelectedCurrency = await _cookieService.GetCurrencyAsync()
            };

            return View(topHeaderViewModel);
        }
    }
}
