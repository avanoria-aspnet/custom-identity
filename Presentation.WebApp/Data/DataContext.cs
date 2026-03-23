using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApp.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(e =>
        {
            e.ToTable("Users");

            e.HasKey(x => x.Id);

            e.Property(x => x.Email).IsRequired();

            e.Property(x => x.PasswordHash).IsRequired();

            e.Property(x => x.PasswordSalt).IsRequired();

            e.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .IsRequired();

            e.HasIndex(x => x.Email)
                .IsUnique();
        });

    }

}
