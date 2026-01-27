using System;

namespace Nexus.Domain.Entities;

public class Usuario
{
    public string Id { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }

    public ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
    public ICollection<Filme> Filmes { get; set; } = new List<Filme>();
    public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
}
