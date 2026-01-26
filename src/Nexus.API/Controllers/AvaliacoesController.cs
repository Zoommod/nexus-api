using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Avaliacao;
using Nexus.Application.Interfaces;

namespace Nexus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacoesController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacoesController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AvaliacaoDto>> ObterPorId(Guid id)
        {
            var avaliacao = await _avaliacaoService.ObterPorIdAsync(id);

            if(avaliacao == null)
                return NotFound(new { mensagem = "Avaliação não encontrada"});
            
            return Ok(avaliacao);
        }

        [HttpGet("jogo/{jogoId}")]
        public async Task<ActionResult<IEnumerable<AvaliacaoDto>>> ObterPorJogo(Guid jogoId)
        {
            var avaliacoes = await _avaliacaoService.ObterPorJogoAsync(jogoId);
            return Ok(avaliacoes);
        }

        [HttpGet("filme/{filmeId}")]
        public async Task<ActionResult<IEnumerable<AvaliacaoDto>>> ObterPorFilme(Guid filmeId)
        {
            var avaliacoes = await _avaliacaoService.ObterPorFilmeAsync(filmeId);
            return Ok(avaliacoes);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<AvaliacaoDto>>> ObterPorUsuario(string usuarioId)
        {
            var avaliacoes = await _avaliacaoService.ObterPorUsuarioAsync(usuarioId);
            return Ok(avaliacoes);
        }

        [HttpPost]
        public async Task<ActionResult<AvaliacaoDto>> Criar([FromBody] CriarAvaliacaoDto dto)
        {
            try
            {
                var usuarioId = "usuario-temporario-123";
                var avaliacao  =await _avaliacaoService.CriarAsync(dto, usuarioId);
                return CreatedAtAction(nameof (ObterPorId), new { id = avaliacao.Id }, avaliacao);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message} );
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AvaliacaoDto>> Atualizar (Guid id, [FromBody] AtualizarAvaliacaoDto dto)
        {
            try
            {
                var usuarioId = "usuario-temporario-123";
                var avaliacao = await _avaliacaoService.AtualizarAsync(id, dto, usuarioId);
                return Ok(avaliacao);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound( new { mensagem = ex.Message });
            }
            catch(UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch(ArgumentException ex)
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
                await _avaliacaoService.DeletarAsync(id, usuarioId);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch(UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
