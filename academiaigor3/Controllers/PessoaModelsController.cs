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
    public class PessoaModelsController : Controller
    {
        private readonly academiaigor3Context _context;

        public PessoaModelsController(academiaigor3Context context)
        {
            _context = context;
        }

        // GET: PessoaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: PessoaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: PessoaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //fução q retorna true ou false e diz se o email existe ou não no banco
        public bool PessoaEmailExiste(string Email, PessoaModel pessoaModel)
        {
            return _context.PessoaModel.Any(e => e.Email.Equals(Email) && e.Codigo != pessoaModel.Codigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Email,DataDeNascimento,QuantideDeFilhos,Salario")] PessoaModel pessoaModel)
        {

            var pessoaEmail = PessoaEmailExiste(pessoaModel.Email, pessoaModel);
            if (pessoaModel.Salario > 13000)
            {
                ModelState.AddModelError("", "Salario inválido");
                return View();
            }
            
            if (pessoaModel.Salario < 1200)
            {
                ModelState.AddModelError("", "Salario inválido");
                return View();
            }
            if (pessoaModel.QuantideDeFilhos < 0)
            {
                ModelState.AddModelError("", "numero de filhos inválido");
                return View();
            }
            if (pessoaModel.DataDeNascimento <= DateTime.Parse("01/01/1990").Date)
            {
                ModelState.AddModelError("", "Data de nascimento invalida");
                return View();
            }
            if (pessoaEmail)
            {
                ModelState.AddModelError("", "O Email já está cadastrado!");
                return View();
            }
            pessoaModel.situação = "Ativo";
            _context.Add(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: PessoaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: PessoaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Email,DataDeNascimento,QuantideDeFilhos,Salario,situação")] PessoaModel pessoaModel)
        {


            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }
            if (pessoaModel.QuantideDeFilhos < 0)
            {
                ModelState.AddModelError("", "numero de filhos inválido");
                return View();
            }
            if (pessoaModel.DataDeNascimento <= DateTime.Parse("01/01/1990").Date)
            {
                ModelState.AddModelError("", "Data de nascimento invalida");
                return View();
            }

            if (ModelState.IsValid)
            {
                var pessoaEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);
                if (pessoaEmail.Count() > 0)
                {
                    ModelState.AddModelError("O Email já está cadastrado!", "");
                    return View(pessoaModel);
                }
                try
                {
                    _context.Update(pessoaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaModelExists(pessoaModel.Codigo))
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
            return View(pessoaModel);



        }

        //logica pessoa pessoa ativa e desativada 
        [HttpGet]
        public async Task<IActionResult> AlterarStatus(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);

            if (pessoaModel.situação.Equals("Ativo"))
            {
                pessoaModel.situação = "Desativado";
            }
            else
            {
                pessoaModel.situação = "Ativo";
            }
            _context.Update(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: PessoaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: PessoaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel.situação.Equals("Desativado"))
            {
                _context.PessoaModel.Remove(pessoaModel);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Codigo == id);
        }
    }
}
