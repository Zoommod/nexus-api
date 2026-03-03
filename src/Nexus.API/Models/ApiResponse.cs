using System;

namespace Nexus.API.Models;

public class ApiResponse<T>
{
    public bool Sucesso { get; set; }
    public T? Dados { get; set; }
    public ErroDetalhes? Erro { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse<T> ComSucesso(T dados)
    {
        return new ApiResponse<T>
        {
            Sucesso = true,
            Dados = dados
        };
    }

    public static ApiResponse<T> ComErro(string codigo, string mensagem, List<string>? detalhes = null)
    {
        return new ApiResponse<T>
        {
            Erro = new ErroDetalhes
            {
                Codigo = codigo,
                Mensagem = mensagem,
                Detalhes = detalhes
            }
        };
    }
}
