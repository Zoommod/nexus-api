using System;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;

namespace Nexus.Infrastructure.Repositories;

public class JogoRepositorio : RepositorioBase<Jogo>, IJogoRepositorio
{
    public JogoRepositorio(NexusDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<Jogo>> ObterPorStatusAsync(StatusMidia status)
    {
        return await _dbSet
            .Where(j => j.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> ObterPorGeneroAsync(Guid generoId)
    {
        return await _dbSet
            .Where(j => j.Generos.Any(g => g.Id == generoId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> BuscarPorTituloAsync(string titulo)
    {
        return await _dbSet
            .Where(j => j.Titulo.Contains(titulo))
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> ObterComGenerosAsync()
    {
        return await _dbSet
            .Include(j => j.Generos)
            .ToListAsync();
    }
}
