using System;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Common;

namespace Nexus.Infrastructure.Extensions;

public static class QueryableExtensions
{

    public static async Task<ResultadoPaginado<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query,
        PaginacaoParametros parametros,
        CancellationToken cancellationToken = default)
    {
        var totalItens = await query.CountAsync(cancellationToken);

        if (totalItens == 0)
            return ResultadoPaginado<T>.Vazio();

        var itens = await query
            .Skip(parametros.ObterPular())
            .Take(parametros.TamanhoPagina)
            .ToListAsync(cancellationToken);

        return ResultadoPaginado<T>.Criar(
            itens,
            totalItens,
            parametros.NumeroPagina,
            parametros.TamanhoPagina);
    }

    public static IQueryable<T> OrderByProperty<T>(
        this IQueryable<T> query,
        string? propriedade,
        bool descendente = false)
    {
        if (string.IsNullOrWhiteSpace(propriedade))
            return query;

        var tipo = typeof(T);
        var parametro = System.Linq.Expressions.Expression.Parameter(tipo, "x");

        var propriedadeInfo = tipo.GetProperties()
            .FirstOrDefault(p => p.Name.Equals(propriedade, StringComparison.OrdinalIgnoreCase));

        if (propriedadeInfo == null)
            return query;

        var propriedadeAcesso = System.Linq.Expressions.Expression.Property(parametro, propriedadeInfo);
        var lambda = System.Linq.Expressions.Expression.Lambda(propriedadeAcesso, parametro);

        var nomeMetodo = descendente ? "OrderByDescending" : "OrderBy";
        var tipoResultado = typeof(Queryable);
        var metodoGenerico = tipoResultado.GetMethods()
            .First(m => m.Name == nomeMetodo && m.GetParameters().Length == 2)
            .MakeGenericMethod(tipo, propriedadeInfo.PropertyType);

        return (IQueryable<T>)metodoGenerico.Invoke(null, new object[] { query, lambda })!;
    }
}
