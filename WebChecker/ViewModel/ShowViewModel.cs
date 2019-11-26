using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup.Localizer;
using WebChecker.Database.Repository;
using WebChecker.Model;

namespace WebChecker.ViewModel
{
    class ShowViewModel
    {
        private readonly ProductRepository ProductRepository;
        public ShowViewModel(Website website)
        {
            ProductRepository = new ProductRepository();

            Products = new ObservableCollection<Product>(ProductRepository.GetProductsByUrl(website.MainUrl));
        }
        public ObservableCollection<Product> Products { get; set; }
    }
}
