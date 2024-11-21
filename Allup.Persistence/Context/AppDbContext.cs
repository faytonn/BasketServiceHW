using Allup.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Allup.Persistence.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public required DbSet<Language> Languages { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<CategoryTranslation> CategoriesTranslations { get; set; }
}
