using System;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Interfaces;

public interface IJogoRepositorio : IRepositorioBase<Jogo>
{
    Task<IEnumerable<Jogo>> ObterPorStatusAsync(StatusMidia status);
    Task<IEnumerable<Jogo>> ObterPorGeneroAsync(Guid generoId);
    Task<IEnumerable<Jogo>> BuscarPorTituloAsync(string titulo);
    Task<IEnumerable<Jogo>> ObterComGenerosAsync();
}
