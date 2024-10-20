using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThucHanhWebMVC.Models;
using X.PagedList;

namespace ThucHanhWebMVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham(int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstSanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(lstSanpham, pageNumber, pageSize);

			return View(list);
		}

        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
		{
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

			return View();
		}

        [Route("ThemSanPhamMoi")]
        [HttpPost]
        public IActionResult ThemSanPhamMoi(TDanhMucSp sp)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sp);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sp);
        }

		[Route("SuaSanPham")]
		[HttpGet]
		public IActionResult SuaSanPham(string maSanPham)
		{
			ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
			ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
			ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
			ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
			ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

			var sp = db.TDanhMucSps.Find(maSanPham);

			return View(sp);
		}

		[Route("SuaSanPham")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SuaSanPham(TDanhMucSp maSanPham)
		{
			if (ModelState.IsValid)
			{
				db.Update(maSanPham);
				db.SaveChanges();
				return RedirectToAction("DanhMucSanPham", "HomeAdmin");
			}
			return View(maSanPham);
		}

		[Route("XoaSanPham")]
		[HttpGet]
		public IActionResult XoaSanPham(string maSanPham)
		{
			TempData["Message"] = "";
			var chiTietSanPham = db.TChiTietSanPhams.Where(x => x.MaSp == maSanPham).ToList();
			if (chiTietSanPham.Count > 0)
			{
				foreach (var item in chiTietSanPham)
				{
					TempData["Message"] = "Sản phẩm này đã có chi tiết sản phẩm không thể xóa!";
					return RedirectToAction("DanhMucSanPham", "HomeAdmin");
				}
			}

			var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSanPham);
			if (anhSanPham.Any())
			{
				db.RemoveRange(anhSanPham);
			}

			if (db.TDanhMucSps.Find(maSanPham) == null)
			{
				TempData["Message"] = "Không tìm thấy sản phẩm!";
				return RedirectToAction("DanhMucSanPham", "HomeAdmin");
			
			}
			else
			{
				db.Remove(db.TDanhMucSps.Find(maSanPham));
				db.SaveChanges();
				TempData["Message"] = "Xóa sản phẩm thành công!";
				return RedirectToAction("DanhMucSanPham", "HomeAdmin");
			}
		}
	}
}
