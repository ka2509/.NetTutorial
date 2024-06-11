using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

//we need to config the database at this file
// Map the database to the actual code
//database connection at json file

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            : base(options)
        {
        }

        //This name of below DbSet is the name of table in database
        public DbSet<Stock> Stock {get; set; }
        public DbSet<Comment> Comments {get; set; }
    }
}