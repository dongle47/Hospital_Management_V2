using QLBenhVien.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLBenhVien.ViewModel
{
    public class MedicineViewModel:BaseViewModel
    {
        private ObservableCollection<Medicine> _List;
        public ObservableCollection<Medicine> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Medicine _SelectedItem;
        public Medicine SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Description = SelectedItem.Description;
                    Price = SelectedItem.Price;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Description;
        public string Description { get => _Description; set { _Description = value; OnPropertyChanged(); } }

        private decimal _Price;
        public decimal Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public MedicineViewModel()
        {
            List = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }

                var displayList = DataProvider.Ins.DB.Medicines.Where(x => x.DisplayName == DisplayName);
                if (displayList.Count() == 0)
                {
                    return true;
                }
                return false;
            },
            (p) =>
            {
                var Medicine = new Medicine()
                {
                    DisplayName = DisplayName,
                    Description=Description,
                    Price=Price,
                };

                DataProvider.Ins.DB.Medicines.Add(Medicine);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Medicine);
            }
            );

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.Medicines.Where(x => x.DisplayName == DisplayName);
                string newDescr = DataProvider.Ins.DB.Medicines.Where(x => x.DisplayName == DisplayName).Select(x=>x.Description).SingleOrDefault();

                if (displayList.Count() != 0 && newDescr == Description)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Medicine = DataProvider.Ins.DB.Medicines.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Medicine.DisplayName = DisplayName;
                Medicine.Description = Description;
                Medicine.Price = Price;

                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
            }
            );
        }
    }
}
