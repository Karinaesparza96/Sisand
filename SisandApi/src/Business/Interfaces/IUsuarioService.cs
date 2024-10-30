using Business.Models;

namespace Business.Interfaces
{
    public interface IUsuarioService
    {
        Task Adicionar(Usuario usuario);

        Task Atualizar(int id, Usuario usuario);

        Task Remover(int usuarioId);
    }
}
