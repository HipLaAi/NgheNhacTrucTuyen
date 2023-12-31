﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial interface INhacDAL
    {
        //bool Create(NhacModel model);
        bool Delete(int idNhac);
        bool Update(NhacModel model);
        List<NhacModel> Search(int pageIndex, int pageSize, out long total, string tenNhac, int idTheLoai, int idNgheSi);
        List<NhacModel> GetByID(int idNhac);
        NhacModel Create(NhacModel model);
        List<NhacModel> GetNhacByIDNgheSi (int idNgheSi);
        bool UpdateView(int idNhac);
        List<NhacModel> TopHot(int top);
    }
}
