using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Repositories.Abstraction;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Allup.Application.Services.Implementations
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetAllAsync(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
        {
            var categories = await _repository.GetAllAsync(predicate, orderBy, include);

            var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);

            return categoryViewModels;
        }

        public Task<CategoryViewModel> GetAsync(int id, int languageId, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
        {
            throw new NotImplementedException();
        }
    }
}
