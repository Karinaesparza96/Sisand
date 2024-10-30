using Business.Interfaces;
using Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    public abstract class BaseController(INotificador notificador) : ControllerBase
    {
        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
        {
            if (!notificador.TemNotificacao())
            {
                return new ObjectResult(result)
                {
                    StatusCode = (int)statusCode
                };
            }
            return BadRequest(new
            {
                errors = notificador.ObterNotificacoes().Select(x => x.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                Notificar(modelState);
            }

            return CustomResponse();
        }

        protected void Notificar(string mensagem)
        {
            notificador.Adicionar(new Notificacao(mensagem));
        }

        protected void Notificar(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);

            foreach (var erro in erros)
            {
                var msg = erro.Exception != null ? erro.Exception.Message : erro.ErrorMessage;

                Notificar(msg);
            }
        }

    }
}
