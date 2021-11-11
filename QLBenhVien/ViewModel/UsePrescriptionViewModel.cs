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
    class UsePrescriptionViewModel:BaseViewModel
    {
        private ObservableCollection<QuantityMedicine> _List;
        public ObservableCollection<QuantityMedicine> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Medicine> _Medicine;
        public ObservableCollection<Medicine> Medicine { get => _Medicine; set { _Medicine = value; OnPropertyChanged(); } }

        private int _IdPrescription = Global.globalId;
        public int IdPrescription { get => _IdPrescription; set { _IdPrescription = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }

        public UsePrescriptionViewModel()
        {
            Medicine = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            List = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == Global.globalId));

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                List = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == Global.globalId));
            }
            );

        }
    }
}
