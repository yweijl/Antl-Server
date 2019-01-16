using Antl.WebServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Antl.WebServer.Infrastructure
{
    public class AntlContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AntlContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Friendship> FriendShips { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<EventDate> EventDates { get; set; }
        public DbSet<UserEventDate> UserEventDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserGroup>()
                .HasKey(pc => new { pc.ApplicationUserId, pc.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(pc => pc.ApplicationUser)
                .WithMany(p => p.UserGroups)
                .HasForeignKey(pc => pc.ApplicationUserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(pc => pc.Group)
                .WithMany(p => p.UserGroups)
                .HasForeignKey(pc => pc.GroupId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=AntlDb;Trusted_Connection=True;ConnectRetryCount=0");
    }
}