using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TVShows.Tables
{
    public class Context : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<ScheduleForWeek> SchedulesForWeek { get; set; }
        public DbSet<CitizensAppeal> CitizensAppeals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("SQLConnection");
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
        }
    }
}
