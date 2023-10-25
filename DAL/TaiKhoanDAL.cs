using Model;
using System.Reflection;

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

        public bool Create(TaiKhoanModel model, ChiTietTaiKhoanModel models)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "createtaikhoan",
                "@idloaitaikhoan", model.IDLoaiTaiKhoan,
                "@tendangnhap", model.TenDangNhap,
                "@matkhau", model.MatKhau,
                "@email", model.Email,
                "@hoten", models.HoTen,
                "@gioitinh", models.GioiTinh,
                "@ngaysinh", models.NgaySinh,
                "@sdt", models.SDT,
                "@diachi", models.DiaChi,
                "@anhdaidien", models.AnhDaiDien);
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

        public bool Update(TaiKhoanModel model, ChiTietTaiKhoanModel models)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "updatetaikhoan",
                "@idtaikhoan", model.IDTaiKhoan,
                "@tendangnhap", model.TenDangNhap,
                "@matkhau", model.MatKhau,
                "@email", model.Email,
                "@hoten", models.HoTen,
                "@gioitinh", models.GioiTinh,
                "@ngaysinh", models.NgaySinh,
                "@sdt", models.SDT,
                "@diachi", models.DiaChi,
                "@anhdaidien", models.AnhDaiDien);
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

        public List<ChiTietTaiKhoanModel> Search(int pageIndex, int pageSize, out long total, string hoTen, string diaChi, string gioiTinh)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "searchchitiettaikhoan",
                    "@pageindex", pageIndex,
                    "@pagesize", pageSize,
                    "@hoten", hoTen,
                    "@diachi", diaChi,
                    "@gioitinh", gioiTinh);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<ChiTietTaiKhoanModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaiKhoanModel GetByID (int idTaiKhoan)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "getbyidtaikhoan",
                    "@idtaikhoan", idTaiKhoan);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<TaiKhoanModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
  
