using System;
using Nexus.Domain.Entities;

namespace Nexus.Domain.Interfaces;

public interface IAvaliacaoRepositorio : IRepositorioBase<Avaliacao>
{
    Task<IEnumerable<Avaliacao>> ObterPorJogosAsync(Guid jogoId);
    Task<IEnumerable<Avaliacao>> ObterPorFilmesAsync(Guid filmeId);
    Task<IEnumerable<Avaliacao>> ObterPorUsuarioAsync(string usuarioId);
    Task<Avaliacao?> ObterComDetalhesAsync(Guid avaliacaoId);
}
