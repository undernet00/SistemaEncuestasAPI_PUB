using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace SistemaEncuestasBL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SistemaEncuestasBL.Data.SistemaEncuestasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            
        }

        protected override void Seed(SistemaEncuestasBL.Data.SistemaEncuestasContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

