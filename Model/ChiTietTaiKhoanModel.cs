using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ChiTietTaiKhoanModel
    {
        public int IDChiTietTaiKhoan { get; set; }
        public int IDTaiKhoan { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string AnhDaiDien { get; set; }
    }
}
