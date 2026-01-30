using System;

namespace Nexus.Application.DTOs.Auth;

public class UsuarioDto
{
    public string Id { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
