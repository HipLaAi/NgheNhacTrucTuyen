using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial interface INgheSiBLL
    {
        NgheSiModel Create(NgheSiModel model);
        bool Delete(int idNgheSi);
        bool Update(NgheSiModel model);
        List<NgheSiModel> Search(int pageIndex, int pageSize, out long total, string tenNgheSi);
        List<NgheSiModel> GetByID(int idNgheSi);
        NgheSiModel GetByName(string tenNgheSi);
        List<NgheSiModel> GetAll();

    }
}
