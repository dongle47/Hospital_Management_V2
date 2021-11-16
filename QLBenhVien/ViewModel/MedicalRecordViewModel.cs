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
    public class MedicalRecordViewModel:BaseViewModel
    {
        private ObservableCollection<MedicalRecord> _List;
        public ObservableCollection<MedicalRecord> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Patient> _Patient;
        public ObservableCollection<Patient> Patient { get => _Patient; set { _Patient = value; OnPropertyChanged(); } }

        private ObservableCollection<Sick> _Sick;
        public ObservableCollection<Sick> Sick { get => _Sick; set { _Sick = value; OnPropertyChanged(); } }

        private ObservableCollection<Prescription> _Prescription;
        public ObservableCollection<Prescription> Prescription { get => _Prescription; set { _Prescription = value; OnPropertyChanged(); } }

        private ObservableCollection<Location> _Location;
        public ObservableCollection<Location> Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }

        private MedicalRecord _SelectedItem;
        public MedicalRecord SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedPatient = SelectedItem.Patient;
                    SelectedSick = SelectedItem.Sick;
                    SelectedPrescription = SelectedItem.Prescription;
                    SelectedLocation = SelectedItem.Location;
                    DateIn = SelectedItem.DateIn;
                    DateOut = SelectedItem.DateOut;
                }
            }
        }

        private Sick _SelectedSick;
        public Sick SelectedSick
        {
            get => _SelectedSick;
            set
            {
                _SelectedSick = value;
                OnPropertyChanged();
            }
        }

        private Prescription _SelectedPrescription;
        public Prescription SelectedPrescription
        {
            get => _SelectedPrescription;
            set
            {
                _SelectedPrescription = value;
                OnPropertyChanged();
            }
        }

        private Location _SelectedLocation;
        public Location SelectedLocation
        {
            get => _SelectedLocation;
            set
            {
                _SelectedLocation = value;
                OnPropertyChanged();
            }
        }

        private Patient _SelectedPatient;
        public Patient SelectedPatient
        {
            get => _SelectedPatient;
            set
            {
                _SelectedPatient = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _DateIn;
        public DateTime? DateIn { get => _DateIn; set { _DateIn = value; OnPropertyChanged(); } }

        private DateTime? _DateOut;
        public DateTime? DateOut { get => _DateOut; set { _DateOut = value; OnPropertyChanged(); } }

        private string _TextSearch;
        public string TextSearch { get => _TextSearch; set { _TextSearch = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand CreateDTCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public MedicalRecordViewModel()
        {

            AddCommand = new RelayCommand<MedicalRecord>((p) =>
            {
                if (SelectedPatient == null || SelectedLocation == null || SelectedSick == null || DateIn == null)
                {
                    return false;
                }
                
                
                return true;
            },
            (p) =>
            {
                var MedicalRecord = new MedicalRecord();
                if (SelectedPrescription == null)
                {
                    MedicalRecord = new Model.MedicalRecord()
                    {
                        IdPatient = SelectedPatient.Id,
                        IdSick = SelectedSick.Id,
                        IdPrescription = null,
                        IdLocation = SelectedLocation.Id,
                        DateIn = DateIn,
                        DateOut = DateOut,
                    };
                }
                else
                {
                    MedicalRecord = new Model.MedicalRecord()
                    {
                        IdPatient = SelectedPatient.Id,
                        IdSick = SelectedSick.Id,
                        IdPrescription = SelectedPrescription.Id,
                        IdLocation = SelectedLocation.Id,
                        DateIn = DateIn,
                        DateOut = DateOut,
                    };
                }

                DataProvider.Ins.DB.MedicalRecords.Add(MedicalRecord);
                DataProvider.Ins.DB.SaveChanges();


                var newIdMR = DataProvider.Ins.DB.MedicalRecords.OrderByDescending(x => x.Id).FirstOrDefault();
                var HospitalFee = new Model.HospitalFee()
                {
                    IdMedicalRecord = newIdMR.Id,
                    TotalFee = 0,
                    Owed = 0,
                };

                DataProvider.Ins.DB.HospitalFees.Add(HospitalFee);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(MedicalRecord);
            }
            );

            EditCommand = new RelayCommand<MedicalRecord>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var getMedicalRecord = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.Id ).SingleOrDefault();
                if(SelectedItem.DateOut == null || SelectedItem.DateOut != DateOut)
                {
                    if (DateOut != null)
                        return true;
                }
              
                if(getMedicalRecord.IdPrescription == null)
                {
                    if(SelectedPrescription != null)
                        return true;
                }
                else
                {
                    if (getMedicalRecord.IdSick != SelectedSick.Id)
                    {
                        return true;
                    }
                }
                return false;
            },
            (p) =>
            {
                var MedicalRecord = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                MedicalRecord.IdPatient = SelectedPatient.Id;
                MedicalRecord.IdSick = SelectedSick.Id;
                MedicalRecord.IdPrescription = SelectedPrescription.Id;
                MedicalRecord.IdLocation = SelectedLocation.Id;
                MedicalRecord.DateIn = DateIn;
                MedicalRecord.DateOut = DateOut;
                DataProvider.Ins.DB.SaveChanges();

                //SelectedItem.CodeMedicalRecord = CodeMedicalRecord;
            }
            );

            DeleteCommand = new RelayCommand<MedicalRecord>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var Fee = DataProvider.Ins.DB.HospitalFees.Where(x => x.IdMedicalRecord == SelectedItem.Id).SingleOrDefault();
                DataProvider.Ins.DB.HospitalFees.Remove(Fee);
                DataProvider.Ins.DB.SaveChanges();

                var MR = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                DataProvider.Ins.DB.MedicalRecords.Remove(MR);
                DataProvider.Ins.DB.SaveChanges();

                List.Remove(MR);
            }
            );

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                List = new ObservableCollection<MedicalRecord>(DataProvider.Ins.DB.MedicalRecords);
                Sick = new ObservableCollection<Sick>(DataProvider.Ins.DB.Sicks);
                Location = new ObservableCollection<Location>(DataProvider.Ins.DB.Locations);
                Prescription = new ObservableCollection<Prescription>(DataProvider.Ins.DB.Prescriptions);
                Patient = new ObservableCollection<Patient>(DataProvider.Ins.DB.Patients);

                var mr = DataProvider.Ins.DB.MedicalRecords.Select(x => x.IdPatient);

                for(int i = 0; i< mr.Count(); i++)
                {
                    for (int j=0; j<Patient.Count(); j++)
                    {
                        if(Patient[j].Id == mr.ToList()[i])
                        {
                            int id = Patient[j].Id;
                            var removePa = DataProvider.Ins.DB.Patients.Where(z => z.Id == id).SingleOrDefault();
                            Patient.Remove(removePa);
                        }
                    }
                }


            }
            );

            CreateDTCommand = new RelayCommand<Window>((p) => 
            { 
                if(SelectedPrescription == null)
                {
                    return true;
                }
                return false;

            }, 
            (p) => 
            {
                if(SelectedItem != null)
                {
                    Global.setGlobalText(SelectedPatient.DisplayName);
                    PrescriptionWindow f = new PrescriptionWindow();
                    f.displayName.Text = "ĐT của " + Global.globalText;
                    f.ShowDialog();
                }
                PrescriptionWindow m = new PrescriptionWindow();
                m.ShowDialog();
                
            }
            );

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TextSearch == null)
                {
                    List = new ObservableCollection<MedicalRecord>(DataProvider.Ins.DB.MedicalRecords);
                }
                else
                {
                    List = new ObservableCollection<MedicalRecord>(DataProvider.Ins.DB.MedicalRecords.Where(x => x.Patient.DisplayName.Contains(TextSearch)));
                }
            }
            );

        }
    }
}
