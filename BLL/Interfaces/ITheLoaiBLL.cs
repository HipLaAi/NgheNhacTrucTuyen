﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial interface ITheLoaiBLL
    {
        TheLoaiModel Create(TheLoaiModel model);
        bool Delete(int idTheLoai);
        bool Update(TheLoaiModel model);
        List<TheLoaiModel> Search(int pageIndex, int pageSize, out long total, string tenTheLoai);
        TheLoaiModel GetByName(string tenTheLoai);
        List<TheLoaiModel> GetByID(int idTheLoai);
        List<TheLoaiModel> GetAll();
        bool DeleteData(TheLoaiDataModel model);
        List<TheLoaiModel> TopHot(int top);
    }
}
