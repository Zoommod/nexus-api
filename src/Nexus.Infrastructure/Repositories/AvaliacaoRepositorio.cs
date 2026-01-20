using System;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;

namespace Nexus.Infrastructure.Repositories;

public class AvaliacaoRepositorio : RepositorioBase<Avaliacao>, IAvaliacaoRepositorio
{
    public AvaliacaoRepositorio(NexusDbContext context) : base(context)
    {

    }

    public async Task<IEnumerable<Avaliacao>> ObterPorJogoAsync(Guid jogoId)
    {
        return await _dbSet
            .Where(a => a.JogoId == jogoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Avaliacao>> ObterPorFilmeAsync(Guid filmeId)
    {
        return await _dbSet
            .Where(a => a.FilmeId == filmeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Avaliacao>> ObterPorUsuarioAsync(string usuarioId)
    {
        return await _dbSet
            .Where(a => a.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<Avaliacao?> ObterComDetalhesAsync(Guid avaliacaoId)
    {
        return await _dbSet
            .Include(a => a.Jogo)
            .Include(a => a.Filme)
            .FirstOrDefaultAsync(a => a.Id == avaliacaoId);
    }
}
