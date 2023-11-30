using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NhacModel
    {
        public int IDNhac { get; set; }
        public string TenNhac { get; set; }
        public int IDTheLoai { get; set; }
        public int IDNgheSi { get; set; }
        public string Audio { get; set; }
        public string IMG { get; set; }
        public string Lyrics { get; set; }
        public int LuotNghe { get; set; }
        public List<NhacModel> list_jsonchitietnhactheonghesi { get; set; }
        public List<NhacModel> list_jsonchitietnhactheotheloai {  get; set; }
    }
}
