using Kioxk.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection.Metadata;

namespace Kioxk.Server.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
               //  Database.EnsureCreated();
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("DefaultSQLiteConnection");
            optionsBuilder.UseSqlite(connectionString);

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder
        //    //    .Entity<Commande>()
        //    //    .HasMany(e => e.Selected)
        //    //.WithOne(e => e.Commande)
        //    //   .OnDelete(DeleteBehavior.Cascade)
        //    //.HasForeignKey(e => e.)
        //    .Entity<Datetime>()
        //    .HasOne()
        //    .WithMany()
        //    .HasForeignKey(e=> e.Commande)
        //    ;


        public DbSet<Livret> Livret { get; set; }
        public DbSet<Datetime> Datetimes { get; set; }
        public DbSet<Int> Ints { get; set; }
        public DbSet<Hashset> Hashsets { get; set; }
        public DbSet<Commande> Commandes { get; set; }

    }
}
