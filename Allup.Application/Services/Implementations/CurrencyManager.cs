using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Repositories.Abstraction;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Allup.Application.Services.Implementations;

public class CurrencyManager : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public CurrencyManager(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task<List<CurrencyViewModel>> GetAllAsync(Expression<Func<Currency, bool>>? predicate = null,
    Func<IQueryable<Currency>, IOrderedQueryable<Currency>>? orderBy = null,
                                    Func<IQueryable<Currency>, IIncludableQueryable<Currency, object>>? include = null)
    {
        var currencies = await _currencyRepository.GetAllAsync();
        var currencyViewModels = _mapper.Map<List<CurrencyViewModel>>(currencies);

        return currencyViewModels;
    }

    public Task<CurrencyViewModel> GetAsync(int id, int CurrencyId, Func<IQueryable<Currency>, IIncludableQueryable<Currency, object>>? include = null)
    {
        throw new NotImplementedException();
    }

    public Task<CurrencyViewModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CurrencyViewModel> GetCurrencyAsync(string isoCode)
    {
        var currencies = await _currencyRepository.GetAllAsync();

        var currency = currencies.FirstOrDefault(x => x.IsoCode.ToLower() == isoCode.ToLower());

        return _mapper.Map<CurrencyViewModel>(currency);
    }
}
