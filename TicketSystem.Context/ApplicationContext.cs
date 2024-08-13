using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Context
{
    public class ApplicationContext: IdentityDbContext<AppUser , IdentityRole<int>,int>
    {
        public virtual DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions): base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            string[] roleNames = { "Admin", "User" };
            foreach (var role in roleNames)
            {
                modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = role.GetHashCode(), Name = role, NormalizedName = role.ToUpper() });
            }

           
            var adminUser = new AppUser
            {
                Id = 1, 
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                MobileNumber = "11223344"
            };

            var adminUserPasswordHash = new PasswordHasher<AppUser>().HashPassword(adminUser, "Pass000");
            modelBuilder.Entity<AppUser>().HasData(adminUser);

            
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = adminUser.Id, RoleId = roleNames[0].GetHashCode() }
            );

            
            var users = new List<AppUser>
            {
                new AppUser { Id = 2, UserName = "user1", NormalizedUserName = "USER1", MobileNumber = "1234567890" },
                new AppUser { Id = 3, UserName = "user2", NormalizedUserName = "USER2", MobileNumber = "1234567891" },
                 new AppUser { Id = 4, UserName = "user3", NormalizedUserName = "USER3", MobileNumber = "1234567892" },
                new AppUser { Id = 5, UserName = "user4", NormalizedUserName = "USER4", MobileNumber = "1234567893" },
                 new AppUser { Id = 6, UserName = "user5", NormalizedUserName = "USER5", MobileNumber = "1234567894" },
                new AppUser { Id = 7, UserName = "user6", NormalizedUserName = "USER6", MobileNumber = "1234567895" },
                 new AppUser { Id = 8, UserName = "user7", NormalizedUserName = "USER7", MobileNumber = "1234567896" },
                new AppUser { Id = 9, UserName = "user8", NormalizedUserName = "USER8", MobileNumber = "1234567897" },
                 new AppUser { Id = 10, UserName = "user9", NormalizedUserName = "USER9", MobileNumber = "1234567899" },
                
                
            };

            foreach (var user in users)
            {
                var userPasswordHash = new PasswordHasher<AppUser>().HashPassword(user, "Password111");
                modelBuilder.Entity<AppUser>().HasData(user);
            }

            
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 2, RoleId = roleNames[1].GetHashCode() },
                new IdentityUserRole<int> { UserId = 3, RoleId = roleNames[1].GetHashCode() }
                
            );
        }

















    }
}
