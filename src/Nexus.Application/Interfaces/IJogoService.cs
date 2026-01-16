using System;
using Nexus.Application.DTOs.Jogo;

namespace Nexus.Application.Interfaces;

public interface IJogoService
{
    Task<JogoDto> CriarAsync(CriarJogoDto dto, string usuarioId);
    Task<JogoDto> AtualizarAsync(Guid id, AtualizarJogoDto dto, string usuarioId);
    Task DeletarAsync(Guid id, string usuarioId);
    Task<JogoDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<JogoDto>> ObterTodosAsync();
    Task<IEnumerable<JogoDto>> ObterPorStatusAsync(int status);
    Task<IEnumerable<JogoDto>> BuscarPorTituloAsync(string titulo);
}
