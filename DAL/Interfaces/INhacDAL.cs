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
        bool Delete(int idNhac);
        bool Update(NhacModel model);
        List<NhacModel> Search(int pageIndex, int pageSize, out long total, string tenNhac, int idTheLoai, int idNgheSi);
        List<NhacModel> GetByID(int idNhac);
    }
}
