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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WebCheck webCheck = new WebCheck(@"https://www.anysoft.pl/razem-taniej-zestaw-must-have-total-commander-winrar-the-bat-snagit");
            var temp = webCheck.FindProduct(@"//h1[@class='name']", @"//price[@id='prCurrent']");
            
        }
    }
}
