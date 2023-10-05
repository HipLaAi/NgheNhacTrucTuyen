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

        public TaiKhoanModel DangNhap(string tenDangNhap, string matKhau)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "logintaikhoan",
                    "@tendangnhap", tenDangNhap,
                    "@matkhau", matKhau);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<TaiKhoanModel>().FirstOrDefault();
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
                "@idloaitaikhoan", model.IDLoaiTaiKhoan,
                "@tendangnhap", model.TenDangNhap,
                "@matkhau", model.MatKhau,
                "@email", model.Email);
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
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError,"deletetaikhoan",
                "@tendangnhap", tenDangNhap);
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

        public bool Update(TaiKhoanModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "updatetaikhoan",
                "@idloaitaikhoan", model.IDLoaiTaiKhoan,
                "@tendangnhap", model.TenDangNhap,
                "@matkhau", model.MatKhau,
                "@email", model.Email);
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

    }
}
  
