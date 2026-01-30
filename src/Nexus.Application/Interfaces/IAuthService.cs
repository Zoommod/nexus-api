using System;
using Nexus.Application.DTOs.Auth;

namespace Nexus.Application.Interfaces;

public interface IAuthService
{
     Task<AuthResponseDto> RegistrarAsync(RegistroDto dto);
     Task<AuthResponseDto> LoginAsync(LoginDto dto);
}
