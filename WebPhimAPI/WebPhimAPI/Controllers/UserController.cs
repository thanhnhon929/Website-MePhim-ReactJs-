using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPhimAPI.Models;

namespace WebPhimAPI.Controllers
{
    public class UserController : ApiController
    {
        PhimDataDataContext data = new PhimDataDataContext();
        // GET: User
        [HttpGet]
        [Route("api/Phim/getuser")]
        public HttpResponseMessage GetUser(String UserName, String Password)
        {
            var User = from us in data.NGUOIXEMs where us.TaiKhoan==UserName && us.MatKhau==Password 
                       select new
                       {
                           us.TaiKhoan,
                           us.MatKhau,
                       };
            return Request.CreateResponse(HttpStatusCode.OK, User);
        }

        [HttpPost]
        [Route("api/Phim/getregis")]
        public HttpResponseMessage Regis(Regis user)
        {

            NGUOIXEM kh = new NGUOIXEM();
            kh.HoTen = user.Hovaten;
            kh.TaiKhoan = user.TenDN;
            kh.MatKhau = user.Password;
            kh.Email = user.Email;
            kh.DienThoai = user.SoDT;
            kh.NgaySinh = DateTime.Parse(user.NgaySinh);
            kh.DiaChi = user.DiaChi;
            data.NGUOIXEMs.InsertOnSubmit(kh);
            data.SubmitChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
