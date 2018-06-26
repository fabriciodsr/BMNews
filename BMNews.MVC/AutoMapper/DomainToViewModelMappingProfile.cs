
using AutoMapper;
using BMNews.Domain.Entities;
using BMNews.MVC.ViewModels;

namespace BMNews.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UsuarioViewModel, Usuario>();
            Mapper.CreateMap<NoticiaViewModel, Noticia>();
        }
    }
}