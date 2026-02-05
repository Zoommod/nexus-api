using System;
using Nexus.Domain.Entities;

namespace Nexus.Domain.Interfaces;

public interface IGeneroRepositorio : IRepositorioBase<Genero>
{
    Task<Genero?> ObterPorNomeAsync(string nome);
    Task<IEnumerable<Genero>> ObterComJogosAsync();
    Task<IEnumerable<Genero>> ObterComFilmesAsync();
    Task<bool> ExistePorNomeAsync(string nome);
    Task<IEnumerable<Genero>> ObterPorIdsAsync(IEnumerable<Guid> ids);
}
