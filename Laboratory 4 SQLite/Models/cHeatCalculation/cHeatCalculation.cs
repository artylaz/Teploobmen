using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.Models.cHeatCalculation
{
    public class cHeatCalculation : INotifyPropertyChanged
    {
        private cMedium workMediumCalc;
        public cMedium WorkMediumCalc
        {
            get { return workMediumCalc; }
            set
            {
                workMediumCalc = value;
                OnPropertyChanged();
            }
        }
        private cMedium outMediumCalc;
        public cMedium OutMediumCalc
        {
            get { return outMediumCalc; }
            set
            {
                outMediumCalc = value;
                OnPropertyChanged();
            }
        }

        private cWall wallCalc;
        public cWall WallCalc
        {
            get { return wallCalc; }
            set
            {
                wallCalc = value;
                OnPropertyChanged();
            }
        }

        public double GetQ()
        {
            return (WorkMediumCalc.Temperature - OutMediumCalc.Temperature)
                / (WorkMediumCalc.GetResistance() + OutMediumCalc.GetResistance() + WallCalc.GetResistance());
        }

        public double GetQ(List<cLayerFlux> lf)
        {
            cWall WallCalc1 = new cWall();
            WallCalc1.lf = lf;
            return (WorkMediumCalc.Temperature - OutMediumCalc.Temperature)
                / (WorkMediumCalc.GetResistance() + OutMediumCalc.GetResistance() + WallCalc1.GetResistance());
        }

        public double GetT()
        {
            return WorkMediumCalc.Temperature - GetQ() * (WorkMediumCalc.GetResistance() + WallCalc.GetResistance(1));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
