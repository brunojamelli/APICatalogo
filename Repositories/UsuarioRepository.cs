using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {        
    }
}
