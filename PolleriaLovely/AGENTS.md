# AGENTS.md

Instrucciones permanentes para trabajar en **PolleriaLovely**, el sistema de gestión de la pollería **Lovely Chicken**.

## Stack tecnológico

- C# / ASP.NET Core **MVC** (.NET 10, `Nullable` habilitado, `ImplicitUsings` habilitado)
- **Entity Framework Core 10** (`Microsoft.EntityFrameworkCore.SqlServer` 10.0.12)
- **SQL Server**; la cadena de conexión vive en `appsettings.json` → `ConnectionStrings:DefaultConnection` y puede variar entre equipos.
- Bootstrap y jQuery incluidos en `wwwroot/lib` (no descargar librerías nuevas sin necesidad).

## Estructura del proyecto

```
PolleriaLovely/
├── Controllers/      # Administrador, Cajero, Categorias, Home, Mozo, Productos, Usuario
├── Data/             # BibliotecaDbContext e InicializadorDatos
├── Migrations/       # Migraciones EF Core
├── Models/           # Categoria, Insumo, Producto, Proveedor, Rol, Usuario, ErrorViewModel
├── ViewModels/       # ViewModels (ej. LoginViewModel)
├── Views/            # Vistas Razor
├── Properties/       # launchSettings.json
├── wwwroot/          # css, js, lib
├── Program.cs        # Servicios, autenticación, inicialización y rutas
└── appsettings.json  # Conexión a BD y logging
```

## Reglas de trabajo

1. **Nombres en español** para clases, propiedades, controladores, vistas y variables siempre que sea razonable.
2. **Respeta la arquitectura MVC** y la estructura existente; sigue el estilo de los archivos vecinos.
3. **Modifica únicamente los archivos necesarios** para cada tarea; no toques archivos fuera del alcance pedido.
4. **Avanza en etapas pequeñas**: una responsabilidad por tarea.
5. **Compila después de cada cambio de código** cuando sea posible (`dotnet build`). Debe terminar en **0 errores**. Si no se ejecutaron las pruebas, indicarlo explícitamente.
6. **No ejecutes migraciones** (`dotnet ef migrations add/...`) sin que se pida explícitamente.
7. **Explica brevemente qué modificaste y por qué** al terminar cada tarea, para facilitar el aprendizaje.
8. **No inventes requisitos** ni hagas refactorizaciones innecesarias; si falta información, pregunta antes de asumir.
9. Antes de editar, revisa los archivos actuales y la rama de trabajo. No hagas merge a `main` sin autorización explícita.

## Seguridad

- Nunca almacenar contraseñas de usuarios en texto plano: guardar únicamente el hash generado por `IPasswordHasher<Usuario>`.
- Las cuentas de prueba y sus contraseñas conocidas solo pueden usarse en entorno **Development**. No habilitar esta inicialización en producción ni reutilizar estas contraseñas.
- No commitear secretos reales; `appsettings.json` no debe contener credenciales sensibles.

## Estado actual (actualizar cada vez que se modifique el proyecto)

- **Rama de trabajo:** `sprint1-seguridad`.
- **Pull Request:** [#1 — Sprint 1: seguridad y autorización por roles](https://github.com/3ckokill/PaginaWeb---GestionPolleria/pull/1). No fusionar hasta completar las pruebas.
- **Autenticación:** `UsuarioController` verifica la contraseña con `PasswordHasher<Usuario>`, genera claims de nombre y rol y crea una cookie de autenticación. `Program.cs` configura `LoginPath = /Usuario/Login`, `AccessDeniedPath = /Usuario/AccesoDenegado` y el middleware de autenticación/autorización.
- **Protección por rol:** `ProductosController`, `CategoriasController` y `AdministradorController` requieren `Administrador`; `CajeroController` requiere `Cajero`; `MozoController` requiere `Mozo`.
- **Inicialización:** `Data/InicializadorDatos.cs` crea los roles que falten: Administrador, Cajero y Mozo.
- **Cuentas de prueba en Development:** `Program.cs` crea cada usuario solo si no existe y si encuentra su rol; las contraseñas se almacenan como hash.
  - Administrador: usuario `admin.prueba`, contraseña inicial `LovelyDemo2026!`.
  - Cajero: usuario `cajero.prueba`, contraseña inicial `LovelyCaja2026!`.
  - Mozo: usuario `mozo.prueba`, contraseña inicial `LovelyMozo2026!`.
  - Si una cuenta ya existía, el inicializador no cambia su contraseña ni su rol. Estas credenciales son solo para pruebas locales; no usar en producción.
- **Vistas:** existen vistas para login, acceso denegado y los paneles de Administrador y Cajero; Productos y Categorías tienen vistas CRUD. El menú compartido muestra Home/Privacy y, para una sesión autenticada, el usuario, el rol y el botón de cierre de sesión; aún no hay enlaces de navegación a todos los módulos.
- **Base de datos/migraciones:** existen migraciones en `Migrations/`; no se añadió ninguna migración en esta tarea.
- **Estado de validación:** el usuario confirmó manualmente que el CRUD de Categorías funciona correctamente: creación, edición, eliminación segura, conservación de `Estado`, validaciones y mejoras visuales. También confirmó que la interfaz mejorada es clara. El conflicto de `Models/Insumo.cs` fue resuelto en el equipo local. En esta corrección se revisó el flujo real de creación de Productos y se reforzó la validación de categoría tanto en navegador como en servidor.
- **Última modificación documentada:** Sprint 2, paso 2: se corrigió el guardado y la edición de Productos asignando al modelo los valores recibidos de precio y categoría, aceptando formatos decimales con punto o coma y comprobando que la categoría exista. No se añadieron migraciones.
- **Validación posterior:** `dotnet build SOL_PolleriaLovely.slnx --no-restore` debe finalizar con 0 errores. Las advertencias conocidas de `System.Security.Cryptography.Pkcs` 9.0.11 no bloquean la compilación. La prueba funcional pendiente es crear y editar un producto con una categoría válida, además de comprobar el mensaje al dejarla vacía.


