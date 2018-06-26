using System.Data.Entity.Migrations;
using BMNews.Data.Context;
using BMNews.Domain.Entities;
using BMNews.Helpers;

namespace BMNews.Data.Seeds
{
    public class UsuarioSeed
    {
        public static void Seed(BMNewsContext context)
        {
            Usuario u = new Usuario();

            u.Nome = "Administrador";
            u.Sobrenome = "do Site";
            u.Login = "adminbmnews";
            u.Senha = Criptografia.Codificador("bmnews");
            u.Email = "bmnews@bmnews.com.br";
            u.Ativo = true;
            u.Admin = true;

            context.Usuarios.AddOrUpdate(u);
        }
    }
}
