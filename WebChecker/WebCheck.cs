using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Model;

namespace WebChecker
{
    class WebCheck
    {
        private readonly string _pageToCheck;
        private HtmlDocument _htmlDoc;
        public WebCheck(string pageToCheck)
        {
            _pageToCheck = pageToCheck;
            HtmlWeb web = new HtmlWeb();
            LoadWebPage(pageToCheck, web);
            
        }

        private void LoadWebPage(string pageToCheck, HtmlWeb web)
        {
            _htmlDoc = web.Load(pageToCheck);
        }

        public List<string> FindLinkOnWeb()
        {
            if (_htmlDoc != null)
            {
                var date = _htmlDoc.DocumentNode?.SelectNodes("//a")?.Select(x => x.Attributes)?.Select(x => x.Select(z => z)?.Where(z => z.Name == "href")).Select(y => y.Select(z => z.Value).First()).ToList();
                return date;
            }
            return null;
        }
        public Product FindProduct(string xPathName, string xPathPrice)
        {
            var price = _htmlDoc.DocumentNode?.SelectNodes(xPathPrice)?.First().InnerText;
            var name = _htmlDoc.DocumentNode?.SelectNodes(xPathName)?.First().InnerText;
            return (!string.IsNullOrWhiteSpace(price) && !string.IsNullOrWhiteSpace(name)) ? new Product(name.Trim(), price.Trim()) : null;
        }
    }
}
