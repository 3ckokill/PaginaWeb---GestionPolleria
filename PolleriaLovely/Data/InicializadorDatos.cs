using Microsoft.EntityFrameworkCore;
using PolleriaLovely.Models;

namespace PolleriaLovely.Data
{
    public static class InicializadorDatos
    {
        public static async Task InicializarAsync(
            BibliotecaDbContext context)
        {
            var rolesIniciales = new[]
            {
                new Rol
                {
                    NombreRol = "Administrador",
                    Descripcion = "Gestiona el sistema y sus usuarios.",
                    Estado = true
                },
                new Rol
                {
                    NombreRol = "Cajero",
                    Descripcion = "Gestiona las operaciones de caja.",
                    Estado = true
                },
                new Rol
                {
                    NombreRol = "Mozo",
                    Descripcion = "Atiende a los clientes y registra pedidos.",
                    Estado = true
                }
            };

            foreach (var rolInicial in rolesIniciales)
            {
                bool existe = await context.Roles
                    .AnyAsync(r =>
                        r.NombreRol == rolInicial.NombreRol);

                if (!existe)
                {
                    context.Roles.Add(rolInicial);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}