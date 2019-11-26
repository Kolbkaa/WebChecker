using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private DateTime _checkDate;
        public WebCheck(string pageToCheck)
        {
            _pageToCheck = pageToCheck;
            _checkDate = DateTime.Now;

            var web = new HtmlWeb();
            web.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";

            LoadWebPage(pageToCheck, web);

        }

        private void LoadWebPage(string pageToCheck, HtmlWeb web)
        {
            try
            {
                _htmlDoc = web.Load(pageToCheck);
            }
            catch (Exception e)
            {
                Debug.Print($"{e.Message}, { pageToCheck}");
            }
            
        }

        public IEnumerable<string> FindLinkOnWeb()
        {
            var linkOnWeb = _htmlDoc?.DocumentNode?.SelectNodes("//a")
                ?.Select(x => x.Attributes.SingleOrDefault(z => z.Name == "href"))?.Where(x => x != null)?.Select(x => x.Value).ToList();

            return linkOnWeb;
        }
        public Product FindProduct(string xPathName, string xPathPrice, string link)
        {
            var pathName = xPathName;
            var pricePrice = xPathPrice;
            var price = _htmlDoc?.DocumentNode?.SelectNodes(pricePrice)?.First().InnerText;
            var name = _htmlDoc?.DocumentNode?.SelectNodes(pathName)?.First().InnerText;

            return (!string.IsNullOrWhiteSpace(price) && !string.IsNullOrWhiteSpace(name)) ? new Product(link, name.Trim(), price.Trim(),_checkDate) : null;
        }
    }
}
