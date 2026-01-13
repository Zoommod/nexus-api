using System;

namespace Nexus.Domain.Entities;

public class Avaliacao : EntidadeBase
{
    public string Conteudo { get; set; } = string.Empty;
    public decimal Nota { get; set; }

    public Guid? JogoId { get; set; }
    public Guid? Jogo { get; set; }

    public Guid? FilmeId { get; set; }
    public Guid? Filme { get; set; }

    public string UsuarioId { get; set; } = string.Empty;
}
