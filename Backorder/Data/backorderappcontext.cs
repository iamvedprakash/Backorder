using Backorder.Models;
using Microsoft.EntityFrameworkCore;


namespace Backorder.Data
{
    public class backorderappcontext : DbContext
    {
        public DbSet<backordersummary> backordersummary { get; set; }
        public DbSet<backorderstatus> backorderstatus { get; set; }
        public DbSet<users> users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FirstApp.Data;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
