using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TheLoaiBLL : ITheLoaiBLL
    {
        private ITheLoaiDAL _res;
        public TheLoaiBLL(ITheLoaiDAL res)
        {
            _res = res;
        }

        public bool Create(TheLoaiModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(int idTheLoai)
        {
            return _res.Delete(idTheLoai);
        }

        public bool Update(TheLoaiModel model)
        {
            return _res.Update(model);
        }

        public List<TheLoaiModel> Search(int pageIndex, int pageSize, out long total, string tenTheLoai)
        {
            return _res.Search(pageIndex, pageSize, out total, tenTheLoai);
        }
        public TheLoaiModel GetByName(string tenTheLoai)
        {
            return _res.GetByName(tenTheLoai);
        }
        public List<TheLoaiModel> GetByID(int idTheLoai)
        {
            return _res.GetByID(idTheLoai);
        }
        public List<TheLoaiModel> GetAll()
        {
            return _res.GetAll();
        }
    }
}