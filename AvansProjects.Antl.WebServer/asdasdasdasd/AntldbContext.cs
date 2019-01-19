using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace asdasdasdasd
{
    public partial class AntldbContext : DbContext
    {
        public AntldbContext()
        {
        }

        public AntldbContext(DbContextOptions<AntldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<EventDates> EventDates { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<FriendShips> FriendShips { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<UserEventDates> UserEventDates { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AntlDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<EventDates>(entity =>
            {
                entity.HasIndex(e => e.EventId);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDates)
                    .HasForeignKey(d => d.EventId);
            });

            modelBuilder.Entity<FriendShips>(entity =>
            {
                entity.HasKey(e => new { e.ApplicationUserId, e.ApplicationUserFriendId });

                entity.HasOne(d => d.ApplicationUserFriend)
                    .WithMany(p => p.FriendShipsApplicationUserFriend)
                    .HasForeignKey(d => d.ApplicationUserFriendId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.FriendShipsApplicationUser)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserEventDates>(entity =>
            {
                entity.HasIndex(e => e.ApplicationUserId);

                entity.HasIndex(e => e.EventDateId);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UserEventDates)
                    .HasForeignKey(d => d.ApplicationUserId);

                entity.HasOne(d => d.EventDate)
                    .WithMany(p => p.UserEventDates)
                    .HasForeignKey(d => d.EventDateId);
            });

            modelBuilder.Entity<UserGroups>(entity =>
            {
                entity.HasKey(e => new { e.ApplicationUserId, e.GroupId });

                entity.HasIndex(e => e.GroupId);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.ApplicationUserId);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}