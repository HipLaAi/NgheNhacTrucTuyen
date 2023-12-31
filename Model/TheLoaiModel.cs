﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TheLoaiModel
    {
        public int IDTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public string AnhDaiDien { get; set; }
        public string MoTa {  get; set; }
        public int SoLuongBaiHat { get; set; }
        public List<NhacModel> list_jsonchitietnhactheotheloai { get; set; }
    }
    public class TheLoaiDataModel
    {
        public List<TheLoaiModel> list_json_idtheloai { get; set; }
    }
}
