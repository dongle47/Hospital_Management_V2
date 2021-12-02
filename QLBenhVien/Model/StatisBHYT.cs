using QLBenhVien.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBenhVien.Model
{
    class StatisBHYT:BaseViewModel
    {
        string _Status = string.Empty;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        int _Count = 0;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }
    }
}
