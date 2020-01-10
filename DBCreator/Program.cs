using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DBCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wpisz IP MSSQL: ");
            string IpSqlServer = Console.ReadLine();

            Console.WriteLine("Wpisz nazwe serwera MSSQL: ");
            string NameSqlServer = Console.ReadLine();

            Console.WriteLine("Wpisz login do bazy danych: ");
            string LoginSqlServer = Console.ReadLine();

            Console.WriteLine("Wpisz hasło serwera bazy danych");
            string PasswordSqlServer = Console.ReadLine();

            
            using var dbContext = new AppDbContext(IpSqlServer,NameSqlServer,LoginSqlServer,PasswordSqlServer);

            try
            {
                if (dbContext.Database.CanConnect())
                {
                    Console.WriteLine("Baza istnieje.");
                }

               
                dbContext.Database.Migrate();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Zakończono pomyślnie");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (SqlException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                Console.ReadKey();
            }


        }



    }
}
