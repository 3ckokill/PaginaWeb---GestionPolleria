using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolleriaLovely.Models;
using PolleriaLovely.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PolleriaLovely.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProductosController : Controller
    {
        private readonly BibliotecaDbContext _context;

        public ProductosController(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.Include(p => p.Categoria).AsNoTracking().OrderBy(p => p.NombreProducto).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var producto = await _context.Productos
                .Include(p => p.Categoria).FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.IdCategoria = new SelectList(await _context.Categorias.AsNoTracking().OrderBy(c => c.NombreCategoria).ToListAsync(), "IdCategoria", "NombreCategoria");
            return View(new Producto { Estado = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreProducto,Descripcion,Precio,Estado,IdCategoria")] Producto producto)
        {
            NormalizarErrorPrecio();
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IdCategoria = new SelectList(await _context.Categorias.AsNoTracking().OrderBy(c => c.NombreCategoria).ToListAsync(), "IdCategoria", "NombreCategoria", producto.IdCategoria);
            return View(producto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            ViewBag.IdCategoria = new SelectList(await _context.Categorias.AsNoTracking().OrderBy(c => c.NombreCategoria).ToListAsync(), "IdCategoria", "NombreCategoria", producto.IdCategoria);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,NombreProducto,Descripcion,Precio,Estado,IdCategoria")] Producto producto)
        {
            if (id != producto.IdProducto) return NotFound();
            NormalizarErrorPrecio();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto)) return NotFound();
                    throw;
                }
            }
            ViewBag.IdCategoria = new SelectList(await _context.Categorias.AsNoTracking().OrderBy(c => c.NombreCategoria).ToListAsync(), "IdCategoria", "NombreCategoria", producto.IdCategoria);
            return View(producto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var producto = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null) _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int? idproduto) =>
            _context.Productos.Any(e => e.IdProducto == idproduto);

        private void NormalizarErrorPrecio()
        {
            if (!ModelState.ContainsKey(nameof(Producto.Precio))) return;
            ModelState.Remove(nameof(Producto.Precio));
            if (Request.Form.TryGetValue(nameof(Producto.Precio), out var valor) &&
                decimal.TryParse(valor, out var precio) && precio >= 0.01m && precio <= 1000000m)
            {
                ModelState.SetModelValue(nameof(Producto.Precio), new Microsoft.AspNetCore.Mvc.ModelBinding.ValueProviderResult(valor));
                return;
            }
            ModelState.AddModelError(nameof(Producto.Precio), "Ingrese un precio válido mayor que cero.");
        }
    }
}
