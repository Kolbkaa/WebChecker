using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WebChecker.Model;
using WebChecker.ViewModel;

namespace WebChecker
{
    /// <summary>
    /// Interaction logic for ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        private readonly ShowViewModel _showViewModel;
        public ShowWindow(Website website)
        {
            _showViewModel = new ShowViewModel(website);
            DataContext = _showViewModel;
            InitializeComponent();
        }

        private void ShowOneMonth_Click(object sender, RoutedEventArgs e)
        {
            var chartWindow = new ChartWindow();
            chartWindow.ShowMonthChart((ProductGrid.SelectedItem as Product).Name);
            chartWindow.ShowDialog();
        }

        private void ShowOneYear_Click(object sender, RoutedEventArgs e)
        {
            var chartWindow = new ChartWindow();
            chartWindow.ShowYearChart((ProductGrid.SelectedItem as Product).Name);
            chartWindow.ShowDialog();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _showViewModel.LoadProductList();
        }
    }
}
