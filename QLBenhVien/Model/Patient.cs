//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBenhVien.Model
{
    using QLBenhVien.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class Patient:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.MedicalRecords = new HashSet<MedicalRecord>();
        }
    
        public int Id { get; set; }

        string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        System.DateTime _DateOfBirth;
        public DateTime DateOfBirth { get => _DateOfBirth; set { _DateOfBirth = value; OnPropertyChanged(); } }

        string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        string _Sex;
        public string Sex { get => _Sex; set { _Sex = value; OnPropertyChanged(); } }

        string _Religion;
        public string Religion { get => _Religion; set { _Religion = value; OnPropertyChanged(); } }

        public virtual BHYT BHYT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
