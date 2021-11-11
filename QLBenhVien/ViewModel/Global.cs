using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBenhVien.ViewModel
{
    static class Global
    {
        public static int globalId { get; private set; }
        public static void setGlobalId(int IdPrescription)
        {
            globalId = IdPrescription;
        }

        public static string globalText { get; private set; }
        public static void setGlobalText(string text)
        {
            globalText = text;
        }
    }
}
