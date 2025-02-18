using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProdutoService
    {
       private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<IEnumerable<Produto>> ListarProdutosAsync()
            => await _produtoRepository.GetAllAsync();

        public async Task<Produto?> ObterProdutoPorIdAsync(int id) 
            => await _produtoRepository.GetByIdAsync(id);

        public async Task AdicionarProdutoAsync(Produto produto)
            => await _produtoRepository.AddAsync(produto);

        public async Task AtualizarProdutoAsync(Produto produto)
            => await _produtoRepository.UpdateAsync(produto);

        public async Task ExcluirProdutoAsync(int id)
            => await _produtoRepository.DeleteAsync(id);
    }
}
