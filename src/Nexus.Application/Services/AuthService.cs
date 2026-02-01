using System;
using Nexus.Application.DTOs.Auth;
using Nexus.Application.Interfaces;

namespace Nexus.Application.Services;

public class AuthService : IAuthService
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public AuthService(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegistrarAsync(RegistroDto dto)
    {
        var usuarioExistente = await _identityService.ObterUsuarioPorEmailAsync(dto.Email);
        if (usuarioExistente != null)
            throw new InvalidOperationException("Este email já está em uso");

        var (sucesso, erros, userId) = await _identityService.CriarUsuarioAsync(
            dto.Email, 
            dto.Senha, 
            dto.NomeCompleto);

        if (!sucesso)
        {
            var mensagensErro = string.Join(", ", erros);
            throw new InvalidOperationException($"Erro ao criar usuário: {mensagensErro}");
        }

        var usuario = await _identityService.ObterUsuarioPorIdAsync(userId);
        if (usuario == null)
            throw new InvalidOperationException("Erro ao buscar usuário criado");

        var token = _tokenService.GerarToken(usuario.Id, usuario.Email, usuario.NomeCompleto);

        return new AuthResponseDto
        {
            Token = token,
            Expiracao = DateTime.UtcNow.AddHours(24),
            Usuario = usuario
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var (sucesso, userId) = await _identityService.ValidarCredenciaisAsync(dto.Email, dto.Senha);
        if (!sucesso)
            throw new UnauthorizedAccessException("Email ou senha inválidos");

        var usuario = await _identityService.ObterUsuarioPorIdAsync(userId);
        if (usuario == null)
            throw new UnauthorizedAccessException("Usuário não encontrado");

        var token = _tokenService.GerarToken(usuario.Id, usuario.Email, usuario.NomeCompleto);

        return new AuthResponseDto
        {
            Token = token,
            Expiracao = DateTime.UtcNow.AddHours(24),
            Usuario = usuario
        };
    }
}
