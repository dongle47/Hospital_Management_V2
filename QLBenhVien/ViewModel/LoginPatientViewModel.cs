using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLBenhVien.ViewModel
{
    class LoginPatientViewModel:BaseViewModel
    {
        public ICommand CloseCommand { get; set; }

        public LoginPatientViewModel()
        {
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();
            }
            );
        }
    }
}
