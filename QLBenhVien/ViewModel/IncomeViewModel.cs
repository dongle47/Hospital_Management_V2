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

        private ObservableCollection<StatisIncome> _StatisIncome = new ObservableCollection<StatisIncome>();
        public ObservableCollection<StatisIncome> StatisIncome { get => _StatisIncome; set { _StatisIncome = value; OnPropertyChanged(); } }
        
        private ObservableCollection<Income> _ListIncome = new ObservableCollection<Income>();
        public ObservableCollection<Income> ListIncome { get => _ListIncome; set { _ListIncome = value; OnPropertyChanged(); } }

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

        private string _StatusStatis;
        public string StatusStatis { get => _StatusStatis; set { _StatusStatis = value; OnPropertyChanged(); } }

        private DateTime _DateStart = DateTime.Now;
        public DateTime DateStart { get => _DateStart; set { _DateStart = value; OnPropertyChanged(); } }

        private DateTime _DateEnd = DateTime.Now;
        public DateTime DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }

        private int _flagDate = -1;
        public int flagDate { get => _flagDate; set { _flagDate = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; set; }
        public ICommand FlagDateCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand CheckDateCommand { get; set; }

        public IncomeViewModel()
        {
            List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.OrderByDescending(x=>x.Id));

            ListIncome = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes);
            StatisIncome = new ObservableCollection<StatisIncome>();

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.OrderByDescending(x => x.Id));
                StatisIncome.Clear();


            }
            );

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListDates = new List<DateTime>();
                for (var dt = DateStart; dt <= DateEnd; dt = dt.AddDays(1))
                {
                    ListDates.Add(dt);
                }
       
                if (TextSearch == null || string.IsNullOrEmpty(TextSearch))
                {
                    StatisIncome.Clear();
                    if (flagDate < 0)
                    {
                        StatusStatis = "Không thể thống kê";
                        List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.OrderByDescending(x=>x.Id));
                    }
                    else if ( DateStart == DateEnd)
                    {
                        StatusStatis = "Thống kê các giao dịch trong ngày";
                        DateTime date2 = DateStart.AddDays(1);
                        List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x=>x.Date >= DateStart).Where(x=>x.Date <= date2).OrderByDescending(x => x.Id));
                        foreach (var itemIncome in List)
                        {
                            StatisIncome.Add(new StatisIncome() { DisplayDate = "Mã GD:" + itemIncome.Id + " TG: "+ Convert.ToDateTime( itemIncome.Date).ToString("hh:mm:ss"), Count = (long)itemIncome.Money });
                        }
                    }
                    else
                    {
                        StatusStatis = "Thống kê theo ngày";

                        List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.Date >= DateStart).Where(x => x.Date <= DateEnd).OrderByDescending(x=>x.Id));
                        ListIncome = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.Date >= DateStart).Where(x => x.Date <= DateEnd).OrderBy(x=>x.Id));
                        
                        foreach (var itemDate in ListDates)
                        {
                            string date = itemDate.ToString("dd-MM-yyyy");
                            long count = 0;
                            foreach (var itemIncome in ListIncome)
                            {
                                if (itemDate.ToString("dd-MM-yyyy") == Convert.ToDateTime(itemIncome.Date).ToString("dd-MM-yyyy"))
                                {
                                    count += (long)itemIncome.Money;
                                }
                            }
                            StatisIncome.Add(new StatisIncome() { DisplayDate = date, Count = count });
                        }
      
                    }
                }
                else
                {
                    StatisIncome.Clear();
                    int idSearch = Convert.ToInt32(TextSearch);
                    if (flagDate < 0)
                    {
                        StatusStatis = "Thống kê theo số giao dịch";

                        List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.IdMR == idSearch).OrderByDescending(x=>x.Id));

                        // thống kê theo số giao dịch
                        foreach (var itemIncome in List)
                        {
                            StatisIncome.Add(new StatisIncome() { DisplayDate = "Mã GD:" + itemIncome.Id + " Ngày: " + Convert.ToDateTime(itemIncome.Date).ToString("dd-MM-yyyy"), Count = (long)itemIncome.Money });
                        }
                    }
                    else if(DateStart == DateEnd)
                    {
                        StatusStatis = "Thống kê trong ngày";
                        DateTime date2 = DateStart.AddDays(1);
                        List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.Date >= DateStart).Where(x => x.Date <= date2).Where(x=>x.IdMR == idSearch).OrderByDescending(x => x.Id));
                        foreach (var itemIncome in List)
                        {
                            StatisIncome.Add(new StatisIncome() { DisplayDate = "Mã GD:" + itemIncome.Id + " TG: " + Convert.ToDateTime(itemIncome.Date).ToString("hh:mm:ss"), Count = (long)itemIncome.Money });
                        }
                        //if(StatisIncome.Count == 0)
                        //{
                        //    StatusStatis = "Không có thông tin";
                        //}
                    }
                    else
                    {
                        StatusStatis = "Thống kê theo số giao dịch";

                        List = new ObservableCollection<Income>(DataProvider.Ins.DB.Incomes.Where(x => x.Date >= DateStart).Where(x => x.Date <= DateEnd).Where(x => x.IdMR == idSearch).OrderByDescending(x=>x.Id));

                        foreach (var itemIncome in List)
                        {
                            StatisIncome.Add(new StatisIncome() { DisplayDate = "Mã GD:" + itemIncome.Id + " Ngày: " + Convert.ToDateTime(itemIncome.Date).ToString("dd-MM-yyyy"), Count = (long)itemIncome.Money });
                        }

                    }
                    if (StatisIncome.Count == 0)
                    {
                        StatusStatis = "Không có thông tin";
                    }
                }

            }
            );

            CheckDateCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                flagDate = flagDate * (-1);
            }
            );

        }
    }
}
