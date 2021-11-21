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
    class DetailPrescriptionViewModel:BaseViewModel
    {
  
        private ObservableCollection<QuantityMedicine> _List;
        public ObservableCollection<QuantityMedicine> List { get => _List;set{_List = value; OnPropertyChanged();} }

        private ObservableCollection<Medicine> _Medicine;
        public ObservableCollection<Medicine> Medicine { get => _Medicine; set { _Medicine = value; OnPropertyChanged(); } }

        private QuantityMedicine _SelectedItem;
        public QuantityMedicine SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    IdQuantityMedicine = SelectedItem.Id;
                    QuantityInPrescription = SelectedItem.Quantity;
                }
                
            }
        }

        private Medicine _SelectedMedicine;
        public Medicine SelectedMedicine
        {
            get => _SelectedMedicine;
            set
            {
                _SelectedMedicine = value;
                OnPropertyChanged();
                if (SelectedMedicine != null)
                {
                    DisplayNameMedicine = SelectedMedicine.DisplayName;
                    PriceMedicine = SelectedMedicine.Price;
                }
            }
        }

        private int _IdPrescription = Global.globalId ;
        public int IdPrescription { get => _IdPrescription; set { _IdPrescription = value; OnPropertyChanged(); } }

        private decimal _TotalPricePrescription;
        public decimal TotalPricePrescription { get => _TotalPricePrescription; set { _TotalPricePrescription = value; OnPropertyChanged(); } }

        private int _IdQuantityMedicine;
        public int IdQuantityMedicine { get => _IdQuantityMedicine; set { _IdQuantityMedicine = value; OnPropertyChanged(); } }

        private string _DisplayNameMedicine;
        public string DisplayNameMedicine { get => _DisplayNameMedicine; set { _DisplayNameMedicine = value; OnPropertyChanged(); } }

        private string _DisplayNamePrescription;
        public string DisplayNamePrescription { get => _DisplayNamePrescription; set { _DisplayNamePrescription = value; OnPropertyChanged(); } }

        private int _Quantity;
        public int Quantity { get => _Quantity; set { _Quantity = value; OnPropertyChanged(); } }

        private int _QuantityInPrescription;
        public int QuantityInPrescription { get => _QuantityInPrescription; set { _QuantityInPrescription = value; OnPropertyChanged(); } }

        private decimal _PriceMedicine;
        public decimal PriceMedicine { get => _PriceMedicine; set { _PriceMedicine = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }


        public DetailPrescriptionViewModel()
        {
            Medicine = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            List = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription));

            AddCommand = new RelayCommand<QuantityMedicine>((p) =>
            {
                var checkSameMedicine = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription && x.IdMedicine == SelectedMedicine.Id);
                if (SelectedMedicine == null || checkSameMedicine.Count() > 0 || Quantity == 0 )
                {
                    return false;
                }
                return true;

            },
            (p) =>
            {
                if(Quantity == null)
                {
                    Quantity = 0;
                }
                var QuantityMedicine = new Model.QuantityMedicine()
                {
                    IdPrescription = IdPrescription,
                    IdMedicine = SelectedMedicine.Id,
                    Price = PriceMedicine * Quantity,
                    Quantity = Quantity
                };

                DataProvider.Ins.DB.QuantityMedicines.Add(QuantityMedicine);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(QuantityMedicine);

                var sumPrice = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription).Sum(x => x.Price);
                TotalPricePrescription = sumPrice;

                var Prescription = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == IdPrescription).SingleOrDefault();
                Prescription.TotalPrice = TotalPricePrescription;

                DataProvider.Ins.DB.SaveChanges();
            }
            );

            EditCommand = new RelayCommand<QuantityMedicine>((p) =>
            {
                var displayList = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.Id == SelectedItem.Id);
                var checkName = DataProvider.Ins.DB.Prescriptions.Where(x => x.DisplayName == DisplayNamePrescription);
                if ((displayList != null && displayList.Count() != 0) || (checkName != null && checkName.Count()!=0))
                {
                    return true;
                }
                return false;
            },
            (p) =>
            {
                if(SelectedItem == null)
                {
                    var Prescription = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == IdPrescription).SingleOrDefault();
                    Prescription.DisplayName = DisplayNamePrescription;
                    DataProvider.Ins.DB.SaveChanges();
                }
                else
                {
                    decimal oldPrice = SelectedItem.Price / SelectedItem.Quantity;
                    var QuantityMedicine = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    var Prescription = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == IdPrescription).SingleOrDefault();

                    QuantityMedicine.Price = oldPrice * QuantityInPrescription;
                    QuantityMedicine.Quantity = QuantityInPrescription;


                    var sumPrice = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription).Sum(x => x.Price);
                    TotalPricePrescription = sumPrice;

                    Prescription.DisplayName = DisplayNamePrescription;
                    Prescription.TotalPrice = sumPrice;
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            );

            DeleteCommand = new RelayCommand<QuantityMedicine>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                return false;
            },
            (p) =>
            {
                var QuantityMedicine = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.QuantityMedicines.Remove(QuantityMedicine);
                DataProvider.Ins.DB.SaveChanges();

                try
                {
                    var sumPrice = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription).Sum(x => x.Price);
                    TotalPricePrescription = sumPrice;
                }
                catch
                {
                    TotalPricePrescription = 0;
                }
                

                List.Remove(QuantityMedicine);

                var Prescription = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == IdPrescription).SingleOrDefault();
                Prescription.TotalPrice = TotalPricePrescription;
                DataProvider.Ins.DB.SaveChanges();
            }
            );

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                List = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription));
            }
            );

        }
    }
}
