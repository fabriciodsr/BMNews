using System;
using System.Linq;
using BMNews.Domain.ValueObject;
using BMNews.Helpers;

namespace BMNews.Domain.Entities
{
    public class Noticia : EntityBase
    {
        public const int TituloMinLength = 6;
        public const int TituloMaxLength = 50;
        public string Titulo { get; private set; }

        public const int ConteudoMinLength = 6;
        public const int ConteudoMaxLength = 1000;
        public string Conteudo { get; private set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        //Construtor EntityFramework
        protected Noticia()
        {

        }

        public Noticia(string titulo, string conteudo)
        {
            SetTitulo(titulo);
            SetConteudo(conteudo);
            DtInclusao = DateTime.Now;
        }

        public void SetTitulo(string titulo)
        {
            Guard.ForNullOrEmptyDefaultMessage(titulo, "Titulo");
            Guard.StringLength("Titulo", titulo, TituloMinLength, TituloMaxLength);
            Titulo = titulo;
        }

        public void SetConteudo(string conteudo)
        {
            Guard.ForNullOrEmptyDefaultMessage(conteudo, "Conteudo");
            Guard.StringLength("Conteudo", conteudo, ConteudoMinLength, ConteudoMaxLength);
            Conteudo = conteudo;
        }
    }
}
