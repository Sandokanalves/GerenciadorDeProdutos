using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;

public class ProdutoServiceTests
{
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
    private readonly ProdutoService _produtoService;

    public ProdutoServiceTests()
    {
        _produtoRepositoryMock = new Mock<IProdutoRepository>();
        _produtoService = new ProdutoService(_produtoRepositoryMock.Object);
    }

    [Fact]
    public async Task ListarProdutosAsync_DeveRetornarListaProdutos()
    {
        // Arrange: Criamos uma lista simulada de produtos
        var produtosFake = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto A", QuantidadeEstoque = 10 },
            new Produto { Id = 2, Nome = "Produto B", QuantidadeEstoque = 5 }
        };

        _produtoRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(produtosFake);

        // Act: Chamamos o método a ser testado
        var resultado = await _produtoService.ListarProdutosAsync();

        // Assert: Verificamos se o resultado contém os produtos esperados
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count());
        Assert.Contains(resultado, p => p.Nome == "Produto A");
    }

    [Fact]
    public async Task ObterProdutoPorIdAsync_DeveRetornarProduto_QuandoExiste()
    {
        // Arrange
        var produtoFake = new Produto { Id = 1, Nome = "Produto Teste", QuantidadeEstoque = 10 };

        _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(produtoFake);

        // Act
        var resultado = await _produtoService.ObterProdutoPorIdAsync(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Produto Teste", resultado.Nome);
    }

    [Fact]
    public async Task ObterProdutoPorIdAsync_DeveRetornarNulo_QuandoNaoExiste()
    {
        // Arrange
        _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Produto)null);

        // Act
        var resultado = await _produtoService.ObterProdutoPorIdAsync(99);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public async Task AdicionarProdutoAsync_DeveAdicionarProduto()
    {
        // Arrange
        var produtoNovo = new Produto { Nome = "Novo Produto", QuantidadeEstoque = 15 };

        _produtoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Produto>()))
            .Returns(Task.CompletedTask);

        // Act
        await _produtoService.AdicionarProdutoAsync(produtoNovo);

        // Assert
        _produtoRepositoryMock.Verify(repo => repo.AddAsync(produtoNovo), Times.Once);
    }

    [Fact]
    public async Task ExcluirProdutoAsync_DeveRemoverProduto()
    {
        // Arrange
        var produtoExistente = new Produto { Id = 1, Nome = "Produto Removido" };

        _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(produtoExistente);
        _produtoRepositoryMock.Setup(repo => repo.DeleteAsync(1))
            .Returns(Task.CompletedTask);

        // Act
        await _produtoService.ExcluirProdutoAsync(1);

        // Assert
        _produtoRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}
