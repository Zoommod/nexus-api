using System;
using Nexus.Domain.Entities;

namespace Nexus.Domain.Interfaces;

public interface IRepositorioBase<T> where T : EntidadeBase
{
    Task<T?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<T>> ObterTodosAsync();

    Task<T> AdicionarAsync(T entidade);
    Task AtualizarAsync(T entidade);
    Task DeletarAsync(Guid id);

    Task<bool> ExisteAsync(Guid id);
}
