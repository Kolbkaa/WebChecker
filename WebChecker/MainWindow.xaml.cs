using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WebChecker.ViewModel;

namespace WebChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            mainViewModel = new MainViewModel();
            DataGrid.DataContext = mainViewModel;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pageToCheck = new PageToCheck(@"https://www.anysoft.pl/", @"//h1[@class='name']", @"//price[@id='prCurrent']");
            pageToCheck.Check();
            var product = pageToCheck.Product.Values;
            mainViewModel.LoadProduct(product.Distinct(Product.PriceNameComparer));


        }
    }
}
