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
    
    public partial class nhanvien_congviec
    {
        public int id { get; set; }
        public int nhanvien_id { get; set; }
        public int congviec_id { get; set; }
        public int quyen_id { get; set; }
    
        public virtual congviec congviec { get; set; }
        public virtual nhanvien nhanvien { get; set; }
        public virtual quyen quyen { get; set; }
    }
}
