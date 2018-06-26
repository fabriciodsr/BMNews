using System;
using System.Collections.Generic;

namespace BMNews.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public bool Admin { get; set; }

        public virtual IEnumerable<Noticia> Noticias { get; set; }
    }
}
