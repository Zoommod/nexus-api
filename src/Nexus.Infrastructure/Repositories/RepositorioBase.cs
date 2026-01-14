using System;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;

namespace Nexus.Infrastructure.Repositories;

public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
{
    protected readonly NexusDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositorioBase(NexusDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> AdicionarAsync(T entidade)
    {
        await _dbSet.AddAsync(entidade);
        await _context.SaveChangesAsync();
        return entidade;
    }

    public async Task AtualizarAsync(T entidade)
    {
        entidade.DataAtualizacao = DateTime.UtcNow;
        _dbSet.Update(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(Guid id)
    {
        var entidade = await ObterPorIdAsync(id);
        if(entidade != null)
        {
            _dbSet.Remove(entidade);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(Guid id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }


}
