using Kioxk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Kioxk.Server.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
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
        public DbSet<Livret>? Livret { get; set; } 
        public DbSet<Commande>? Commandes { get; set; } 

    }
}
