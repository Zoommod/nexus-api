using System;

namespace Nexus.Application.DTOs.Filme;

public class AtualizarFilmeDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataLancamento { get; set; }
    public string? Diretor { get; set; }
    public int? DuracaoMinutos { get; set; }
    public string? UrlImagemPoster { get; set; }
    public int Status { get; set; }
    public decimal? NotaUsuario { get; set; }
    public List<Guid> GenerosIds { get; set; } = new();
}
