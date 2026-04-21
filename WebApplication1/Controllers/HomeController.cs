using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // Model dữ liệu
        public class Thuoc
        {
            public int Id { get; set; }
            public string TenThuoc { get; set; }
            public string GioUong { get; set; }
            public bool DaUong { get; set; }
            public string Loai { get; set; }
        }

        // FAKE DATABASE
        public static List<Thuoc> dbThuoc = new List<Thuoc>
        {
            new Thuoc { Id = 1, TenThuoc = "Huyết Áp Losartan 50mg", GioUong = "08:00", DaUong = false, Loai = "Thuốc Kê Đơn" },
            new Thuoc { Id = 2, TenThuoc = "Vitamin C 500mg", GioUong = "12:00", DaUong = true, Loai = "TP Chức Năng" }
        };

        public ActionResult Index()
        {
            return View(dbThuoc);
        }

        [HttpPost]
        public ActionResult ThemThuoc(string TenThuoc, string GioUong, string Loai)
        {
            if (!string.IsNullOrEmpty(TenThuoc))
            {
                dbThuoc.Add(new Thuoc
                {
                    Id = dbThuoc.Count + 1,
                    TenThuoc = TenThuoc,
                    GioUong = GioUong,
                    DaUong = false,
                    Loai = Loai
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CapNhatSucKhoe(int ChieuCao, int CanNang)
        {
            if (ChieuCao > 250 || ChieuCao < 50 || CanNang > 150 || CanNang < 20)
            {
                ViewBag.ErrorMsg = "⚠️ Chiều cao hoặc Cân nặng không hợp lý (Vượt ngưỡng)!";
            }
            else
            {
                ViewBag.SuccessMsg = "✔️ Cập nhật hồ sơ bệnh án thành công!";
                ViewBag.CC = ChieuCao;
                ViewBag.CN = CanNang;
            }
            return View("Index", dbThuoc);
        }

        public ActionResult XacNhanUong(int id)
        {
            var t = dbThuoc.FirstOrDefault(x => x.Id == id);
            if (t != null) t.DaUong = true;
            return RedirectToAction("Index");
        }
    }
}
