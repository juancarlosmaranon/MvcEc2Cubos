using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcEc2Cubos.Data;
using MvcEc2Cubos.Models;

namespace MvcEc2Cubos.Controllers
{
    public class CubosController : Controller
    {
        private readonly CubosContext _context;

        public CubosController(CubosContext context)
        {
            _context = context;
        }

        // GET: Cuboes
        public async Task<IActionResult> Index()
        {
              return _context.Cubo != null ? 
                          View(await _context.Cubo.ToListAsync()) :
                          Problem("Entity set 'CubosContext.Cubo'  is null.");
        }

        // GET: Cuboes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cubo == null)
            {
                return NotFound();
            }

            var cubo = await _context.Cubo
                .FirstOrDefaultAsync(m => m.IdCubo == id);
            if (cubo == null)
            {
                return NotFound();
            }

            return View(cubo);
        }

        // GET: Cuboes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuboes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCubo,Nombre,Marca,Imagen,Precio")] Cubo cubo)
        {
            if (ModelState.IsValid)
            {
                cubo.IdCubo = await GetMax();
                _context.Add(cubo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cubo);
        }

        public async Task<int> GetMax()
        {
            return await _context.Cubo.MaxAsync(x => x.IdCubo) + 1;
        }

        // GET: Cuboes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cubo == null)
            {
                return NotFound();
            }

            var cubo = await _context.Cubo.FindAsync(id);
            if (cubo == null)
            {
                return NotFound();
            }
            return View(cubo);
        }

        // POST: Cuboes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCubo,Nombre,Marca,Imagen,Precio")] Cubo cubo)
        {
            if (id != cubo.IdCubo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cubo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuboExists(cubo.IdCubo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cubo);
        }

        // GET: Cuboes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cubo == null)
            {
                return NotFound();
            }

            var cubo = await _context.Cubo
                .FirstOrDefaultAsync(m => m.IdCubo == id);
            if (cubo == null)
            {
                return NotFound();
            }

            return View(cubo);
        }

        // POST: Cuboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cubo == null)
            {
                return Problem("Entity set 'CubosContext.Cubo'  is null.");
            }
            var cubo = await _context.Cubo.FindAsync(id);
            if (cubo != null)
            {
                _context.Cubo.Remove(cubo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuboExists(int id)
        {
          return (_context.Cubo?.Any(e => e.IdCubo == id)).GetValueOrDefault();
        }
    }
}
