# AGENTS.md

Instrucciones permanentes para trabajar en **PolleriaLovely**, el sistema de gestión de la pollería **Lovely Chicken**.

## Stack tecnológico

- C# / ASP.NET Core **MVC** (.NET 10, `Nullable` habilitado, `ImplicitUsings` habilitado)
- **Entity Framework Core 10** (`Microsoft.EntityFrameworkCore.SqlServer` 10.0.12)
- **SQL Server**: instancia `.\SQLEXPRESS01`, base de datos `PolleriaLovely`
- La cadena de conexión vive en `appsettings.json` → `ConnectionStrings:DefaultConnection`
- Bootstrap y jQuery incluidos en `wwwroot/lib` (no descargar librerías nuevas sin necesidad)

## Estructura del proyecto

```
PolleriaLovely/
├── Controllers/      # Administrador, Cajero, Home, Mozo, Usuario
├── Data/             # BibliotecaDbContext (nombre heredado de la plantilla)
├── Migrations/       # Migraciones EF Core
├── Models/           # Entidades: Categoria, Insumo, Producto, Proveedor, Rol, Usuario, ErrorViewModel
├── ViewModels/       # ViewModels (ej. LoginViewModel)
├── Views/            # Vistas Razor (Home, Shared)
├── Properties/       # launchSettings.json (puertos 56587/56588)
├── wwwroot/          # css, js, lib
├── Program.cs        # Registro de servicios y rutas
└── appsettings.json  # Conexión a BD y logging
```

## Reglas de trabajo

1. **Nombres en español** para clases, propiedades, controladores, vistas y variables siempre que sea razonable (patrón del proyecto: `NombreProducto`, `IdCategoria`, `Estado`, etc.).
2. **Respeta la arquitectura MVC** y la estructura existente; sigue el estilo de los archivos vecinos (atributos `[Key, DatabaseGenerated(Identity)]`, `[Required, StringLength(n)]`, configuración fluent API en `OnModelCreating`).
3. **Modifica únicamente los archivos necesarios** para cada tarea; no toques archivos fuera del alcance pedido.
4. **Avanza en etapas pequeñas**: una responsabilidad por tarea (models, relación, migración, controlador, vista…).
5. **Compila después de cada cambio de código** cuando sea posible (`dotnet build`; en este entorno WSL: `"/mnt/c/Program Files/dotnet/dotnet.exe" build`). Debe terminar en **0 errores**. Las advertencias CS8618 (propiedades no nulas sin inicializar) ya son un patrón existente del proyecto.
6. **No ejecutes migraciones (`dotnet ef migrations add/...`) sin que se pida explícitamente**; cuando se pidan, una sola migración por cambio de modelo.
7. **Explica brevemente qué modificaste y por qué** al terminar cada tarea, para facilitar el aprendizaje.
8. **No inventes requisitos** ni hagas refactorizaciones innecesarias; si falta información, pregunta antes de asumir.
9. **No asumas que se conoce el proyecto APPLOGIN del docente.** Si hay que seguir detalles de sus guías, primero revisa los archivos disponibles del repositorio antes de decidir.

## Seguridad

- **Nunca almacenar contraseñas en texto plano**: guardar solo hash (implementación pendiente en la etapa de autenticación).
- No commitear secretos; `appsettings.json` solo debe contener valores no sensibles (autenticación de Windows / `Trusted_Connection`).

## Orden general de trabajo

Cuando corresponda, seguir este orden:

1. **Models** (entidades con atributos de validación)
2. **DbContext y relaciones** (`Data/BibliotecaDbContext.cs`: DbSets + fluent API)
3. **Configuración** (`appsettings.json`, `Program.cs`)
4. **Migraciones** (solo cuando se solicite)
5. **ViewModels** (con validaciones `[Required]` etc.)
6. **Controllers**
7. **Views**
8. **Pruebas**

## Estado actual (última revisión)

- Modelos `Rol` y `Usuario` completos, relacionados 1‑a‑muchos mediante `IdRol` (FK) configurado en `BibliotecaDbContext`.
- DbSets `Roles` y `Usuarios` registrados; migraciones `mg1` (vacía) y `mg2` (tablas `Roles`, `Usuarios`) presentes en `Migrations/`.
- `ViewModels/LoginViewModel.cs` creado con `NombreUsuario` y `Contraseña` (`[Required]`).
- Pendiente del flujo de autenticación: hash de contraseñas, controlador/login de acceso y vistas.
- Dato a alinear: `Usuario.Contrasena` (sin ñ) vs `LoginViewModel.Contraseña` (con ñ).
