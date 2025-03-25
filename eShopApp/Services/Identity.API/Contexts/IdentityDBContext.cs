﻿using Identity.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Contexts
{
    public class IdentityDBContext : DbContext
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
    : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Define composite key
            //    // Configure the UserRole entity as a join table for User and Role.
            //    modelBuilder.Entity<UserRole>()
            //        .HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite primary key.
            //    //Defines the many-to-many relationship between User and Role.
            //    modelBuilder.Entity<UserRole>()
            //        .HasOne(ur => ur.User)
            //        .WithMany(u => u.UserRoles)
            //        .HasForeignKey(ur => ur.UserId);
            //    modelBuilder.Entity<UserRole>()
            //        .HasOne(ur => ur.Role)
            //        .WithMany(r => r.UserRoles)
            //        .HasForeignKey(ur => ur.RoleId);
            //    // Seed initial data for Roles, Users, Clients, and UserRoles.
            //    modelBuilder.Entity<Role>().HasData(
            //        new Role { Id = 1, Name = "Admin", Description = "Admin Role" },
            //        new Role { Id = 2, Name = "Editor", Description = " Editor Role" },
            //        new Role { Id = 3, Name = "User", Description = "User Role" }
            //    );
            //    modelBuilder.Entity<Client>().HasData(
            //        new Client
            //        {
            //            Id = 1,
            //            ClientId = "Client1",
            //            Name = "Client Application 1",
            //            ClientURL = "https://client1.com"
            //        },
            //        new Client
            //        {
            //            Id = 2,
            //            ClientId = "Client2",
            //            Name = "Client Application 2",
            //            ClientURL = "https://client2.com"
            //        }
            //    );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SigningKey> SigningKeys { get; set; }
    }
}
