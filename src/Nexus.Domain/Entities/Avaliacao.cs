using System;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Entities;

public class Avaliacao : EntidadeBase
{
    public string Conteudo { get; set; } = string.Empty;
    public decimal Nota { get; set; }

    public Guid? JogoId { get; set; }
    public Jogo? Jogo { get; set; }

    public Guid? FilmeId { get; set; }
    public Filme? Filme { get; set; }

    public string UsuarioId { get; set; } = string.Empty;
}
