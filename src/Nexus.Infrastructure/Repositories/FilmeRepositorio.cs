using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;
using Nexus.Infrastructure.Extensions;

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

    public async Task<ResultadoPaginado<Filme>> ObterTodosPorUsuarioPaginadoAsync(
        string usuarioId,
        PaginacaoParametros parametros)
    {
        var query = _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.UsuarioId == usuarioId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametros.Busca))
        {
            var buscaLower = parametros.Busca.ToLower();
            query = query.Where(f =>
                f.Titulo.ToLower().Contains(buscaLower) ||
                (f.Diretor != null && f.Diretor.ToLower().Contains(buscaLower)));
        }

        query = parametros.OrdenarPor switch
        {
            "Titulo" => query.OrderByProperty(nameof(Filme.Titulo), parametros.EhOrdenacaoDescendente()),
            "DataLancamento" => query.OrderByProperty(nameof(Filme.DataLancamento), parametros.EhOrdenacaoDescendente()),
            "NotaUsuario" => query.OrderByProperty(nameof(Filme.NotaUsuario), parametros.EhOrdenacaoDescendente()),
            "DataCriacao" => query.OrderByProperty(nameof(Filme.DataCriacao), parametros.EhOrdenacaoDescendente()),
            _ => query.OrderBy(f => f.Titulo)
        };

        return await query.ToPaginatedListAsync(parametros);
    }

    public async Task<ResultadoPaginado<Filme>> ObterComFiltrosAsync(string usuarioId, FiltroFilmeParametros filtros)
    {
        var query = _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.UsuarioId == usuarioId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtros.Titulo))
        {
            var tituloLower = filtros.Titulo.ToLower();
            query = query.Where(f => f.Titulo.ToLower().Contains(tituloLower));
        }

        if (!string.IsNullOrWhiteSpace(filtros.Diretor))
        {
            var diretorLower = filtros.Diretor.ToLower();
            query = query.Where(f => f.Diretor != null && f.Diretor.ToLower().Contains(diretorLower));
        }

        if (filtros.Status.HasValue)
        {
            query = query.Where(f => f.Status == filtros.Status.Value);
        }

        if (filtros.GeneroId.HasValue)
        {
            query = query.Where(f => f.Generos.Any(g => g.Id == filtros.GeneroId.Value));
        }

        if (filtros.NotaMinima.HasValue)
        {
            query = query.Where(f => f.NotaUsuario >= filtros.NotaMinima.Value);
        }

        if (filtros.NotaMaxima.HasValue)
        {
            query = query.Where(f => f.NotaUsuario <= filtros.NotaMaxima.Value);
        }

        if (filtros.AnoLancamentoMinimo.HasValue)
        {
            query = query.Where(f => f.DataLancamento != null && f.DataLancamento.Value.Year >= filtros.AnoLancamentoMinimo.Value);
        }

        if (filtros.AnoLancamentoMaximo.HasValue)
        {
            query = query.Where(f => f.DataLancamento != null && f.DataLancamento.Value.Year <= filtros.AnoLancamentoMaximo.Value);
        }

        if (filtros.DuracaoMinima.HasValue)
        {
            query = query.Where(f => f.DuracaoMinutos >= filtros.DuracaoMinima.Value);
        }

        if (filtros.DuracaoMaxima.HasValue)
        {
            query = query.Where(f => f.DuracaoMinutos <= filtros.DuracaoMaxima.Value);
        }

        if (!string.IsNullOrWhiteSpace(filtros.Busca))
        {
            var buscaLower = filtros.Busca.ToLower();
            query = query.Where(f =>
                f.Titulo.ToLower().Contains(buscaLower) ||
                (f.Diretor != null && f.Diretor.ToLower().Contains(buscaLower)));
        }

        query = filtros.OrdenarPor switch
        {
            "Titulo" => query.OrderByProperty(nameof(Filme.Titulo), filtros.EhOrdenacaoDescendente()),
            "DataLancamento" => query.OrderByProperty(nameof(Filme.DataLancamento), filtros.EhOrdenacaoDescendente()),
            "NotaUsuario" => query.OrderByProperty(nameof(Filme.NotaUsuario), filtros.EhOrdenacaoDescendente()),
            "DataCriacao" => query.OrderByProperty(nameof(Filme.DataCriacao), filtros.EhOrdenacaoDescendente()),
            _ => query.OrderBy(f => f.Titulo)
        };

        return await query.ToPaginatedListAsync(filtros);
    }

    public async Task<IEnumerable<Filme>> ObterPorStatusAsync(StatusMidia status, string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.UsuarioId == usuarioId && f.Status == status)
            .OrderBy(f => f.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> ObterPorGeneroAsync(Guid generoId, string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.UsuarioId == usuarioId && f.Generos.Any(g => g.Id == generoId))
            .OrderBy(f => f.Titulo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Filme>> BuscarPorTituloAsync(string titulo, string usuarioId)
    {
        return await _context.Filmes
            .Include(f => f.Generos)
            .Where(f => f.UsuarioId == usuarioId && f.Titulo.ToLower().Contains(titulo.ToLower()))
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
