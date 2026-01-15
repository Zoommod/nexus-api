using System;
using Nexus.Application.DTOs.Comum;

namespace Nexus.Application.DTOs.Filme;

public class FilmeDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataLancamento { get; set; }
    public string? Diretor { get; set; }
    public int? DuracaoMinutos { get; set; }
    public string? UrlImagemPoster { get; set; }
    public int Status { get; set; }
    public decimal? NotaUsuario { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public List<GeneroSimplificadoDto> Generos { get; set; }= new();
}