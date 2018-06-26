using BMNews.App.Interface;
using BMNews.Domain.Entities;
using BMNews.Domain.Interfaces.Services;

namespace BMNews.App
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService)
            : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }
    }
}
