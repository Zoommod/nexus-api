using System;

namespace Nexus.Domain.Common;


public class ResultadoPaginado<T>
{
    public IReadOnlyList<T> Itens { get; init; } = new List<T>();

    public int PaginaAtual { get; init; }

    public int TamanhoPagina { get; init; }

    public int TotalItens { get; init; }

    public int TotalPaginas { get; init; }

    public bool TemPaginaAnterior => PaginaAtual > 1;

    public bool TemProximaPagina => PaginaAtual < TotalPaginas;

    public int PrimeiraPagina => TotalPaginas > 0 ? 1 : 0;
    public int UltimaPagina => TotalPaginas;

    public int PrimeiroItemPagina => TotalItens == 0 ? 0 : ((PaginaAtual - 1) * TamanhoPagina) + 1;

    public int UltimoItemPagina => Math.Min(PaginaAtual * TamanhoPagina, TotalItens);


    public static ResultadoPaginado<T> Criar(
        IReadOnlyList<T> itens,
        int totalItens,
        int numeroPagina,
        int tamanhoPagina)
    {
        var totalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina);

        return new ResultadoPaginado<T>
        {
            Itens = itens,
            PaginaAtual = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = totalItens,
            TotalPaginas = totalPaginas
        };
    }

    public static ResultadoPaginado<T> Vazio() => new()
    {
        Itens = new List<T>(),
        PaginaAtual = 1,
        TamanhoPagina = 20,
        TotalItens = 0,
        TotalPaginas = 0
    };
}
