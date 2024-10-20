using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC.Repository;

namespace ThucHanhWebMVC.ViewComponents
{
	public class LoaiSpMenuViewComponent : ViewComponent
	{
		private readonly ILoaiSpRepository _loaiSpRepository;

		public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRepository)
		{
			_loaiSpRepository = loaiSpRepository;
		}

		public IViewComponentResult Invoke()
		{
			var loaiSps = _loaiSpRepository.GetAllLoaiSp().OrderBy(x => x.Loai);
			return View(loaiSps);
		}

	}
}
