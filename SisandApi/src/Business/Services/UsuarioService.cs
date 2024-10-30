using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;

namespace Business.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, 
                                INotificador notificador) 
        : BaseService(notificador), IUsuarioService
    {
        public async Task Adicionar(Usuario usuario)
        {
            if(!ValidarUsuario(new UsuarioValidation(), usuario)) return;

            var result = await usuarioRepository.Buscar(x => x.CPF == usuario.CPF);

            if (result.Any())
            {
                Notificar("Já existe um usuário com este CPF informado.");
                return;
            }

            await usuarioRepository.Adicionar(usuario);
        }

        public async Task Atualizar(int id, Usuario usuario)
        {   
            var usuarioBanco = await usuarioRepository.ObterPorId(id);

            if (usuarioBanco == null)
            {
                Notificar("Usuário não existe.");
                return;
            }

            if (!ValidarUsuario(new UsuarioValidation(), usuario)) return;

            var result = await usuarioRepository.Buscar(x => x.CPF == usuario.CPF && 
                                                             x.Id != id);
            if (result.Any())
            {
                Notificar("Já existe um usuário com este CPF informado.");
                return;
            }

            usuarioBanco.NomeCompleto = usuario.NomeCompleto;
            usuarioBanco.CPF = usuarioBanco.CPF;
            usuarioBanco.DataNascimento = usuario.DataNascimento;

            await usuarioRepository.Atualizar(usuarioBanco);
        }

        public async Task Remover(int usuarioId)
        {
            var usuario = await usuarioRepository.ObterPorId(usuarioId);

            if (usuario == null)
            {
                Notificar("Usuário não existe.");
                return;
            }

            await usuarioRepository.Remover(usuario);
        }
    }
}
