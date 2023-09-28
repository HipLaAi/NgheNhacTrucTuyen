using Model;
using DAL;
using BLL;

namespace BLL
{
    public class TaiKhoanBLL:ITaiKhoanBLL
    {
        private ITaiKhoanDAL _res;
        public TaiKhoanBLL(ITaiKhoanDAL res)
        {
            _res = res;
        }
        public TaiKhoanModel GetTaiKhoan(string tenDangNhap)
        {
            return _res.GetTaiKhoan(tenDangNhap);
        }
        public bool Create (TaiKhoanModel model)
        {
            return _res.Create(model);
        }
        public bool Delete (string tenDangNhap)
        {
            return _res.Delete(tenDangNhap);
        }
    }
}
