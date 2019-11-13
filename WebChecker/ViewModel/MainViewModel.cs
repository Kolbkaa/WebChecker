using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Annotations;
using WebChecker.Database.Repository;
using WebChecker.Model;

namespace WebChecker.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly WebsiteRepository _websiteRepository;
        public Website Website { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Website> WebsiteCollection { get; set; }
        public ObservableCollection<PageToCheck> PageToCheckCollection { get; set; }

        public MainViewModel()
        {
            _websiteRepository = new WebsiteRepository();

            Products = new ObservableCollection<Product>();
            Website = new Website();
            PageToCheckCollection = new ObservableCollection<PageToCheck>();

            var websiteFromDb = _websiteRepository.GetAll();
            WebsiteCollection = websiteFromDb != null ? new ObservableCollection<Website>(websiteFromDb) : new ObservableCollection<Website>();

        }

        public void LoadProduct(IEnumerable<Product> productCollection)
        {
            foreach (var product in productCollection)
            {
                Products.Add(product);
            }
            PropertyChanged(this, new PropertyChangedEventArgs("LoadProduct"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddWebsiteToCollection()
        {
            var website = new Website()
            {
                MainUrl = Website.MainUrl,
                CartButtonXPatch = Website.CartButtonXPatch,
                NameXPath = Website.NameXPath,
                PriceXPath = Website.PriceXPath
            };

            _websiteRepository.Add(website);
            WebsiteCollection.Add(website);
        }

        public void DeleteElement(int selectedIndex)
        {
            WebsiteCollection.RemoveAt(selectedIndex);
        }

        public void LoadEditValue(Website selectedItem)
        {
            Website = selectedItem;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Website"));
        }
        public void EditValue()
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Website"));
            _websiteRepository.Edit(Website);
            Website = new Website();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Website"));

        }
    }
}
