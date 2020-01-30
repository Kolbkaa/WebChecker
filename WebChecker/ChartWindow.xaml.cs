using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using WebChecker.Annotations;
using WebChecker.Database.Repository;
using WebChecker.Model;

namespace WebChecker
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window, INotifyPropertyChanged
    {
        public readonly ProductRepository ProductRepository;

        public ChartWindow()
        {
            ProductRepository = new ProductRepository();
            DataContext = this;
        }
        public void PrepareChart(IEnumerable<Product> list, string name)
        {


            MinPrice = list.Select(x => x.Price.GetDecimal()).Min().ToString();
            MaxPrice = list.Select(x => x.Price.GetDecimal()).Max().ToString();
            CalculateChangePercent(list);
            InitializeComponent();
            var lineSeries = new LineSeries();
            lineSeries.Title = name;
            var chartValues = new ChartValues<decimal>();
            chartValues.AddRange(list.Select(x => x.Price.GetDecimal().Value));
            lineSeries.Values = chartValues;

            SeriesCollection = new SeriesCollection();
            SeriesCollection.Add(lineSeries);

            Labels = list.Select(x => x.CheckDate.ToShortDateString()).ToArray();
            YFormatter = value => value.ToString("C");
            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
            OnPropertyChanged(nameof(YFormatter));
            OnPropertyChanged(nameof(MaxPrice));
            OnPropertyChanged(nameof(MinPrice));

        }

        private void CalculateChangePercent(IEnumerable<Product> list)
        {
            var lastTwoPrice = list.OrderByDescending(x => x.CheckDate).Take(2).Select(x => x.Price).ToArray();
            if (lastTwoPrice.Length == 2)
            {
                var priceOne = lastTwoPrice[0].GetDecimal();
                var priceTwo = lastTwoPrice[1].GetDecimal();
                ChangePercent = (((priceOne - priceTwo) / priceTwo)).Value.ToString("p");
                return;
            }

            ChangePercent = "0%";
        }

        public void ShowYearChart(string name, string link)
        {
            var list = ProductRepository.GetProductsByNameAndLinkFromYear(name,link);
            PrepareChart(list, name);
        }
        public void ShowMonthChart(string name,string link)
        {
            var list = ProductRepository.GetProductsByNameAndLinkFromMonth(name,link);
            PrepareChart(list, name);
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string ChangePercent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
