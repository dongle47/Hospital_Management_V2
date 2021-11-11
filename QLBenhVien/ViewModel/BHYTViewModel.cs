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
    public class BHYTViewModel:BaseViewModel
    {
        private ObservableCollection<BHYT> _List;
        public ObservableCollection<BHYT> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Patient> _Patient;
        public ObservableCollection<Patient> Patient { get => _Patient; set { _Patient = value; OnPropertyChanged(); } }


        private BHYT _SelectedItem;
        public BHYT SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    CodeBHYT = SelectedItem.CodeBHYT;
                    SelectedPatient = SelectedItem.Patient;
                    DateStart = SelectedItem.DateStart;
                    DateEnd = SelectedItem.DateEnd;
                    Reduction = SelectedItem.Reduction;
                }
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

        private string _CodeBHYT;
        public string CodeBHYT { get => _CodeBHYT; set { _CodeBHYT = value; OnPropertyChanged(); } }

        private DateTime? _DateStart;
        public DateTime? DateStart { get => _DateStart; set { _DateStart = value; OnPropertyChanged(); } }

        private DateTime? _DateEnd;
        public DateTime? DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }

        private string _Reduction;
        public string Reduction 
        { 
            get => _Reduction; 
            set 
            { 
                _Reduction = value; 
                OnPropertyChanged(); 
            } 
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public BHYTViewModel()
        {
            List = new ObservableCollection<BHYT>(DataProvider.Ins.DB.BHYTs);

            Patient = new ObservableCollection<Patient>(DataProvider.Ins.DB.Patients);

            AddCommand = new RelayCommand<BHYT>((p) =>
            {
                if (string.IsNullOrEmpty(CodeBHYT))
                {
                    return false;
                }

                var displayList = DataProvider.Ins.DB.BHYTs.Where(x => x.CodeBHYT == CodeBHYT);

                if (displayList == null || displayList.Count() != 0)
                {
                    return false;
                }

                if (SelectedPatient == null)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                var BHYT = new Model.BHYT()
                {
                    CodeBHYT = CodeBHYT,
                    IdPatient = SelectedPatient.Id,
                    DateStart = DateStart,
                    DateEnd = DateEnd,
                    Reduction = Reduction,
                };

                DataProvider.Ins.DB.BHYTs.Add(BHYT);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(BHYT);
            }
            );

            EditCommand = new RelayCommand<BHYT>((p) =>
            {
                if (SelectedPatient == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.BHYTs.Where(x => x.CodeBHYT == SelectedItem.CodeBHYT);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                return false;
            },
            (p) =>
            {
                var BHYT = DataProvider.Ins.DB.BHYTs.Where(x => x.IdPatient == SelectedItem.IdPatient).SingleOrDefault();
                BHYT.CodeBHYT = CodeBHYT;
                BHYT.IdPatient = SelectedPatient.Id;
                BHYT.DateStart = DateStart;
                BHYT.DateEnd = DateEnd;
                BHYT.Reduction = Reduction;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.CodeBHYT = CodeBHYT;
            }
            );
        }
    }
}
