using QLBenhVien.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBenhVien.Model
{
    class BillList:BaseViewModel
    {
        public int IdMR { get; set; }

        string _PatientName;
        public string PatientName { get => _PatientName; set { _PatientName = value; OnPropertyChanged(); } }

        string _CodeBHYT;
        public string CodeBHYT { get => _CodeBHYT; set { _CodeBHYT = value; OnPropertyChanged(); } }

        decimal _TotalFee;
        public decimal TotalFee { get => _TotalFee; set { _TotalFee = value; OnPropertyChanged(); } }

        decimal? _Owed;
        public decimal? Owed { get => _Owed; set { _Owed = value; OnPropertyChanged(); } }
    }
}
