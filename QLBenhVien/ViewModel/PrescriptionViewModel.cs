using QLBenhVien.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace QLBenhVien.ViewModel
{
    class PrescriptionViewModel : BaseViewModel
    {
        private ObservableCollection<Prescription> _List;
        public ObservableCollection<Prescription> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Prescription _SelectedItem;
        public Prescription SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    DateCreated = DateCreated;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _TextSearch;
        public string TextSearch { get => _TextSearch; set { _TextSearch = value; OnPropertyChanged(); } }

        private DateTime? _DateCreated;
        public DateTime? DateCreated { get => _DateCreated; set { _DateCreated = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UseCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        public PrescriptionViewModel()
        {
            List = new ObservableCollection<Prescription>(DataProvider.Ins.DB.Prescriptions);
            DateCreated = DateTime.Now;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }

                var displayList = DataProvider.Ins.DB.Prescriptions.Where(x => x.DisplayName == DisplayName);

                if (displayList == null || displayList.Count() != 0)
                {
                    return false;
                }

                return true;
            },
            (p) =>
            {
                var Prescription = new Prescription()
                {
                    DisplayName = DisplayName,
                    DateCreated = DateCreated,
                };

                DataProvider.Ins.DB.Prescriptions.Add(Prescription);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Prescription);
            }
            );

            UseCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                Global.setGlobalId(SelectedItem.Id);
                UsePrescriptionWindow f = new UsePrescriptionWindow();
                f.ShowDialog();
            }
            );

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                Global.setGlobalId(SelectedItem.Id);
                DetailPrescriptionWindow f = new DetailPrescriptionWindow();
                f.IdPrescription.Text = SelectedItem.Id.ToString();
                f.DisplayNamePrescription.Text = SelectedItem.DisplayName.ToString();
                f.TotalPricePrescription.Text = SelectedItem.TotalPrice.ToString();

                f.ShowDialog();
            }
            );

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var checkMR = DataProvider.Ins.DB.MedicalRecords.Where(x => x.IdPrescription == SelectedItem.Id);
                var checkDP = DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == SelectedItem.Id);
                if(checkMR.Count() != 0 || checkDP.Count()!=0)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Prescrip = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.Prescriptions.Remove(Prescrip);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(Prescrip);
                DisplayName = "";
            }
            );

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TextSearch == null)
                {
                    List = new ObservableCollection<Prescription>(DataProvider.Ins.DB.Prescriptions);
                }
                else
                {
                    List = new ObservableCollection<Prescription>(DataProvider.Ins.DB.Prescriptions.Where(x => x.DisplayName.Contains(TextSearch)));
                }
            }
            );

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                List = new ObservableCollection<Prescription>(DataProvider.Ins.DB.Prescriptions);
                DateCreated = DateTime.Now;
            }
            );

        }
    }
}
