using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Database.Repository;
using WebChecker.Tool;

namespace WebChecker.Logic
{
    class GeneratorReport : IDisposable
    {
        private readonly string _pageUrl;
        private int _actualListCount = 0;
        private int _earlierListCont = 0;
        private int _newProductCount = 0;
        private int _oldProductCont = 0;
        private double _changeUpPercent = 0;
        private double _changeDownPercent = 0;
        private readonly MemoryStream _ms;
        public MemoryStream Raport { get
            {
                _sw.Flush();
                _ms.Position = 0;
                return _ms;
            } }
        private readonly StreamWriter _sw;

        public  string RaportName { get; }

        private readonly DateRepository _dateRepository;
        private readonly ProductRepository _productRepository;


        public GeneratorReport(string pagePath)
        {
            
            _pageUrl = pagePath;
            RaportName = pagePath.GetHashCode().ToString() + ".csv";

            _ms = new MemoryStream();
            _sw = new StreamWriter(_ms, Encoding.UTF8);
            _sw.WriteLine($"\"LINK\";\"NAZWA\";\"DATA SPRAWDZENIA\";\"CENA\";\"DATA SPRAWDZENIE\";\"CENA\";\"PROCENT ZMIANY\"");

            _dateRepository = new DateRepository();
            _productRepository = new ProductRepository();
        }

        public void Generate()
        {
          
            var dateGap = _dateRepository.TwoLastDateCheck(_pageUrl);

            if (dateGap.Length < 2)
            {
                return;
            }

          
            var actualList = _productRepository.GetProductsByUrlFromDate(_pageUrl, dateGap[0]);
            var earlierList = _productRepository.GetProductsByUrlFromDate(_pageUrl, dateGap[1]);

            var tempActualList = new Dictionary<string, Model.Product>(actualList);
            var tempEarlierList = new Dictionary<string, Model.Product>(earlierList);

            _actualListCount = actualList.Count;
            _earlierListCont = earlierList.Count;

            foreach (var productFromActualList in actualList)
            {
                if (earlierList.ContainsKey(productFromActualList.Key))
                {
                    double changePercent = (1 - (double.Parse(earlierList[productFromActualList.Key].Price.Replace(".", ",")) / double.Parse(productFromActualList.Value.Price.Replace(".",",")))) * 100;
                    
                    _sw.WriteLine($"\"{productFromActualList.Value.Link}\";\"{productFromActualList.Key}\";\"{earlierList[productFromActualList.Key].CheckDate}\";\"{earlierList[productFromActualList.Key].Price}\";\"{productFromActualList.Value.CheckDate}\";\"{productFromActualList.Value.Price}\";\"{changePercent}\"");
                    
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
                 _sw.WriteLine($"\"{product.Value.Link}\";\"{product.Value.Name}\";\"{product.Value.CheckDate}\";\"{product.Value.Price}\";\"-\";\"-\";\"-\"");

            }
            foreach (var product in tempActualList)
            {
                _sw.WriteLine($"\"{product.Value.Link}\";\"{product.Value.Name}\";\"-\";\"-\";\"{product.Value.CheckDate}\";\"{product.Value.Price}\";\"-\"");

            }
            _newProductCount = tempActualList.Count;
            _oldProductCont = tempEarlierList.Count;
            
        }
     
        public string GetShortMessage()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_pageUrl);
            sb.AppendLine($"Znaleziono produktów: {_actualListCount}");
            sb.AppendLine($"Maksymalna zmiana ceny w górę: {_changeUpPercent}");
            sb.AppendLine($"Maksymalna zmiana ceny w dół: {_changeDownPercent}");
            sb.AppendLine($"Nowych produktów: {_newProductCount}");
            sb.AppendLine($"Starych produktów: {_oldProductCont}");
            return sb.ToString();
        }

        public void Dispose()
        {
            _sw.Dispose();
            Raport.Dispose();
        }
    }
}
