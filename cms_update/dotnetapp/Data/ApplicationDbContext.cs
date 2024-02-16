using dotnetapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Container> Containers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Issue> Issues { get; set; }

        public DbSet<User> Users {get; set;}

 protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<User>() 
                .HasMany(u => u.Assignments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Issues)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Container>()
                .HasOne(c => c.Assignment)
                .WithOne(a => a.Container)
                .HasForeignKey<Assignment>(a => a.ContainerId)
                .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Assignment>()
            //     .HasMany(a => a.Issues)
            //     .WithOne(i => i.Assignment)
            //     .HasForeignKey(i => i.AssignmentId)
            //     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.Assignment)
                .WithOne(i => i.Issue) 
                .HasForeignKey<Issue>(i => i.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
}