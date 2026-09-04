using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FridayFilm.Application.Abstracts.Repositories;

public interface IWriteRepository<TEntity>
{
    Task AddAsync(TEntity entity,
        CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    Task SaveChangeAsync();
}
