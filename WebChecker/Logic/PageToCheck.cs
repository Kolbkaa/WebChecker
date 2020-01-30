using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebChecker.Annotations;
using WebChecker.Database.Repository;
using WebChecker.Logic;
using WebChecker.Tool;

namespace WebChecker.Model
{
    public class PageToCheck : INotifyPropertyChanged
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

        public async Task Check()
        {
            await Task.Run(() =>
            {
                //Status = StatusEnum.Sprawdzanie;
                //do
                //{
                //    var link = _linkToCheck.Dequeue();
                //    OnPropertyChanged(nameof(LinkCheckedCount));
                //    OnPropertyChanged(nameof(AllLink));
                //    if (!_linkChecked.Contains(link))
                //    {
                                       

                //        var webCheck = new WebCheck(link);
                //        var linkList = webCheck.FindLinkOnWeb();

                //        if (linkList != null)
                //        {
                //            linkList = PrepareLink(linkList);
                //            foreach (var l in linkList.Where(l => !_linkChecked.Contains(l) && !_linkToCheck.Contains(l)))
                //            {
                //                _linkToCheck.Enqueue(l);

                //            }
                //            OnPropertyChanged(nameof(LinkToCheckCount));
                //            OnPropertyChanged(nameof(AllLink));

                //        }

                //        var product = webCheck.FindProduct(_webNameProductPosition, _webPriceProductPosition, link);

                //        if (product != null)
                //        {
                //            if (!Product.Values.Any(x => x.Name == product.Name && x.Price == product.Price))
                //                Product.Add(link, product);
                //            OnPropertyChanged(nameof(ProductCount));
                //        }

                //    }
                //    _linkChecked.Add(link);

                //    OneLinkCheck?.Invoke(Product.Count, _linkChecked.Count, _linkToCheck.Count, WebUrl);



                //} while (_linkToCheck.Count > 0);

                //AllLinkCheck?.Invoke(Product.Count, _linkChecked.Count, _linkToCheck.Count, WebUrl);
                //Status = StatusEnum.Zakończono;
                var productRepository = new ProductRepository();
                productRepository.SaveAll(Product);
                var raport = new GeneratorReport(WebUrl);
                raport.Generate();
                var mail = new SendMail();
                mail.SendReport(raport.GetShortMessage(), raport.Raport, raport.RaportName, WebUrl);
            });
        }
        private List<string> PrepareLink(IEnumerable<string> urlList)
        {
            string newLink;
            var newList = new List<string>();
            foreach (var link in urlList)
            {
                newLink = link;
                if (newLink != null && newLink.StartsWith("/"))
                {
                    var stringBuilder = new StringBuilder(WebUrl);


                    stringBuilder.Append(newLink);
                    newLink = stringBuilder.ToString();
                }
                if (newLink.Contains(WebUrl))
                    newList.Add(newLink);
            }
            return newList;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum StatusEnum { Oczekiwanie, Zakończono, Sprawdzanie }
    }

}
