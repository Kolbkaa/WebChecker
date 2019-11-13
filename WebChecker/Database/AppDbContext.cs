using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebChecker.Database.Entity;

namespace WebChecker.Database
{
    class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=WebChecker;Integrated Security=True");
        }

        public void CreateDb()
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Database.Migrate();
            }
        }

        public DbSet<WebsiteEntity> WebsiteEntities { get; set; }
    }
}
