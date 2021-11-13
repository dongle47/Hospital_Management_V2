using QLBenhVien.Model;
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
        public bool IsLogin { get; set; }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginPatientViewModel()
        {

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Login(p);
            }
            );

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();
            }
            );

        }

        void Login(Window p)
        {
            if (p == null)
            {
                return;
            }

            var accCount = DataProvider.Ins.DB.Patients.Where(x => x.Id == Id).Count();

            if (accCount > 0)
            {
                p.Close();
                Global.setGlobalId(Id);

                var IdPatient = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdPatient).SingleOrDefault();
                var NamePatient = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.DisplayName).SingleOrDefault();

                var date = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.DateOfBirth).SingleOrDefault();
                var Address = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Address).SingleOrDefault();
                var Phone = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Phone).SingleOrDefault();
                var Sex = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Sex).SingleOrDefault();
                var Religion = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Religion).SingleOrDefault();

                var IdSick = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdSick).SingleOrDefault();
                var NameSick = DataProvider.Ins.DB.Sicks.Where(x => x.Id == IdSick).Select(x => x.DisplayName).SingleOrDefault();

                var IdLocation = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdLocation).SingleOrDefault();
                var NameLocation = DataProvider.Ins.DB.Locations.Where(x => x.Id == IdLocation).Select(x => x.DisplayName).SingleOrDefault();

                var DateIn = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.DateIn).SingleOrDefault();

                var DateOut = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.DateOut).SingleOrDefault();

                var CodeBHYT = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.CodeBHYT).SingleOrDefault();
                var DateStart = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.DateStart).SingleOrDefault();
                var DateEnd = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.DateEnd).SingleOrDefault();
           
                MainWindowPatient f = new MainWindowPatient();
                f.NamePatient.Text = NamePatient;
                f.DateBirth.Text = date.ToString("dd/MM/yyyy");
                f.AddressPatient.Text = Address;
                f.PhonePatient.Text = Phone;
                f.SexPatient.Text = Sex;
                f.ReligionPatient.Text = Religion;

                f.NameSick.Text = NameSick;
                f.NameLocation.Text = NameLocation;
                f.DateIn.Text = ((DateTime)DateIn).ToString("dd/MM/yyyy");
                if(DateOut == null)
                {
                    f.DateOut.Text = "Chưa xuất viện";
                }
                else
                {
                    f.DateOut.Text = ((DateTime)DateOut).ToString("dd/MM/yyyy");
                }
                if(CodeBHYT == "0")
                {
                    f.CodeBHYT.Text = "Chưa có";
                }
                else
                {
                    f.CodeBHYT.Text = CodeBHYT;
                    f.DateStart.Text = ((DateTime)DateStart).ToString("dd/MM/yyyy");
                    f.DateEnd.Text = ((DateTime)DateEnd).ToString("dd/MM/yyyy");
                }
                                
                f.Show();
            }
            else
            {
                MessageBox.Show("Mã bệnh án không tồn tại");
            }
        }
    }
}
