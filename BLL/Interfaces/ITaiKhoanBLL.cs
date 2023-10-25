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
        TaiKhoanModel GetByID(int idTaiKhoan);
        TaiKhoanModel DangNhap(string tenDangNhap, string matKhau);
        bool Create(TaiKhoanModel model, ChiTietTaiKhoanModel models);
        bool Delete(string tenDangNhap);
        bool Update(TaiKhoanModel model, ChiTietTaiKhoanModel models);
        public List<ChiTietTaiKhoanModel> Search(int pageIndex, int pageSize, out long total, string hoTen, string diaChi, string gioiTinh);
    }
}
