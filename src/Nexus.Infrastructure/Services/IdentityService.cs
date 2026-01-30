using System;
using Microsoft.AspNetCore.Identity;
using Nexus.Application.DTOs.Auth;
using Nexus.Application.Interfaces;
using Nexus.Domain.Entities;
using Nexus.Infrastructure.Identity;

namespace Nexus.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(bool sucesso, string[] erros, string userId)> CriarUsuarioAsync(
        string email,
        string senha,
        string nomeCompleto
    )
    {
        var usuario = new ApplicationUser
        {
            UserName = email,
            Email = email,
            NomeCompleto = nomeCompleto,
            DataCriacao = DateTime.UtcNow
        };

        var resultado = await _userManager.CreateAsync(usuario, senha);

        if (!resultado.Succeeded)
        {
            var erros = resultado.Errors.Select(e => e.Description).ToArray();
            return(false, erros, string.Empty);
        }

        return (true, Array.Empty<string>(), usuario.Id);
    }

    public async Task<(bool sucesso, string userId)> ValidarCredenciaisAsync(
        string email,
        string senha
    )
    {
        var usuario = await _userManager.FindByEmailAsync(email);
        if(usuario == null)
            return (false, string.Empty);

        var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, senha);
        if(!senhaCorreta)
            return (false, string.Empty);

        return(true, usuario.Id);
    }

    public async Task<UsuarioDto?> ObterUsuarioPorIdAsync(string userId)
    {
        var usuario = await _userManager.FindByIdAsync(userId);
        if(usuario == null)
            return null;
        
        return new UsuarioDto
        {
            Id = usuario.Id,
            NomeCompleto = usuario.NomeCompleto,
            Email = usuario.Email!
        };
    }

    public async Task<UsuarioDto?> ObterUsuarioPorEmailAsync(string email)
    {
        var usuario = await _userManager.FindByEmailAsync(email);
        if(usuario == null)
            return null;
        
        return new UsuarioDto
        {
            Id = usuario.Id,
            NomeCompleto = usuario.NomeCompleto,
            Email = usuario.Email!
        };
    }

}
