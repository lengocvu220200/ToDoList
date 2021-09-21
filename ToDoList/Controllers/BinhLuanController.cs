using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class BinhLuanController : Controller
    {
        BinhLuanModel conn = new BinhLuanModel();
        CongViecModel cv = new CongViecModel();
        todolistEntities todo = new todolistEntities();
        public ActionResult BinhLuan(int id)
        {
            ViewBag.dsBL = conn.dsBinhLuan(id);
            var getCongViec = (from p in todo.congviecs
                               where p.congviec_id == id
                               select p).ToList();
            SelectList dsCongViec = new SelectList(getCongViec, "congviec_id", "congviec_ten");
            ViewBag.dsCongViec = dsCongViec;
            ViewBag.CV = getCongViec;
            int idNV = Int32.Parse(Session["MaNhanVien"].ToString());
            var getNhanVien = (from p in todo.nhanviens
                               where p.nhanvien_id == idNV
                               select p).ToList();
            SelectList dsNhanVien = new SelectList(getNhanVien, "nhanvien_id", "nhanvien_ten");
            ViewBag.dsNhanVien = dsNhanVien;
            var getfiles = (from p in todo.filechiases
                         where p.congviec_id == id
                         select p).ToList();
            ViewBag.File = getfiles;
            return View();
        }
        [HttpPost]
        public ActionResult BinhLuan(binhluan bl)
        {
            conn.themBinhLuan(bl);
            return RedirectToAction("BinhLuan");
        }

        public ActionResult XoaBinhLuan(int id)
        {
            conn.XoaBinhLuan(id);
            //return view;
            return Redirect(Request.UrlReferrer.ToString());//load lại trang
            //return RedirectToAction("BinhLuan/"+id,"BinhLuan");//điều hướng đến trang
        }
        [HttpPost]
        public ActionResult ThemFile(filechiase file)
        {
            cv.ThemFile(file);
            return Redirect(Request.UrlReferrer.ToString());//load lại trang
        }

        public ActionResult XoaFile(int id)
        {
            cv.XoaFile(id);
            return Redirect(Request.UrlReferrer.ToString());//load lại trang
        }
    }
}