using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using MySqlConnector;

namespace Repositorio
{
    public class AcessoDados : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string database = "heroi_db";
            string password = "SservKopsBacLog";
            string server = "localhost";
            string user = "root";
            string port = "3307";
            string sslM = "none";

            string stringConexao = String.Format("server={0}; port ={1}; uid={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            if (!options.IsConfigured)
            {
                options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
            }


        }
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Superpoderes> Poder { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Heroi>()
                .HasMany(h => h.SuperPoderes)
                .WithMany(sp => sp.Herois)
                .UsingEntity<Dictionary<string, object>>(
                    "HeroisSuperpoderes",
                    j => j
                        .HasOne<Superpoderes>()
                        .WithMany()
                        .HasForeignKey("SuperpoderId")
                        .HasConstraintName("FK_HeroisSuperpoderes_Superpoderes_SuperpoderId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Heroi>()
                        .WithMany()
                        .HasForeignKey("HeroiId")
                        .HasConstraintName("FK_HeroisSuperpoderes_Heroi_HeroiId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("HeroiId", "SuperpoderId");
                        j.ToTable("HeroisSuperpoderes");
                    });

            // Outras configurações e entidades
        }

    }


}
    


