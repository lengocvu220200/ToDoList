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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class todolistEntities : DbContext
    {
        public todolistEntities()
            : base("name=todolistEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<binhluan> binhluans { get; set; }
        public virtual DbSet<chucvu> chucvus { get; set; }
        public virtual DbSet<congviec> congviecs { get; set; }
        public virtual DbSet<filechiase> filechiases { get; set; }
        public virtual DbSet<nhanvien> nhanviens { get; set; }
        public virtual DbSet<nhanvien_congviec> nhanvien_congviec { get; set; }
        public virtual DbSet<phamvi> phamvis { get; set; }
        public virtual DbSet<quyen> quyens { get; set; }
    }
}
