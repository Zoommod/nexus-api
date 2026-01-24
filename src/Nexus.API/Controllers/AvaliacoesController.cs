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
    }
}
