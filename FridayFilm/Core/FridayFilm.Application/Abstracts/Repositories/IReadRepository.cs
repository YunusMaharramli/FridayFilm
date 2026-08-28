using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FridayFilm.Application.Abstracts.Repositories;

public interface IReadRepository<TEntity>
{ 
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default
        );
    IQueryable<TEntity> Query();
}
