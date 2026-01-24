using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Genero;
using Nexus.Application.Interfaces;
using Nexus.Application.Services;
using Nexus.Domain.Entities;

namespace Nexus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GenerosController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GenerosController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroDto>>> ObterTodos()
        {
            var generos = await _generoService.ObterTodosAsync();
            return Ok(generos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroDto>> ObterPorId(Guid id)
        {
            var genero = await _generoService.ObterPorIdAsync(id);

            if(genero == null)
                return NotFound(new { mensagem = "Gênero não encontrado" });
            
            return Ok(genero);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<GeneroDto>> ObterPorNome(string nome)
        {
            var genero = await _generoService.ObterPorNomeAsync(nome);
            if(genero == null)
                return NotFound(new { mensagem = "Gênero não encontrado" });
            
            return Ok(genero);
        }

        [HttpPost]
        public async Task<ActionResult<GeneroDto>> Criar([FromBody] CriarGeneroDto dto)
        {
            try
            {
                var genero = await _generoService.CriarAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = genero.Id}, genero);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(new { mensagem = ex.Message});
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneroDto>> Atualizar(Guid id, [FromBody] AtualizarGeneroDto dto)
        {
            try
            {
                var genero = await _generoService.AtualizarAsync(id, dto);
                return Ok(genero);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(Guid id)
        {
            try
            {
                await _generoService.DeletarAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
