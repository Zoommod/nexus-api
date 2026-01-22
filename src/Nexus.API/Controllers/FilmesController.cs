using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Filme;
using Nexus.Application.Interfaces;

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

    }
}
