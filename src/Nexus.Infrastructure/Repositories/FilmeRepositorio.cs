using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;

namespace Nexus.Infrastructure.Repositories;

public class FilmeRepositorio : RepositorioBase<Filme>, IFilmeRepositorio
{
    public FilmeRepositorio(NexusDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Filme>> ObterTodosPorUsuarioAsync(string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.UsuarioId == usuarioId)
            .OrderBy(f => f.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> ObterPorStatusAsync(StatusMidia status, string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.Status == status && f.UsuarioId == usuarioId)
            .OrderBy(f => f.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> ObterPorGeneroAsync(Guid generoId, string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.Generos.Any(g => g.Id == generoId) && f.UsuarioId == usuarioId)
            .OrderBy(f => f.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> BuscarPorTituloAsync(string titulo, string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.Titulo.Contains(titulo) && f.UsuarioId == usuarioId)
            .OrderBy(f => f.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> ObterComGenerosAsync(string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.UsuarioId == usuarioId)
            .OrderBy(f => f.Titulo)
            .ToListAsync();
    }
}
