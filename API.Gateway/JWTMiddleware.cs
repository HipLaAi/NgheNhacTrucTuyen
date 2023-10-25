using Microsoft.Extensions.Options;
using BLL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace API.Gateway
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private ITaiKhoanBLL _taikhoanBusiness;
        
        public JWTMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ITaiKhoanBLL taiKhoanBusiniess)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _taikhoanBusiness = taiKhoanBusiniess;
        }

        public async Task GenerateToken(HttpContext context)
        {
            var taiKhoan = context.Request.Form["username"].ToString();
            var matKhau = context.Request.Form["password"].ToString();
            var user = _taikhoanBusiness.DangNhap(taiKhoan, matKhau);
            var response = new { MaNguoiDung = user.IDTaiKhoan, Email = user.Email, Token = user.Token };
            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
            return;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            if (!context.Request.Path.Equals("/api/token", StringComparison.Ordinal))
            {
                return _next(context);
            }
            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context);
            }
            context.Response.StatusCode = 400;
            return context.Response.WriteAsync("Bad request.");
        }
    }
}