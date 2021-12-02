using QLBenhVien.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBenhVien.Model
{
    class StatisIncome:BaseViewModel
    {
        string _DisplayDate = string.Empty;
        public string DisplayDate { get => _DisplayDate; set { _DisplayDate = value; OnPropertyChanged(); } }

        long _Count = 0;
        public long Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }
    }
}
