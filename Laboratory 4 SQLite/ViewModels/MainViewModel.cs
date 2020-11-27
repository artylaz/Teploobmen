using Laboratory_4_SQLite.Models;
using Laboratory_4_SQLite.Models.cHeatCalculation;
using Laboratory_4_SQLite.Views.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace Laboratory_4_SQLite.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ApplicationContext db;

        private cHeatCalculation ch;
        public cHeatCalculation Ch
        {
            get { return ch; }
            set
            {
                ch = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            db = new ApplicationContext();
            db.Materials.Load();
            Materials = db.Materials.Local.ToBindingList();

            chartVM = new ChartViewModel();

            Ch = new cHeatCalculation
            {
                WorkMediumCalc = new cMedium(),
                OutMediumCalc = new cMedium(),

                WallCalc = new cWall()
            };

            Ch.WallCalc.lf = new List<cLayerFlux>(1)
            {
                new cLayerFlux()
            };

            try
            {
                string jsonDataFromDisk = File.ReadAllText(@"cHeatCalculation.json", Encoding.UTF8);
                var myObject = JsonConvert.DeserializeObject<cHeatCalculation>(jsonDataFromDisk);

                if (myObject.WallCalc.CountLayers == 2)
                    ComboBoxSelectedItemLayers = "Два";

                Ch = myObject;

                ThicknessOne = myObject.WallCalc.lf[0].Thickness;

                if (comboBoxSelectedItemLayers == "Два")
                    ThicknessTwo = myObject.WallCalc.lf[1].Thickness;

                GetQ = String.Format("{0:f1}", myObject.GetQ());
                GetT = String.Format("{0:f1}", myObject.GetT());
            }
            catch (FileNotFoundException)
            {
                Ch.WorkMediumCalc.Temperature = 900;
                Ch.OutMediumCalc.Temperature = 20;
                Ch.WorkMediumCalc.Alfa = 1000;
                Ch.OutMediumCalc.Alfa = 20;
                ThicknessOne = 1;
                GetQ = "1278,4";
                GetT = "83,9";
            }
        }

        #region Materials Data

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

        private Material selectedMaterialFirst;
        public Material SelectedMaterialFirst
        {
            get { return selectedMaterialFirst; }
            set
            {
                selectedMaterialFirst = value;
                OnPropertyChanged();
            }
        }

        private Material selectedMaterialSecond;
        public Material SelectedMaterialSecond
        {
            get { return selectedMaterialSecond; }
            set
            {
                selectedMaterialSecond = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ComboBoxLayers

        private List<string> layers = new List<string>() { "Один", "Два" };
        public List<string> Layers
        {
            get { return layers; }
            set
            {
                layers = value;
                OnPropertyChanged();
            }
        }

        private Visibility visibilityLayerTwo;
        public Visibility VisibilityLayerTwo
        {
            get { return visibilityLayerTwo; }
            set
            {
                visibilityLayerTwo = value;

                OnPropertyChanged();
            }
        }

        private string comboBoxSelectedItemLayers;
        public string ComboBoxSelectedItemLayers
        {
            get { return comboBoxSelectedItemLayers; }
            set
            {
                comboBoxSelectedItemLayers = value;
                OnPropertyChanged();

                if (comboBoxSelectedItemLayers == "Один")
                {
                    VisibilityLayerTwo = Visibility.Hidden;

                    Ch.WallCalc.lf?.Clear();

                    Ch.WallCalc.lf = new List<cLayerFlux>(1);
                    Ch.WallCalc.lf.Add(new cLayerFlux());
                }

                if (comboBoxSelectedItemLayers == "Два")
                {
                    VisibilityLayerTwo = Visibility.Visible;

                    Ch.WallCalc.lf?.Clear();

                    Ch.WallCalc.lf = new List<cLayerFlux>(2);
                    Ch.WallCalc.lf.Add(new cLayerFlux());
                    Ch.WallCalc.lf.Add(new cLayerFlux());

                    SelectedMaterialSecond = Materials.ElementAt(0);

                }
            }
        }
        #endregion

        #region Thickness
        private double thicknessOne;
        public double ThicknessOne
        {
            get { return thicknessOne; }
            set
            {
                thicknessOne = value;
                OnPropertyChanged();
            }
        }

        private double thicknessTwo;
        public double ThicknessTwo
        {
            get { return thicknessTwo; }
            set
            {
                thicknessTwo = value;

                OnPropertyChanged();
            }
        }
        #endregion

        #region Calculate
        private double getQ;
        public string GetQ
        {
            get { return String.Format("{0:f1}", getQ); }
            set
            {
                getQ = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        private double getT;
        public string GetT
        {
            get { return String.Format("{0:f1}", getT); }
            set
            {
                getT = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        private RelayCommand calculateСommand;
        public RelayCommand CalculateСommand
        {
            get
            {
                return calculateСommand ??
                  (calculateСommand = new RelayCommand((obg) =>
                  {
                      if (comboBoxSelectedItemLayers == "Один")
                      {
                          Ch.WallCalc.lf[0].Lamda = selectedMaterialFirst.Value;
                          Ch.WallCalc.lf[0].Thickness = ThicknessOne;
                      }

                      if (comboBoxSelectedItemLayers == "Два")
                      {
                          Ch.WallCalc.lf[0].Lamda = selectedMaterialFirst.Value;
                          Ch.WallCalc.lf[0].Thickness = ThicknessOne;

                          Ch.WallCalc.lf[1].Lamda = selectedMaterialSecond.Value;
                          Ch.WallCalc.lf[1].Thickness = ThicknessTwo;
                      }

                      GetQ = Convert.ToString(Ch.GetQ());
                      GetT = Convert.ToString(Ch.GetT());

                      string jsonData = JsonConvert.SerializeObject(ch, new JsonSerializerSettings
                      {
                          ContractResolver = new CamelCasePropertyNamesContractResolver()
                      });

                      using (FileStream fs = new FileStream(@"cHeatCalculation.json", FileMode.Create))
                      {
                          byte[] array = Encoding.UTF8.GetBytes(jsonData);
                          fs.Write(array, 0, array.Length);
                      }

                  }));
            }
        }
        #endregion

        #region MaterialWindow

        private MaterialWindow materialWindow;
        private RelayCommand showMaterialWindowСommand;
        public RelayCommand ShowMaterialWindowСommand
        {
            get
            {
                return showMaterialWindowСommand ??
                  (showMaterialWindowСommand = new RelayCommand((obg) =>
                  {
                      if (!IsWindowOpen<MaterialWindow>())
                      {
                          materialWindow = new MaterialWindow();
                          var vm = new MaterialsViewModel
                          {
                              db = db,
                              Materials = db.Materials.Local.ToBindingList()

                          };
                          materialWindow.DataContext = vm;
                          materialWindow.Show();
                      }

                  }));
            }
        }
        #endregion

        #region Chart

        private ChartViewModel chartVM;

        private RelayCommand addDotCommand;
        public RelayCommand AddDotCommand
        {
            get
            {
                return addDotCommand ??
                    (addDotCommand = new RelayCommand(obg =>
                    {

                        chartVM.Dots.Add( new DotOnChart
                        {
                            GetQ = Convert.ToDouble(GetQ),
                            OutMediumCalcTemperature = Ch.OutMediumCalc.Temperature
                        });

                        chartVM.PointsGetQ.Add(Convert.ToDouble(GetQ));
                        chartVM.PointsTemperature.Add(Ch.OutMediumCalc.Temperature);
                    }));
            }
        }

        private ChartWindow chartWindow;
        private RelayCommand openChartCommand;
        public RelayCommand OpenChartCommand
        {
            get
            {
                return openChartCommand ??
                    (openChartCommand = new RelayCommand(obj =>
                    {
                        if (!IsWindowOpen<ChartWindow>())
                        {
                            chartWindow = new ChartWindow
                            {
                                DataContext = chartVM
                            };
                            chartWindow.Show();
                        }
                        
                    }));
            }
        }
        #endregion

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
