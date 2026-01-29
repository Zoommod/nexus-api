using System;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Application.DTOs.Auth;

public class RegistroDto
{
    [Required(ErrorMessage = "O nome completo é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
    public string ConfirmarSenha { get; set; } = string.Empty;
}
