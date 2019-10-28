using System;
using System.Collections.Generic;
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
        public PageToCheck(string webURL, string nameProductPostion, string priceProductPosition)
        {
            _webURL = webURL;
            _webNameProductPosition = nameProductPostion;
            _webPriceProductPosition = priceProductPosition;

            _productDictrionary = new Dictionary<string, Product>();
        }

    }
}
