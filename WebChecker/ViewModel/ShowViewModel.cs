using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup.Localizer;
using WebChecker.Annotations;
using WebChecker.Database.Repository;
using WebChecker.Model;

namespace WebChecker.ViewModel
{
    class ShowViewModel: INotifyPropertyChanged
    {
        private readonly ProductRepository ProductRepository;
        private readonly Website _website;
        public ShowViewModel(Website website)
        {
            ProductRepository = new ProductRepository();
            this._website = website;
            LoadProductList();
        }

        public void LoadProductList()
        {
            if (string.IsNullOrWhiteSpace(Filter))
            {
                Products = new ObservableCollection<Product>(ProductRepository.GetProductsByUrl(_website.MainUrl));
            }
            else
            {
                Products = new ObservableCollection<Product>(ProductRepository.GetProductsByUrl(_website.MainUrl).Where(x=> x.Name == Filter));
                OnPropertyChanged(nameof(Products));
            }
        }

        public ObservableCollection<Product> Products { get; set; }
        public string Filter { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
