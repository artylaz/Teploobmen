using Laboratory_4_SQLite.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laboratory_4_SQLite.ViewModels
{
    public class MaterialsViewModel : INotifyPropertyChanged
    {
        public ApplicationContext db;

        IEnumerable<Material> materials;
        public IEnumerable<Material> Materials
        {
            get { return materials; }
            set
            {
                materials = value;
                OnPropertyChanged();
            }
        }

        #region Команда при закрытии
        private RelayCommand closingCommand;
        public RelayCommand ClosingCommand
        {
            get
            {
                return closingCommand ??
                    (closingCommand = new RelayCommand(obj =>
                    {
                        
                    }));
            }
        }
        #endregion

        private RelayCommand editMaterialsСommand;
        public RelayCommand EditMaterialsСommand
        {
            get
            {
                return editMaterialsСommand ??
                  (editMaterialsСommand = new RelayCommand((selectedItem) =>
                  {
                      db.SaveChanges();
                  }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
