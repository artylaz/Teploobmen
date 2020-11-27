using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.Models
{
    class DotOnChart : INotifyPropertyChanged
    {
        private double getQ;
        public double GetQ
        {
            get { return getQ; }
            set
            {
                getQ = value;
                OnPropertyChanged();
            }
        }

        private double outMediumCalcTemperature;
        public double OutMediumCalcTemperature
        {
            get { return outMediumCalcTemperature; }
            set
            {
                outMediumCalcTemperature = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
