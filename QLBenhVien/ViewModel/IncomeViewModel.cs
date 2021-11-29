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
    class IncomeViewModel:BaseViewModel
    {
        private ObservableCollection<Income> _List;
        public ObservableCollection<Income> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Income _SelectedItem;
        public Income SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        private string _TextSearch;
        public string TextSearch { get => _TextSearch; set { _TextSearch = value; OnPropertyChanged(); } }

        private DateTime _DateStart = DateTime.Now;
        public DateTime DateStart { get => _DateStart; set { _DateStart = value; OnPropertyChanged(); } }

        private DateTime _DateEnd = DateTime.Now;
        public DateTime DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }


        public ICommand SearchMrCommand { get; set; }
        public ICommand SearchDate { get; set; }
        public ICommand LoadedWindowCommand { get; set; }


        public IncomeViewModel()
        {
            List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.OrderByDescending(x=>x.Id));

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                
            }
            );

            SearchMrCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TextSearch == null || TextSearch == "")
                {
                    List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes);
                }
                else
                {
                    int idSearch = Convert.ToInt32(TextSearch);
                    List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.IdMR == idSearch));
                }
            }
            );

            SearchDate = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(TextSearch != null)
                {
                    int idSearch = Convert.ToInt32(TextSearch);
                    var tList = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.Id == idSearch));
                    List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.Date >= DateStart).Where(x=>x.Date <= DateEnd).Where(x=>x.IdMR == idSearch));

                }
                else
                {
                    List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.OrderByDescending(x => x.Id).Where(x => x.Date >= DateStart).Where(x => x.Date <= DateEnd));
                }
            }
            );

        }
    }
}
