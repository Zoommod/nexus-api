using System;
using Nexus.Domain.Enums;

namespace Nexus.Domain.Entities;

public class Jogo : EntidadeBase
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataLancamento { get; set; }
    public string? Desenvolvedora { get; set; }
    public string? Publicadora { get; set; }
    public string? UrlImagemCapa { get; set; }

    public StatusMidia Status { get; set; } = StatusMidia.PretendeAssistir;

    public decimal? NotaUsuario { get; set; }

    public ICollection<Genero> Generos { get; set;} = new List<Genero>();
    public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

    public string UsuarioId { get; set; } = string.Empty;
    

}
