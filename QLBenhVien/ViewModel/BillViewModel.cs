using QLBenhVien.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLBenhVien.ViewModel
{
    class BillViewModel:BaseViewModel
    {
        private ObservableCollection<BillList> _List;
        public ObservableCollection<BillList> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<HospitalFee> _HospitalFee;
        public ObservableCollection<HospitalFee> HospitalFee { get => _HospitalFee; set { _HospitalFee = value; OnPropertyChanged(); } }

        private ObservableCollection<Patient> _Patient;
        public ObservableCollection<Patient> Patient { get => _Patient; set { _Patient = value; OnPropertyChanged(); } }

        private ObservableCollection<BHYT> _BHYT;
        public ObservableCollection<BHYT> BHYT { get => _BHYT; set { _BHYT = value; OnPropertyChanged(); } }

        private ObservableCollection<Location> _Location;
        public ObservableCollection<Location> Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }

        private ObservableCollection<MedicalRecord> _MedicalRecord;
        public ObservableCollection<MedicalRecord> MedicalRecord { get => _MedicalRecord; set { _MedicalRecord = value; OnPropertyChanged(); } }


        private string _Money;
        public string Money { get => _Money; set { _Money = value; OnPropertyChanged(); } }

        private BillList _SelectedItem;
        public BillList SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    var idPre = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.IdPrescription).SingleOrDefault();
                    var totalPre = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == idPre).Select(x => x.TotalPrice).SingleOrDefault();
                    var idLoca = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.IdLocation).SingleOrDefault();
                    var priceLoca = DataProvider.Ins.DB.Locations.Where(x => x.Id == idLoca).Select(x => x.Price).SingleOrDefault();
                    var dateOut = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.DateOut).SingleOrDefault();
                    var dateIn = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.DateIn).SingleOrDefault();
                    
                    if(dateOut == null)
                    {
                        dateOut = DateTime.Now;
                    }

                    if(idPre == null)
                    {
                        totalPre = 0;
                    }


                    double day = ((DateTime)dateOut - (DateTime)dateIn).TotalDays;
                    decimal totalPriceLoca = (priceLoca * (decimal)day);
                    decimal newTotalFee = totalPriceLoca + (decimal)totalPre;

                    var hospitalFee = DataProvider.Ins.DB.HospitalFees.Where(x => x.IdMedicalRecord == SelectedItem.IdMR).SingleOrDefault();
                    
                    if(hospitalFee.TotalFee == 0 && hospitalFee.Owed == 0)
                    {
                        hospitalFee.TotalFee = Math.Round(newTotalFee);
                        hospitalFee.Owed = Math.Round(newTotalFee);
                        DataProvider.Ins.DB.SaveChanges();
                    } 
                    else if(hospitalFee.TotalFee != 0 && hospitalFee.Owed != 0)
                    {
                        decimal bonus = Math.Round(newTotalFee - hospitalFee.TotalFee);
                        if(bonus != 0)
                        {
                            hospitalFee.TotalFee = Math.Round(newTotalFee);
                            //decimal fee = Math.Round((decimal)hospitalFee.Owed + bonus);
                            hospitalFee.Owed += bonus;
                            
                            DataProvider.Ins.DB.SaveChanges();
                        }
                    }
                }
            }
        }


        public ICommand EditCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DetailBillCommand { get; set; }
        public BillViewModel()
        {
            //List = new ObservableCollection<BillList>();
            //Patient = new ObservableCollection<Patient>(DataProvider.Ins.DB.Patients);
            //MedicalRecord = new ObservableCollection<MedicalRecord>(DataProvider.Ins.DB.MedicalRecords);
            //BHYT = new ObservableCollection<BHYT>(DataProvider.Ins.DB.BHYTs);
            //Location = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations);

            //var List1 = DataProvider.Ins.DB.HospitalFees
            //    .Join(DataProvider.Ins.DB.MedicalRecords,
            //          h => h.IdMedicalRecord,
            //          m => m.Id,
            //          (h, m) => new
            //          {
            //              id = m.Id,
            //              idPatine = m.IdPatient,
            //              totalFee = h.TotalFee,
            //              owed = h.Owed,
            //          }
            //          ).Take(100);

            //var List2 = List1
            //.Join(DataProvider.Ins.DB.Patients,
            //l1 => l1.idPatine,
            //pa => pa.Id,
            //(l1, pa) => new
            //{
            //    idMR = l1.id,
            //    idPa = pa.Id,
            //    namePa = pa.DisplayName,
            //    totalFee = l1.totalFee,
            //    owed = l1.owed,
            //}
            //).Take(100);

            //var List3 = List2
            //.Join(DataProvider.Ins.DB.BHYTs,
            //l2 => l2.idPa,
            //bh => bh.IdPatient,
            //(l2, bh) => new
            //{
            //    idMR = l2.idMR,
            //    namePa = l2.namePa,
            //    codeBh = bh.CodeBHYT,
            //    totalFee = l2.totalFee,
            //    owed = l2.owed,
            //}
            //).Take(100);

            //foreach (var item in List3)
            //{
            //    //idmr, paitenname, code, total, owe
            //    BillList billList = new BillList();
            //    billList.IdMR = item.idMR;
            //    billList.PatientName = item.namePa;
            //    if (item.codeBh == "0")
            //    {
            //        billList.CodeBHYT = "Hết hiệu lực";
            //    }
            //    else
            //    {
            //        billList.CodeBHYT = "Còn hiệu lực";
            //    }
            //    billList.TotalFee = item.totalFee;
            //    billList.Owed = item.owed;

            //    List.Add(billList);
            //}

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                List = new ObservableCollection<BillList>();
                Patient = new ObservableCollection<Patient>(DataProvider.Ins.DB.Patients);
                MedicalRecord = new ObservableCollection<MedicalRecord>(DataProvider.Ins.DB.MedicalRecords);
                BHYT = new ObservableCollection<BHYT>(DataProvider.Ins.DB.BHYTs);
                Location = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations);

                var List1 = DataProvider.Ins.DB.HospitalFees
                    .Join(DataProvider.Ins.DB.MedicalRecords,
                          h => h.IdMedicalRecord,
                          m => m.Id,
                          (h, m) => new
                          {
                              id = m.Id,
                              idPatine = m.IdPatient,
                              totalFee = h.TotalFee,
                              owed = h.Owed,
                          }
                          ).Take(100);

                var List2 = List1
                .Join(DataProvider.Ins.DB.Patients,
                l1 => l1.idPatine,
                pa => pa.Id,
                (l1, pa) => new
                {
                    idMR = l1.id,
                    idPa = pa.Id,
                    namePa = pa.DisplayName,
                    totalFee = l1.totalFee,
                    owed = l1.owed,
                }
                ).Take(100);

                var List3 = List2
                .Join(DataProvider.Ins.DB.BHYTs,
                l2 => l2.idPa,
                bh => bh.IdPatient,
                (l2, bh) => new
                {
                    idMR = l2.idMR,
                    namePa = l2.namePa,
                    codeBh = bh.CodeBHYT,
                    totalFee = l2.totalFee,
                    owed = l2.owed,
                }
                ).Take(100);

                foreach (var item in List3)
                {
                    //idmr, paitenname, code, total, owe
                    BillList billList = new BillList();
                    billList.IdMR = item.idMR;
                    billList.PatientName = item.namePa;
                    if (item.codeBh == "0")
                    {
                        billList.CodeBHYT = "Hết hiệu lực";
                    }
                    else
                    {
                        billList.CodeBHYT = "Còn hiệu lực";
                    }
                    billList.TotalFee = item.totalFee;
                    billList.Owed = item.owed;

                    List.Add(billList);
                }
            }
            );

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Money) || SelectedItem == null)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var hospitalFee = DataProvider.Ins.DB.HospitalFees.Where(x => x.IdMedicalRecord == SelectedItem.IdMR).SingleOrDefault();

                var idPa = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.IdPatient).SingleOrDefault();
                var reduction = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == idPa).Select(x => x.Reduction).SingleOrDefault();

                decimal reduceMoney = (Decimal.Parse(reduction) / 100)*hospitalFee.TotalFee;

                hospitalFee.TotalFee -= Math.Round(reduceMoney);
                hospitalFee.Owed = Math.Round((decimal)hospitalFee.Owed - reduceMoney - Decimal.Parse(Money));

                if(hospitalFee.Owed < 0)
                {
                    hospitalFee.Owed = 0;
                }

                DataProvider.Ins.DB.SaveChanges();
            }
            );

            DetailBillCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                Global.setGlobalId(SelectedItem.IdMR);
                var IdPatient = DataProvider.Ins.DB.MedicalRecords.Where(g => g.Id == Global.globalId).Select(y => y.IdPatient).SingleOrDefault();
                var namePatient = DataProvider.Ins.DB.Patients.Where(x => x.Id == IdPatient).Select(x => x.DisplayName).SingleOrDefault();
                Global.setGlobalText(namePatient);


                var IdPrescription = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == Global.globalId).Select(x => x.IdPrescription).SingleOrDefault();
                var totalPrescription = DataProvider.Ins.DB.Prescriptions.Where(x => x.Id == IdPrescription).Select(x => x.TotalPrice).SingleOrDefault();

                var dateOut = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.DateOut).SingleOrDefault();
                var dateIn = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.DateIn).SingleOrDefault();

                if (dateOut == null)
                {
                    dateOut = DateTime.Now;
                }

                var idLocation = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.IdMR).Select(x => x.IdLocation).SingleOrDefault();
                var priceLocation = DataProvider.Ins.DB.Locations.Where(x => x.Id == idLocation).Select(x => x.Price).SingleOrDefault();


                double TotalDay = ((DateTime)dateOut - (DateTime)dateIn).TotalDays;
                decimal totalPriceLocation = (priceLocation * (decimal)TotalDay);

                if (IdPrescription == null)
                {
                    totalPrescription = 0;
                }

                decimal totalFee = totalPriceLocation + (decimal)totalPrescription;

                var NameLocation = DataProvider.Ins.DB.Locations.Where(x => x.Id == idLocation).Select(x => x.DisplayName).SingleOrDefault();

                var hospitalFee = DataProvider.Ins.DB.HospitalFees.Where(x => x.IdMedicalRecord == SelectedItem.IdMR).SingleOrDefault();
                var reduction = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == IdPatient).Select(x => x.Reduction).SingleOrDefault();

                decimal reduceMoney = (Decimal.Parse(reduction) / 100) * hospitalFee.TotalFee;

                hospitalFee.TotalFee -= Math.Round(reduceMoney);

                DetailBillWindow f = new DetailBillWindow();
                f.TotalPricePrescription.Text = totalPrescription.ToString();
                f.TotalDayLocation.Text = Math.Round(TotalDay).ToString();
                f.TotalPriceLocation.Text = Math.Round(totalPriceLocation) .ToString();
                f.NameLocation.Text = NameLocation;
                f.ReductionPercent.Text = reduction;
                f.ReductionPrice.Text = reduceMoney.ToString();
                f.TotalHospitalFee.Text = Math.Round(totalFee).ToString();
                f.FinalFee.Text = (Math.Round(totalFee) - Math.Round(reduceMoney)).ToString();

                f.ShowDialog();
            }
            );




            //LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
            //    var L1 = DataProvider.Ins.DB.MedicalRecords.DefaultIfEmpty()
            //    .Join(DataProvider.Ins.DB.Locations.DefaultIfEmpty(),
            //          l1 => l1.IdLocation,
            //          l => l.Id,
            //          (l1, l) => new
            //          {
            //              IdMR = l1.Id,
            //              IdPre = l1.IdPrescription,
            //              LocationPrice = l.Price,
            //              DateIn = l1.DateIn,
            //              DateOut = l1.DateOut,
            //          }
            //          ).Take(100);

            //    var L2 = L1.DefaultIfEmpty()
            //    .Join(DataProvider.Ins.DB.Prescriptions.DefaultIfEmpty(),
            //          l1 => l1.IdPre,
            //          pr => pr.Id,
            //          (l1, pr) => new
            //          {
            //              IdMR = l1.IdMR,
            //              PrePrice = pr.TotalPrice,
            //              LocationPrice = l1.LocationPrice,
            //              DateIn = l1.DateIn,
            //              DateOut = l1.DateOut,
            //          }
            //          ).Take(100);

                

            //    foreach (var item in L2)
            //    {
            //        var hospitalFee = DataProvider.Ins.DB.HospitalFees.Where(x => x.IdMedicalRecord == item.IdMR).SingleOrDefault();

            //        if (hospitalFee.TotalFee == 0 && hospitalFee.Owed == 0)
            //        {
            //            DateTime newDateOut = new DateTime();
            //            decimal newPrePrice = 0;
            //            if (item.DateOut == null)
            //            {
            //                newDateOut = DateTime.Now;
            //            }
            //            else
            //            {
            //                newDateOut = (DateTime)item.DateOut;
            //            }
            //            if (item.PrePrice != null)
            //            {
            //                newPrePrice = (decimal)item.PrePrice;
            //            }
            //            double day = (newDateOut - (DateTime)item.DateIn).TotalDays;
            //            decimal totalLocationPrice = item.LocationPrice * (decimal)day;
            //            decimal TotalFee = totalLocationPrice + newPrePrice;

            //            hospitalFee.TotalFee = TotalFee;
            //            hospitalFee.Owed = TotalFee;
            //            DataProvider.Ins.DB.SaveChanges();

            //        }
            //    }
            //}
            //);




            //var List1 = DataProvider.Ins.DB.HospitalFees
            //.Join(DataProvider.Ins.DB.MedicalRecords,
            //      h => h.IdMedicalRecord,
            //      m => m.Id,
            //      (h, m) => new
            //      {
            //          id = m.Id,
            //          idPatine = m.IdPatient,
            //          totalFee = h.TotalFee,
            //          owed = h.Owed,
            //      }
            //      ).Take(100);

            //var List2 = List1
            //.Join(DataProvider.Ins.DB.Patients,
            //l1 => l1.idPatine,
            //pa => pa.Id,
            //(l1, pa) => new
            //{
            //    idMR = l1.id,
            //    idPa = pa.Id,
            //    namePa = pa.DisplayName,
            //    totalFee = l1.totalFee,
            //    owed = l1.owed,
            //}
            //).Take(100);

            //var List3 = List2
            //.Join(DataProvider.Ins.DB.BHYTs,
            //l2 => l2.idPa,
            //bh => bh.IdPatient,
            //(l2, bh) => new
            //{
            //    idMR = l2.idMR,
            //    namePa = l2.namePa,
            //    codeBh = bh.CodeBHYT,
            //    totalFee = l2.totalFee,
            //    owed = l2.owed,
            //}
            //).Take(100);

            //foreach (var item in List3)
            //{
            //    BillList billList = new BillList();
            //    billList.IdMR = item.idMR;
            //    billList.PatientName = item.namePa;
            //    billList.CodeBHYT = item.codeBh;
            //    billList.TotalFee = item.totalFee;
            //    billList.Owed = item.owed;

            //    List.Remove(billList);
            //}
            //foreach (var item in List3)
            //{
            //    //idmr, paitenname, code, total, owe
            //    BillList billList = new BillList();
            //    billList.IdMR = item.idMR;
            //    billList.PatientName = item.namePa;
            //    if (item.codeBh == "0")
            //    {
            //        billList.CodeBHYT = "Hết hiệu lực";
            //    }
            //    else
            //    {
            //        billList.CodeBHYT = "Còn hiệu lực";
            //    }
            //    billList.TotalFee = item.totalFee;
            //    billList.Owed = item.owed;

            //    List.Add(billList);
            //}



            //CreateDTCommand = new RelayCommand<Window>((p) =>
            //{
            //    if (SelectedPrescription == null)
            //    {
            //        return true;
            //    }
            //    return false;

            //},
            //(p) =>
            //{
            //    Global.setGlobalText(SelectedPatient.DisplayName);
            //    PrescriptionWindow f = new PrescriptionWindow();
            //    f.displayName.Text = "ĐT của " + Global.globalText;
            //    f.ShowDialog();

            //}
            //);
        }
    }
}
