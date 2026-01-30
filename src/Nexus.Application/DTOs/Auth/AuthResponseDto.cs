using System;
using Nexus.Domain.Entities;

namespace Nexus.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
    public UsuarioDto Usuario { get; set; } = null!;
}
