using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.Models.cHeatCalculation
{
    public class cWall : INotifyPropertyChanged
    {
        private List<cLayerFlux> _lf;
        public List<cLayerFlux> lf
        {
            get { return _lf; }
            set
            {
                _lf = value;
                OnPropertyChanged();
            }
        }

        public int CountLayers { get { return lf.Count; } }

        public double GetResistance()
        {
            if (CountLayers == 1)
                return lf[0].GetResistance();

            else if (CountLayers == 2)
                return lf[0].GetResistance() + lf[1].GetResistance();

            return 0;
        }

        //Для метода GetT
        public double GetResistance(int CountLayers)
        {
            return lf[0].GetResistance();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
