using SistemaEncuestasBL.Models;
using System.Data.Entity;

namespace SistemaEncuestasBL.Data
{
    public class SistemaEncuestasContext : DbContext
    {
        #region Atributos
        private static SistemaEncuestasContext sistemaEncuestasContext = null;
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<PreguntaOM> PreguntasOM { get; set; }
        public DbSet<PreguntaOS> PreguntasOS { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<RespuestaOM> RespuestasOM { get; set; }
        public DbSet<RespuestaOS> RespuestasOS { get; set; }
        public DbSet<RespuestaTL> RespuestasTL { get; set; }
        public DbSet<Opcion> Opciones { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }

        #endregion

        #region Constructores
        public SistemaEncuestasContext() : base("Database")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SistemaEncuestasContext, SistemaEncuestasBL.Migrations.Configuration>());
            Database.CreateIfNotExists();
        }

        #endregion

        public static SistemaEncuestasContext Crear()
        {
            if (sistemaEncuestasContext == null)
                sistemaEncuestasContext = new SistemaEncuestasContext();

            return new SistemaEncuestasContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();

            // Configuración de relaciones 1 a muchos.
            //modelBuilder.Entity<RespuestaTL>()
            //    .HasRequired<PreguntaTL>(b => b.Pregunta)
            //    .WithMany(a => a.Respuestas)
            //    .HasForeignKey<int>(b => b.PreguntaID);

            //modelBuilder.Entity<PreguntaOM>()
            //    .HasRequired<Encuestas>(b => b.Encuestas)
            //    .WithMany(a => a.Preguntas)
            //    .HasForeignKey<int>(b => b.EncuestaID);
        }


    }
}
