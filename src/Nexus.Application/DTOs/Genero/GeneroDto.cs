using System;

namespace Nexus.Application.DTOs.Genero;

public class GeneroDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
}
