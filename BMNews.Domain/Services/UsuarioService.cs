using BMNews.Domain.Entities;
using BMNews.Domain.Interfaces.Repositories;
using BMNews.Domain.Interfaces.Services;

namespace BMNews.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
            : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

    }
}
