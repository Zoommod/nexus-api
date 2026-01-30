using System;

namespace Nexus.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(string userId, string email, string nomeCompleto);

}
