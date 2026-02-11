using System;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Interfaces;

public interface IJogoRepositorio : IRepositorioBase<Jogo>
{
    Task<IEnumerable<Jogo>> ObterTodosPorUsuarioAsync(string usuarioId);
    Task<IEnumerable<Jogo>> ObterPorStatusAsync(StatusMidia status, string usuarioId);
    Task<IEnumerable<Jogo>> ObterPorGeneroAsync(Guid generoId, string usuarioId);
    Task<IEnumerable<Jogo>> BuscarPorTituloAsync(string titulo, string usuarioId);
    Task<IEnumerable<Jogo>> ObterComGenerosAsync(string usuarioId);
    Task<ResultadoPaginado<Jogo>> ObterTodosPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros);
}
