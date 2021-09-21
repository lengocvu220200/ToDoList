using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using CaptchaMvc.HtmlHelpers;
namespace ToDoList.Controllers
{
    public class CongViecController : Controller
    {
        CongViecModel conn = new CongViecModel();
        todolistEntities todo = new todolistEntities();
        
        public ActionResult Index()
        {
            if(Session["MaNhanVien"] != null)
            {
                int id = Int32.Parse(Session["MaNhanVien"].ToString());
                ViewBag.thamgia = conn.layDSThamGia(id);
                ViewBag.all = conn.layDSCaNhan(id);
            }
            return View();
        }
        public ActionResult Create()
        {
            todolistEntities td = new todolistEntities();
            var getPhamVi = td.phamvis.ToList();
            SelectList list = new SelectList(getPhamVi, "phamvi_id", "phamvi_ten");
            ViewBag.TenPhamVi = list;
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(congviec cv)
        {
            int id = Int32.Parse(Session["MaNhanVien"].ToString());
            conn.them(cv, id);
            //Response.Write("<script>alert('Thành công');</script>");
            TempData["ThemThanhCong"] = "Thêm thành công.";
            return RedirectToAction(nameof(Create));
            
        }
        public ActionResult Edit(int id)
        {
            todolistEntities td = new todolistEntities();
            ViewBag.cv = conn.layCV(id);
            var getPhamVi = td.phamvis.ToList();
            SelectList list = new SelectList(getPhamVi, "phamvi_id", "phamvi_ten");
            ViewBag.TenPhamVi = list;
            return View(conn.layCV(id));
        }
        [HttpPost]
        public ActionResult Edit(congviec cv)
        {
            conn.sua(cv);
            TempData["SuaThanhCong"] = "Sửa thành công.";
            if (Int32.Parse(Session["MaChucVu"].ToString()) == 1)
            {
                return RedirectToAction(nameof(Edit));
                //return RedirectToAction("NhanVien/Index");//load lại trang
            }
            return RedirectToAction("Index");
            
        }
        public ActionResult Delete(int id)
        {
            conn.xoa(id);
            TempData["XoaThanhCong"] = "Xóa thành công.";
            //return RedirectToAction("Index");
            //return Redirect(Request.UrlReferrer.ToString());
            return Redirect(Request.UrlReferrer.ToString());//load lại trang
        }
        public ActionResult HoanThanh(int id)
        {

            congviec cv = conn.layCV(id);

            congviec c = new congviec();
            c.congviec_id = cv.congviec_id;
            c.congviec_ten = cv.congviec_ten;
            c.congviec_ngaybatdau = cv.congviec_ngaybatdau;
            c.congviec_ngayketthuc = cv.congviec_ngayketthuc;
            c.congviec_ngayhoanthanh = DateTime.Now;
            c.phamvi_id = cv.phamvi_id;
            conn.sua(c);
            TempData["HoanThanh"] = "Hoàn thành thành công.";
            return RedirectToAction("Index");
        }
        public ActionResult CongViecHoanThanh()
        {
            if (Session["MaNhanVien"] != null)
            {
                int id = Int32.Parse(Session["MaNhanVien"].ToString());
                ViewBag.dunghan = conn.layDSDungHan(id);
                ViewBag.trehan = conn.layDSTreHan(id);
            }
            
            return View();
        }

        //------------------------------------------------------------------
        
        public ActionResult XoaNguoiLam(int id)
        {
            conn.XoaNguoiLam(id);
            TempData["XoaThanhCong"] = "Xóa thành công.";
            //return RedirectToAction("ThemNguoiLam");
            return Redirect(Request.UrlReferrer.ToString());//load lại trang
        }
        
        

        public ActionResult ThemNguoiLam(int id)
        {
            var dsNguoiLam = (from s in todo.nhanvien_congviec
                              
                              where s.congviec_id == id
                              select s).ToList();
            ViewBag.dsNguoiLam = dsNguoiLam;

            var congViec  = (from p in todo.congviecs
                             where p.congviec_id == id
                             select p).ToList();
            SelectList ds = new SelectList(congViec, "congviec_id", "congviec_ten");
            ViewBag.TenCongViec = ds;

            
            var nguoiLam = (from p in todo.nhanviens
                            select p).ToList();
            SelectList list = new SelectList(nguoiLam, "nhanvien_id", "nhanvien_ten");
            ViewBag.TenNguoiLam = list;

            var quyen = (from p in todo.quyens
                            where p.quyen_id == 2
                            select p).ToList();
            SelectList tenQuyen = new SelectList(quyen, "quyen_id", "quyen_ten");
            ViewBag.TenQuyen = tenQuyen;
            return View();
        }
        [HttpPost]
        public ActionResult ThemNguoiLam(nhanvien_congviec cv)
        {
            conn.themNguoiLam(cv);
            TempData["ThemThanhCong"] = "Thêm thành công.";
            return RedirectToAction("ThemNguoiLam");
        }

        [HttpPost]
        public ActionResult TimKiemCV(string searchString)
        {
            ViewBag.TuKhoa = searchString;
            if (Session["MaNhanVien"] != null)
            {
                
                int id = Int32.Parse(Session["MaNhanVien"].ToString());
                /*
                var cv = (from c in todo.congviecs
                         join n in todo.nhanvien_congviec on c.congviec_id equals n.congviec_id
                         where n.nhanvien_id == id
                     select c);
                if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
                {
                    cv = cv.Where(c => c.congviec_ten.Contains(searchString)); //lọc theo chuỗi tìm kiếm
                }
                */
                ViewBag.CV = conn.TimKiemCV(searchString, id);
            }

            TempData["tim"] = "Welcome back!";
            return View();
        }
        
    }


}