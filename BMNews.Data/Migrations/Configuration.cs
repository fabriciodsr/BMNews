using BMNews.Data.Context;
using BMNews.Data.Seeds;

namespace BMNews.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BMNewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BMNewsContext context)
        {
            UsuarioSeed.Seed(context);
        }
    }
}
