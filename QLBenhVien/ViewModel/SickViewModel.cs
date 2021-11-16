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
    public class SickViewModel:BaseViewModel
    {
        private ObservableCollection<Sick> _List;
        public ObservableCollection<Sick> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Sick _SelectedItem;
        public Sick SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _TextSearch;
        public string TextSearch { get => _TextSearch; set { _TextSearch = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }


        public SickViewModel()
        {
            List = new ObservableCollection<Sick>(DataProvider.Ins.DB.Sicks);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }

                var displayList = DataProvider.Ins.DB.Sicks.Where(x => x.DisplayName == DisplayName);

                if (displayList == null || displayList.Count() != 0)
                {
                    return false;
                }

                return true;
            },
            (p) =>
            {
                var Sick = new Sick()
                {
                    DisplayName = DisplayName
                };

                DataProvider.Ins.DB.Sicks.Add(Sick);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Sick);
            }
            );

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.Sicks.Where(x => x.DisplayName == DisplayName);
                if (displayList.Count() != 0)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Sick = DataProvider.Ins.DB.Sicks.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Sick.DisplayName = DisplayName;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DisplayName;
            }
            );

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                {
                    return false;
                }
                var check = DataProvider.Ins.DB.MedicalRecords.Where(x => x.IdSick == SelectedItem.Id);
                if (check.Count() != 0)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Sick = DataProvider.Ins.DB.Sicks.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                DataProvider.Ins.DB.Sicks.Remove(Sick);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(Sick);
                DisplayName = "";
            }
            );

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TextSearch == null)
                {
                    List = new ObservableCollection<Sick>(DataProvider.Ins.DB.Sicks);
                }
                else
                {
                    List = new ObservableCollection<Sick>(DataProvider.Ins.DB.Sicks.Where(x=>x.DisplayName.Contains(TextSearch)));
                }
            }
            );
        }
    }
}
