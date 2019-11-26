using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Annotations;
using WebChecker.Database.Repository;

namespace WebChecker.Model
{
    class PageToCheck : INotifyPropertyChanged
    {
        public string WebUrl { get; }
        public int LinkToCheckCount => _linkToCheck.Count;
        public int LinkCheckedCount => _linkChecked.Count;
        public int AllLink => LinkToCheckCount + LinkCheckedCount;
        public int ProductCount => Product.Count;

        public StatusEnum Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }


        private readonly string _webNameProductPosition;
        private readonly string _webPriceProductPosition;

        public Dictionary<string, Product> Product { get; }

        private readonly Queue<string> _linkToCheck;
        private readonly List<string> _linkChecked;
        private StatusEnum _status;

        public event Action<int, int, int, string> OneLinkCheck;
        public event Action<int, int, int, string> AllLinkCheck;

        public PageToCheck(string webUrl, string nameProductPosition, string priceProductPosition)
        {
            WebUrl = webUrl.EndsWith("/") ? new string(webUrl.Take(webUrl.Length - 1).ToArray()) : webUrl;

            _webNameProductPosition = nameProductPosition;
            _webPriceProductPosition = priceProductPosition;

            Product = new Dictionary<string, Product>();
            _linkChecked = new List<string>();
            _linkToCheck = new Queue<string>();

            _linkToCheck.Enqueue(webUrl);
        }

        public void Check()
        {
            Task.Run(() =>
            {
                Status = StatusEnum.Sprawdzanie;
                do
                {
                    var link = _linkToCheck.Dequeue();
                    OnPropertyChanged(nameof(LinkCheckedCount));
                    OnPropertyChanged(nameof(AllLink));
                    if (!_linkChecked.Contains(link))
                    {
                        var webCheck = new WebCheck(link);
                        var linkList = webCheck.FindLinkOnWeb();

                        if (linkList != null)
                        {
                            linkList = PrepareLink(linkList);
                            foreach (var l in linkList.Where(l => !_linkChecked.Contains(l) && !_linkToCheck.Contains(l)))
                            {
                                _linkToCheck.Enqueue(l);

                            }
                            OnPropertyChanged(nameof(LinkToCheckCount));
                            OnPropertyChanged(nameof(AllLink));

                        }

                        var product = webCheck.FindProduct(_webNameProductPosition, _webPriceProductPosition, link);

                        if (product != null)
                        {
                            if (!Product.Values.Any(x => x.Name == product.Name && x.Price == product.Price))
                                Product.Add(link, product);
                            OnPropertyChanged(nameof(ProductCount));
                        }

                    }
                    _linkChecked.Add(link);

                    OneLinkCheck?.Invoke(Product.Count, _linkChecked.Count, _linkToCheck.Count, WebUrl);



                } while (_linkToCheck.Count > 0);

                AllLinkCheck?.Invoke(Product.Count, _linkChecked.Count, _linkToCheck.Count, WebUrl);
                Status = StatusEnum.Zakończono;
                var productRepository = new ProductRepository();
                productRepository.SaveAll(Product);
            });
        }
        private List<string> PrepareLink(IEnumerable<string> urlList)
        {
            var newList = new List<string>();
            foreach (var link in urlList)
            {
                if (link == null || !link.StartsWith("/")) continue;

                var stringBuilder = new StringBuilder(WebUrl);
                //stringBuilder.Append(link.Split('?')?.First());
                stringBuilder.Append(link);

                if (stringBuilder.ToString().Contains(WebUrl))
                    newList.Add(stringBuilder.ToString());
            }
            return newList;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal enum StatusEnum { Oczekiwanie, Zakończono, Sprawdzanie }
    }

}
