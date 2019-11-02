using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Model;

namespace WebChecker
{
    internal class WebCheck
    {
        private readonly string _pageToCheck;
        private HtmlDocument _htmlDoc;
        public WebCheck(string pageToCheck)
        {
            _pageToCheck = pageToCheck;

            var web = new HtmlWeb();

            LoadWebPage(pageToCheck, web);

        }

        private void LoadWebPage(string pageToCheck, HtmlWeb web)
        {
            _htmlDoc = web.Load(pageToCheck);
        }

        public IEnumerable<string> FindLinkOnWeb()
        {

            if (_htmlDoc == null) return null;

            //var attrib = _htmlDoc.DocumentNode?.SelectNodes("//a")?.Select(x => x.Attributes);
            //var hrefAttrib = attrib?.Select(x => x.Select(z => z)?.Where(z => z.Name == "href"));
            //var date = hrefAttrib?.Select(y => y.Select(z => z.Value)?.FirstOrDefault()).ToList();

            var attrib = _htmlDoc.DocumentNode?.SelectNodes("//a")
                ?.Select(x => x.Attributes.SingleOrDefault(z => z.Name == "href"))?.Where(x=>x != null)?.Select(x=>x.Value).ToList();

            //var hrefAttrib = attrib?.Select(x => x.Select(z => z)?.Where(z => z.Name == "href"));
            //var date = hrefAttrib?.Select(y => y.Select(z => z.Value)?.FirstOrDefault()).ToList();
            return attrib;
        }
        public Product FindProduct(string xPathName, string xPathPrice)
        {
            var price = _htmlDoc.DocumentNode?.SelectNodes(xPathPrice)?.First().InnerText;
            var name = _htmlDoc.DocumentNode?.SelectNodes(xPathName)?.First().InnerText;
            return (!string.IsNullOrWhiteSpace(price) && !string.IsNullOrWhiteSpace(name)) ? new Product(name.Trim(), price.Trim()) : null;
        }
    }
}
