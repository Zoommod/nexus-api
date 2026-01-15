using System;

namespace Nexus.Application.DTOs.Jogo;

public class AtualizarJogoDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataLancamento { get; set; }
    public string? Desenvolvedora { get; set; }
    public string? Publicadora { get; set; }
    public string? UrlImagemCapa { get; set; }
    public int Status { get; set; }
    public decimal? NotaUsuario { get; set; }
    public List<Guid> GenerosIds { get; set; } = new();
}
