using System;
using Nexus.Domain.Common;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Common;


public record FiltroJogoParametros : PaginacaoParametros
{

    public string? Titulo { get; init; }
    public string? Desenvolvedora { get; init; }
    public StatusMidia? Status { get; init; }
    public Guid? GeneroId { get; init; }
    public decimal? NotaMinima { get; init; }
    public decimal? NotaMaxima { get; init; }
    public int? AnoLancamentoMinimo { get; init; }
    public int? AnoLancamentoMaximo { get; init; }
}
