using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;

namespace Nexus.Infrastructure.Repositories;

public class GeneroRepositorio : RepositorioBase<Genero>, IGeneroRepositorio
{
    public GeneroRepositorio(NexusDbContext context) : base(context)
    {
    }

    public async Task<Genero?> ObterPorNomeAsync(string nome)
    {
        return await _context.Generos
            .FirstOrDefaultAsync(g => g.Nome.ToLower() == nome.ToLower());
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

    public async Task<IEnumerable<Genero>> ObterPorIdsAsync(IEnumerable<Guid> ids)
    {
        return await _context.Generos
            .Where(g => ids.Contains(g.Id))
            .ToListAsync();
    }
}
