using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NgheSiDAL : INgheSiDAL
    {
        private IDatabaseHelper _dbHelper;
        public NgheSiDAL(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public NgheSiModel Create(NgheSiModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "createnghesi",
                "@tennghesi", model.TenNgheSi,
                "@anhdaidien", model.AnhDaiDien,
                "@mota", model.MoTa);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return result.ConvertTo<NgheSiModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int idNgheSi)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "deletenghesi",
                "@idnghesi", idNgheSi);
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

        public bool Update(NgheSiModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "updatenghesi",
                "@idnghesi", model.IDNgheSi,
                "@tennghesi", model.TenNgheSi,
                "@anhdaidien", model.AnhDaiDien,
                "@mota", model.MoTa,
                "@soluongbaihat", model.SoLuongBaiHat);
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

        public List<NgheSiModel> Search(int pageIndex, int pageSize, out long total, string tenNgheSi)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "searchnghesi",
                    "@pageindex", pageIndex,
                    "@pagesize", pageSize,
                    "@tennghesi", tenNgheSi);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<NgheSiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NgheSiModel> GetByID(int idNgheSi)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "detailnghesi",
                    "@idnghesi", idNgheSi);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<NgheSiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NgheSiModel GetByName(string tenNgheSi)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "getbytennghesi",
                    "@tennghesi", tenNgheSi);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<NgheSiModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NgheSiModel> GetAll()
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "getallnghesi");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<NgheSiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
