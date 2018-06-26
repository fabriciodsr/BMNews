
using System;

namespace BMNews.Domain.Entities
{
    public class Noticia
    {
        public int NoticiaId { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string PalavraChave { get; set; }
        public bool Disponivel { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
