using System;
using Nexus.Application.DTOs.Comum;

namespace Nexus.Application.DTOs.Jogo;

public class JogoDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataLancamento { get; set; }
    public string? Desenvolvedora { get; set; }
    public string? Publicadora { get; set; }
    public string? UrlImagemCapa { get; set; }
    public int Status { get; set; }
    public decimal? NotaUsuario { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizcao { get; set; }

    public List<GeneroSimplificadoDto> Generos { get; set; } = new();
}
