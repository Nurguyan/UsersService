using Microsoft.EntityFrameworkCore;
using UsersService.Models;

namespace UsersService.Data
{
    public sealed class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .IsRequired();
            modelBuilder.Entity<User>()
            .Property(u => u.Surname)
            .IsRequired()
            .HasMaxLength(50);
            modelBuilder.Entity<User>()
            .Property(u => u.Age)
            .IsRequired(); 
            modelBuilder.Entity<User>()
            .Property(u => u.Sex)
            .IsRequired();
            modelBuilder.Entity<User>()
            .Property(u => u.IsActive)
            .IsRequired();
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Phones)
                .WithOne(p => p.User);
            });

            // Phone
            modelBuilder.Entity<Phone>()
            .Property(p => p.Id)
            .IsRequired();
            modelBuilder.Entity<Phone>()
            .Property(p => p.UserId)
            .IsRequired();
            modelBuilder.Entity<Phone>()
            .Property(p => p.Number)
            .IsRequired()
            .HasMaxLength(15);
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasOne(p => p.User)
                .WithMany(u => u.Phones)
                .HasForeignKey(u => u.UserId);
            });
        }
    }
}
