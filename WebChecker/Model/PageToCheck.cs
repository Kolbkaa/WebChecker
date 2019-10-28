using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChecker.Model
{
    class PageToCheck
    {
        private readonly string _webURL;
        private readonly string _webNameProductPosition;
        private readonly string _webPriceProductPosition;
        private Dictionary<string, Product> _productDictrionary;
        private Queue<string> _linkToCheck;
        private List<string> _linkChecked;
        public PageToCheck(string webURL, string nameProductPostion, string priceProductPosition)
        {
            _webURL = webURL;
            _webNameProductPosition = nameProductPostion;
            _webPriceProductPosition = priceProductPosition;

            _productDictrionary = new Dictionary<string, Product>();
            _linkChecked = new List<string>();
            _linkToCheck = new Queue<string>();

            _linkToCheck.Enqueue(webURL);
        }

        public void Check()
        {
            do
            {
                var link = _linkToCheck.Dequeue();
                if (!_linkChecked.Contains(link))
                {
                    WebCheck webCheck = new WebCheck(link);
                    var LinkList = webCheck.FindLinkOnWeb();
                    //dopisać metodę która będzie obrabiać linki
                    foreach (var l in LinkList)
                    {
                        if (!_linkChecked.Contains(l))
                        {
                            _linkToCheck.Enqueue(l);
                        }
                    }
                    var product = webCheck.FindProduct(_webNameProductPosition, _webPriceProductPosition);
                    if (product != null) _productDictrionary.Add(link, product);
                }
                Debug.WriteLine(_linkToCheck.Count);
            } while (_linkToCheck.Count > 0);
        }

    }
}
