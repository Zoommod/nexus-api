using System;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;

namespace Nexus.Infrastructure.Repositories;

public class GeneroRepositorio :RepositorioBase<Genero>, IGeneroRepositorio
{
    public GeneroRepositorio(NexusDbContext context) : base(context)
    {
        
    }

    public async Task<Genero?> ObterPorNomeAsync(string nome)
    {
        return await _dbSet
            .FirstOrDefaultAsync(g => g.Nome == nome);
    }

    public async Task<IEnumerable<Genero>> ObterComJogosAsync()
    {
        return await _dbSet
            .Include(g => g.Jogos)
            .ToListAsync();
    }

    public async Task<IEnumerable<Genero>> ObterComFilmesAsync()
    {
        return await _dbSet
            .Include(g => g.Filmes)
            .ToListAsync();
    }

    public async Task<bool> ExistePorNomeAsync(string nome)
    {
        return await _dbSet
            .AnyAsync(g => g.Nome == nome);
    }
}
