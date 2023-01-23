using LoginAppWithSessionInAsp.Net.Models;
using LoginAppWithSessionInAsp.Net.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAppWithSessionInAsp.Net.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserDataModel> Users { get; set; }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
