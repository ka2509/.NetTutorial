using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//we need to config the database at this file
// Map the database to the actual code
//database connection at json file

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ApplicationDBContext(DbContextOptions dbContextOptions)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        : base(dbContextOptions)
        {

        }

        //This name of below DbSet is the name of table in database
        public DbSet<Stock> Stock {get; set; }
        public DbSet<Comment> Comments {get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<IdentityRole> roles = new List<IdentityRole> {
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "admin"
                },
                new IdentityRole {
                    Name = "User",
                    NormalizedName = "user"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<Stock>()
                .HasMany(s => s.Comments)
                .WithOne(c => c.Stock)
                .HasForeignKey(c => c.StockId)
                .OnDelete(DeleteBehavior.Cascade);
            

        }
    }
}