using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhacDAL : INhacDAL
    {
        private IDatabaseHelper _dbHelper;
        public NhacDAL(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        //public bool Create(NhacModel model)
        //{
        //    string msgError = "";
        //    try
        //    {
        //        var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "createnhac",
                //"@tennhac", model.TenNhac,
                //"@idtheloai", model.IDTheLoai,
                //"@idnghesi", model.IDNgheSi,
                //"@audio", model.Audio,
                //"@img", model.IMG,
                //"@lyrics", model.Lyrics);
        //        if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
        //        {
        //            throw new Exception(Convert.ToString(result) + msgError);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //-------------------------------------------------
        public NhacModel Create(NhacModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "createnhac",
                "@tennhac", model.TenNhac,
                "@idtheloai", model.IDTheLoai,
                "@idnghesi", model.IDNgheSi,
                "@audio", model.Audio,
                "@img", model.IMG,
                "@lyrics", model.Lyrics);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return result.ConvertTo<NhacModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------

        public bool Delete(int idNhac)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "deletenhac",
                "@idnhac", idNhac);
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

        public bool Update(NhacModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "updatenhac",
                "@idnhac", model.IDNhac,
                "@tennhac", model.TenNhac,
                "@idtheloai", model.IDTheLoai,
                "@idnghesi", model.IDNgheSi,
                "@audio", model.Audio,
                "@img", model.IMG,
                "@lyrics", model.Lyrics);
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

        public List<NhacModel> Search(int pageIndex, int pageSize, out long total, string tenNhac, int idTheLoai, int idNgheSi)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "searchnhac",
                    "@pageindex", pageIndex,
                    "@pagesize", pageSize,
                    "@tennhac", tenNhac,
                    "@idtheloai", idTheLoai,
                    "@idnghesi", idNgheSi);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<NhacModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NhacModel> GetByID(int idNhac)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "detailnhac",
                    "@idnhac", idNhac);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<NhacModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
