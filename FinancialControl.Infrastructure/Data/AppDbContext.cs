using FinancialControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Expense>().ToTable("Expenses");
        modelBuilder.Entity<Salary>().ToTable("Salaries");
        modelBuilder.Entity<Category>().ToTable("Categories");

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.Property(e => e.Value).HasColumnType("numeric(18,2)");
            entity.Property(e => e.CategoryId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Card).HasMaxLength(50);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.Property(s => s.Value).HasColumnType("numeric(18,2)");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(c => c.Name).IsUnique();
        });
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added ||
                e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var baseEntity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                baseEntity.SetUpdatedAt(DateTime.UtcNow);
            }
            else if (entry.State == EntityState.Modified)
            {
                baseEntity.SetUpdatedAt(DateTime.UtcNow);
            }
        }
    }
}