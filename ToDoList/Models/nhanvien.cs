//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToDoList.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class nhanvien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nhanvien()
        {
            this.binhluans = new HashSet<binhluan>();
            this.filechiases = new HashSet<filechiase>();
            this.nhanvien_congviec = new HashSet<nhanvien_congviec>();
        }
    
        public int nhanvien_id { get; set; }
        public string nhanvien_ten { get; set; }
        public string nhanvien_gioitinh { get; set; }
        public System.DateTime nhanvien_ngaysinh { get; set; }
        public int nhanvien_cmnd { get; set; }
        public string nhanvien_email { get; set; }
        public string nhanvien_matkhau { get; set; }
        public Nullable<int> chucvu_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<binhluan> binhluans { get; set; }
        public virtual chucvu chucvu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<filechiase> filechiases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<nhanvien_congviec> nhanvien_congviec { get; set; }
    }
}
