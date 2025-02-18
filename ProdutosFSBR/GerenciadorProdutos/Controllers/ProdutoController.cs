using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorProdutos.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        
        public async  Task<IActionResult> Index()
        {
            var produtos = await _produtoService.ListarProdutosAsync();
            if (produtos == null || !produtos.Any())  
            {
                produtos = new List<Produto>();  
            }
            return View(produtos);
        }

        
        public IActionResult Details(int id)
        {
            var produto = _produtoService.ObterProdutoPorIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoService.AdicionarProdutoAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _produtoService.ObterProdutoPorIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _produtoService.AtualizarProdutoAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoService.ObterProdutoPorIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _produtoService.ObterProdutoPorIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            if (produto.QuantidadeEstoque > 0)
            {
                TempData["Erro"] = "Não é possível remover um produto que ainda tem estoque.";
                return RedirectToAction("Delete", new { id });
            }

           await _produtoService.ExcluirProdutoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
