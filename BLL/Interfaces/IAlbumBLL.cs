using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial interface IAlbumBLL
    {
        bool Create(AlbumModel model);
        bool Delete(string idAlbum);
        bool Update(AlbumModel model);
        public List<AlbumModel> Search(int pageIndex, int pageSize, out long total, string tenAlbum);
    }
}
