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

        public void ShowMonthChart(string name)
        {
            
            var list = ProductRepository.GetProductsByNameFromMonth(name);
            InitializeComponent();
            var lineSeries = new LineSeries();
            lineSeries.Title = name;
            var chartValues = new ChartValues<double>();
            chartValues.AddRange(list.Select(x => Convert.ToDouble(x.Price.Replace('.', ','))));
            lineSeries.Values = chartValues;

            SeriesCollection = new SeriesCollection();
            SeriesCollection.Add(lineSeries);

            Labels = list.Select(x => x.CheckDate.ToShortDateString()).ToArray();
            YFormatter = value => value.ToString("C");
            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
            OnPropertyChanged(nameof(YFormatter));

        }
        public void ShowYearChart(string name)
        {

            var list = ProductRepository.GetProductsByNameFromMonth(name);
            InitializeComponent();
            var lineSeries = new LineSeries();
            lineSeries.Title = name;
            var chartValues = new ChartValues<double>();
            chartValues.AddRange(list.Select(x => Convert.ToDouble(x.Price.Replace('.', ','))));
            lineSeries.Values = chartValues;

            SeriesCollection = new SeriesCollection();
            SeriesCollection.Add(lineSeries);

            Labels = list.Select(x => x.CheckDate.ToShortDateString()).ToArray();
            YFormatter = value => value.ToString("C");
            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
            OnPropertyChanged(nameof(YFormatter));

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
