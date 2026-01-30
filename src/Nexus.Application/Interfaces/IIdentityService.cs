using System;
using Nexus.Application.DTOs.Auth;

namespace Nexus.Application.Interfaces;

public interface IIdentityService
{
    Task<(bool sucesso, string[] erros, string userId)> CriarUsuarioAsync(
        string email,
        string senha,
        string nomeCompleto);
    
    Task<(bool sucesso, string userId)> ValidarCredenciaisAsync(
        string email,
        string senha);

    Task<UsuarioDto?> ObterUsuarioPorIdAsync(string userId);
    Task<UsuarioDto?> ObterUsuarioPorEmailAsync(string email);
}
