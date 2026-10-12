using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolleriaLovely.Data;
using PolleriaLovely.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPasswordHasher<Usuario>,
    PasswordHasher<Usuario>>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opciones =>
    {
        opciones.LoginPath = "/Usuario/Login";
        opciones.AccessDeniedPath = "/Usuario/Login";
        opciones.Cookie.HttpOnly = true;
        opciones.Cookie.IsEssential = true;
        opciones.SlidingExpiration = true;
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider
        .GetRequiredService<BibliotecaDbContext>();

    var hasher = scope.ServiceProvider
        .GetRequiredService<IPasswordHasher<Usuario>>();

    var existeUsuario = await context.Usuarios
        .AnyAsync(u => u.NombreUsuario == "admin.prueba");

    if (!existeUsuario)
    {
        var rol = await context.Roles
            .FirstOrDefaultAsync(r => r.NombreRol == "Administrador");

        if (rol != null)
        {
            var usuario = new Usuario
            {
                NombreUsuario = "admin.prueba",
                Nombre = "Administrador",
                Apellido = "Prueba",
                Correo = "admin.prueba@lovely.local",
                Telefono = "999999999",
                Estado = true,
                IdRol = rol.IdRol
            };

            usuario.Contrasena = hasher.HashPassword(
                usuario,
                "LovelyDemo2026!"
            );

            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
