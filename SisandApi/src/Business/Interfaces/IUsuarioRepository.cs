using System.Linq.Expressions;
using Business.Models;

namespace Business.Interfaces
{
    public interface IUsuarioRepository
    {
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task<Usuario?> ObterPorId(int usuarioId);

        Task<IEnumerable<Usuario>> ObterTodos();

        Task Remover(Usuario usuarioId);

        Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> expression);

    }
}
