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
        Task<TViewModel> GetAsync(int id);
        Task<List<TViewModel>> GetAllAsync(Expression<Func<Category, bool>>? predicate = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
                                    Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);
    }
}
