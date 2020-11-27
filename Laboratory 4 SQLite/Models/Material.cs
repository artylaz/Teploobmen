using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.Models
{
    public class Material : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        private double value;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public double Value
        {
            get { return value; }
            set
            {
                this.value = value;
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
