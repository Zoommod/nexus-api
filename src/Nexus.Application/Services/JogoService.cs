using System;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.VisualBasic;
using Nexus.Application.DTOs.Jogo;
using Nexus.Application.Interfaces;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;

namespace Nexus.Application.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepositorio _jogoRepositorio;
    private readonly IGeneroRepositorio _generoRepositorio;
    private readonly IMapper _mapper;

    public JogoService(
        IJogoRepositorio jogoRepositorio,
        IGeneroRepositorio generoRepositorio,
        IMapper mapper)

    {
        _jogoRepositorio = jogoRepositorio;
        _generoRepositorio = generoRepositorio;
        _mapper = mapper;
    }

    public async Task<JogoDto> CriarAsync(CriarJogoDto dto, string usuarioId)
    {
        if(dto.NotaUsuario.HasValue && (dto.NotaUsuario < 0 || dto.NotaUsuario > 10))
            throw new ArgumentException("A nota deve estar entre 0 e 10");
        
            var jogo = _mapper.Map<Jogo>(dto);

        jogo.Id = Guid.NewGuid();
        jogo.DataCriacao = DateTime.UtcNow;
        jogo.UsuarioId = usuarioId;

        if (dto.GenerosIds.Any())
        {
            var generos = new List<Genero>();
            foreach(var generoId in dto.GenerosIds)
            {
                var genero = await _generoRepositorio.ObterPorIdAsync(generoId);
                if(genero != null)
                    generos.Add(genero);
            }
            jogo.Generos = generos;
        }

        var jogoSalvo = await _jogoRepositorio.AdicionarAsync(jogo);

        return _mapper.Map<JogoDto>(jogoSalvo);
    }

    public async Task<JogoDto> AtualizarAsync(Guid id, AtualizarJogoDto dto, string usuarioId)
    {
        var jogo = await _jogoRepositorio.ObterPorIdAsync(id);
        if(jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");
        
        if(jogo.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para editar esse jogo");
        
        if (dto.NotaUsuario.HasValue && (dto.NotaUsuario < 0 || dto.NotaUsuario > 10))
            throw new ArgumentException("A nota deve estar entre 0 e 10");
        
        jogo.Titulo = dto.Titulo;
        jogo.Descricao = dto.Descricao;
        jogo.DataLancamento = dto.DataLancamento;
        jogo.Desenvolvedora = dto.Desenvolvedora;
        jogo.Publicadora = dto.Publicadora;
        jogo.UrlImagemCapa = dto.UrlImagemCapa;
        jogo.Status = (StatusMidia)dto.Status;
        jogo.NotaUsuario = dto.NotaUsuario;
        jogo.DataAtualizacao = DateTime.UtcNow;

        if (dto.GenerosIds.Any())
        {
            var generos = new List<Genero>();
            foreach(var generoId in dto.GenerosIds)
            {
                var genero = await _generoRepositorio.ObterPorIdAsync(generoId);
                if(genero != null)
                    generos.Add(genero);
            }
            jogo.Generos = generos;
        }

        await _jogoRepositorio.AtualizarAsync(jogo);

        return _mapper.Map<JogoDto>(jogo);
        
    }

    public async Task DeletarAsync(Guid id, string usuarioId)
    {
        var jogo = await _jogoRepositorio.ObterPorIdAsync(id);
        if(jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");
        if(jogo.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para deletar esse jogo");
        
        await _jogoRepositorio.DeletarAsync(id);
    }

    public async Task<JogoDto?> ObterPorIdAsync(Guid id)
    {
        var jogo = await _jogoRepositorio.ObterPorIdAsync(id);
        return jogo != null ? _mapper.Map<JogoDto>(jogo) : null;
    }

    public async Task<IEnumerable<JogoDto>> ObterTodosAsync()
    {
        var jogos = await _jogoRepositorio.ObterComGenerosAsync();
        return _mapper.Map<IEnumerable<JogoDto>>(jogos);
    }

    public async Task<IEnumerable<JogoDto>> ObterPorStatusAsync(int status)
    {
        var jogos = await _jogoRepositorio.ObterPorStatusAsync((StatusMidia)status);
        return _mapper.Map<IEnumerable<JogoDto>>(jogos);
    }

    public async Task<IEnumerable<JogoDto>> BuscarPorTituloAsync(string titulo)
    {
        var jogos = await _jogoRepositorio.BuscarPorTituloAsync(titulo);
        return _mapper.Map<IEnumerable<JogoDto>>(jogos);
    }



}
