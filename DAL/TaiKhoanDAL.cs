using Model;

namespace DAL
{
    public class TaiKhoanDAL : ITaiKhoanDAL
    {
        private IDatabaseHelper _dbHelper;
        public TaiKhoanDAL(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public TaiKhoanModel GetTaiKhoan(string tenDangNhap)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "gettaikhoan",
                     "@tendangnhap", tenDangNhap);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<TaiKhoanModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(TaiKhoanModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "createtaikhoan",
                "@tendangnhap", model.TenDangNhap,
                "@matkhau", model.MatKhhau,
                "@hoten", model.HoTen,
                "@gioitinh", model.GioiTinh,
                "@ngaysinh", model.NgaySinh,
                "@sdt", model.SDT,
                "@diachi", model.DiaChi,
                "@loaitaikhoan", model.LoaiTaiKhoan);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string tenDangNhap)
        {
            string msgError = "";
            bool kq; // Khởi tạo mặc định là false
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedure(out msgError, "deletetaikhoan",
                     "@tendangnhap", tenDangNhap);                
                // Kiểm tra kết quả trả về từ hàm ExecuteScalarSProcedureWithTransaction
                if (Convert.ToInt32(result) > 0)
                {
                    kq = true; // Xóa thành công, đặt kq thành true
                }
                else
                {
                    kq = false;
                }
                return kq;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
  
