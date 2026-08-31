using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Common;
using FridayFilm.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FridayFilm.Infrastructure.Repositories;

public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly FridayFilmDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    public ReadRepository(FridayFilmDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();
        if (predicate is not null)
            query = query.Where(predicate);
        return await query.FirstOrDefaultAsync();

    }
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet.AsQueryable();
    }
    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = _dbSet.AsNoTracking(); 

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync();
    }
}

