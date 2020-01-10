using System;
using System.Collections.Generic;
using System.Text;
using WebChecker.Database;

namespace DBCreator
{
    class AppDbContext : WebChecker.Database.AppDbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(string ipSqlServer, string nameSqlSever, string loginSqlServer, string passSqlServer)
        {
            _ipSqlServer = ipSqlServer;
            _nameSqlSever = nameSqlSever;
            _loginSqlServer = loginSqlServer;
            _passSqlServer = passSqlServer;
        }


    }
}
