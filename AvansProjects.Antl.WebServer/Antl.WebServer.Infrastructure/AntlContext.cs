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


            modelBuilder.Entity<Friendship>().HasKey(k => new {k.ApplicationUserId, ApplicationUserTwoId = k.ApplicationUserFriendId});

            modelBuilder.Entity<Friendship>()
                .HasOne(fs => fs.ApplicationUser)
                .WithMany(u => u.Friendships)
                .HasForeignKey(fs => fs.ApplicationUserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(fs => fs.ApplicationUserFriend)
                .WithMany(u => u.USerFriendships)
                .HasForeignKey(fs => fs.ApplicationUserFriendId).OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    }
}