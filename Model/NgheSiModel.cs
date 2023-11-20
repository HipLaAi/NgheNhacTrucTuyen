using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NgheSiModel
    {
        public int IDNgheSi { get; set; }
        public string TenNgheSi { get; set; }
        public string AnhDaiDien { get; set; }
        public string MoTa {  get; set; }
        public int SoLuongBaiHat { get; set; }
        public List<NhacModel> list_jsonchitietnhactheonghesi { get; set; }
        public List<AlbumModel> list_jsonchitietalbumtheonghesi { get; set; }
    }
}
