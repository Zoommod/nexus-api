using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Filme;
using Nexus.Application.Interfaces;
using Nexus.Domain.Entities;

namespace Nexus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService _filmeService;

        public FilmesController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeDto>>> ObterTodos()
        {
            var filmes = await _filmeService.ObterTodosAsync();
            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeDto>> ObterPorId(Guid id)
        {
            var filme = await _filmeService.ObterPorIdAsync(id);

            if(filme == null)
                return NotFound(new {mensagem = "Filme não encontrado" });
            
            return Ok(filme);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<FilmeDto>>> ObterPorStatus(int status)
        {
            var filmes = await _filmeService.ObterPorStatusAsync(status);
            return Ok(filmes);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<FilmeDto >>> BuscarPorDiretor([FromQuery] string diretor)
        {
            if(string.IsNullOrWhiteSpace(diretor))
                return BadRequest(new {mensagem = "O nome do diretor deve ser informado" });
            
            var filmes = await _filmeService.ObterPorDiretorAsync(diretor);
            return Ok(filmes);
        }

        [HttpPost]
        public async Task<ActionResult<FilmeDto>> Criar([FromBody] CriarFilmeDto dto)
        {
            try{
                var usuarioId = "usuario-temporario-123";
                var filme = await _filmeService.CriarAsync(dto, usuarioId);
                return CreatedAtAction(nameof(ObterPorId), new { id = filme.Id}, filme);
            }
            catch (ArgumentException ex){
                return BadRequest(new { mensagem = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FilmeDto>> Atualizar(Guid id, [FromBody] AtualizarFilmeDto dto)
        {
            try
            {
                var usuarioId = "usuario-temporario-123";
                var filme = await _filmeService.AtualizarAsync(id, dto, usuarioId);
                return Ok(filme);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch(UnauthorizedAccessException)
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
                await _filmeService.DeletarAsync(id, usuarioId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
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
