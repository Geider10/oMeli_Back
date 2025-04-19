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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("User");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();
                entity.Property(u => u.Name).IsRequired().HasMaxLength(50);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Phone).IsRequired().HasMaxLength(20);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Date_Creation).IsRequired(); 
            });

            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd().IsRequired();
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
        }
    }
}
