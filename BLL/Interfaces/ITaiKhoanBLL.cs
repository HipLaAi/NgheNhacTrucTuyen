﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL
{
    public partial interface ITaiKhoanBLL
    {
        TaiKhoanModel GetTaiKhoan(string tenDangNhap);
        bool Create(TaiKhoanModel model);
        //bool Update(TaiKhoanModel model);
        bool Delete(string tenDangNhap);
        //public List<TaiKhoanModel> Search(int pageIndex, int pageSize, out long total, string hoTen, string diaChi, DateTime ngaySinh, string gioiTinh, string SDT);
    }
}
