using System;
using Microsoft.AspNetCore.Identity;

namespace Nexus.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string NomeCompleto { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
