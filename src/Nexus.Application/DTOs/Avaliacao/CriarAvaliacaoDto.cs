using System;

namespace Nexus.Application.DTOs.Avaliacao;

public class CriarAvaliacaoDto
{
    public string Conteudo { get; set; } = string.Empty;
    public decimal Nota { get; set; }

    public Guid? JogoId { get; set; }
    public Guid? FilmeId { get; set; }
}
