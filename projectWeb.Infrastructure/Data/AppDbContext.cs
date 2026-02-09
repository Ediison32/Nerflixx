using DefaultNamespace;
using Microsoft.EntityFrameworkCore;

namespace projectWeb.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    // public DbSet<Profile> Profiles { get; set; } // Aun no creada en Domain, comentada por seguridad
    public DbSet<Title> Titles { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<TitleGenre> TitleGenres { get; set; }
    public DbSet<MyList> MyLists { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<UserEntitlement> UserEntitlements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de llave compuesta para TitleGenre (OBLIGATORIO)
        modelBuilder.Entity<TitleGenre>()
            .HasKey(tg => new { tg.TitleId, tg.GenreId });

        modelBuilder.Entity<TitleGenre>()
            .HasOne(tg => tg.Title)
            .WithMany(t => t.TitleGenres)
            .HasForeignKey(tg => tg.TitleId);

        modelBuilder.Entity<TitleGenre>()
            .HasOne(tg => tg.Genre)
            .WithMany(g => g.TitleGenres)
            .HasForeignKey(tg => tg.GenreId);
    }
}