using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AlbumModel
    {
        public int IDAlbum {  get; set; }
        public int IDNgheSi { get; set; }
        public string TenAlbum { get; set; }
        public string MoTa { get; set; }
        public string AnhDaiDien { get; set; }
        public List<ChiTietAlbum> list_json_chitietalbum { get; set; }
        public List<NhacModel> list_jsonnhactrongalbum { get; set; }
        public List<NhacModel> list_jsonnhactheonghesikhongcotrongalbum { get; set; }
    }

    public class ChiTietAlbum
    {
        public int IDChiTietAlbum { get; set; }
        public int IDAlbum { get; set;}
        public int IDNhac { get; set; }
        public int Status { get; set; }
    }
}