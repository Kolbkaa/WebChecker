using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WebChecker.Model
{
    class PageToCheck
    {
        private readonly string _webUrl;
        private readonly string _webNameProductPosition;
        private readonly string _webPriceProductPosition;

        public Dictionary<string, Product> Product { get; }

        private readonly Queue<string> _linkToCheck;
        private readonly List<string> _linkChecked;

        public event Action<int, int, int> OneLinkCheck;
        public event Action<int, int, int> AllLinkCheck;

        public PageToCheck(string webUrl, string nameProductPosition, string priceProductPosition)
        {
            _webUrl = webUrl.EndsWith("/") ? new string(webUrl.Take(webUrl.Length - 1).ToArray()) : webUrl;

            _webNameProductPosition = nameProductPosition;
            _webPriceProductPosition = priceProductPosition;

            Product = new Dictionary<string, Product>();
            _linkChecked = new List<string>();
            _linkToCheck = new Queue<string>();

            _linkToCheck.Enqueue(webUrl);
        }

        public void Check()
        {
            do
            {
                var link = _linkToCheck.Dequeue();

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
                    }

                    var product = webCheck.FindProduct(link, _webNameProductPosition, _webPriceProductPosition);

                    if (product != null) Product.Add(link, product);
                }
                _linkChecked.Add(link);

                OneLinkCheck?.Invoke(Product.Count, _linkChecked.Count, _linkToCheck.Count);

            } while (_linkToCheck.Count > 0);

            AllLinkCheck?.Invoke(Product.Count, _linkChecked.Count, _linkToCheck.Count);
        }
        private List<string> PrepareLink(IEnumerable<string> urlList)
        {
            var newList = new List<string>();
            foreach (var link in urlList)
            {
                if (link == null || !link.StartsWith("/")) continue;

                var stringBuilder = new StringBuilder(_webUrl);
                stringBuilder.Append(link.Split('?')?.First());

                if (stringBuilder.ToString().Contains(_webUrl))
                    newList.Add(stringBuilder.ToString());
            }
            return newList;
        }

    }
}
