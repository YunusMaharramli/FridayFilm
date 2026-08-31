using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Common;
using FridayFilm.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FridayFilm.Infrastructure.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly FridayFilmDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    public WriteRepository(FridayFilmDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }




}
