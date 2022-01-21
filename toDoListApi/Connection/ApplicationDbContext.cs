using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Model;

namespace toDoListApi.Connection
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<SubTask> SubTask { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            base.OnModelCreating(modelBuilder);
        }
    }
}
