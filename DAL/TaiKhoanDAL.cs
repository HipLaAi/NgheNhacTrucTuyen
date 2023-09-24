using Model;

namespace DAL
{
    public class TaiKhoanDAL:ITaiKhoanDAL
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
    }
}
