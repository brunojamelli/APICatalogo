using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context): base(context)
    {       
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }

    public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams)
    {
        return GetAll()
        .OrderBy(prod => prod.Nome)
        .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
        .Take(produtosParams.PageSize).ToList();

    }

    public PagedList<Produto> GetProdutosPaged(ProdutosParameters produtosParams)
    {
        var produtos = GetAll().OrderBy(prod => prod.ProdutoId).AsQueryable();
        var produtosPaginados = PagedList<Produto>.
        ToPagedList(produtos,produtosParams.PageNumber, produtosParams.PageSize);
        return produtosPaginados;
    }
}