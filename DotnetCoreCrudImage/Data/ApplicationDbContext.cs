using DotnetCoreCrudImage.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotnetCoreCrudImage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Post { get; set; }
    }
}
