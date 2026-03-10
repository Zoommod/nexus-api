using System;
using FluentValidation;
using Nexus.Application.DTOs.Jogo;

namespace Nexus.Application.Validators.Jogo;

public class AtualizarJogoDtoValidator : AbstractValidator<AtualizarJogoDto>
{
    public AtualizarJogoDtoValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O titulo é obrigatório")
            .MinimumLength(2).WithMessage("O titulo deve ter no mínimo 2 caracteres")
            .MaximumLength(200).WithMessage("O titulo deve ter no máximo 200 caracteres")
            .When(x => x.Titulo != null);
        
        RuleFor(x => x.NotaUsuario).InclusiveBetween(0, 10)
            .WithMessage("A nota deve estar entre 0 e 10")
            .When(x => x.NotaUsuario.HasValue);
        
        RuleFor(x => x.DataLancamento)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de lançamento não pode ser no futuro")
            .When(x => x.DataLancamento.HasValue);
        
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status inválido")
            .When(x => x.Status.HasValue);
        
        RuleFor(x => x.Desenvolvedora)
            .MaximumLength(100).WithMessage("O nome da desenvolvedora deve ter no máximo 100 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Desenvolvedora));
    }
}
