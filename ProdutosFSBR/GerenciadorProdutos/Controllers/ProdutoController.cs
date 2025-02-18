using Microsoft.AspNetCore.Mvc;

namespace GerenciadorProdutos.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
