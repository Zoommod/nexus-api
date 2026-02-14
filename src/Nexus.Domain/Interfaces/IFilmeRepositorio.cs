using System;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Interfaces;

public interface IFilmeRepositorio : IRepositorioBase<Filme>
{
    Task<IEnumerable<Filme>> ObterTodosPorUsuarioAsync(string usuarioId);
    Task<IEnumerable<Filme>> ObterPorStatusAsync(StatusMidia status, string usuarioId);
    Task<IEnumerable<Filme>> ObterPorGeneroAsync(Guid generoId, string usuarioId);
    Task<IEnumerable<Filme>> BuscarPorTituloAsync(string titulo, string usuarioId);
    Task<IEnumerable<Filme>> ObterComGenerosAsync(string usuarioId);
    Task<ResultadoPaginado<Filme>> ObterTodosPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros);
}
