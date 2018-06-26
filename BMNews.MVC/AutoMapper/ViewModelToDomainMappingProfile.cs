
using AutoMapper;
using BMNews.Domain.Entities;
using BMNews.MVC.ViewModels;

namespace BMNews.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
            Mapper.CreateMap<Noticia, NoticiaViewModel>();
        }
    }
}