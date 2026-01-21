using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Jogo;
using Nexus.Application.Interfaces;

namespace Nexus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogosController : ControllerBase
{
    private readonly IJogoService _jogoService;

    public JogosController(IJogoService jogoService)
    {
        _jogoService = jogoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JogoDto>>> ObterTodos()
    {
        var jogos = await _jogoService.ObterTodosAsync();
        return Ok(jogos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JogoDto>> ObterPorId(Guid id)
    {
        var jogo = await _jogoService.ObterPorIdAsync(id);

        if (jogo == null)
            return NotFound(new { mensagem = "Jogo não encontrado" });

        return Ok(jogo);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<JogoDto>>> ObterPorStatus(int status)
    {
        var jogos = await _jogoService.ObterPorStatusAsync(status);
        return Ok(jogos);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<JogoDto>>> BuscarPorTitulo([FromQuery] string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return BadRequest(new { mensagem = "O título deve ser informado" });

        var jogos = await _jogoService.BuscarPorTituloAsync(titulo);
        return Ok(jogos);
    }

    [HttpPost]
    public async Task<ActionResult<JogoDto>> Criar([FromBody] CriarJogoDto dto)
    {
        try
        {
            var usuarioId = "usuario-temporario-123";

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
            var usuarioId = "usuario-temporario-123";

            var jogo = await _jogoService.AtualizarAsync(id, dto, usuarioId);
            return Ok(jogo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
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
            var usuarioId = "usuario-temporario-123";

            await _jogoService.DeletarAsync(id, usuarioId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid();
        }
    }
}
