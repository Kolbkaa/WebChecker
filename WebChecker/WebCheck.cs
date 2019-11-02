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

        public List<string> FindLinkOnWeb()
        {

            //if (_htmlDoc != null)
            //{
            //    var at = _htmlDoc.DocumentNode?.SelectNodes("//meta").Select(x => x.Attributes)?.Select(x => x.Select(z => z)?.Where(z => z.Value == "robots")); ;
            //    //var robot = at.Select(x => x.Select(z=> z.Name));
            //}

            if (_htmlDoc == null) return null;
            var attrib = _htmlDoc.DocumentNode?.SelectNodes("//a")?.Select(x => x.Attributes);
            var hrefAttrib = attrib?.Select(x => x.Select(z => z)?.Where(z => z.Name == "href"));
            var date = hrefAttrib?.Select(y => y.Select(z => z.Value)?.FirstOrDefault()).ToList();
            return date;
        }
        public Product FindProduct(string xPathName, string xPathPrice)
        {
            var price = _htmlDoc.DocumentNode?.SelectNodes(xPathPrice)?.First().InnerText;
            var name = _htmlDoc.DocumentNode?.SelectNodes(xPathName)?.First().InnerText;
            return (!string.IsNullOrWhiteSpace(price) && !string.IsNullOrWhiteSpace(name)) ? new Product(name.Trim(), price.Trim()) : null;
        }
    }
}
