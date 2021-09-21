using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class NhanVienController : Controller
    {
        NhanVienModel conn = new NhanVienModel();
        CongViecModel cvModel = new CongViecModel();
        todolistEntities todo = new todolistEntities();
        public ActionResult Index()
        {
            int id = Int32.Parse(Session["MaNhanVien"].ToString());
            ViewBag.DSNV = conn.layDSNV();
            ViewBag.NV = conn.layNhanVien(id);
            return View();
        }
        public ActionResult ThemNhanVien()
        {
            var getChucVu = todo.chucvus.ToList();
            SelectList list = new SelectList(getChucVu, "chucvu_id", "chucvu_ten");
            ViewBag.TenChucVu = list;
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhanVien(nhanvien nv)
        {
            conn.themNhanVien(nv);
            TempData["ThemThanhCong"] = "Thêm thành công.";
            return RedirectToAction("ThemNhanVien");
        }
        public ActionResult SuaNhanVien(int id)
        {
            var getChucVu = todo.chucvus.ToList();
            SelectList list = new SelectList(getChucVu, "chucvu_id", "chucvu_ten");
            ViewBag.TenChucVu = list;
            return View(conn.layNhanVien(id));
        }
        [HttpPost]
        public ActionResult SuaNhanVien(nhanvien nv)
        {
            conn.suaNhanVien(nv);
            TempData["SuaThanhCong"] = "Sửa thành công.";
            //return Redirect(Request.UrlReferrer.ToString());//load lại trang
            return RedirectToAction("Index");
        }
        public ActionResult XoaNhanVien(int id)
        {
            conn.xoaNhanVien(id);
            TempData["XoaThanhCong"] = "Xóa thành công.";
            return RedirectToAction("Index");
        }
        public ActionResult XemCVNhanVien(int id)
        {
            ViewBag.CVTuTao = cvModel.layDSCaNhan(id);
            ViewBag.CVThamGia = cvModel.layDSThamGia(id);
            ViewBag.CVTreHan = cvModel.layDSTreHan(id);
            ViewBag.CVDungHan = cvModel.layDSDungHan(id);
            return View();
        }
        [HttpPost]
        public ActionResult TimKiemNV(string searchString)
        {
            ViewBag.TuKhoa = searchString;
            /*
            var cv = (from c in todo.nhanviens
                        select c);
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                cv = cv.Where(c => c.nhanvien_ten.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            */
            ViewBag.NV = conn.TimKiemNV(searchString);

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = TempData["message"];

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(nhanvien model)
        {

            if (ModelState.IsValid)
            {
                var data = conn.Login(model);
                if (data.Count() > 0)
                {
                    //add session
                    Session["MaNhanVien"] = data.FirstOrDefault().nhanvien_id;
                    Session["TenNhanVien"] = data.FirstOrDefault().nhanvien_ten;
                    Session["MaChucVu"] = data.FirstOrDefault().chucvu_id;
                    //Session["TenDangNhap"] = data.FirstOrDefault().taikhoan;
                    foreach (var item in data)
                    {
                        if (item.chucvu_id == 1)
                        {
                            //return RedirectToAction("Index");
                            return Redirect("NhanVien/Index");
                        }
                        else
                        {
                            //return RedirectToAction("Index");
                            return Redirect("CongViec/Index");
                        }
                    }

                }
                else
                {
                    TempData["message"] = "Tên đăng hoặc mật khẩu không chính xác.";
                    ViewBag.mes = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        public ActionResult StaffInFo(int id)
        {

            ViewBag.TT = conn.layThongTinNV(id);
            return View(conn.layThongTinNV(id));
        }
        [HttpPost]
        public ActionResult StaffInFo(nhanvien nv)
        {
            //nhanvien n = new nhanvien();

            conn.suaNhanVien(nv);
            TempData["SuaThanhCong"] = "Sửa thành công.";
            return Redirect(Request.UrlReferrer.ToString());//load lại trang
            //return RedirectToAction("Index");
        }
    }
}