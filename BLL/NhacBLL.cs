﻿using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NhacBLL : INhacBLL
    {
        private INhacDAL _res;
        public NhacBLL(INhacDAL res)
        {
            _res = res;
        }

        public bool Create(NhacModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(int idNhac)
        {
            return _res.Delete(idNhac);
        }

        public bool Update(NhacModel model)
        {
            return _res.Update(model);
        }

        public List<NhacModel> Search(int pageIndex, int pageSize, out long total, string tenNhac, int idTheLoai, int idNgheSi)
        {
            return _res.Search(pageIndex, pageSize, out total, tenNhac, idTheLoai, idNgheSi);
        }
        public List<NhacModel> GetByID(int idNhac)
        {
            return _res.GetByID(idNhac);
        }
    }
}