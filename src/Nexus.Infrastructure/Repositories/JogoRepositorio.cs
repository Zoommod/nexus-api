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

    public async Task<IEnumerable<Jogo>> ObterTodosPorUsuarioAsync(string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.UsuarioId == usuarioId)
            .OrderBy(j => j.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> ObterPorStatusAsync(StatusMidia status, string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.Status == status && j.UsuarioId == usuarioId)
            .OrderBy(j => j.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> ObterPorGeneroAsync(Guid generoId, string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.Generos.Any(g => g.Id == generoId) && j.UsuarioId == usuarioId)
            .OrderBy(j => j.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> BuscarPorTituloAsync(string titulo, string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.Titulo.Contains(titulo) && j.UsuarioId == usuarioId)
            .OrderBy(j => j.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> ObterComGenerosAsync(string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.UsuarioId == usuarioId)
            .OrderBy(j => j.Titulo)
            .ToListAsync();
    }
}
