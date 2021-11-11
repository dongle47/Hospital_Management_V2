using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBenhVien.Model
{
    class BillList
    {
        public int IdMR { get; set; }
        public string PatientName { get; set; }
        public string CodeBHYT { get; set; }
        //public decimal Reduction { get; set; }
        //public string NameLocation { get; set; }
        //public decimal PriceLocation { get; set; }

        //public DateTime DateInMR { get; set; }

        public decimal TotalFee { get; set; }
        public decimal? Owed { get; set; }
    }
}
