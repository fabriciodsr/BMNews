using BMNews.Domain.Entities;
using BMNews.Domain.Interfaces.Repositories;
using BMNews.Domain.Interfaces.Services;

namespace BMNews.Domain.Services
{
    public class NoticiaService : ServiceBase<Noticia>, INoticiaService
    {
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaService(INoticiaRepository noticiaRepository)
            : base(noticiaRepository)
        {
            _noticiaRepository = noticiaRepository;
        }
    }
}
