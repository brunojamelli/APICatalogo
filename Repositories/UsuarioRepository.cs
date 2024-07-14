using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {        
    }
    public PagedList<Usuario> GetUsuariosPaged(UsuariosParameters usuariosParameters)
    {
        var usuarios = GetAll().OrderBy(usr => usr.UsuarioID).AsQueryable();
        var usuariosPaginados = PagedList<Usuario>.
        ToPagedList(usuarios,usuariosParameters.PageNumber, usuariosParameters.PageSize);
        return usuariosPaginados;
    }

    public PagedList<Usuario> GetUsuarioFiltroNome(UsuariosFiltroNome filtroParams)
    {
        var usuarios = GetAll().AsQueryable();
        if (!string.IsNullOrEmpty(filtroParams.Nome))
        {
            usuarios = usuarios.Where(usr => usr.Nome.Contains(filtroParams.Nome));
        }
        var usuariosFiltrados = PagedList<Usuario>
        .ToPagedList(
            usuarios, filtroParams.PageNumber, filtroParams.PageSize
        );

        return usuariosFiltrados;
    }
}
