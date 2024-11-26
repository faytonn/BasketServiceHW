using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Repositories.Abstraction;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Allup.Application.Services.Implementations;

public class LanguageManager : ILanguageService
{
    private readonly ILanguageRepository _languageRepository;
    private readonly IMapper _mapper;

    public LanguageManager(ILanguageRepository languageRepository, IMapper mapper)
    {
        _languageRepository = languageRepository;
        _mapper = mapper;
    }

    public async Task<List<LanguageViewModel>> GetAllAsync(Expression<Func<Language, bool>>? predicate = null,
    Func<IQueryable<Language>, IOrderedQueryable<Language>>? orderBy = null,
                                    Func<IQueryable<Language>, IIncludableQueryable<Language, object>>? include = null)
    {
        var languages = await _languageRepository.GetAllAsync();
        var languagesViewModels = _mapper.Map<List<LanguageViewModel>>(languages);

        return languagesViewModels;
    }

    public Task<LanguageViewModel> GetAsync(int id, int languageId, Func<IQueryable<Language>, IIncludableQueryable<Language, object>>? include = null)
    {
        throw new NotImplementedException();
    }

    public Task<LanguageViewModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<LanguageViewModel> GetLanguageAsync(string isoCode)
    {
        var languages = await _languageRepository.GetAllAsync();

        var language = languages.FirstOrDefault(x => x.IsoCode.ToLower() == isoCode.ToLower());

        return _mapper.Map<LanguageViewModel>(language);
    }
}
