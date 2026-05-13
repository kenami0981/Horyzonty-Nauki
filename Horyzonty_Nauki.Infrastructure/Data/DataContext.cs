using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horyzonty_Nauki.Domain;
using Microsoft.EntityFrameworkCore;

namespace Horyzonty_Nauki.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Config> Configs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().Property(a => a.Category).HasConversion<string>();
        }
    }
}
