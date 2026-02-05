using Nexus.Application.DTOs.Jogo;
using Nexus.Domain.Enums;

namespace Nexus.Application.Interfaces;

public interface IJogoService
{
    Task<IEnumerable<JogoDto>> ObterTodosPorUsuarioAsync(string usuarioId);
    Task<JogoDto?> ObterPorIdAsync(Guid id, string usuarioId);
    Task<IEnumerable<JogoDto>> ObterPorStatusAsync(StatusMidia status, string usuarioId);
    Task<IEnumerable<JogoDto>> BuscarPorTituloAsync(string titulo, string usuarioId);
    Task<JogoDto> CriarAsync(CriarJogoDto dto, string usuarioId);
    Task<JogoDto> AtualizarAsync(Guid id, AtualizarJogoDto dto, string usuarioId);
    Task DeletarAsync(Guid id, string usuarioId);
}
