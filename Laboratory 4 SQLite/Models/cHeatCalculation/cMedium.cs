using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.Models.cHeatCalculation
{
    public class cMedium : INotifyPropertyChanged
    {
        private double temperature;
        public double Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged();
            }
        }
        private double alfa;
        public double Alfa
        {
            get { return alfa; }
            set
            {
                alfa = value;
                OnPropertyChanged();
            }
        }

        public double GetResistance()
        {
            return 1 / Alfa;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
