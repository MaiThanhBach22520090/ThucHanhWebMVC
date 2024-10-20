using ThucHanhWebMVC.Models;

namespace ThucHanhWebMVC.Repository
{
	public interface ILoaiSpRepository
	{
		TLoaiSp Add(TLoaiSp loaiSp);
		TLoaiSp Update(TLoaiSp loaiSp);
		TLoaiSp Delete(string maLoaiSp);
		TLoaiSp GetByMaLoaiSp(string maLoaiSp);

		IEnumerable<TLoaiSp> GetAllLoaiSp();
	}
}
