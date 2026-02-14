using System;
using Nexus.Application.DTOs.Filme;
using Nexus.Domain.Common;
using Nexus.Domain.Enums;

namespace Nexus.Application.Interfaces;

public interface IFilmeService
{
    Task<IEnumerable<FilmeDto>> ObterTodosPorUsuarioAsync(string usuarioId);
    Task<FilmeDto?> ObterPorIdAsync(Guid id, string usuarioId);
    Task<IEnumerable<FilmeDto>> ObterPorStatusAsync(StatusMidia status, string usuarioId);
    Task<IEnumerable<FilmeDto>> BuscarPorTituloAsync(string titulo, string usuarioId);
    Task<FilmeDto> CriarAsync(CriarFilmeDto dto, string usuarioId);
    Task<FilmeDto> AtualizarAsync(Guid id, AtualizarFilmeDto dto, string usuarioId);
    Task DeletarAsync(Guid id, string usuarioId);
    Task<ResultadoPaginado<FilmeDto>> ObterTodosPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros);
}
