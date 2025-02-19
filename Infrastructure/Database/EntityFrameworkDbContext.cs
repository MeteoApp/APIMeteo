using APIMeteo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMeteo.Infrastructure.Database
{
    public class EntityFrameworkDbContext : DbContext
    {
        public EntityFrameworkDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        
    }
}