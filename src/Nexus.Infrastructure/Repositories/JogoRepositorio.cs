using System;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;
using Nexus.Infrastructure.Extensions;

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

    public async Task<ResultadoPaginado<Jogo>> ObterTodosPorUsuarioPaginadoAsync(
        string usuarioId,
        PaginacaoParametros parametros)
    {
        var query = _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.UsuarioId == usuarioId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametros.Busca))
        {
            var buscaLower = parametros.Busca.ToLower();
            query = query.Where(j =>
                j.Titulo.ToLower().Contains(buscaLower) ||
                (j.Desenvolvedora != null && j.Desenvolvedora.ToLower().Contains(buscaLower)));
        }

        query = parametros.OrdenarPor switch
        {
            "Titulo" => query.OrderByProperty(nameof(Jogo.Titulo), parametros.EhOrdenacaoDescendente()),
            "DataLancamento" => query.OrderByProperty(nameof(Jogo.DataLancamento), parametros.EhOrdenacaoDescendente()),
            "NotaUsuario" => query.OrderByProperty(nameof(Jogo.NotaUsuario), parametros.EhOrdenacaoDescendente()),
            "DataCriacao" => query.OrderByProperty(nameof(Jogo.DataCriacao), parametros.EhOrdenacaoDescendente()),
            _ => query.OrderBy(j => j.Titulo)
        };

        return await query.ToPaginatedListAsync(parametros);
    }

    public async Task<ResultadoPaginado<Jogo>> ObterComFiltrosAsync(string usuarioId, FiltroJogoParametros filtros)
    {
        var query = _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.UsuarioId == usuarioId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtros.Titulo))
        {
            var tituloLower = filtros.Titulo.ToLower();
            query = query.Where(j => j.Titulo.ToLower().Contains(tituloLower));
        }

        if (!string.IsNullOrWhiteSpace(filtros.Desenvolvedora))
        {
            var devLower = filtros.Desenvolvedora.ToLower();
            query = query.Where(j => j.Desenvolvedora != null && j.Desenvolvedora.ToLower().Contains(devLower));
        }

        if (filtros.Status.HasValue)
        {
            query = query.Where(j => j.Status == filtros.Status.Value);
        }

        if (filtros.GeneroId.HasValue)
        {
            query = query.Where(j => j.Generos.Any(g => g.Id == filtros.GeneroId.Value));
        }

        if (filtros.NotaMinima.HasValue)
        {
            query = query.Where(j => j.NotaUsuario >= filtros.NotaMinima.Value);
        }

        if (filtros.NotaMaxima.HasValue)
        {
            query = query.Where(j => j.NotaUsuario <= filtros.NotaMaxima.Value);
        }

        if (filtros.AnoLancamentoMinimo.HasValue)
        {
            query = query.Where(j => j.DataLancamento != null && j.DataLancamento.Value.Year >= filtros.AnoLancamentoMinimo.Value);
        }

        if (filtros.AnoLancamentoMaximo.HasValue)
        {
            query = query.Where(j => j.DataLancamento != null && j.DataLancamento.Value.Year <= filtros.AnoLancamentoMaximo.Value);
        }

        if (!string.IsNullOrWhiteSpace(filtros.Busca))
        {
            var buscaLower = filtros.Busca.ToLower();
            query = query.Where(j =>
                j.Titulo.ToLower().Contains(buscaLower) ||
                (j.Desenvolvedora != null && j.Desenvolvedora.ToLower().Contains(buscaLower)));
        }

        query = filtros.OrdenarPor switch
        {
            "Titulo" => query.OrderByProperty(nameof(Jogo.Titulo), filtros.EhOrdenacaoDescendente()),
            "DataLancamento" => query.OrderByProperty(nameof(Jogo.DataLancamento), filtros.EhOrdenacaoDescendente()),
            "NotaUsuario" => query.OrderByProperty(nameof(Jogo.NotaUsuario), filtros.EhOrdenacaoDescendente()),
            "DataCriacao" => query.OrderByProperty(nameof(Jogo.DataCriacao), filtros.EhOrdenacaoDescendente()),
            _ => query.OrderBy(j => j.Titulo)
        };

        return await query.ToPaginatedListAsync(filtros);
    }

    public async Task<IEnumerable<Jogo>> ObterPorStatusAsync(StatusMidia status, string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.UsuarioId == usuarioId && j.Status == status)
            .OrderBy(j => j.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> ObterPorGeneroAsync(Guid generoId, string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.UsuarioId == usuarioId && j.Generos.Any(g => g.Id == generoId))
            .OrderBy(j => j.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Jogo>> BuscarPorTituloAsync(string titulo, string usuarioId)
    {
        return await _context.Jogos
            .Include(j => j.Generos)
            .Where(j => j.UsuarioId == usuarioId && j.Titulo.ToLower().Contains(titulo.ToLower()))
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
