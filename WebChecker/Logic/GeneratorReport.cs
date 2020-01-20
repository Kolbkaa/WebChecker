using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Database.Repository;

namespace WebChecker.Logic
{
    class GeneratorReport
    {
        private readonly string _pageUrl;
        private int _actualListCount = 0;
        private int _earlierListCont = 0;
        private double _changeUpPercent = 0;
        private double _changeDownPercent = 0;
        private StringBuilder raportCsv;


        public GeneratorReport(string pagePath)
        {
            raportCsv = new StringBuilder($"LINK,NAZWA,DATA SPRAWDZENIA,CENA,DATA SPRAWDZENIE,CENA,PROCENT ZMIANY{Environment.NewLine}");
            _pageUrl = pagePath;
        }

        public void Generate()
        {
            var dateRepository = new DateRepository();
            var dateGap = dateRepository.TwoLastDateCheck(_pageUrl);

            if (dateGap.Length < 2)
            {
                return;
            }

            var productRepository = new ProductRepository();

            var actualList = productRepository.GetProductsByUrlFromDate(_pageUrl, dateGap[0]);
            var earlierList = productRepository.GetProductsByUrlFromDate(_pageUrl, dateGap[1]);

            var tempActualList = new Dictionary<string, Model.Product>(actualList);
            var tempEarlierList = new Dictionary<string, Model.Product>(earlierList);

            _actualListCount = actualList.Count;
            _earlierListCont = earlierList.Count;

            foreach (var productFromActualList in actualList)
            {
                if (earlierList.ContainsKey(productFromActualList.Key))
                {
                    double changePercent = (1 - (double.Parse(earlierList[productFromActualList.Key].Price) / double.Parse(productFromActualList.Value.Price))) - 100;
                    raportCsv.AppendLine($"{productFromActualList.Value.Link},{productFromActualList.Key},{earlierList[productFromActualList.Key].CheckDate},{earlierList[productFromActualList.Key].Price},{productFromActualList.Value.CheckDate},{productFromActualList.Value.Price},{changePercent}");
                
                    if(_changeUpPercent < changePercent)
                    {
                        _changeUpPercent += changePercent;
                    }

                    if(_changeDownPercent > changePercent)
                    {
                        _changeDownPercent += changePercent;
                    }

                    tempEarlierList.Remove(productFromActualList.Key);
                    tempActualList.Remove(productFromActualList.Key);
                }
              
            }
            foreach (var product in tempEarlierList)
            {
                raportCsv.AppendLine($"{product.Value.Link},{product.Value.Name},{product.Value.CheckDate},{product.Value.Price},-,-,-");

            }
            foreach (var product in tempActualList)
            {
                raportCsv.AppendLine($"{product.Value.Link},{product.Value.Name},-,-,{product.Value.CheckDate},{product.Value.Price},-");

            }
        }
    }
}
