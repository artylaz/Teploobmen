using Laboratory_4_SQLite.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.ViewModels
{
    class ChartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DotOnChart> Dots { get; set; }
        public SeriesCollection Series { get; set; }
        public ChartValues<double> PointsGetQ { get; set; }
        public ChartValues<double> PointsTemperature { get; set; }

        private DotOnChart selectedItem;
        public DotOnChart SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand removeDotCommand;
        public RelayCommand RemoveDotCommand
        {
            get
            {
                return removeDotCommand ??
                    (removeDotCommand = new RelayCommand(obj =>
                    {
                        if (obj is DotOnChart dot)
                        {
                            Dots.Remove(dot);
                            PointsGetQ.Remove(dot.GetQ);
                            PointsTemperature.Remove(dot.OutMediumCalcTemperature);

                        }
                    },
                    (obj) => Dots.Count > 0));
            }
        }

        private RelayCommand clearDotsCommand;
        public RelayCommand ClearDotsCommand
        {
            get
            {
                return clearDotsCommand ??
                    (clearDotsCommand = new RelayCommand(obj =>
                    {
                        Dots.Clear();
                        PointsGetQ.Clear();
                        PointsTemperature.Clear();
                    },
                    (obj) => Dots.Count > 0));
            }
        }


        public ChartViewModel()
        {
            Dots = new ObservableCollection<DotOnChart>
            {

            };
            Series = new SeriesCollection
            {
                new LineSeries
                {

                    Values = PointsGetQ = new ChartValues<double>
                    {

                    },
                     Title = "Теплового поток"

                },
                new LineSeries
                {
                    Values = PointsTemperature = new ChartValues<double>
                    {

                    },
                    Title = "Температура окружающей среды"
                }
            };

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
