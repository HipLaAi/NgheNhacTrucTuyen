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
        public string TenAlbum { get; set; }
        public string MoTa { get; set; }
        public string AnhDaiDien { get; set; }
        public List<ChiTietAlbum> list_json_chitietalbum { get; set; }
    }

    public class ChiTietAlbum
    {
        public string IDChiTietAlbum { get; set; }
        public string IDAlbum { get; set;}
        public string IDNhac { get; set; }
    }
}