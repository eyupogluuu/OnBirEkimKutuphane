//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnBirEkimKutuphane.Models.entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class admin
    {
        public int adminID { get; set; }
        public string adSoyad { get; set; }
        public string email { get; set; }
        public string sifre { get; set; }
        public short rol { get; set; }
    
        public virtual rol rol1 { get; set; }
    }
}
