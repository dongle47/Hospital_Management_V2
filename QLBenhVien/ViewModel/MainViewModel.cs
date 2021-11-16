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
        public ICommand PatientCommand { get; set; }
        public ICommand MedicalRecordCommand { get; set; }
        public ICommand BHYTCommand { get; set; }
        public ICommand SickCommand { get; set; }
        public ICommand MedicineCommand { get; set; }
        public ICommand LocationCommand { get; set; }
        public ICommand PrescriptionCommand { get; set; }
        public ICommand BillCommand { get; set; }
        public ICommand PrintCommand { get; set; }

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

            PatientCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                PatientWindow f = new PatientWindow();
                f.ShowDialog();
            }
            );

            LocationCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LocationWindow f = new LocationWindow();
                f.ShowDialog();
            }
            );

            MedicineCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MedicineWindow f = new MedicineWindow();
                f.ShowDialog();
            }
            );

            SickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                SickWindow f = new SickWindow();
                f.ShowDialog();
            }
            );

            MedicalRecordCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MedicalRecordWindow f = new MedicalRecordWindow();
                f.ShowDialog();
            }
            );

            BHYTCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                BHYTWindow f = new BHYTWindow();
                f.ShowDialog();
            }
            );

            PrescriptionCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                PrescriptionWindow f = new PrescriptionWindow();
                f.ShowDialog();
            }
            );

            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                BillWindow f = new BillWindow();
                f.ShowDialog();
            }
            );

            PrintCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MainWindowPatient f = new MainWindowPatient();
                f.ShowDialog();
            }
            );
        }
    }
}
