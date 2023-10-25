using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class AlbumDAL : IAlbumDAL
    {
        private IDatabaseHelper _dbHelper;
        public AlbumDAL(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public bool Create(AlbumModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "createalbum",
                "@idnghesi",model.IDNgheSi,
                "@tenalbum", model.TenAlbum,
                "@mota", model.MoTa,
                "@anhdaidien", model.AnhDaiDien,
                "@list_json_chitietalbum", model.list_json_chitietalbum != null ? MessageConvert.SerializeObject(model.list_json_chitietalbum) : null);
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

        public bool Delete(int idAlbum)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "deletealbum",
                "@idalbum", idAlbum);
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

        public bool Update(AlbumModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "updatealbum",
                "@idalbum", model.IDAlbum,
                "@idnghesi", model.IDNgheSi,
                "@tenalbum", model.TenAlbum,
                "@mota", model.MoTa,
                "@anhdaidien", model.AnhDaiDien,
                "@list_json_chitietalbum", model.list_json_chitietalbum != null ? MessageConvert.SerializeObject(model.list_json_chitietalbum) : null);
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

        public List<AlbumModel> Search(int pageIndex, int pageSize, out long total, string tenAlbum)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "searchalbum",
                    "@pageindex", pageIndex,
                    "@pagesize", pageSize,
                    "@tenalbum", tenAlbum);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<AlbumModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AlbumModel> TopNew(int top)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "newalbum",
                    "@top", top);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<AlbumModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AlbumModel> GetByID(int idAlbum)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "detailalbum",
                    "@idalbum", idAlbum);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<AlbumModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AlbumModel GetByName(string tenAlbum)
        {
            string msgError = "";
            try
            {
                var dn = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "getbytenalbum",
                    "@tenalbum", tenAlbum);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dn.ConvertTo<AlbumModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
