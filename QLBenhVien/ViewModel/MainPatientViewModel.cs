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
    class MainPatientViewModel:BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _TextSearch;
        public string TextSearch { get => _TextSearch; set { _TextSearch = value; OnPropertyChanged(); } }

        private string _NamePatient;
        public string NamePatient { get => _NamePatient; set { _NamePatient = value; OnPropertyChanged(); } }

        private string _AddressPatient;
        public string AddressPatient { get => _AddressPatient; set { _AddressPatient = value; OnPropertyChanged(); } }

        private string _PhonePatient;
        public string PhonePatient { get => _PhonePatient; set { _PhonePatient = value; OnPropertyChanged(); } }

        private string _SexPatient;
        public string SexPatient { get => _SexPatient; set { _SexPatient = value; OnPropertyChanged(); } }

        private string _ReligionPatient;
        public string ReligionPatient { get => _ReligionPatient; set { _ReligionPatient = value; OnPropertyChanged(); } }

        private string _DateBirth;
        public string DateBirth { get => _DateBirth; set { _DateBirth = value; OnPropertyChanged(); } }

        private string _NameSick;
        public string NameSick { get => _NameSick; set { _NameSick = value; OnPropertyChanged(); } }

        private string _NameLocation;
        public string NameLocation { get => _NameLocation; set { _NameLocation = value; OnPropertyChanged(); } }

        private string _DateIn;
        public string DateIn { get => _DateIn; set { _DateIn = value; OnPropertyChanged(); } }

        private string _DateOut;
        public string DateOut { get => _DateOut; set { _DateOut = value; OnPropertyChanged(); } }

        private string _CodeBHYT;
        public string CodeBHYT { get => _CodeBHYT; set { _CodeBHYT = value; OnPropertyChanged(); } }

        private string _DateStart;
        public string DateStart { get => _DateStart; set { _DateStart = value; OnPropertyChanged(); } }

        private string _DateEnd;
        public string DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }

        public ICommand UseCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DetailBillCommand { get; set; }
        public ICommand SearchCommand { get; set; }


        public MainPatientViewModel()
        {

            UseCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var presId = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdPrescription).SingleOrDefault();
                if(presId != null)
                {
                    MessageBox.Show(""+(int)presId);
                    Global.setGlobalId((int)presId);
                    UsePrescriptionWindow f = new UsePrescriptionWindow();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Hồ sơ không có đơn thuốc");
                }
                
            }
            );

            DetailBillCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {

                //Global.setGlobalId(SelectedItem.IdMR);
                var IdPatient = DataProvider.Ins.DB.MedicalRecords.Where(g => g.Id == Id).Select(y => y.IdPatient).SingleOrDefault();
                var namePatient = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.DisplayName).SingleOrDefault();
                Global.setGlobalText(namePatient);


                var IdPrescription = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdPrescription).SingleOrDefault();
                var totalPrescription = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == IdPrescription).Select(x => x.TotalPrice).SingleOrDefault();

                var dateOut = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.DateOut).SingleOrDefault();
                var dateIn = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.DateIn).SingleOrDefault();

                if (dateOut == null)
                {
                    dateOut = DateTime.Now;
                }

                var idLocation = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdLocation).SingleOrDefault();
                var priceLocation = DataProvider.Ins.DB.Locations.Where(x => x.Id == idLocation).Select(x => x.Price).SingleOrDefault();


                double TotalDay = ((DateTime)dateOut - (DateTime)dateIn).TotalDays;
                decimal totalPriceLocation = (priceLocation * (decimal)TotalDay);

                if (IdPrescription == null)
                {
                    totalPrescription = 0;
                }

                decimal totalFee = totalPriceLocation + (decimal)totalPrescription;

                var NameLocation = DataProvider.Ins.DB.Locations.Where(x => x.Id == idLocation).Select(x => x.DisplayName).SingleOrDefault();

                var hospitalFee = DataProvider.Ins.DB.HospitalFees.Where(x => x.IdMedicalRecord == Id).SingleOrDefault();
                var reduction = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.Reduction).SingleOrDefault();

                decimal reduceMoney = (Decimal.Parse(reduction) / 100) * hospitalFee.TotalFee;

                hospitalFee.TotalFee -= Math.Round(reduceMoney);

                DetailBillWindow f = new DetailBillWindow();
                f.TotalPricePrescription.Text = totalPrescription.ToString();
                f.TotalDayLocation.Text = Math.Round(TotalDay).ToString();
                f.TotalPriceLocation.Text = Math.Round(totalPriceLocation).ToString();
                f.NameLocation.Text = NameLocation;
                f.ReductionPercent.Text = reduction;
                f.ReductionPrice.Text = reduceMoney.ToString();
                f.TotalHospitalFee.Text = Math.Round(totalFee).ToString();
                f.FinalFee.Text = (hospitalFee.TotalFee - Math.Round(reduceMoney)).ToString();

                f.ShowDialog();
            }
            );

            SearchCommand = new RelayCommand<object>((p) =>
            {
                if(TextSearch != null || TextSearch != "")
                {
                    return true;
                }
                return false;
            },
            (p) =>
            {
                Global.setGlobalId(int.Parse(TextSearch));
                Id = Global.globalId;

                var IdPatient = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdPatient).SingleOrDefault();
                var namePatient = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.DisplayName).SingleOrDefault();

                var date = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.DateOfBirth).SingleOrDefault();
                var address = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Address).SingleOrDefault();
                var phone = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Phone).SingleOrDefault();
                var sex = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Sex).SingleOrDefault();
                var religion = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.Religion).SingleOrDefault();

                var idSick = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdSick).SingleOrDefault();
                var nameSick = DataProvider.Ins.DB.Sicks.Where(x => x.Id == idSick).Select(x => x.DisplayName).SingleOrDefault();

                var idLocation = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.IdLocation).SingleOrDefault();
                var nameLocation = DataProvider.Ins.DB.Locations.Where(x => x.Id == idLocation).Select(x => x.DisplayName).SingleOrDefault();

                var dateIn = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.DateIn).SingleOrDefault();

                var dateOut = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Id).Select(x => x.DateOut).SingleOrDefault();

                var codeBHYT = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.CodeBHYT).SingleOrDefault();
                var dateStart = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.DateStart).SingleOrDefault();
                var dateEnd = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.DateEnd).SingleOrDefault();

                NamePatient = namePatient;
                DateBirth = date.ToString("dd/MM/yyyy");
                AddressPatient = address;
                PhonePatient = phone;
                SexPatient = sex;
                ReligionPatient = religion;

                NameSick = nameSick;
                NameLocation = nameLocation;
                DateIn = ((DateTime)dateIn).ToString("dd/MM/yyyy");
                if (dateOut == null)
                {
                    DateOut = "Chưa xuất viện";
                }
                else
                {
                    DateOut = ((DateTime)dateOut).ToString("dd/MM/yyyy");
                }
                if (codeBHYT == "0")
                {
                    CodeBHYT = "Chưa có";
                }
                else
                {
                    CodeBHYT = codeBHYT;
                    DateStart = ((DateTime)dateStart).ToString("dd/MM/yyyy");
                    DateEnd = ((DateTime)dateEnd).ToString("dd/MM/yyyy");
                }
            }
            );


            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Id = Global.globalId;
            }
            );
        }
    }
}
