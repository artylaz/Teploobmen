using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.Models.cHeatCalculation
{
    public class cLayerFlux : INotifyPropertyChanged
    {
        private double lamda;
        public double Lamda
        {
            get { return lamda; }
            set
            {
                lamda = value;
                OnPropertyChanged();
            }
        }
        private double thickness;
        public double Thickness
        {
            get { return thickness; }
            set
            {
                thickness = value;
                OnPropertyChanged();
            }
        }

        public double GetResistance()
        {
            return Thickness / Lamda;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
