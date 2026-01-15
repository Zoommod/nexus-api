using AutoMapper;
using Nexus.Application.DTOs.Avaliacao;
using Nexus.Application.DTOs.Comum;
using Nexus.Application.DTOs.Filme;
using Nexus.Application.DTOs.Genero;
using Nexus.Application.DTOs.Jogo;
using Nexus.Domain.Entities;

namespace Nexus.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<Jogo, JogoDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.Generos, opt => opt.MapFrom(src => src.Generos));

        CreateMap<CriarJogoDto, Jogo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
            .ForMember(dest => dest.Generos, opt => opt.Ignore())
            .ForMember(dest => dest.Avaliacoes, opt => opt.Ignore());

        CreateMap<AtualizarJogoDto, Jogo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
            .ForMember(dest => dest.Generos, opt => opt.Ignore())
            .ForMember(dest => dest.Avaliacoes, opt => opt.Ignore());
        
        CreateMap<Filme, FilmeDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.Generos, opt => opt.MapFrom(src => src.Generos));

        CreateMap<CriarFilmeDto, Filme>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
            .ForMember(dest => dest.Generos, opt => opt.Ignore())
            .ForMember(dest => dest.Avaliacoes, opt => opt.Ignore());

        CreateMap<AtualizarFilmeDto, Filme>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
            .ForMember(dest => dest.Generos, opt => opt.Ignore())
            .ForMember(dest => dest.Avaliacoes, opt => opt.Ignore());

        
        CreateMap<Genero, GeneroDto>();
        CreateMap<CriarGeneroDto, Genero>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.Jogos, opt => opt.Ignore())
            .ForMember(dest => dest.Filmes, opt => opt.Ignore());

        CreateMap<AtualizarGeneroDto, Genero>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.Jogos, opt => opt.Ignore())
            .ForMember(dest => dest.Filmes, opt => opt.Ignore());

        CreateMap<Genero, GeneroSimplificadoDto>();

        
        CreateMap<Avaliacao, AvaliacaoDto>()
            .ForMember(dest => dest.JogoTitulo, opt => opt.MapFrom(src => src.Jogo != null ? src.Jogo.Titulo : null))
            .ForMember(dest => dest.FilmeTitulo, opt => opt.MapFrom(src => src.Filme != null ? src.Filme.Titulo : null));

        CreateMap<CriarAvaliacaoDto, Avaliacao>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
            .ForMember(dest => dest.Jogo, opt => opt.Ignore())
            .ForMember(dest => dest.Filme, opt => opt.Ignore());

        CreateMap<AtualizarAvaliacaoDto, Avaliacao>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
            .ForMember(dest => dest.JogoId, opt => opt.Ignore())
            .ForMember(dest => dest.FilmeId, opt => opt.Ignore())
            .ForMember(dest => dest.Jogo, opt => opt.Ignore())
            .ForMember(dest => dest.Filme, opt => opt.Ignore());
    }
}