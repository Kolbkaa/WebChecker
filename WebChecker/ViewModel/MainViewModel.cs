using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Annotations;
using WebChecker.Model;

namespace WebChecker.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }

        public MainViewModel()
        {
            Products = new ObservableCollection<Product>();
        }

        public void LoadProduct(IEnumerable<Product> productCollection)
        {
            foreach (var product in productCollection)
            {
                Products.Add(product);
            }
            PropertyChanged(this,new PropertyChangedEventArgs("LoadProduct"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

      
    }
}
