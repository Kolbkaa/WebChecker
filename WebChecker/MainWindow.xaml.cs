using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebChecker.Model;
using WebChecker.Properties;
using WebChecker.Tool;
using WebChecker.ViewModel;

namespace WebChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;
        private Mode _mode = Mode.Add;

        public MainWindow()
        {
            InitializeComponent();

            if (Settings.Default.confSqlServer == false)
            {
                new ConfigurationWindows().ShowDialog();
            }

            if (Settings.Default.confSqlServer == true)
            {
                _mainViewModel = new MainViewModel();
                this.DataContext = _mainViewModel;

            }
            else
            {
                MainPanel.IsEnabled = false;
                Error.ShowError("Baza danych nie dostępna.");
            }
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var website in _mainViewModel.WebsiteCollection)
            {
                if (!website.ToCheck) continue;
                var pageToCheck = new PageToCheck(website.MainUrl, website.NameXPath, website.PriceXPath);
                //pageToCheck.OneLinkCheck += PageToCheckOnAllLinkCheck;
                _mainViewModel.PageToCheckCollection.Add(pageToCheck);
                pageToCheck.Check();
            }
            
        }

        private void PageToCheckOnAllLinkCheck(int arg1, int arg2, int arg3, string arg4)
        {

            Debug.Print($"{arg4}: Product: {arg1}, Link Checked: {arg2}, Link to check: {arg3}");
        }

        private void AddToCheck_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == Mode.Add)
            {
                _mainViewModel.AddWebsiteToCollection();
            }
            else
            {
                _mainViewModel.EditValue();
                _mode = Mode.Add;
            }

        }

        private void DeleteValueMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var website = WebsiteDataGrid.SelectedItem as Website;
            _mainViewModel.DeleteElement(website);
        }

        private void EditValue_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Edit;
            _mainViewModel.LoadEditValue(WebsiteDataGrid.SelectedItem as Website);
        }

        private enum Mode
        {
            Edit, Add
        }

        private void Show_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var website = WebsiteDataGrid.SelectedItem as Website;
            new ShowWindow(website).ShowDialog();
        }

        private void ShowAll_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowConfigurationWindow(object sender, RoutedEventArgs e)
        {
            new ConfigurationWindows().ShowDialog();
            
        }
    }
}
