using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class MyAppDbContext:DbContext 
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        { 
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Salarie> Salaries { get; set; }
    }
}
