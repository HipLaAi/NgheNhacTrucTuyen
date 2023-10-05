using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL
{
    public partial interface ITaiKhoanBLL
    {
        TaiKhoanModel DangNhap(string tenDangNhap, string matKhau);
        bool Create(TaiKhoanModel model);
        bool Delete(string tenDangNhap);
        bool Update(TaiKhoanModel model);
        //TaiKhoanModel GetTaiKhoan(string tenDangNhap);

        //public List<TaiKhoanModel> Search(int pageIndex, int pageSize, out long total, string hoTen, string diaChi, DateTime ngaySinh, string gioiTinh, string SDT);
    }
}
