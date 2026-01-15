using System;

namespace Nexus.Application.DTOs.Avaliacao;

public class AtualizarAvaliacaoDto
{
    public string Conteudo { get; set; } = string.Empty;
    public decimal Nota { get; set; }
}
