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

    public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco filtroParams)
    {
        var produtos = GetAll().AsQueryable();
        if (filtroParams.Preco.HasValue && !string.IsNullOrEmpty(filtroParams.PrecoCriterio))
        {
            if(filtroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(prod => prod.Preco > filtroParams.Preco.Value)
                .OrderBy(prod => prod.Preco);
            }
        }

        var produtosFiltrados = PagedList<Produto>
        .ToPagedList(
            produtos, filtroParams.PageNumber, filtroParams.PageSize
        );

        return produtosFiltrados;
    }
}