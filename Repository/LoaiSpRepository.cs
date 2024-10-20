using ThucHanhWebMVC.Models;

namespace ThucHanhWebMVC.Repository
{
	public class LoaiSpRepository : ILoaiSpRepository
	{

		private readonly QlbanVaLiContext _context;

		public LoaiSpRepository(QlbanVaLiContext context) 
		{
			_context = context;
		}

		public TLoaiSp Add(TLoaiSp loaiSp)
		{
			_context.TLoaiSps.Add(loaiSp);
			_context.SaveChanges();
			return loaiSp;
		}

		public TLoaiSp Delete(string maLoaiSp)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TLoaiSp> GetAllLoaiSp()
		{
			return _context.TLoaiSps;
		}

		public TLoaiSp GetByMaLoaiSp(string maLoaiSp)
		{
			return _context.TLoaiSps.Find(maLoaiSp);
		}

		public TLoaiSp Update(TLoaiSp loaiSp)
		{
			_context.TLoaiSps.Update(loaiSp);
			_context.SaveChanges();
			return loaiSp;
		}
	}
}
