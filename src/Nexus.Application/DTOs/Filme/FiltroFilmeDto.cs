using Nexus.Domain.Common;
using Nexus.Domain.Enums;

namespace Nexus.Application.DTOs.Filme;

public record class FiltroFilmeDto : PaginacaoParametros
{
    public string? Titulo { get; init; }
    public string? Diretor { get; init; }
    public StatusMidia? Status { get; init; }
    public Guid? GeneroId { get; init; }
    public decimal? NotaMinima { get; init; }
    public decimal? NotaMaxima { get; init; }
    public int? AnoLancamentoMinimo { get; init; }
    public int? AnoLancamentoMaximo { get; init; }
    public int? DuracaoMinima { get; init; }
    public int? DuracaoMaxima { get; init; }
}
