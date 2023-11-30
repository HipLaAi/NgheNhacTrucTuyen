using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial interface IAlbumDAL
    {
        bool Create(AlbumModel model);
        bool Delete(int idAlbum);
        bool Update(AlbumModel model);
        public List<AlbumModel> Search(int pageIndex, int pageSize, out long total, string tenAlbum);
        List<AlbumModel> TopNew(int top);
        List<AlbumModel> GetByID(int idAlbum);
        AlbumModel GetByName(string tenAlbum);
        List<AlbumModel> TopHot(int top);

    }
}
