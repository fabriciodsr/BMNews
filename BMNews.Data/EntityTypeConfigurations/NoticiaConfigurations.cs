using System.Data.Entity.ModelConfiguration;
using BMNews.Domain.Entities;

namespace BMNews.Data.EntityTypeConfigurations
{
    public class NoticiaConfigurations : EntityTypeConfiguration<Noticia>
    {
        public NoticiaConfigurations()
        {
            HasKey(u => u.NoticiaId);

            Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.Conteudo)
                .IsRequired()
                .HasMaxLength(1000);

            Property(x => x.PalavraChave)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.UsuarioId);
        }
    }
}
