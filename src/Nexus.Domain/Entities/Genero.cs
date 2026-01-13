using System;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Entities;

public class Genero : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }

    public ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
    public ICollection<Filme> Filmes { get; set; } = new List<Filme>();
}
