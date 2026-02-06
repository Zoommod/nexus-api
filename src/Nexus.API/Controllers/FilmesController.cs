using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Filme;
using Nexus.Application.Interfaces;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;

namespace Nexus.API.Controllers;

[Authorize]
public class FilmesController : BaseController
{
    private readonly IFilmeService _filmeService;

    public FilmesController(IFilmeService filmeService)
    {
        _filmeService = filmeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilmeDto>>> ObterTodos()
    {
        var usuarioId = ObterUsuarioId();
        var filmes = await _filmeService.ObterTodosPorUsuarioAsync(usuarioId);
        return Ok(filmes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmeDto>> ObterPorId(Guid id)
    {
        var usuarioId = ObterUsuarioId();
        var filme = await _filmeService.ObterPorIdAsync(id, usuarioId);

        if (filme == null)
            return NotFound(new { mensagem = "Filme não encontrado" });

        return Ok(filme);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<FilmeDto>>> ObterPorStatus(StatusMidia status)
    {
        var usuarioId = ObterUsuarioId();
        var filmes = await _filmeService.ObterPorStatusAsync(status, usuarioId);
        return Ok(filmes);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<FilmeDto>>> BuscarPorTitulo([FromQuery] string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return BadRequest(new { mensagem = "O título deve ser informado" });

        var usuarioId = ObterUsuarioId();
        var filmes = await _filmeService.BuscarPorTituloAsync(titulo, usuarioId);
        return Ok(filmes);
    }

    [HttpPost]
    public async Task<ActionResult<FilmeDto>> Criar([FromBody] CriarFilmeDto dto)
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            var filme = await _filmeService.CriarAsync(dto, usuarioId);
            return CreatedAtAction(nameof(ObterPorId), new { id = filme.Id }, filme);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FilmeDto>> Atualizar(Guid id, [FromBody] AtualizarFilmeDto dto)
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            var filme = await _filmeService.AtualizarAsync(id, dto, usuarioId);
            return Ok(filme);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletar(Guid id)
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            await _filmeService.DeletarAsync(id, usuarioId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}
