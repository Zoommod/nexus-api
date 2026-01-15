using System;

namespace Nexus.Application.DTOs.Avaliacao;

public class AvaliacaoDto
{
    public Guid Id { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public decimal Nota { get; set; }
    public DateTime DataCriacao { get; set; }
    
    public Guid? JogoId { get; set; }
    public string? JogoTitulo { get; set; }

    public Guid? FilmeId { get; set; }
    public string? FilmeTitulo { get; set; }
}
