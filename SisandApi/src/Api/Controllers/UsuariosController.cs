using System.Net;
using Api.Dtos;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosController(INotificador notificador, 
                                    IUsuarioRepository usuarioRepository,
                                    IMapper mapper,
                                    IUsuarioService usuarioService) 
        : BaseController(notificador)
    {   

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDto>> ObterPorId(int id)
        {
            var usuario = await usuarioRepository.ObterPorId(id);

            if (usuario == null)
            {
                Notificar("Usuário não existe.");
                return CustomResponse();
            }

            return mapper.Map<UsuarioDto>(usuario);
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioDto>> ObterTodos()
        {
            var usuarios = await usuarioRepository.ObterTodos();

            return mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Adicionar(UsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            await usuarioService.Adicionar(mapper.Map<Usuario>(usuarioDto));

            return CustomResponse(HttpStatusCode.Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody]UsuarioDto usuarioDto)
        {   
            if (id != usuarioDto.Id)
            {
                Notificar("Os ids informados não são iguais.");
                return CustomResponse();
            }
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            await usuarioService.Atualizar(id, mapper.Map<Usuario>(usuarioDto));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remover(int id)
        {
            await usuarioService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

    }
}
