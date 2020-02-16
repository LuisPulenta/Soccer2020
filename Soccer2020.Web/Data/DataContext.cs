using Microsoft.EntityFrameworkCore;
using Soccer2020.Web.Data.Entities;

namespace Soccer2020.Web.Data
{
    public class DataContext : DbContext
    {

        #region Constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        #endregion

        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<GroupDetailEntity> GroupDetails { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<MatchEntity> Matches { get; set; }
        public DbSet<TournamentEntity> Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TeamEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}