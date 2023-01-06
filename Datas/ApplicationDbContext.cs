//using Cube_4.models;

using Cube4solo.models;
using Cube4solo.Models;
using Microsoft.EntityFrameworkCore;

namespace Cube4solo.Datas
{
    public class ApplicationDbContext : DbContext
    {
         public DbSet<Users> Users { get; set; }
         public DbSet<Sites> Sites { get; set; }
         public DbSet<Services> Services { get; set; }

         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
    }
}