using System;
using Nexus.Application.DTOs.Genero;

namespace Nexus.Application.Interfaces;

public interface IGeneroService
{
    Task<GeneroDto> CriarAsync(CriarGeneroDto dto);
    Task<GeneroDto> AtualizarAsync(Guid id, AtualizarGeneroDto dto);
    Task DeletarAsync(Guid id);
    Task<GeneroDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<GeneroDto>> ObterTodosAsync();
    Task<GeneroDto?> ObterPorNomeAsync(string nome);
}
