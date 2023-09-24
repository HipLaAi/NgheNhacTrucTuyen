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
    }
}
