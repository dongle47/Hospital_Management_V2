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
    using System;
    using System.Collections.Generic;
    
    public partial class HospitalFee
    {
        public int IdMedicalRecord { get; set; }
        public decimal TotalFee { get; set; }
        public Nullable<decimal> Owed { get; set; }
    
        public virtual MedicalRecord MedicalRecord { get; set; }
    }
}