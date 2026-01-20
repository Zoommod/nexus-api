using System;
using Nexus.Application.DTOs.Avaliacao;

namespace Nexus.Application.Interfaces;

public interface IAvaliacaoService
{
    Task<AvaliacaoDto> CriarAsync(CriarAvaliacaoDto dto, string usuarioId);
    Task<AvaliacaoDto> AtualizarAsync(Guid id, AtualizarAvaliacaoDto dto, string usuarioId);
    Task DeletarAsync(Guid id, string usuarioId);
    Task<AvaliacaoDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<AvaliacaoDto>> ObterPorJogoAsync(Guid jogoId);
    Task<IEnumerable<AvaliacaoDto>> ObterPorFilmeAsync(Guid filmeId);
    Task<IEnumerable<AvaliacaoDto>> ObterPorUsuarioAsync(string usuarioId);
}
