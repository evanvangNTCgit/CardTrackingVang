using CardTrackingVang.Models;
using CardTrackingVang.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CardTrackingVang.DataAccess
{
    public class DataContext : DbContext
    {
        // The tables in my sqllite DB file.
        public DbSet<Card> Card { get; set; }
        public DbSet<CardType> CardType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionDb = $"Filename={PathDB.GetPath("CardsDB.db")}";

            optionsBuilder.UseSqlite(connectionDb);
        }

        // Making my DB relationships.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.CardType)
                .WithMany()
                .HasForeignKey(c => c.CardTypeID);

        }
    }
}
