using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebChecker.Properties;

namespace WebChecker.ViewModel
{
    [Serializable]
    public class ConfigurationViewModel  
    {
        public ConfigurationViewModel()
        {
            IpSqlServer = Settings.Default.ipSqlServer;
            NameSqlServer = Settings.Default.nameSqlServer;
            LoginSqlServer = Settings.Default.loginSqlServer;
            PasswordSqlServer = Settings.Default.passSqlServer;

            SmtpServer = Properties.Settings.Default.smtpSerwer;
            SmtpPort = Properties.Settings.Default.smtpPort;
            SmtpUsername = Properties.Settings.Default.smtpUsername;
            SmtpPassword = Properties.Settings.Default.smtpPassword;
            Ssl = Properties.Settings.Default.ssl;
        }

        public string IpSqlServer { get; set; }
        public string NameSqlServer { get; set; }
        public string LoginSqlServer { get; set; }
        public string PasswordSqlServer { get; set; }

        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public bool Ssl { get; set; }

        public void SaveDbConfiguration()
        {
            Settings.Default.ipSqlServer = IpSqlServer;
            Settings.Default.nameSqlServer = NameSqlServer;
            Settings.Default.loginSqlServer = LoginSqlServer;
            Settings.Default.passSqlServer = PasswordSqlServer;
            Settings.Default.Save();

           

        }

        public void SaveMailConfiguration()
        {

            Settings.Default.smtpSerwer = SmtpServer;
            Settings.Default.smtpPort = SmtpPort;
            Settings.Default.smtpUsername = SmtpUsername;
            Settings.Default.smtpPassword = SmtpPassword;
            Settings.Default.ssl = Ssl;
            Settings.Default.Save();
        }

    }
}
