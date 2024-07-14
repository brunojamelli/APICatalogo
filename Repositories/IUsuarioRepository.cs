using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    // IEnumerable<Produto> GetProdutosPorCategoria(int id);
    PagedList<Usuario> GetUsuariosPaged(UsuariosParameters usuariosParameters);

    PagedList<Usuario> GetUsuarioFiltroNome(UsuariosFiltroNome filtroParams);
}
