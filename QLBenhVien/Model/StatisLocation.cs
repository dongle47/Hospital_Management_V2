using QLBenhVien.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBenhVien.Model
{
    class StatisLocation:BaseViewModel
    {
        string _DisplayName = string.Empty;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        int _Count = 0;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }
    }
}
