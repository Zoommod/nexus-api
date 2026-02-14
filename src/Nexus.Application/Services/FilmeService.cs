using System;
using AutoMapper;
using Nexus.Application.DTOs.Filme;
using Nexus.Application.Interfaces;
using Nexus.Domain.Common;
using Nexus.Domain.Entities;
using Nexus.Domain.Enums;
using Nexus.Domain.Interfaces;

namespace Nexus.Application.Services;

public class FilmeService : IFilmeService
{
    private readonly IFilmeRepositorio _filmeRepositorio;
    private readonly IGeneroRepositorio _generoRepositorio;
    private readonly IMapper _mapper;

    public FilmeService(
        IFilmeRepositorio filmeRepositorio,
        IGeneroRepositorio generoRepositorio,
        IMapper mapper)
    {
        _filmeRepositorio = filmeRepositorio;
        _generoRepositorio = generoRepositorio;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FilmeDto>> ObterTodosPorUsuarioAsync(string usuarioId)
    {
        var filmes = await _filmeRepositorio.ObterTodosPorUsuarioAsync(usuarioId);
        return _mapper.Map<IEnumerable<FilmeDto>>(filmes);
    }

    public async Task<FilmeDto?> ObterPorIdAsync(Guid id, string usuarioId)
    {
        var filme = await _filmeRepositorio.ObterPorIdAsync(id);

        if (filme == null || filme.UsuarioId != usuarioId)
            return null;

        return _mapper.Map<FilmeDto>(filme);
    }

    public async Task<IEnumerable<FilmeDto>> ObterPorStatusAsync(StatusMidia status, string usuarioId)
    {
        var filmes = await _filmeRepositorio.ObterPorStatusAsync(status, usuarioId);
        return _mapper.Map<IEnumerable<FilmeDto>>(filmes);
    }

    public async Task<IEnumerable<FilmeDto>> BuscarPorTituloAsync(string titulo, string usuarioId)
    {
        var filmes = await _filmeRepositorio.BuscarPorTituloAsync(titulo, usuarioId);
        return _mapper.Map<IEnumerable<FilmeDto>>(filmes);
    }

    public async Task<FilmeDto> CriarAsync(CriarFilmeDto dto, string usuarioId)
    {
        var filme = _mapper.Map<Filme>(dto);
        filme.UsuarioId = usuarioId;
        filme.DataCriacao = DateTime.UtcNow;

        if (dto.GenerosIds != null && dto.GenerosIds.Any())
        {
            var generos = await _generoRepositorio.ObterPorIdsAsync(dto.GenerosIds);

            if (generos.Count() != dto.GenerosIds.Count())
                throw new ArgumentException("Um ou mais gêneros informados não existem");

            filme.Generos = generos.ToList();
        }

        await _filmeRepositorio.AdicionarAsync(filme);

        return _mapper.Map<FilmeDto>(filme);
    }

    public async Task<FilmeDto> AtualizarAsync(Guid id, AtualizarFilmeDto dto, string usuarioId)
    {
        var filme = await _filmeRepositorio.ObterPorIdAsync(id);

        if (filme == null)
            throw new KeyNotFoundException("Filme não encontrado");

        if (filme.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para atualizar este filme");

        filme.Titulo = dto.Titulo;
        filme.Descricao = dto.Descricao;
        filme.DataLancamento = dto.DataLancamento;
        filme.Diretor = dto.Diretor;
        filme.DuracaoMinutos = dto.DuracaoMinutos;
        filme.UrlImagemPoster = dto.UrlImagemPoster;
        filme.Status = (StatusMidia)dto.Status;
        filme.NotaUsuario = dto.NotaUsuario;

        if (dto.GenerosIds != null && dto.GenerosIds.Any())
        {
            var generos = await _generoRepositorio.ObterPorIdsAsync(dto.GenerosIds);

            if (generos.Count() != dto.GenerosIds.Count())
                throw new ArgumentException("Um ou mais gêneros informados não existem");

            filme.Generos = generos.ToList();
        }

        await _filmeRepositorio.AtualizarAsync(filme);

        return _mapper.Map<FilmeDto>(filme);
    }

    public async Task DeletarAsync(Guid id, string usuarioId)
    {
        var filme = await _filmeRepositorio.ObterPorIdAsync(id);

        if (filme == null)
            throw new KeyNotFoundException("Filme não encontrado");

        if (filme.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para deletar este filme");

        await _filmeRepositorio.DeletarAsync(filme.Id);
    }

    public async Task<ResultadoPaginado<FilmeDto>> ObterTodosPorUsuarioPaginadoAsync(string usuarioId, PaginacaoParametros parametros)
    {
        var filmesPaginados = await _filmeRepositorio.ObterTodosPorUsuarioPaginadoAsync(usuarioId, parametros);
        
        var filmesDto = _mapper.Map<IReadOnlyList<FilmeDto>>(filmesPaginados.Itens);
        
        return ResultadoPaginado<FilmeDto>.Criar(
            filmesDto,
            filmesPaginados.TotalItens,
            filmesPaginados.PaginaAtual,
            filmesPaginados.TamanhoPagina);
    }
    
}
