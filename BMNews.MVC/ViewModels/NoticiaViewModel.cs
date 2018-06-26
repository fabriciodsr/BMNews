using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BMNews.MVC.ViewModels
{
    public class NoticiaViewModel
    {
        [Key]
        public int NoticiaId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Titulo")]
        [MaxLength(250, ErrorMessage = "Máximo de {1} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo de {1} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Preencha o campo Conteúdo")]
        [AllowHtml]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "Preencha o campo Palavra Chave")]
        public string PalavraChave { get; set; }

        [DisplayName("Data de Criação")]
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Disponivel?")]
        public bool Disponivel { get; set; }
        [DisplayName("Autor")]
        public int UsuarioId { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }
    }
}