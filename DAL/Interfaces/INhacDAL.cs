using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial interface INhacDAL
    {
        bool Create(NhacModel model);
        bool Delete(string idNhac);
        bool Update(NhacModel model);
        public List<NhacModel> Search(int pageIndex, int pageSize, out long total, string tenNhac, int idTheLoai, int idNgheSi);
    }
}
