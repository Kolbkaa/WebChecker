using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using MailKit;
using WebChecker.Database;
using WebChecker.Properties;
using WebChecker.Tool;
using WebChecker.ViewModel;

namespace WebChecker
{
    /// <summary>
    /// Interaction logic for ConfigurationWindows.xaml
    /// </summary>
    public partial class ConfigurationWindows : Window
    {
        private readonly ConfigurationViewModel _configurationViewModel;
        public ConfigurationWindows()
        {
            var serializableService = new SerializableService<ConfigurationViewModel>();

            _configurationViewModel = serializableService.Deserialize();
            this.DataContext = _configurationViewModel;
            InitializeComponent();
        }

        private void SaveDatabaseConfiguration_OnClick(object sender, RoutedEventArgs e)
        {
            //_configurationViewModel.SaveDbConfiguration();
            var SerializableService = new SerializableService<ConfigurationViewModel>();
            SerializableService.Serialize(_configurationViewModel);
            MessageBox.Show("Zapisano ustawienia.", "Ustawienia", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CheckDatabaseConnection_OnClick(object sender, RoutedEventArgs e)
        {
            bool isConnection;
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    isConnection = dbContext.Database.CanConnect();
                }
            }
            catch (SqlException)
            {
                isConnection = false;
            }
            

            if (isConnection)
            {
                MessageBox.Show("Ustawienia bazy danych poprawne.", "Test połączenia", MessageBoxButton.OK, MessageBoxImage.Information);
                Settings.Default.confSqlServer = true;
                Settings.Default.Save();
            }
            else
            {
                Error.ShowError("Ustawienia bazy danych nie poprawne.");
            }
        }

        private void CheckMailConnection_OnClick(object sender, RoutedEventArgs e)
        {
            var sendMail = new SendMail();
            var checkConnection = sendMail.CheckConnect();

            if (checkConnection)
            {
                MessageBox.Show("Ustawienia poczty poprawne.", "Test połączenia", MessageBoxButton.OK, MessageBoxImage.Information);
                Settings.Default.smtpCorrectConf = true;
                Settings.Default.Save();
            }
            else
            {
                Error.ShowError("Ustawienia poczty nie poprawne.");
            }
        }

        private void SaveMailConfiguration_OnClick(object sender, RoutedEventArgs e)
        {
            //_configurationViewModel.SaveMailConfiguration();
            var SerializableService = new SerializableService<ConfigurationViewModel>();
            SerializableService.Serialize(_configurationViewModel);
            MessageBox.Show("Zapisano ustawienia.", "Ustawienia", MessageBoxButton.OK, MessageBoxImage.Information);


        }
    }
}
