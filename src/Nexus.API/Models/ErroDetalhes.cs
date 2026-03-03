using System;

namespace Nexus.API.Models;

public class ErroDetalhes
{
    public string Codigo { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public List<string>? Detalhes { get; set; }
}
