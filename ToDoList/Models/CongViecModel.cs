using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.Models
{
    public class CongViecModel
    {
        todolistEntities todo = new todolistEntities();
        DBConnection db = new DBConnection();
        //Lấy danh sách công việc
        public IEnumerable<congviec> layDS()
        {
            return todo.congviecs.ToList();
        }
        //Lấy công việc theo mã công việc
        public congviec layCV(int id)
        {
            return todo.congviecs.First(m => m.congviec_id.CompareTo(id) == 0);
        }
        //Hàm lấy công việc bản thân tạo
        public IEnumerable<congviec> layDSCaNhan(int id)
        {
            var cv = (from p in todo.congviecs
                      join c in todo.nhanvien_congviec on p.congviec_id equals c.congviec_id
                      where c.nhanvien_id == id && p.congviec_ngayketthuc>=DateTime.Today && p.congviec_ngayhoanthanh==null
                      select p).ToList();
            return cv;
        }
        
        public IEnumerable<congviec> layDSThamGia(int id)
        {
            var cv = (from p in todo.congviecs
                      join c in todo.nhanvien_congviec on p.congviec_id equals c.congviec_id
                      where c.nhanvien_id == id & p.congviec_ngayketthuc < DateTime.Today && p.congviec_ngayhoanthanh == null
                      select p).ToList();
            return cv;
        }
        //lấy danh sách công việc đúng hạn
        public IEnumerable<congviec> layDSDungHan(int id)
        {
            var cv = (from p in todo.congviecs
                      join c in todo.nhanvien_congviec on p.congviec_id equals c.congviec_id
                      where c.nhanvien_id == id && p.congviec_ngayhoanthanh <= p.congviec_ngayketthuc
                      select p).ToList();
            
            return cv;
        }
        //Lấy danh sách công việc trể hẹn
        public IEnumerable<congviec> layDSTreHan(int id)
        {
            var cv = (from p in todo.congviecs
                      join c in todo.nhanvien_congviec on p.congviec_id equals c.congviec_id
                      where c.nhanvien_id == id && p.congviec_ngayhoanthanh > p.congviec_ngayketthuc
                      select p).ToList();
            return cv;
        }
        //thêm công việc
        public void them(congviec cv, int id)
        {
            todo.congviecs.Add(cv);
            todo.SaveChanges();
            nhanvien_congviec nvcv = new nhanvien_congviec();
            nvcv.nhanvien_id = id;
            nvcv.congviec_id = cv.congviec_id;//lấy id công việc mới dc thêm
            nvcv.quyen_id = 1;
            todo.nhanvien_congviec.Add(nvcv);
            todo.SaveChanges();
        }
        public void sua(congviec cv)
        {
            congviec c = layCV(cv.congviec_id);
            c.congviec_ten = cv.congviec_ten;
            c.congviec_ngaybatdau = cv.congviec_ngaybatdau;
            c.congviec_ngayketthuc = cv.congviec_ngayketthuc;
            c.congviec_ngayhoanthanh = cv.congviec_ngayhoanthanh;
            c.phamvi_id = cv.phamvi_id;
            todo.SaveChanges();
        }
        public void xoa(int id)
        {
            nhanvien_congviec n = layNVCV(id);
            todo.nhanvien_congviec.Remove(n);
            todo.SaveChanges();

            congviec c = layCV(id);
            todo.congviecs.Remove(c);
            todo.SaveChanges();
        }
        public IEnumerable<congviec> TimKiemCV(string searchString, int id)
        {
            var cv = (from c in todo.congviecs
                      join n in todo.nhanvien_congviec on c.congviec_id equals n.congviec_id
                      where n.nhanvien_id == id
                      select c);
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                cv = cv.Where(c => c.congviec_ten.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            return cv;
        }
        //lấy người làm công việc theo mã nguoilam_congviec
        public nhanvien_congviec layNguoiLam(int id)
        {

            return todo.nhanvien_congviec.First(m => m.id.CompareTo(id) == 0); ;
        }
        public filechiase layFile(int id)
        {

            return todo.filechiases.First(m => m.id.CompareTo(id) == 0); ;
        }
        //Thêm người làm chung cho công việc
        public void themNguoiLam(nhanvien_congviec cv)
        {
            todo.nhanvien_congviec.Add(cv);
            todo.SaveChanges();
        }
        //Xóa người làm ra khỏi công việc
        public void XoaNguoiLam(int id)
        {
            nhanvien_congviec c = layNguoiLam(id);
            todo.nhanvien_congviec.Remove(c);
            todo.SaveChanges();
        }
        //Lấy nhân viên tham gia công việc theo mã công việc
        public nhanvien_congviec layNVCV(int id)
        {
            return todo.nhanvien_congviec.First(m => m.congviec_id.CompareTo(id) == 0);
        }
        //Thêm File
        public void ThemFile(filechiase file)
        {
            todo.filechiases.Add(file);
            todo.SaveChanges();
        }
        public void XoaFile(int id)
        {
            filechiase c = layFile(id);
            todo.filechiases.Remove(c);
            todo.SaveChanges();
        }
        //-================================================================End công việc
        
        //==============================================================End Bình luận
        /*
        
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
        */
    }
}