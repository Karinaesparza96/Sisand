using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class UsuarioRepository(AppDbContext db) : IUsuarioRepository
    {
        public async Task<Usuario?> ObterPorId(int id)
        {
            return await db.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await db.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task Adicionar(Usuario usuario)
        {
            db.Add(usuario);
            await db.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            db.Usuarios.Update(usuario);
            await db.SaveChangesAsync();
        }
        public async Task Remover(Usuario usuario)
        {
            db.Usuarios.Remove(usuario);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> expression)
        {
            return await db.Usuarios.Where(expression).ToListAsync();
        }
    }
}
