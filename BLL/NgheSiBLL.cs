using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NgheSiBLL : INgheSiBLL
    {
        private INgheSiDAL _res;
        public NgheSiBLL(INgheSiDAL res)
        {
            _res = res;
        }

        public bool Create(NgheSiModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(int idNgheSi)
        {
            return _res.Delete(idNgheSi);
        }

        public bool Update(NgheSiModel model)
        {
            return _res.Update(model);
        }

        public List<NgheSiModel> Search(int pageIndex, int pageSize, out long total, string tenNgheSi)
        {
            return _res.Search(pageIndex, pageSize, out total, tenNgheSi);
        }
    }
}
