using System;
using AutoMapper;
using Nexus.Application.DTOs.Filme;
using Nexus.Application.Interfaces;
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
        IMapper mapper
    )
    {
        _filmeRepositorio = filmeRepositorio;
        _generoRepositorio = generoRepositorio;
        _mapper = mapper;
    }

    public async Task<FilmeDto> CriarAsync(CriarFilmeDto dto, string usuarioId)
    {
        if (dto.NotaUsuario.HasValue && (dto.NotaUsuario < 0 || dto.NotaUsuario > 10))
            throw new ArgumentException("A nota deve estar entre 0 e 10");

        if (dto.DuracaoMinutos.HasValue && dto.DuracaoMinutos <= 0)
            throw new ArgumentException("A duração deve ser maior que zero");

        var filme = _mapper.Map<Filme>(dto);

        filme.Id = Guid.NewGuid();
        filme.DataCriacao = DateTime.UtcNow;
        filme.UsuarioId = usuarioId;

        if (dto.GenerosIds.Any())
        {
            var generos = new List<Genero>();
            foreach (var generoId in dto.GenerosIds)
            {
                var genero = await _generoRepositorio.ObterPorIdAsync(generoId);
                if (genero != null)
                    generos.Add(genero);
            }
            filme.Generos = generos;
        }

        var filmeSalvo = await _filmeRepositorio.AdicionarAsync(filme);

        return _mapper.Map<FilmeDto>(filmeSalvo);
    }

        public async Task<FilmeDto> AtualizarAsync(Guid id, AtualizarFilmeDto dto, string usuarioId)
    {
        var filme = await _filmeRepositorio.ObterPorIdAsync(id);
        if (filme == null)
            throw new KeyNotFoundException("Filme não encontrado");

        if (filme.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para editar este filme");

        if (dto.NotaUsuario.HasValue && (dto.NotaUsuario < 0 || dto.NotaUsuario > 10))
            throw new ArgumentException("A nota deve estar entre 0 e 10");

        if (dto.DuracaoMinutos.HasValue && dto.DuracaoMinutos <= 0)
            throw new ArgumentException("A duração deve ser maior que zero");

        filme.Titulo = dto.Titulo;
        filme.Descricao = dto.Descricao;
        filme.DataLancamento = dto.DataLancamento;
        filme.Diretor = dto.Diretor;
        filme.DuracaoMinutos = dto.DuracaoMinutos;
        filme.UrlImagemPoster = dto.UrlImagemPoster;
        filme.Status = (StatusMidia)dto.Status;
        filme.NotaUsuario = dto.NotaUsuario;
        filme.DataAtualizacao = DateTime.UtcNow;

        if (dto.GenerosIds.Any())
        {
            var generos = new List<Genero>();
            foreach (var generoId in dto.GenerosIds)
            {
                var genero = await _generoRepositorio.ObterPorIdAsync(generoId);
                if (genero != null)
                    generos.Add(genero);
            }
            filme.Generos = generos;
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

        await _filmeRepositorio.DeletarAsync(id);
    }

    public async Task<FilmeDto?> ObterPorIdAsync(Guid id)
    {
        var filme = await _filmeRepositorio.ObterPorIdAsync(id);
        return filme != null ? _mapper.Map<FilmeDto>(filme) : null;
    }

    public async Task<IEnumerable<FilmeDto>> ObterTodosAsync()
    {
        var filmes = await _filmeRepositorio.ObterComGenerosAsync();
        return _mapper.Map<IEnumerable<FilmeDto>>(filmes);
    }

    public async Task<IEnumerable<FilmeDto>> ObterPorStatusAsync(int status)
    {
        var filmes = await _filmeRepositorio.ObterPorStatusAsync((StatusMidia)status);
        return _mapper.Map<IEnumerable<FilmeDto>>(filmes);
    }

    public async Task<IEnumerable<FilmeDto>> BuscarPorTituloAsync(string titulo)
    {
        var filmes = await _filmeRepositorio.BuscarPorTituloAsync(titulo);
        return _mapper.Map<IEnumerable<FilmeDto>>(filmes);
    }

    public async Task<IEnumerable<FilmeDto>> ObterPorDiretorAsync(string diretor)
    {
        var filmes = await _filmeRepositorio.ObterPorDiretorAsync(diretor);
        return _mapper.Map<IEnumerable<FilmeDto>>(filmes);
    }

}
