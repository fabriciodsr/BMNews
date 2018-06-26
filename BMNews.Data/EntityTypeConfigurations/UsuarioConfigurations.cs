using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using BMNews.Domain.Entities;

namespace BMNews.Data.EntityTypeConfigurations
{
    public class UsuarioConfigurations : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfigurations()
        {
            HasKey(u => u.UsuarioId);

            Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(u => u.Sobrenome)
                .IsRequired()
                .HasMaxLength(150);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Email", 1) { IsUnique = true }));

            Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Login", 1) { IsUnique = true }));

            Property(u => u.Senha)
                .IsRequired()
				.HasMaxLength(50);
        }
    }
}
