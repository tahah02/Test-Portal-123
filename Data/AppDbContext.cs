using Microsoft.EntityFrameworkCore;
using ChannelApp.Models;

namespace ChannelApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .IsRequired()
                .HasConversion(email => email.ToLowerInvariant(), email => email);
        }
    }
}