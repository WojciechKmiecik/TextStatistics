using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TextStatistics.Dal.DataModel;

namespace TextStatistics.Dal
{
    internal class TextStatisticsContext : DbContext
    {
        public TextStatisticsContext(DbContextOptions<TextStatisticsContext> options)
        : base(options)
        { }
    private static bool created = false;
        public TextStatisticsContext()
        {
            if (!created)
            {
                Database.EnsureCreated();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatisticsEntity>().ToTable("Statistics");
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=TextStatistics;Trusted_Connection=True;ConnectRetryCount=0");
        }

        public DbSet<StatisticsEntity> Statistics { get; set; }
    }
}
