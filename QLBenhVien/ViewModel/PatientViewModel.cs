using QLBenhVien.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLBenhVien.ViewModel
{
    public class PatientViewModel:BaseViewModel
    {
        private ObservableCollection<Patient> _List;
        public ObservableCollection<Patient> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Patient _SelectedItem;
        public Patient SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    DateOfBirth = SelectedItem.DateOfBirth;
                    Address = SelectedItem.Address;
                    Phone = SelectedItem.Phone;
                    Sex = SelectedItem.Sex;
                    Religion = SelectedItem.Religion;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Sex;
        public string Sex { get => _Sex; set { _Sex = value; OnPropertyChanged(); } }

        private string _Religion;
        public string Religion { get => _Religion; set { _Religion = value; OnPropertyChanged(); } }

        private DateTime _DateOfBirth;
        public DateTime DateOfBirth { get => _DateOfBirth; set { _DateOfBirth = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public PatientViewModel()
        {
            List = new ObservableCollection<Patient>(DataProvider.Ins.DB.Patients);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }

                var displayList = DataProvider.Ins.DB.Patients.Where(x => x.DisplayName == DisplayName);

                if (displayList.Count() != 0)
                {
                    return false;
                }

                return true;
            },
            (p) =>
            {
                var Patient = new Patient()
                {
                    DisplayName = DisplayName,
                    DateOfBirth = DateOfBirth,
                    Address = Address,
                    Phone = Phone,
                    Sex=Sex,
                    Religion=Religion,
                };

                DataProvider.Ins.DB.Patients.Add(Patient);
                DataProvider.Ins.DB.SaveChanges();

                var newIdPa = DataProvider.Ins.DB.Patients.OrderByDescending(x => x.Id).FirstOrDefault();
                var BHYT = new Model.BHYT()
                {
                    IdPatient = newIdPa.Id,
                    CodeBHYT = 0.ToString(),
                    Reduction = 0.ToString(),
                };

                DataProvider.Ins.DB.BHYTs.Add(BHYT);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Patient);
            }
            );

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.Patients.Where(x => x.DisplayName == DisplayName);
                var dateOfBirth = DataProvider.Ins.DB.Patients.Where(x => x.DateOfBirth == DateOfBirth);
                var diaChi = DataProvider.Ins.DB.Patients.Where(x => x.Address == Address);
                var dt = DataProvider.Ins.DB.Patients.Where(x => x.Phone == Phone);
                var sex = DataProvider.Ins.DB.Patients.Where(x => x.Sex == Sex);
                var religion = DataProvider.Ins.DB.Patients.Where(x => x.Religion == Religion);

                if (displayList.Count() != 0 && dateOfBirth.Count() != 0 && diaChi.Count() != 0 && dt.Count() != 0 && sex.Count() != 0 && religion.Count() != 0)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var Patient = DataProvider.Ins.DB.Patients.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Patient.DisplayName = DisplayName;
                Patient.DateOfBirth = DateOfBirth; 
                Patient.Address = Address;
                Patient.Phone = Phone;
                Patient.Sex = Sex;
                Patient.Religion = Religion;

                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
            }
            );
        }
    }
}
