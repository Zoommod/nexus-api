using System;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;
using Nexus.Infrastructure.Extensions;

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
    
    public async Task<ResultadoPaginado<Avaliacao>> ObterPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros)
    {
        var query = _context.Avaliacoes
            .Include(a => a.Jogo)
            .Include(a => a.Filme)
            .Where(a => a.UsuarioId == usuarioId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametros.Busca))
        {
            var buscaLower = parametros.Busca.ToLower();
            query = query.Where(a => 
                a.Conteudo.ToLower().Contains(buscaLower) ||
                (a.Jogo != null && a.Jogo.Titulo.ToLower().Contains(buscaLower)) ||
                (a.Filme != null && a.Filme.Titulo.ToLower().Contains(buscaLower)));
        }

        query = parametros.OrdenarPor switch
        {
            "Nota" => query.OrderByProperty(nameof(Avaliacao.Nota), parametros.EhOrdenacaoDescendente()),
            "DataCriacao" => query.OrderByProperty(nameof(Avaliacao.DataCriacao), parametros.EhOrdenacaoDescendente()),
            _ => query.OrderByDescending(a => a.DataCriacao)
        };

        return await query.ToPaginatedListAsync(parametros);
    }
}
