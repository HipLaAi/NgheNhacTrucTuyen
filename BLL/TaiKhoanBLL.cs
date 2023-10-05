﻿using Model;
using DAL;
using BLL;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class TaiKhoanBLL:ITaiKhoanBLL
    {
        private ITaiKhoanDAL _res;
        private string secret;
        public TaiKhoanBLL(ITaiKhoanDAL res, IConfiguration configuration)
        {
            _res = res;
            secret = configuration["AppSettings:Secret"];
        }

        //function login, create token
        public TaiKhoanModel DangNhap(string tenDangnhap, string matkhau)
        {
            var user = _res.DangNhap(tenDangnhap, matkhau);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);              //create key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.TenDangNhap.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128CbcHmacSha256)  // create token by encode key with data user
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public bool Create(TaiKhoanModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(string tenDangNhap)
        {
            return _res.Delete(tenDangNhap);
        }

        public bool Update(TaiKhoanModel model)
        {
            return _res.Update(model);
        }


        //public TaiKhoanModel GetTaiKhoan(string tenDangNhap)
        //{
        //    return _res.GetTaiKhoan(tenDangNhap);
        //}
    }
}