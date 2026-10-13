using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolleriaLovely.Models;
using PolleriaLovely.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PolleriaLovely.Controllers
{
    public class ProductosController : Controller
    {
        [Authorize(Roles = "Administrador")]
        private readonly BibliotecaDbContext _context;

        public ProductosController(BibliotecaDbContext context)
        {
            _context = context;
        }

        // GET: PRODUCTOS
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: PRODUCTOS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: PRODUCTOS/Create
        public IActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(
                _context.Categorias,
                "IdCategoria",
                "NombreCategoria");
            return View();
        }

        // POST: PRODUCTOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreProducto,Descripcion,Precio,Estado,IdCategoria")] Producto producto)
        {

            if (ModelState.IsValid)
            {

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.IdCategoria = new SelectList(
                _context.Categorias,
                "IdCategoria",
                "NombreCategoria",
                producto.IdCategoria);

            return View(producto);
        }

        // GET: PRODUCTOS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            ViewBag.IdCategoria = new SelectList(
                _context.Categorias,
                "IdCategoria",
                "NombreCategoria",
                producto.IdCategoria);

            return View(producto);
        }

        // POST: PRODUCTOS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("IdProducto,NombreProducto,Descripcion,Precio,Estado,IdCategoria")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

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
                    if (!ProductoExists(producto.IdProducto))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            ViewBag.IdCategoria = new SelectList(
                _context.Categorias,
                "IdCategoria",
                "NombreCategoria",
                producto.IdCategoria);

            return View(producto);
        }

        // GET: PRODUCTOS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: PRODUCTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int? idproduto)
        {
            return _context.Productos.Any(e => e.IdProducto == idproduto);
        }
    }
}