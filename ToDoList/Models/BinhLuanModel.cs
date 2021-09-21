using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class BinhLuanModel
    {
        todolistEntities todo = new todolistEntities();
        DBConnection db = new DBConnection();
        //Lấy các bình luận theo mã công việc
        public IEnumerable<binhluan> dsBinhLuan(int id)
        {
            /*var bl = (from s in todo.binhluans
                      join c in todo.nhanviens on s.nhanvien_id equals c.nhanvien_id
                      where s.congviec_id == id
                      select new
                      {
                          nhanvien_id = s.nhanvien_id,
                          nhanvien_ten = c.nhanvien_ten,
                          congviec_id = s.congviec_id,
                          binhluan_noidung = s.binhluan_noidung,
                          binhluan_thoigianbinhluan = s.binhluan_thoigianbinhluan

                      }).ToList();*/
            var cv = (from s in todo.binhluans
                      join c in todo.nhanviens on s.nhanvien_id equals c.nhanvien_id
                      where s.congviec_id == id
                      select s).ToList();
            return cv;
        }
        // Thêm bình luận
        public void themBinhLuan(binhluan cv)
        {
            todo.binhluans.Add(cv);
            todo.SaveChanges();
        }
        /*
        public void themBinhLuan(binhluan bl)
        {
            string sql = "INSERT INTO binhluan(congviec_id, nhanvien_id, binhluan_noidung, binhluan_thoigianbinhluan) values('"+bl.congviec_id+"', '"+bl.nhanvien_id+"',N'"+bl.binhluan_noidung+"', "+DateTime.Now+")";
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            //Mở kết nối
            con.Open();
            cmd.ExecuteNonQuery();
            //Đóng kết nối
            cmd.Dispose();
            con.Close();
            
        }
        */
        //lấy bình luận theo mã bình luận
        public binhluan layBL(int id)
        {
            return todo.binhluans.First(m => m.id.CompareTo(id) == 0);
        }
        //Xóa bình luận
        public void XoaBinhLuan(int id)
        {
            binhluan n = layBL(id);
            todo.binhluans.Remove(n);
            todo.SaveChanges();

        }
    }
}