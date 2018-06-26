
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BMNews.MVC.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(150, ErrorMessage = "Máximo de {1} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo de {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo Sobrenome")]
        [MaxLength(150, ErrorMessage = "Máximo de {1} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo de {1} caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Preencha o campo Login")]
        [MaxLength(30, ErrorMessage = "Máximo de {1} caracteres")]
        [MinLength(6, ErrorMessage = "Mínimo de {1} caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Preencha o campo Senha")]
        [MaxLength(32, ErrorMessage = "Máximo de {1} caracteres")]
        [MinLength(6, ErrorMessage = "Mínimo de {1} caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Preencha o campo E-mail")]
        [MaxLength(100, ErrorMessage = "Máximo de {1} caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um E-mail válido!")]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Preencha o campo E-mail")]
        public bool Ativo { get; set; }

        [DisplayName("Administrador")]
        public bool Admin { get; set; }

        public virtual IEnumerable<NoticiaViewModel> Noticias { get; set; }

    }
}