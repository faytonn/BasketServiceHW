using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Repositories.Abstraction;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Allup.Application.Services.Implementations;

public class ProductManager : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductManager(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>>? predicate = null, 
        Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
        Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        var products = await _repository.GetAllAsync(predicate, orderBy, include);

        var productViewModels = _mapper.Map<List<ProductViewModel>>(products);
        return productViewModels;
    }

    public async Task<ProductViewModel> GetAsync(int id, int languageId, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        var product = await _repository.GetAsync(x => x.Id == id, include: x => x.Include(y => y.ProductTranslations!.Where(pt => pt.LanguageId == languageId)));
        var productViewModel = _mapper.Map<ProductViewModel>(product);

        return productViewModel;
    }

    public async Task<ProductViewModel> GetByIdAsync(int id)
    {
        var product = await _repository.GetAsync(id);
        var productViewModel = _mapper.Map<ProductViewModel>(product);

        return productViewModel;
    }
}
