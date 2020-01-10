using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebChecker.Database.Entity;
using WebChecker.Properties;

namespace WebChecker.Database
{
    public class AppDbContext : DbContext
    {
        protected string _ipSqlServer;
        protected string _nameSqlSever;
        protected string _loginSqlServer;
        protected string _passSqlServer;

        public AppDbContext()
        {
            _ipSqlServer = Settings.Default.ipSqlServer;
            _nameSqlSever = Settings.Default.nameSqlServer;
            _loginSqlServer = Settings.Default.loginSqlServer;
            _passSqlServer = Settings.Default.passSqlServer;
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=WebChecker;Integrated Security=True");
            optionsBuilder.UseSqlServer($@"Data Source={_ipSqlServer}\{_nameSqlSever};Initial Catalog=WebChecker;User Id={_loginSqlServer};Password={_passSqlServer};");
        }

        //public void CreateDb()
        //{
        //    using (var dbContext = new AppDbContext())
        //    {
        //        dbContext.Database.Migrate();

        //    }
        //}

        public bool IsConnect()
        {
            if (string.IsNullOrWhiteSpace(_ipSqlServer) || string.IsNullOrWhiteSpace(_nameSqlSever))
            {
                return false;
            }

            using (var dbContext = new AppDbContext())
            {
                return dbContext.Database.CanConnect();
            }

            
        }

        public DbSet<WebsiteEntity> WebsiteEntities { get; set; }
        public DbSet<ProductEntity> ProductEntity { get; set; }
    }
}
