using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.ViewModels;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Allup.Application.UI.Services.Implementations;

public class CookieManager : ICookieService
{
    private readonly ILanguageService _languageService;
    private readonly ICurrencyService _currencyService;
    private readonly IHttpContextAccessor _contextAccessor;

    public CookieManager(ILanguageService languageService, ICurrencyService currencyService, IHttpContextAccessor contextAccessor)
    {
        _languageService = languageService;
        _currencyService = currencyService;
        _contextAccessor = contextAccessor;
    }

    public async Task<CurrencyViewModel> GetCurrencyAsync()
    {
        var currencies = await _currencyService.GetAllAsync();
        var currencyIsoCode = _contextAccessor.HttpContext.Request.Cookies["currency"] ?? "az-az";
        var selectedCurrency = await _currencyService.GetCurrencyAsync(currencyIsoCode);

        return selectedCurrency;
    }

    public async Task<LanguageViewModel> GetLanguageAsync()
    {
        var languages = await _languageService.GetAllAsync();
        var culture = _contextAccessor.HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
        var isoCode = culture?.Substring(culture.LastIndexOf("=") + 1) ?? "en-Us";
        var selectedLanguage = await _languageService.GetLanguageAsync(isoCode);

        return selectedLanguage;
    }
}
