//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MineMovie_Nhom7_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHI_TIET_PHIM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHI_TIET_PHIM()
        {
            this.DANH_GIA = new HashSet<DANH_GIA>();
        }
    
        public int ID_PHIM { get; set; }
        public string TEN_PHIM { get; set; }
        public string MO_TA { get; set; }
        public Nullable<System.TimeSpan> THOI_LUONG { get; set; }
        public string QUOC_GIA { get; set; }
        public string DAO_DIEN { get; set; }
        public string HINH_ANH { get; set; }
        public string TRAILER { get; set; }
        public Nullable<int> NAM_PH { get; set; }
        public Nullable<int> ID_DM { get; set; }
        public Nullable<int> ID_TL { get; set; }
        public string IMDB_ID { get; set; }
    
        public virtual PHIM PHIM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANH_GIA> DANH_GIA { get; set; }
    }
}
