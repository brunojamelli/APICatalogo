using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedList<Categoria> GetCategoriasPaged(CategoriaParameters catParams);

    PagedList<Categoria> GetGategoriasFiltroNome(CategoriasFiltroNome filtroParams);

}
