using Allup.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Allup.Application.Services.Abstracts
{
    public interface ICrudService<TViewModel, TEntity> 
    {
        Task<TViewModel> GetAsync(int id, int languageId, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
        Task<List<TViewModel>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    }
}
