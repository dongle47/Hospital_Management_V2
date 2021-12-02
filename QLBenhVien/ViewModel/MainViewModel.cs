using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace QLBenhVien.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand MedicalRecordCommand { get; set; }
        public ICommand PrescriptionCommand { get; set; }
        public ICommand BillCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand IncomeCommand { get; set; }
        public ICommand StatisCommand { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                IsLoaded = true;
                if (p == null)
                {
                    return;
                }
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                {
                    return;
                }

                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            }
            );

            MedicalRecordCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MedicalRecordWindow f = new MedicalRecordWindow();
                f.Show();
                f.Close();
            }
            );

            PrescriptionCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                PrescriptionWindow f = new PrescriptionWindow();
                f.Show();
                f.Close();
            }
            );

            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                BillWindow f = new BillWindow();
                f.Show();
                f.Close();
            }
            );

            PrintCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                //PrintWindow f = new PrintWindow();
                //f.ShowDialog();
            }
            );

            IncomeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IncomeWindow f = new IncomeWindow();
                f.Show();
                f.Close();
            }
            );

            StatisCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                StatisWindow f = new StatisWindow();
                f.Show();
                f.Close();
            }
            );
        }
    }
}
