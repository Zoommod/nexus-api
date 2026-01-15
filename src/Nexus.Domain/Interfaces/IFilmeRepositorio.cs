using System;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Interfaces;

public interface IFilmeRepositorio : IRepositorioBase<Filme>
{
    Task<IEnumerable<Filme>> ObterPorStatusAsync(StatusMidia status);
    Task<IEnumerable<Filme>> ObterPorGeneroAsync(Guid generoId);
    Task<IEnumerable<Filme>> BuscarPorTituloAsync(string titulo);
    Task<IEnumerable<Filme>> ObterComGenerosAsync();
    Task<IEnumerable<Filme>> ObterPorDiretorAsync(string diretor);
}
