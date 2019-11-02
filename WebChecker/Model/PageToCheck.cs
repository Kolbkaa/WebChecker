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
            var timeStart = DateTime.Now;
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
                    var product = webCheck.FindProduct(_webNameProductPosition, _webPriceProductPosition);
                    if (product != null) Product.Add(link, product);
                }
                _linkChecked.Add(link);
                //Debug.WriteLine($"LinkToCheck: {_linkToCheck.Count}, LinkChecked:{_linkChecked.Count},ProductList:{_product.Count}, LINK: {link}");

            } while (_linkToCheck.Count > 0);
            Debug.WriteLine((DateTime.Now - timeStart));
            Debug.WriteLine($"LinkToCheck: {_linkToCheck.Count}, LinkChecked:{_linkChecked.Count},ProductList:{Product.Count}");
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
