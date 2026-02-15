using System;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;

namespace Nexus.Domain.Interfaces;

public interface IAvaliacaoRepositorio : IRepositorioBase<Avaliacao>
{
    Task<IEnumerable<Avaliacao>> ObterPorJogoAsync(Guid jogoId);
    Task<IEnumerable<Avaliacao>> ObterPorFilmeAsync(Guid filmeId);
    Task<IEnumerable<Avaliacao>> ObterPorUsuarioAsync(string usuarioId);
    Task<Avaliacao?> ObterComDetalhesAsync(Guid avaliacaoId);
    Task<ResultadoPaginado<Avaliacao>> ObterPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros);
}
