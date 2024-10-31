using Business.Models;
using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Data.Seed
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelper.EnsureSeedData(app).Wait();
        }
    }
    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication application)
        {
            var services = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();

                await EnsureSeedTables(context);
            }
        }

        private static async Task EnsureSeedTables(AppDbContext context)
        {
            if (context.Usuarios.Any()) return;

            var usuario = new Usuario
            {
                Id = 1,
                CPF = "21739759001",
                NomeCompleto = "Fulano",
                DataNascimento = DateTime.Parse("1996-10-31")
            };

            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
        }
    }
}
