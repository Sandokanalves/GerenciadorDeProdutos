
# ***FSBR: Desafio CRUD de Produtos com ASP.NET Core MVC***
# **Documentação do Projeto - Gerenciador de Produtos**

## **1. Visão Geral do Projeto**

O **Gerenciador de Produtos** é um sistema desenvolvido em **ASP.NET Core MVC** seguindo os princípios da **Arquitetura Limpa** para garantir separação de responsabilidades, fácil manutenção e testabilidade.

## **2. Tecnologias Utilizadas**

- **ASP.NET Core MVC**
- **Entity Framework Core 7.0**
- **SQL Server**
- **xUnit (para testes unitários)**
- **FluentAssertions (para asserções mais legíveis nos testes)**

## **3. Estrutura do Projeto (Arquitetura Limpa)**

O projeto é baseado na **Arquitetura Limpa**, separando as responsabilidades em diferentes camadas:

- **Domain (Domínio)**: Contém as entidades do sistema, que representam os dados e as regras de negócio.
- **Application (Aplicação)**: Implementa os casos de uso do sistema, como serviços e lógica de negócio.
- **Infrastructure (Infraestrutura)**: Contém implementações de persistência (Repositories, configurações do banco de dados).
- **GerenciadorProdutos (MVC)**: Implementação do ASP.NET Core MVC (Controllers e Views para interação com o usuário).

### **Principais Componentes**

- **Entities (Entidades)**: Representam os modelos de dados, como `Produto`.
- **Repository (Repositório)**: Responsável pela comunicação com o banco de dados.
- **Service (Serviço)**: Contém a lógica de negócio e intermedia entre o Repository e os Controllers.
- **Controller**: Gerencia as requisições HTTP e chama os serviços apropriados.

## **4. Como Rodar o Projeto**

### **4.1. Requisitos**

- **.NET SDK 7.0+**
- **SQL Server instalado ou um container com SQL Server**
- **Visual Studio ou VS Code**
- **Git (para clonar o repositório, se necessário)**

### **4.2. Passos para Executar**

1. **Clone o repositório** (se ainda não clonou):

   ```bash
   git clone https://github.com/Sandokanalves/GerenciadorDeProdutos.git
   cd GerenciadorProdutos
   ```

2. **Configure a string de conexão no \*\*\*\*\*\*\*\*****`appsettings.json`**:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVIDOR;Database=GerenciadorProdutos;User Id=SEU_USUARIO;Password=SUA_SENHA;"
   }
   ```

3. **Aplicar as migrações e criar o banco de dados**:

   ```bash
   dotnet ef database update
   ```

4. **Executar o projeto**:

   ```bash
   dotnet run
   ```

   Ou execute no Visual Studio pressionando `F5`.

5. **Acesse o sistema**: Abra o navegador e acesse: `http://localhost:5000/Produto`

## **5. Como Rodar os Testes Unitários**

### **5.1. Estrutura dos Testes**

Os testes foram implementados utilizando **xUnit** e **FluentAssertions** para garantir que os métodos funcionem corretamente.

- **ProdutoServiceTests**: Testa os métodos do serviço de produtos.

  - `ObterTodosProdutos_DeveRetornarListaDeProdutos`: Verifica se a lista de produtos é retornada corretamente.
  - `ObterProdutoPorId_DeveRetornarProduto_QuandoIdExiste`: Testa se um produto específico é retornado corretamente pelo ID.
  - `ExcluirProduto_DeveRemoverProduto`: Testa a remoção de um produto do banco de dados.

- **ProdutoControllerTests**: Testa as ações do controller.

  - `Index_DeveRetornarViewComListaDeProdutos`: Verifica se a página de listagem carrega corretamente.
  - `Create_DeveRedirecionarParaIndex_QuandoProdutoCriadoComSucesso`: Testa a criação de um produto.
  - `Delete_DeveRetornarNotFound_QuandoProdutoNaoExiste`: Testa o comportamento da exclusão quando o produto não existe.

### **5.2. Executando os Testes no Visual Studio**

1. No **Visual Studio**, acesse o menu `Testes` > `Executar Todos os Testes`.
2. Se estiver em português, procure por `Executar Todos os Testes`.
3. O resultado dos testes aparecerá no **Test Explorer**, mostrando quais passaram e quais falharam.

### **5.3. Executando os Testes via Terminal**

Se preferir rodar os testes pelo terminal, execute o comando:

```bash
   dotnet test
```

Isso mostrará quais testes passaram ou falharam.



