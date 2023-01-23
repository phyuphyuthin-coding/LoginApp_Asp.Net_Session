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

        //private readonly string connectionString;
        //private readonly IConfiguration _configuration;

        //public AppDbContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    connectionString = _configuration.GetConnectionString("DBConnection");
        //}

        //public AppDbContext()
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connectionString);
        //}

        public DbSet<UserDataModel> Users { get; set; }
    }
}
