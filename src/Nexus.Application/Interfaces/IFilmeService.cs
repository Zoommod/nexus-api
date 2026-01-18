using System;
using Nexus.Application.DTOs.Filme;

namespace Nexus.Application.Interfaces;

public interface IFilmeService
{
    Task<FilmeDto> CriarAsync(CriarFilmeDto dto, string usuarioId);
    Task<FilmeDto> AtualizarAsync(Guid id, AtualizarFilmeDto dto, string usuarioId);
    Task DeletarAsync(Guid id, string usuarioId);
    Task<FilmeDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<FilmeDto>> ObterTodosAsync();
    Task<IEnumerable<FilmeDto>> ObterPorStatusAsync(int status);
    Task<IEnumerable<FilmeDto>> BuscarPorTituloAsync(string titulo);
    Task<IEnumerable<FilmeDto>> ObterPorDiretorAsync(string diretor);
}
