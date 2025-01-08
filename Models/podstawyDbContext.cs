using Microsoft.EntityFrameworkCore;

namespace wzorzec3f2.Models;

public class podstawyDbContext : DbContext
{
    public podstawyDbContext(DbContextOptions<podstawyDbContext> options) : base(options)
    {
    }
    
    public DbSet<Car> Cars { get; set; }
    public DbSet<Engine> Engines { get; set; }
}