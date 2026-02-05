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

    public async Task<IEnumerable<JogoDto>> ObterTodosPorUsuarioAsync(string usuarioId)
    {
        var jogos = await _jogoRepositorio.ObterTodosPorUsuarioAsync(usuarioId);
        return _mapper.Map<IEnumerable<JogoDto>>(jogos);
    }

    public async Task<JogoDto?> ObterPorIdAsync(Guid id, string usuarioId)
    {
        var jogo = await _jogoRepositorio.ObterPorIdAsync(id);

        if (jogo == null || jogo.UsuarioId != usuarioId)
            return null;

        return _mapper.Map<JogoDto>(jogo);
    }

    public async Task<IEnumerable<JogoDto>> ObterPorStatusAsync(StatusMidia status, string usuarioId)
    {
        var jogos = await _jogoRepositorio.ObterPorStatusAsync(status, usuarioId);
        return _mapper.Map<IEnumerable<JogoDto>>(jogos);
    }

    public async Task<IEnumerable<JogoDto>> BuscarPorTituloAsync(string titulo, string usuarioId)
    {
        var jogos = await _jogoRepositorio.BuscarPorTituloAsync(titulo, usuarioId);
        return _mapper.Map<IEnumerable<JogoDto>>(jogos);
    }

    public async Task<JogoDto> CriarAsync(CriarJogoDto dto, string usuarioId)
    {
        var jogo = _mapper.Map<Jogo>(dto);
        jogo.UsuarioId = usuarioId;
        jogo.DataCriacao = DateTime.UtcNow;

        if (dto.GenerosIds != null && dto.GenerosIds.Any())
        {
            var generos = await _generoRepositorio.ObterPorIdsAsync(dto.GenerosIds);

            if (generos.Count() != dto.GenerosIds.Count())
                throw new ArgumentException("Um ou mais gêneros informados não existem");

            jogo.Generos = generos.ToList();
        }

        await _jogoRepositorio.AdicionarAsync(jogo);

        return _mapper.Map<JogoDto>(jogo);
    }

    public async Task<JogoDto> AtualizarAsync(Guid id, AtualizarJogoDto dto, string usuarioId)
    {
        var jogo = await _jogoRepositorio.ObterPorIdAsync(id);

        if (jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");

        if (jogo.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para atualizar este jogo");

        jogo.Titulo = dto.Titulo;
        jogo.Descricao = dto.Descricao;
        jogo.DataLancamento = dto.DataLancamento;
        jogo.Desenvolvedora = dto.Desenvolvedora;
        jogo.Publicadora = dto.Publicadora;
        jogo.UrlImagemCapa = dto.UrlImagemCapa;
        jogo.Status = (StatusMidia)dto.Status;
        jogo.NotaUsuario = dto.NotaUsuario;

        if (dto.GenerosIds != null && dto.GenerosIds.Any())
        {
            var generos = await _generoRepositorio.ObterPorIdsAsync(dto.GenerosIds);

            if (generos.Count() != dto.GenerosIds.Count())
                throw new ArgumentException("Um ou mais gêneros informados não existem");

            jogo.Generos = generos.ToList();
        }

        await _jogoRepositorio.AtualizarAsync(jogo);

        return _mapper.Map<JogoDto>(jogo);
    }

    public async Task DeletarAsync(Guid id, string usuarioId)
    {
        var jogo = await _jogoRepositorio.ObterPorIdAsync(id);

        if (jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");

        if (jogo.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para deletar este jogo");

        await _jogoRepositorio.DeletarAsync(jogo.Id);
    }
}
