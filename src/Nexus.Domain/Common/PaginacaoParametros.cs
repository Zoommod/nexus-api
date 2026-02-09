namespace Nexus.Domain.Common;

public record class PaginacaoParametros
{
    private const int TamanhoPaginaMaximo = 100;
    private const int TamanhoPaginaPadrao = 20;

    private int _numeroPagina = 1;
    private int _tamanhoPagina = TamanhoPaginaPadrao;

    public int NumeroPagina
    {
        get => _numeroPagina;
        init => _numeroPagina = value < 1 ? 1 : value;
    }

    public int TamanhoPagina
    {
        get => _tamanhoPagina;
        init => _tamanhoPagina = value switch
        {
            < 1 => TamanhoPaginaPadrao,
            > TamanhoPaginaMaximo => TamanhoPaginaMaximo,
            _ => value
        };
    }

    public string? OrdenarPor { get; set; }
    public string Direcao { get; set; } = "asc";
    public string? Busca { get; set; }
    public int ObterPular() => (NumeroPagina - 1) * TamanhoPagina;

    public bool EhOrdenacaoDescendente() =>
        Direcao.Equals("desc", StringComparison.OrdinalIgnoreCase);
}
