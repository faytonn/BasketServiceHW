using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.Services.Implementations;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Repositories.Abstraction;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Globalization;
using System.Linq.Expressions;

namespace Allup.Application.Services.Implementations;

public class ProductManager : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ExternalApiService _externalApiService;
    private readonly ICurrencyService _currencyService;
    private readonly ICookieService _cookieService;

    public ProductManager(IProductRepository repository, IMapper mapper, ExternalApiService externalApiService, ICurrencyService currencyService, ICookieService cookieService)
    {
        _repository = repository;
        _mapper = mapper;
        _externalApiService = externalApiService;
        _currencyService = currencyService;
        _cookieService = cookieService;
    }

    public async Task<List<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>>? predicate = null,
                                    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
                                    Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        var language = await _cookieService.GetLanguageAsync();
        var products = await _repository.GetAllAsync(include: x => x
                         .Include(y => y.ProductTranslations!.Where(z => z.LanguageId == language.Id)));

        var productViewModels = _mapper.Map<List<ProductViewModel>>(products);

        var currency = await _cookieService.GetCurrencyAsync();

        var coefficient = await _externalApiService.GetCurrencyCoefficient(currency.CurrencyCode ?? "azn");
        var culture = new CultureInfo(currency.IsoCode?? "az-az");

        foreach (var item in productViewModels)
        {
            item.FormattedPrice = (item.Price / coefficient).ToString("C", culture);
        }

        return productViewModels;
    }

    public async Task<ProductViewModel> GetAsync(int id, int languageId, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        var product = await _repository.GetAsync(x => x.Id == id, include: x => x.Include(y => y.ProductTranslations!.Where(pt => pt.LanguageId == languageId)));
        
        var productViewModel = _mapper.Map<ProductViewModel>(product);

        var currency = await _cookieService.GetCurrencyAsync();

        var coefficient = await _externalApiService.GetCurrencyCoefficient(currency.CurrencyCode ?? "azn");
        var culture = new CultureInfo(currency.IsoCode?? "az-az");

        productViewModel.FormattedPrice = (productViewModel.Price / coefficient).ToString("C", culture);

        return productViewModel;
    }

    public async Task<ProductViewModel> GetByIdAsync(int id)
    {
        var product = await _repository.GetAsync(id);
        var productViewModel = _mapper.Map<ProductViewModel>(product);

        return productViewModel;
    }
}
