using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Supermarket.Model
{
    public class SupermarketContext : DbContext
    {
        public DbSet<User> Utilizatori { get; set; }
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Producator> Producatori { get; set; }
        public DbSet<Categorie> Categorii { get; set; }
        public DbSet<Stoc> Stocuri { get; set; }
        public DbSet<Bon> Bonuri { get; set; }
        public DbSet<BonProdus> BonProduse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-U0JE4M4;Database=Supermarket;User Id=...;Password=...;Encrypt=false;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, NumeUtilizator = "admin", Parola = "admin", TipUtilizator = "admin" },
                new User { Id = 2, NumeUtilizator = "casier", Parola = "casier", TipUtilizator = "casier" }
            );

            modelBuilder.Entity<BonProdus>()
                .HasKey(bp => new { bp.BonId, bp.ProdusId });
        }
        public List<Produs> SearchProducts(string nume, string codBare, DateTime? dataExpirare, int? producatorId, int? categorieId)
        {
            var parameters = new[]
            {
                new SqlParameter("@Nume", (object)nume ?? DBNull.Value),
                new SqlParameter("@CodBare", (object)codBare ?? DBNull.Value),
                new SqlParameter("@DataExpirare", (object)dataExpirare ?? DBNull.Value),
                new SqlParameter("@ProducatorId", (object)producatorId ?? DBNull.Value),
                new SqlParameter("@CategorieId", (object)categorieId ?? DBNull.Value)
            };

            return Produse.FromSqlRaw("EXEC SearchProducts @Nume, @CodBare, @DataExpirare, @ProducatorId, @CategorieId", parameters).ToList();
        }
    }
}
