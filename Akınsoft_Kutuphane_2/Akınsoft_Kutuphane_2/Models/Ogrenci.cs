//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Akınsoft_Kutuphane_2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ogrenci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ogrenci()
        {
            this.OduncTeslim = new HashSet<OduncTeslim>();
        }
    
        public int OgrenciIId { get; set; }
        public string OgrenciNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public Nullable<int> FakulteId { get; set; }
        public Nullable<int> BolumId { get; set; }
        public string Adres { get; set; }
        public string MemleketAdres { get; set; }
        public Nullable<System.DateTime> KayitTarihi { get; set; }
        public string CepTelefonu { get; set; }
    
        public virtual Bolum Bolum { get; set; }
        public virtual Fakulte Fakulte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OduncTeslim> OduncTeslim { get; set; }
    }
}
