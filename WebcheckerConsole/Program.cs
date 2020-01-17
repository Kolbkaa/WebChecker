using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebChecker.Database.Repository;
using WebChecker.Model;
using WebChecker.Tool;
using WebChecker.ViewModel;

namespace WebcheckerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var _websiteRepository = new WebsiteRepository();
            var _websiteCollection = _websiteRepository.GetAll();
            var _pageToCheckTaskList = new List<Task>();

            foreach (var pageToCheck in _websiteCollection.Select(website => new PageToCheck(website.MainUrl, website.NameXPath, website.PriceXPath)))
            {
                Console.WriteLine($"Sprawdzam {pageToCheck.WebUrl} ...");
                pageToCheck.AllLinkCheck += ((arg1, arg2, arg3, arg4) =>
                    Console.WriteLine(
                        $"{arg4}{Environment.NewLine} Przeszukano: {arg2}{Environment.NewLine} Produktów: {arg1}{Environment.NewLine}"));

                _pageToCheckTaskList.Add(pageToCheck.Check());
            }

            Task.WaitAll(_pageToCheckTaskList.ToArray());
        }
    }
}
