using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class NhanVienModel
    {
        todolistEntities todo = new todolistEntities();
        DBConnection db = new DBConnection();
        //Lấy nhân viên tham gia công việc theo mã công việc
        public nhanvien_congviec layNVCV(int id)
        {
            return todo.nhanvien_congviec.First(m => m.congviec_id.CompareTo(id) == 0);
        }
        //Lấy danh sách nhân viên
        public nhanvien layNhanVien(int id)
        {
            return todo.nhanviens.First(m => m.nhanvien_id.CompareTo(id) == 0);
        }

        //Sửa nhân viên
        public void suaNhanVien(nhanvien nv)
        {
            nhanvien c = layNhanVien(nv.nhanvien_id);
            c.nhanvien_ten = nv.nhanvien_ten;
            c.nhanvien_gioitinh = nv.nhanvien_gioitinh;
            c.nhanvien_ngaysinh = nv.nhanvien_ngaysinh;
            c.nhanvien_cmnd = nv.nhanvien_cmnd;
            c.chucvu_id = nv.chucvu_id;
            c.nhanvien_email = nv.nhanvien_email;
            c.nhanvien_matkhau = nv.nhanvien_matkhau;
            todo.SaveChanges();
        }

        //Xóa nhân viên
        public void xoaNhanVien(int id)
        {
            nhanvien c = layNhanVien(id);
            todo.nhanviens.Remove(c);
            todo.SaveChanges();
        }

        //Lấy danh sách nhân viên
        public IEnumerable<nhanvien> layDSNV()
        {
            return todo.nhanviens.ToList();
        }
        //Thêm nhân viên
        public void themNhanVien(nhanvien nv)
        {
            todo.nhanviens.Add(nv);
            todo.SaveChanges();
        }

        //tìm nhân viên
        public IEnumerable<nhanvien> TimKiemNV(string searchString)
        {
            var cv = (from c in todo.nhanviens
                      select c);
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                cv = cv.Where(c => c.nhanvien_ten.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            return cv;
        }
        public nhanvien layThongTinNV(int id)
        {
            return todo.nhanviens.First(m => m.nhanvien_id.CompareTo(id) == 0);
        }
        //Đăng nhập
        public IEnumerable<nhanvien> Login(nhanvien model)
        {
            var cv = (from p in todo.nhanviens
                      where p.nhanvien_email.Equals(model.nhanvien_email) && p.nhanvien_matkhau.Equals(model.nhanvien_matkhau)
                      select p).ToList();
            return cv;
        }
    }
}