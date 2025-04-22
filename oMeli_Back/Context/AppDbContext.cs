using oMeli_Back.Entities;
using Microsoft.EntityFrameworkCore;
namespace oMeli_Back.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {}
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<PlanEntity> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("User");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                entity.Property(u => u.Name).IsRequired().HasMaxLength(50);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Phone).IsRequired().HasMaxLength(20);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Date_Creation);
            });

            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<UserRoleEntity>(entity => {
                entity.ToTable("UserRole");
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne<UserEntity>(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

                entity.HasOne<RoleEntity>(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            });

          
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Buyer" },
                new RoleEntity { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Seller" },
                new RoleEntity { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Admin" }
            );
            modelBuilder.Entity<PlanEntity>(entity =>
            {
                entity.ToTable("Plan");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
                entity.Property(p => p.PublicationLimited).IsRequired();
                entity.Property(p => p.PublicationCustom).IsRequired();
                entity.Property(p => p.StoreCustom).IsRequired();
                entity.Property(p => p.PublicationUnlimited).IsRequired();
                entity.Property(p => p.ViewStatics).IsRequired();
                entity.Property(p => p.CSVImport).IsRequired();
            });
            modelBuilder.Entity<PlanEntity>().HasData(
                new PlanEntity
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Free",
                    PublicationLimited = 5,
                    PublicationCustom = false,
                    StoreCustom = false,
                    PublicationUnlimited = false,
                    ViewStatics = false,
                    CSVImport = false
                },
                new PlanEntity
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Pro",
                    PublicationLimited = 15,
                    PublicationCustom = true,
                    StoreCustom = true,
                    PublicationUnlimited = false,
                    ViewStatics = false,
                    CSVImport = false
                },
                new PlanEntity
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Premium",
                    PublicationLimited = 15,
                    PublicationCustom = true,
                    StoreCustom = true,
                    PublicationUnlimited = true,
                    ViewStatics = true,
                    CSVImport = true
                }

            );

            modelBuilder.Entity<Subscription>(entity => {
                entity.ToTable("Subscription");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.UserId).IsRequired();
                entity.Property(s => s.PlanId).IsRequired();
                entity.Property(s => s.DateStart).IsRequired();
                entity.Property(s => s.DateEnd);
                entity.Property(s => s.DateRenovation);
                entity.Property(s => s.State);
                entity.Property(s => s.RenovationAutomatic);
            });
        }
    }
}
