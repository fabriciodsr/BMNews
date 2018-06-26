using BMNews.App.Interface;
using BMNews.Domain.Entities;
using BMNews.Domain.Interfaces.Services;

namespace BMNews.App
{
    public class NoticiaAppService : AppServiceBase<Noticia>, INoticiaAppService
    {
        private readonly INoticiaService _noticiaService;

        public NoticiaAppService(INoticiaService noticiaService)
            : base(noticiaService)
        {
            _noticiaService = noticiaService;
        }
    }
}
