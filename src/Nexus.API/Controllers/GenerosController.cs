using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.DTOs.Genero;
using Nexus.Application.Interfaces;
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
    }
}
