using System;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.VisualBasic;
using Nexus.Application.DTOs.Jogo;
using Nexus.Application.Interfaces;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;

namespace Nexus.Application.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepositorio _jogoRepositorio;
    private readonly IGeneroRepositorio _generoRepositorio;
    private readonly IMapper _mapper;

    public JogoService(IJogoRepositorio jogoRepositorio, IGeneroRepositorio generoRepositorio, IMapper mapper)
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

        if (dto.GenerosIds != null && dto.GenerosIds.Any())
        {
            var generos = new List<Genero>();
            foreach (var generoId in dto.GenerosIds)
            {
                var genero = await _generoRepositorio.ObterPorIdAsync(generoId);
                if (genero != null)
                    generos.Add(genero);
            }
            jogo.Generos = generos;
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

        _mapper.Map(dto, jogo);

        if (dto.GenerosIds != null)
        {
            var generos = new List<Genero>();
            foreach (var generoId in dto.GenerosIds)
            {
                var genero = await _generoRepositorio.ObterPorIdAsync(generoId);
                if (genero != null)
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

        if (jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");

        if (jogo.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para deletar este jogo");

        await _jogoRepositorio.DeletarAsync(jogo.Id);
    }

        public async Task<ResultadoPaginado<JogoDto>> ObterTodosPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros)
    {
        var jogosPaginados = await _jogoRepositorio.ObterTodosPorUsuarioPaginadoAsync(usuarioId, parametros);
        var jogosDto = _mapper.Map<IReadOnlyList<JogoDto>>(jogosPaginados.Itens);

        return ResultadoPaginado<JogoDto>.Criar(
            jogosDto,
            jogosPaginados.TotalItens,
            jogosPaginados.PaginaAtual,
            jogosPaginados.TamanhoPagina);
    }

    public async Task<ResultadoPaginado<JogoDto>> ObterComFiltrosAsync(string usuarioId, FiltroJogoParametros filtros)
    {
        var jogosPaginados = await _jogoRepositorio.ObterComFiltrosAsync(usuarioId, filtros);
        var jogosDto = _mapper.Map<IReadOnlyList<JogoDto>>(jogosPaginados.Itens);

        return ResultadoPaginado<JogoDto>.Criar(
            jogosDto,
            jogosPaginados.TotalItens,
            jogosPaginados.PaginaAtual,
            jogosPaginados.TamanhoPagina);
    }
}
