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
    }
}