using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Jogo;
using Nexus.Application.Interfaces;
using Nexus.Domain.Common;
using Nexus.Domain.Enums;

namespace Nexus.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class JogosController : BaseController
{
    private readonly IJogoService _jogoService;

    public JogosController(IJogoService jogoService)
    {
        _jogoService = jogoService;
    }

    [HttpGet]
    [Obsolete("Use GET /api/jogos/paginado para melhor performance")]
    public async Task<ActionResult<IEnumerable<JogoDto>>> ObterTodos()
    {
        var usuarioId = ObterUsuarioId();
        var jogos = await _jogoService.ObterTodosPorUsuarioAsync(usuarioId);
        return Ok(jogos);
    }

    [HttpGet("paginado")]
    public async Task<ActionResult<ResultadoPaginado<JogoDto>>> ObterTodosPaginado(
        [FromQuery] PaginacaoParametros parametros
    )
    {
        var usuarioId = ObterUsuarioId();
        var resultado = await _jogoService.ObterTodosPorUsuarioPaginadoAsync(usuarioId, parametros);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JogoDto>> ObterPorId(Guid id)
    {
        var usuarioId = ObterUsuarioId();
        var jogo = await _jogoService.ObterPorIdAsync(id, usuarioId);

        if (jogo == null)
            return NotFound(new { mensagem = "Jogo não encontrado" });

        return Ok(jogo);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<JogoDto>>> ObterPorStatus(StatusMidia status)
    {
        var usuarioId = ObterUsuarioId();
        var jogos = await _jogoService.ObterPorStatusAsync(status, usuarioId);
        return Ok(jogos);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<JogoDto>>> BuscarPorTitulo([FromQuery] string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return BadRequest(new { mensagem = "O título deve ser informado" });

        var usuarioId = ObterUsuarioId();
        var jogos = await _jogoService.BuscarPorTituloAsync(titulo, usuarioId);
        return Ok(jogos);
    }

    [HttpPost]
    public async Task<ActionResult<JogoDto>> Criar([FromBody] CriarJogoDto dto)
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            var jogo = await _jogoService.CriarAsync(dto, usuarioId);
            return CreatedAtAction(nameof(ObterPorId), new { id = jogo.Id }, jogo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<JogoDto>> Atualizar(Guid id, [FromBody] AtualizarJogoDto dto)
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            var jogo = await _jogoService.AtualizarAsync(id, dto, usuarioId);
            return Ok(jogo);
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
            await _jogoService.DeletarAsync(id, usuarioId);
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
