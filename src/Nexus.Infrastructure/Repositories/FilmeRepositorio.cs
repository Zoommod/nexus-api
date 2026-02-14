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

    public async Task<ResultadoPaginado<Filme>> ObterTodosPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros)
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
            _ => query.OrderBy(f => f.Titulo) // Padrão
        };

        return await query.ToPaginatedListAsync(parametros);
    }
}
