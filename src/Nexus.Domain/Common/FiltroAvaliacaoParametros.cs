using Nexus.Domain.Common;

namespace Nexus.Domain.Common;

public record class FiltroAvaliacaoParametros : PaginacaoParametros
{
    public string? Conteudo { get; init; }
    public decimal? NotaMinima { get; init; }
    public decimal? NotaMaxima { get; init; }
    public bool? ApenasJogos { get; init; }
    public bool? ApenasFilmes { get; init; }

    public DateTime? DataCriacaoMinima { get; init; }
    public DateTime? DataCriacaoMaxima { get; init; }

}
