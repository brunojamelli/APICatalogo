using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorCategoria(int id);

    IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams);

    PagedList<Produto> GetProdutosPaged(ProdutosParameters produtosParams);
}
