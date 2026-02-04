using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nexus.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected string ObterUsuarioId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        return userId;
    }

    protected string ObterUsuarioEmail()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        return email;
    }

    protected string ObterUsuarioNome()
    {
        var nome = User.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(nome))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        return nome;
    }

    protected void ValidarPropriedade(string usuarioIdRecurso)
    {
        var usuarioIdToken = ObterUsuarioId();

        if (usuarioIdRecurso != usuarioIdToken)
            throw new UnauthorizedAccessException("Você não tem permissão para acessar este recurso");
    }
}
