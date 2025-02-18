
using Moq;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using GerenciadorProdutos.Controllers;
using Domain.Repositories;

public class ProdutoControllerTests
{
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
    private readonly ProdutoService _produtoService;
    private readonly ProdutoController _produtoController;

    public ProdutoControllerTests()
    {
        _produtoRepositoryMock = new Mock<IProdutoRepository>();
        _produtoService = new ProdutoService(_produtoRepositoryMock.Object);
        _produtoController = new ProdutoController(_produtoService);
    }

    [Fact]
    public async Task Index_DeveRetornarViewComListaProdutos()
    {
        
        var produtosFake = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto A" },
            new Produto { Id = 2, Nome = "Produto B" }
        };

        _produtoRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(produtosFake);

       
        var resultado = await _produtoController.Index() as ViewResult;

        
        Assert.NotNull(resultado);
        var model = resultado.Model as List<Produto>;
        Assert.Equal(2, model.Count);
    }


[Fact]
    public async Task Edit_DeveRetornarNotFoundSeProdutoNaoExistir()
    {
        
        var resultado = await _produtoController.Edit(999);

      
        Assert.IsType<NotFoundResult>(resultado);
    }
}
