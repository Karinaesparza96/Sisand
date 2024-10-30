using Business.Models.Validations.Documentos;
using FluentValidation;

namespace Business.Models.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(a => a.CPF!.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("O campo CPF precisa ter {ComparisonValue} e foi fornecido {PropertyValue} caracteres.");

            RuleFor(a => CpfValidacao.Validar(a.CPF!)).Equal(true)
                .WithMessage("O CPF fornecido é inválido.");

            RuleFor(a => a.NomeCompleto)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(a => a.DataNascimento)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Must(date => date < DateTime.Now)
                .WithMessage("O campo {PropertyName} deve ser válida.");
        }
    }
}
