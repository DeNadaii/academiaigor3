using academiaigor3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace academiaigor3.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Cadastro()
        {
            return View();
        }
        public ActionResult Edicao()
        {
            return View(new EmpresaModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Codigo, Nome, NomeFantasia, Cnpj")] EmpresaModel EmpresaModel)
        {
            {
                return View();
            }
        }
    }

    public class EmpresaModel
    {
    }
}


