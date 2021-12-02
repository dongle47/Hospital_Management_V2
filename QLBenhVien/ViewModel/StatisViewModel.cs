using QLBenhVien.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLBenhVien.ViewModel
{
    class StatisViewModel:BaseViewModel
    {
        private ObservableCollection<StatisSick> _StatisSick = new ObservableCollection<StatisSick>();
        public ObservableCollection<StatisSick> StatisSick { get => _StatisSick; set { _StatisSick = value; OnPropertyChanged(); } }

        private ObservableCollection<StatisBHYT> _StatisBHYT = new ObservableCollection<StatisBHYT>();
        public ObservableCollection<StatisBHYT> StatisBHYT { get => _StatisBHYT; set { _StatisBHYT = value; OnPropertyChanged(); } }

        private ObservableCollection<StatisLocation> _StatisLocation = new ObservableCollection<StatisLocation>();
        public ObservableCollection<StatisLocation> StatisLocation { get => _StatisLocation; set { _StatisLocation = value; OnPropertyChanged(); } }

        private ObservableCollection<StatisMedicine> _StatisMedicine = new ObservableCollection<StatisMedicine>();
        public ObservableCollection<StatisMedicine> StatisMedicine { get => _StatisMedicine; set { _StatisMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<MedicalRecord> _ListMR = new ObservableCollection<MedicalRecord>();
        public ObservableCollection<MedicalRecord> ListMR { get => _ListMR; set { _ListMR = value; OnPropertyChanged(); } }

        private ObservableCollection<Sick> _ListSick = new ObservableCollection<Sick>();
        public ObservableCollection<Sick> ListSick { get => _ListSick; set { _ListSick = value; OnPropertyChanged(); } }

        private ObservableCollection<BHYT> _ListBHYT = new ObservableCollection<BHYT>();
        public ObservableCollection<BHYT> ListBHYT { get => _ListBHYT; set { _ListBHYT = value; OnPropertyChanged(); } }

        private ObservableCollection<Location> _ListLocation = new ObservableCollection<Location>();
        public ObservableCollection<Location> ListLocation { get => _ListLocation; set { _ListLocation = value; OnPropertyChanged(); } }

        private ObservableCollection<Medicine> _ListMedicine = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<QuantityMedicine> _ListQM = new ObservableCollection<QuantityMedicine>();
        public ObservableCollection<QuantityMedicine> ListQM { get => _ListQM; set { _ListQM = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        public StatisViewModel()
        {
            ListMR = new ObservableCollection<MedicalRecord>(DataProvider.Ins.DB.MedicalRecords);
            ListSick = new ObservableCollection<Sick>(DataProvider.Ins.DB.Sicks);
            ListBHYT = new ObservableCollection<BHYT>(DataProvider.Ins.DB.BHYTs);
            ListLocation = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations);
            ListMedicine = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            ListQM = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines);

            StatisSick = new ObservableCollection<StatisSick>();
            StatisBHYT = new ObservableCollection<StatisBHYT>();
            StatisLocation = new ObservableCollection<StatisLocation>();
            StatisMedicine = new ObservableCollection<StatisMedicine>();

            


            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                ListMR = new ObservableCollection<MedicalRecord>(DataProvider.Ins.DB.MedicalRecords);
                ListSick = new ObservableCollection<Sick>(DataProvider.Ins.DB.Sicks);
                ListBHYT = new ObservableCollection<BHYT>(DataProvider.Ins.DB.BHYTs);
                ListLocation = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations);
                ListMedicine = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
                ListQM = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines);

                StatisSick.Clear();
                StatisBHYT.Clear();
                StatisLocation.Clear();
                StatisMedicine.Clear();
                // statis sick
                foreach (var itemSick in ListSick)
                {
                    int count = 0;
                    string nameSick = itemSick.DisplayName;
                    foreach (var itemMR in ListMR)
                    {
                        if (itemSick.DisplayName == itemMR.Sick.DisplayName)
                        {
                            count++;
                        }
                    }
                    if (count != 0)
                    {
                        StatisSick.Add(new StatisSick() { DisplayName = nameSick, Count = count });
                    }
                }

                //statis bhyt
                int countYes = 0;
                int countNo = 0;
                foreach (var item in ListBHYT)
                {
                    if(item.CodeBHYT == "0")
                    {
                        countNo++;
                    }
                    else
                    {
                        countYes++;
                    }
                }
                StatisBHYT.Add(new StatisBHYT() { Status = "Chưa có", Count = countNo });
                StatisBHYT.Add(new StatisBHYT() { Status = "Đã có", Count = countYes });

                //statis location
                foreach (var itemLocation in ListLocation)
                {
                    int count = 0;
                    string nameSick = itemLocation.DisplayName;
                    foreach (var itemMR in ListMR)
                    {
                        if (itemLocation.DisplayName == itemMR.Location.DisplayName)
                        {
                            count++;
                        }
                    }
                    if (count != 0)
                    {
                        StatisLocation.Add(new StatisLocation() { DisplayName = nameSick, Count = count });
                    }
                }

                //statis medicine
                foreach (var itemMedicine in ListMedicine)
                {
                    int count = 0;
                    string nameMedicine = itemMedicine.DisplayName;
                    foreach (var itemQM in ListQM)
                    {
                        if (itemQM.Medicine.DisplayName == itemMedicine.DisplayName)
                        {
                            count+=itemQM.Quantity;
                        }
                    }
                    if (count != 0)
                    {
                        StatisMedicine.Add(new StatisMedicine() { DisplayName = nameMedicine, Count = count });
                    }
                }

            }
            );

        }
    }
}
