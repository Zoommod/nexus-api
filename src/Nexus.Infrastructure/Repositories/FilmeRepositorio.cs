using System;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Infrastructure.Repositories;

public class FilmeRepositorio : RepositorioBase<Filme>, IFilmeRepositorio
{
    public FilmeRepositorio(NexusDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<Filme>> ObterPorStatusAsync(StatusMidia status)
    {
        return await _dbSet
            .Where(f => f.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> ObterPorGeneroAsync(Guid generoId)
    {
        return await _dbSet
            .Where(f => f.Generos.Any(g => g.Id == generoId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> BuscarPorTituloAsync(string titulo)
    {
        return await _dbSet
            .Where(f => f.Titulo.Contains(titulo))
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> ObterComGenerosAsync()
    {
        return await _dbSet
            .Include(f => f.Generos)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> ObterPorDiretorAsync(string diretor)
    {
        return await _dbSet
            .Where(f => f.Diretor != null && f.Diretor.Contains(diretor))
            .ToListAsync();
    }

}
