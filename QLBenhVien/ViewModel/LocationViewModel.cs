using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QLBenhVien.Model;

namespace QLBenhVien.ViewModel
{
    public class LocationViewModel:BaseViewModel
    {
        private ObservableCollection<Location> _List;
        public ObservableCollection<Location> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Location _SelectedItem;
        public Location SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Price = SelectedItem.Price;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _TextSearch;
        public string TextSearch { get => _TextSearch; set { _TextSearch = value; OnPropertyChanged(); } }

        private decimal _Price;
        public decimal Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }


        public LocationViewModel()
        {
            List = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.Locations.Where(x => x.DisplayName == DisplayName);
                if (displayList.Count() != 0)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Location = new Location() 
                { 
                    DisplayName = DisplayName,
                    Price = Price,
                };

                DataProvider.Ins.DB.Locations.Add(Location);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Location);
            }
            );

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.Locations.Where(x => x.DisplayName == DisplayName);
                var newPrice = DataProvider.Ins.DB.Locations.Where(x => x.DisplayName == DisplayName).Select(x => x.Price).SingleOrDefault();

                if (displayList.Count() != 0 && newPrice == Price)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Location = DataProvider.Ins.DB.Locations.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Location.DisplayName = DisplayName;
                Location.Price = Price;

                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;

                //var LocationList = List.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                //LocationList.DisplayName = DisplayName;

                //OnPropertyChanged("List");
            }
            );

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                {
                    return false;
                }

                var check = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Location.DisplayName == DisplayName);

                if ( check.Count()!=0)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Location = DataProvider.Ins.DB.Locations.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.Locations.Remove(Location);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(Location);
                DisplayName = "";
                Price = 0;

            }
            );

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TextSearch == null)
                {
                    List = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations);
                }
                else
                {
                    List = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations.Where(x => x.DisplayName.Contains(TextSearch)));
                }
            }
            );

        }
    }
}
