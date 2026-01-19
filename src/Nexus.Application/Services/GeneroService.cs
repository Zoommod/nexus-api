using AutoMapper;
using Nexus.Application.DTOs.Genero;
using Nexus.Application.Interfaces;
using Nexus.Domain.Entities;
using Nexus.Domain.Interfaces;

namespace Nexus.Application.Services;

public class GeneroService : IGeneroService
{
    private readonly IGeneroRepositorio _generoRepositorio;
    private readonly IMapper _mapper;

    public GeneroService(
        IGeneroRepositorio generoRepositorio,
        IMapper mapper)
    {
        _generoRepositorio = generoRepositorio;
        _mapper = mapper;
    }

    public async Task<GeneroDto> CriarAsync(CriarGeneroDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do gênero é obrigatório");

        var generoExistente = await _generoRepositorio.ObterPorNomeAsync(dto.Nome);
        if (generoExistente != null)
            throw new InvalidOperationException($"Já existe um gênero com o nome '{dto.Nome}'");

        var genero = _mapper.Map<Genero>(dto);

        genero.Id = Guid.NewGuid();
        genero.DataCriacao = DateTime.UtcNow;

        var generoSalvo = await _generoRepositorio.AdicionarAsync(genero);

        return _mapper.Map<GeneroDto>(generoSalvo);
    }

    public async Task<GeneroDto> AtualizarAsync(Guid id, AtualizarGeneroDto dto)
    {
        var genero = await _generoRepositorio.ObterPorIdAsync(id);
        if (genero == null)
            throw new KeyNotFoundException("Gênero não encontrado");

        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do gênero é obrigatório");

        var generoComMesmoNome = await _generoRepositorio.ObterPorNomeAsync(dto.Nome);
        if (generoComMesmoNome != null && generoComMesmoNome.Id != id)
            throw new InvalidOperationException($"Já existe outro gênero com o nome '{dto.Nome}'");

        genero.Nome = dto.Nome;
        genero.Descricao = dto.Descricao;
        genero.DataAtualizacao = DateTime.UtcNow;

        await _generoRepositorio.AtualizarAsync(genero);

        return _mapper.Map<GeneroDto>(genero);
    }

    public async Task DeletarAsync(Guid id)
    {
        var genero = await _generoRepositorio.ObterPorIdAsync(id);
        if (genero == null)
            throw new KeyNotFoundException("Gênero não encontrado");
            
        await _generoRepositorio.DeletarAsync(id);
    }

    public async Task<GeneroDto?> ObterPorIdAsync(Guid id)
    {
        var genero = await _generoRepositorio.ObterPorIdAsync(id);
        return genero != null ? _mapper.Map<GeneroDto>(genero) : null;
    }

    public async Task<IEnumerable<GeneroDto>> ObterTodosAsync()
    {
        var generos = await _generoRepositorio.ObterTodosAsync();
        return _mapper.Map<IEnumerable<GeneroDto>>(generos);
    }

    public async Task<GeneroDto?> ObterPorNomeAsync(string nome)
    {
        var genero = await _generoRepositorio.ObterPorNomeAsync(nome);
        return genero != null ? _mapper.Map<GeneroDto>(genero) : null;
    }
}
