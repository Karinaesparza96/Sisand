using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;
using Business.Notificacoes;
using FluentValidation.Results;

namespace Business.Services
{
    public abstract class BaseService(INotificador notificador)
    {
        protected bool ValidarUsuario(UsuarioValidation validator, Usuario entity)
        {
            var validationResult = validator.Validate(entity);

            if (validationResult.IsValid) return true;

            Notificar(validationResult);

            return false;

        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                notificador.Adicionar(new Notificacao(error.ErrorMessage));
            }
        }

        protected void Notificar(string mensagem)
        {
            notificador.Adicionar(new Notificacao(mensagem));
        }
    }
}
