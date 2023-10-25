using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL
{
    public class AlbumBLL: IAlbumBLL
    {
        private IAlbumDAL _res;
        public AlbumBLL(IAlbumDAL res)
        {
            _res = res;
        }

        public bool Create(AlbumModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(int idAlbum)
        {
            return _res.Delete(idAlbum);
        }

        public bool Update(AlbumModel model)
        {
            return _res.Update(model);
        }

        public List<AlbumModel> Search(int pageIndex, int pageSize, out long total, string tenAlbum)
        {
            return _res.Search(pageIndex, pageSize, out total, tenAlbum);
        }
    }
}
