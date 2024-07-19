using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {        
        
    }
    public PagedList<Categoria> GetCategoriasPaged(CategoriaParameters catParams)
    {
        var categorias = GetAll().OrderBy(cat => cat.CategoriaId).AsQueryable();
        var categoriasPaginados = PagedList<Categoria>.
        ToPagedList(categorias,catParams.PageNumber, catParams.PageSize);
        return categoriasPaginados;
    }

    public PagedList<Categoria> GetGategoriasFiltroNome(CategoriasFiltroNome filtroParams)
    {
        var categorias = GetAll().AsQueryable();
        if (!string.IsNullOrEmpty(filtroParams.Nome))
        {
            categorias = categorias.Where(cat => cat.Nome.Contains(filtroParams.Nome, StringComparison.OrdinalIgnoreCase));
        }
        var categoriasFiltered = PagedList<Categoria>
        .ToPagedList(
            categorias, filtroParams.PageNumber, filtroParams.PageSize
        );

        return categoriasFiltered;
    }
}
