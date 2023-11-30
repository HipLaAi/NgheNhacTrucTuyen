using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TheLoaiDAL : ITheLoaiDAL
    {
        private IDatabaseHelper _dbHelper;
        public TheLoaiDAL(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public TheLoaiModel Create(TheLoaiModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "createtheloai",
                "@tentheloai", model.TenTheLoai,
                "@anhdaidien", model.AnhDaiDien,
                "@mota", model.MoTa);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return result.ConvertTo<TheLoaiModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int idTheLoai)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "deletetheloai",
                "@idtheloai", idTheLoai);
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

        public bool Update(TheLoaiModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "updatetheloai",
                "@idtheloai", model.IDTheLoai,
                "@tentheloai", model.TenTheLoai,
                "@anhdaidien", model.AnhDaiDien,
                "@mota",model.MoTa,
                "@soluongbaihat",model.SoLuongBaiHat);
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

        public List<TheLoaiModel> Search(int pageIndex, int pageSize, out long total, string tenTheLoai)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "searchtheloai",
                    "@pageindex", pageIndex,
                    "@pagesize", pageSize,
                    "@tentheloai", tenTheLoai);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<TheLoaiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TheLoaiModel GetByName(string tenTheLoai)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "getbytentheloai",
                    "@tentheloai", tenTheLoai);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<TheLoaiModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TheLoaiModel> GetByID(int idTheLoai)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "detailtheloai",
                    "@idtheloai", idTheLoai);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<TheLoaiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TheLoaiModel> GetAll()
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "getalltheoai");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<TheLoaiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteData(TheLoaiDataModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "deletenhieutheloai",
                "@list_json_idtheloai", model.list_json_idtheloai != null ? MessageConvert.SerializeObject(model.list_json_idtheloai) : null);
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
        public List<TheLoaiModel> TopHot(int top)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "toptheloai",
                    "@top", top);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<TheLoaiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
