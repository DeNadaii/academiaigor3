using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcademiaIgor.Models;
using academiaigor3.Data;


namespace academiaigor3.Controllers
{
    public class EmpresaModelsController : Controller
    {
        private readonly academiaigor3Context _context;

        public EmpresaModelsController(academiaigor3Context context)
        {
            _context = context;
        }

        // GET: EmpresaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpresaModels.ToListAsync());
        }

        // GET: EmpresaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaModels = await _context.EmpresaModels
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (empresaModels == null)
            {
                return NotFound();
            }

            return View(empresaModels);
        }

        // GET: EmpresaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpresaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,NomeFantasia,Cnpj")] EmpresaModels empresaModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresaModels);
        }

        // GET: EmpresaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaModels = await _context.EmpresaModels.FindAsync(id);
            if (empresaModels == null)
            {
                return NotFound();
            }
            return View(empresaModels);
        }

        // POST: EmpresaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,NomeFantasia,Cnpj")] EmpresaModels empresaModels)
        {
            if (id != empresaModels.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaModelsExists(empresaModels.Codigo))
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
            return View(empresaModels);
        }

        // GET: EmpresaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaModels = await _context.EmpresaModels
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (empresaModels == null)
            {
                return NotFound();
            }

            return View(empresaModels);
        }

        // POST: EmpresaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresaModels = await _context.EmpresaModels.FindAsync(id);
            _context.EmpresaModels.Remove(empresaModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaModelsExists(int id)
        {
            return _context.EmpresaModels.Any(e => e.Codigo == id);
        }
    }
}
