using Academy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Academy.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Student> Students { get; set; }
}
