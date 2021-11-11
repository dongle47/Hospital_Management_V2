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
    class DetailBillViewModel:BaseViewModel
    {
        private ObservableCollection<QuantityMedicine> _QuantityMedicine;
        public ObservableCollection<QuantityMedicine> QuantityMedicine { get => _QuantityMedicine; set { _QuantityMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<Medicine> _Medicine;
        public ObservableCollection<Medicine> Medicine { get => _Medicine; set { _Medicine = value; OnPropertyChanged(); } }

        private int _IdMedicalRecord = Global.globalId;
        public int IdMedicalRecord { get => _IdMedicalRecord; set { _IdMedicalRecord = value; OnPropertyChanged(); } }

        private string _NamePatient = Global.globalText;
        public string NamePatient { get => _NamePatient; set { _NamePatient = value; OnPropertyChanged(); } }

        private decimal _TotalPricePrescription;
        public decimal TotalPricePrescription { get => _TotalPricePrescription; set { _TotalPricePrescription = value; OnPropertyChanged(); } }


        public ICommand LoadedWindowCommand { get; set; }

        public DetailBillViewModel()
        {
            //var IdPrescription = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == IdMedicalRecord).Select(x => x.IdPrescription).SingleOrDefault();
            //QuantityMedicine = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription));
            //Medicine = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);


            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                IdMedicalRecord = Global.globalId;
                NamePatient = Global.globalText;
                var IdPrescription = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == IdMedicalRecord).Select(x => x.IdPrescription).SingleOrDefault();
                QuantityMedicine = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription));
            }
            );
        }

    }
}
