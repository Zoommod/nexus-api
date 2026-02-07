using AutoMapper;
using Nexus.Application.DTOs.Avaliacao;
using Nexus.Application.Interfaces;
using Nexus.Domain.Entities;
using Nexus.Domain.Interfaces;

namespace Nexus.Application.Services;

public class AvaliacaoService : IAvaliacaoService
{
    private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;
    private readonly IJogoRepositorio _jogoRepositorio;
    private readonly IFilmeRepositorio _filmeRepositorio;
    private readonly IMapper _mapper;

    public AvaliacaoService(
        IAvaliacaoRepositorio avaliacaoRepositorio,
        IJogoRepositorio jogoRepositorio,
        IFilmeRepositorio filmeRepositorio,
        IMapper mapper)
    {
        _avaliacaoRepositorio = avaliacaoRepositorio;
        _jogoRepositorio = jogoRepositorio;
        _filmeRepositorio = filmeRepositorio;
        _mapper = mapper;
    }

    public async Task<AvaliacaoDto> CriarAsync(CriarAvaliacaoDto dto, string usuarioId)
    {
        if (dto.Nota < 0 || dto.Nota > 10)
            throw new ArgumentException("A nota deve estar entre 0 e 10");

        if (string.IsNullOrWhiteSpace(dto.Conteudo))
            throw new ArgumentException("O conteúdo da avaliação é obrigatório");

        if (dto.JogoId.HasValue && dto.FilmeId.HasValue)
            throw new ArgumentException("Uma avaliação não pode ser de um jogo E um filme ao mesmo tempo");

        if (!dto.JogoId.HasValue && !dto.FilmeId.HasValue)
            throw new ArgumentException("Uma avaliação deve ser de um jogo OU de um filme");

        if (dto.JogoId.HasValue)
        {
            var jogo = await _jogoRepositorio.ObterPorIdAsync(dto.JogoId.Value);
            if (jogo == null)
                throw new KeyNotFoundException("Jogo não encontrado");
            
            if(jogo.UsuarioId != usuarioId)
                throw new UnauthorizedAccessException("Você não pode avaliar jogos de outros usuários");
        }


        if (dto.FilmeId.HasValue)
        {
            var filme = await _filmeRepositorio.ObterPorIdAsync(dto.FilmeId.Value);
            if (filme == null)
                throw new KeyNotFoundException("Filme não encontrado");
            
            if(filme.UsuarioId != usuarioId)
                throw new UnauthorizedAccessException("Você não pode avaliar filmes de outros usuários");
        }

        var avaliacao = _mapper.Map<Avaliacao>(dto);

        avaliacao.Id = Guid.NewGuid();
        avaliacao.DataCriacao = DateTime.UtcNow;
        avaliacao.UsuarioId = usuarioId;

        var avaliacaoSalva = await _avaliacaoRepositorio.AdicionarAsync(avaliacao);

        var avaliacaoCompleta = await _avaliacaoRepositorio.ObterComDetalhesAsync(avaliacaoSalva.Id);

        return _mapper.Map<AvaliacaoDto>(avaliacaoCompleta);
    }

    public async Task<AvaliacaoDto> AtualizarAsync(Guid id, AtualizarAvaliacaoDto dto, string usuarioId)
    {
        var avaliacao = await _avaliacaoRepositorio.ObterPorIdAsync(id);
        if (avaliacao == null)
            throw new KeyNotFoundException("Avaliação não encontrada");

        if (avaliacao.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para editar esta avaliação");

        if (dto.Nota < 0 || dto.Nota > 10)
            throw new ArgumentException("A nota deve estar entre 0 e 10");

        if (string.IsNullOrWhiteSpace(dto.Conteudo))
            throw new ArgumentException("O conteúdo da avaliação é obrigatório");

        avaliacao.Conteudo = dto.Conteudo;
        avaliacao.Nota = dto.Nota;
        avaliacao.DataAtualizacao = DateTime.UtcNow;

        await _avaliacaoRepositorio.AtualizarAsync(avaliacao);

        var avaliacaoCompleta = await _avaliacaoRepositorio.ObterComDetalhesAsync(avaliacao.Id);

        return _mapper.Map<AvaliacaoDto>(avaliacaoCompleta);
    }

    public async Task DeletarAsync(Guid id, string usuarioId)
    {
        var avaliacao = await _avaliacaoRepositorio.ObterPorIdAsync(id);
        if (avaliacao == null)
            throw new KeyNotFoundException("Avaliação não encontrada");

        if (avaliacao.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para deletar esta avaliação");

        await _avaliacaoRepositorio.DeletarAsync(id);
    }

    public async Task<AvaliacaoDto?> ObterPorIdAsync(Guid id)
    {
        var avaliacao = await _avaliacaoRepositorio.ObterComDetalhesAsync(id);
        return avaliacao != null ? _mapper.Map<AvaliacaoDto>(avaliacao) : null;
    }

    public async Task<IEnumerable<AvaliacaoDto>> ObterPorJogoAsync(Guid jogoId)
    {
        var avaliacoes = await _avaliacaoRepositorio.ObterPorJogoAsync(jogoId);
        return _mapper.Map<IEnumerable<AvaliacaoDto>>(avaliacoes);
    }

    public async Task<IEnumerable<AvaliacaoDto>> ObterPorFilmeAsync(Guid filmeId)
    {
        var avaliacoes = await _avaliacaoRepositorio.ObterPorFilmeAsync(filmeId);
        return _mapper.Map<IEnumerable<AvaliacaoDto>>(avaliacoes);
    }

    public async Task<IEnumerable<AvaliacaoDto>> ObterPorUsuarioAsync(string usuarioId)
    {
        var avaliacoes = await _avaliacaoRepositorio.ObterPorUsuarioAsync(usuarioId);
        return _mapper.Map<IEnumerable<AvaliacaoDto>>(avaliacoes);
    }
}
