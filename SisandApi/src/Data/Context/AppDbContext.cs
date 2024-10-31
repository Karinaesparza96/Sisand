using Business.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {   
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(builder);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Usuario)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                        entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                        entry.Property("DataCadastro").IsModified = false;
                    }

                    if (entry.State == EntityState.Deleted)
                    {
                        entry.Property("Excluido").CurrentValue = true;
                        entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                        entry.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
