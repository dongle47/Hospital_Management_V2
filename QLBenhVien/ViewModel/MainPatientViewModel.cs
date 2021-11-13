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

        public ICommand UseCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DetailBillCommand { get; set; }


        public MainPatientViewModel()
        {
            Id = Global.globalId;

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

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Id = Global.globalId;
            }
            );
        }
    }
}
