using System;
using AutoMapper.Internal.Mappers;

namespace Nexus.Application.DTOs.Filme;

public class CriarFilmeDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataLancamento { get; set; }
    public string? Diretor { get; set; }
    public int? DuracaoMinutos { get; set; }
    public string? UrlImagemPoster { get; set; }
    public int Status { get; set; } = 1;
    public decimal? NotaUsuario { get; set; }
    public List<Guid> GenerosIds { get; set; } = new();
}
