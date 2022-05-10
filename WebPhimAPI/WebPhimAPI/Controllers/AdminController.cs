using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPhimAPI.Models;

namespace WebPhimAPI.Controllers
{
    public class AdminController : ApiController
    {
        PhimDataDataContext data = new PhimDataDataContext();

        [HttpGet]
        [Route("api/Admin/getphim")]
        public HttpResponseMessage GetPhim()
        {
            var l = from p in data.PHIMs
                    join n in data.NHASANXUATs on p.MaNSX equals n.MaNSX
                    select new
                    {
                        p.TenPhim,
                        p.MoTa,
                        p.MaPhim,
                        p.AnhBia,
                        p.NgayCapNhat,
                        p.MaTheLoai,
                        n.TenNSX,
                        p.LuotXem
                    };

            var ListPhim = from phim in l
                           join t in data.THELOAIs on phim.MaTheLoai equals t.MaTheLoai
                           select new
                           {
                               phim.TenPhim,
                               phim.MoTa,
                               phim.MaPhim,
                               phim.AnhBia,
                               phim.NgayCapNhat,
                               t.TenTheLoai,
                               phim.TenNSX,
                               phim.LuotXem
                           };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }


        [HttpGet]
        [Route("api/Admin/getnsx")]
        public HttpResponseMessage GetNSX()
        {
            var nsx = from n in data.NHASANXUATs select new { 
                n.MaNSX,
                n.TenNSX
            }; 
            return Request.CreateResponse(HttpStatusCode.OK, nsx);
        }

        [HttpGet]
        [Route("api/Admin/gettl")]
        public HttpResponseMessage GetTL()
        {
            var tl = from t in data.THELOAIs select new { 
                t.MaTheLoai,
                t.TenTheLoai
            };
            return Request.CreateResponse(HttpStatusCode.OK, tl);
        }

        [HttpPost]
        [Route("api/Admin/addfilm")]
        public HttpResponseMessage Regis(AddFilm user)
        {

            PHIM add = new PHIM();
            add.MaPhim = user.MaPhim;
            add.TenPhim = user.TenPhim;
            add.AnhBia = user.AnhBia;
            add.MoTa = user.MoTa;
            add.MaNSX = user.MaNSX;
            add.MaTheLoai = user.MaTheLoai;
            add.NgayCapNhat = DateTime.Parse(user.NgayCapNhat);
            add.LuotXem = 0;
            data.PHIMs.InsertOnSubmit(add);

            VIDEO vd = new VIDEO();
            vd.MaPhim = user.MaPhim;
            vd.Video1 = user.Link;
            vd.TenPhim = user.TenPhim;
            vd.MoTa = user.MoTa;
            vd.AnhBia = user.AnhBia;
            vd.NgayCapNhat = DateTime.Parse(user.NgayCapNhat);
            data.VIDEOs.InsertOnSubmit(vd);

            data.SubmitChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }


        [HttpGet]
        [Route("api/Admin/getnsx")]
        public HttpResponseMessage GetSX(int id)
        {
            var nsx = from n in data.NHASANXUATs where n.MaNSX==id
                      select new
                      {
                          n.MaNSX,
                          n.TenNSX
                      };
            return Request.CreateResponse(HttpStatusCode.OK, nsx);
        }

        [HttpGet]
        [Route("api/Admin/gettl")]
        public HttpResponseMessage GetTheL(int id)
        {
            var tl = from t in data.THELOAIs where t.MaTheLoai==id
                     select new
                     {
                         t.MaTheLoai,
                         t.TenTheLoai
                     };
            return Request.CreateResponse(HttpStatusCode.OK, tl);
        }


        [HttpDelete]
        [Route("api/Admin/deletefilm")]
        public HttpResponseMessage DeleteFilm(int id)
        {
            var tl = data.PHIMs.SingleOrDefault(n=>n.MaPhim==id);
            data.PHIMs.DeleteOnSubmit(tl);
            data.SubmitChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "da xoa");
        }

        [HttpGet]
        [Route("api/Admin/getlinkfilm")]
        public HttpResponseMessage GetLinkFilm(int id)
        {
            var tl = data.VIDEOs.SingleOrDefault(n => n.MaPhim == id);
   
            return Request.CreateResponse(HttpStatusCode.OK, tl.Video1);
        }

        [HttpPut]
        [Route("api/updatefilm")]
        public HttpResponseMessage Update(AddFilm user)
        {
            var add = data.PHIMs.SingleOrDefault(n => n.MaPhim == user.MaPhim);

            add.TenPhim = user.TenPhim;
            add.AnhBia = user.AnhBia;
            add.MoTa = user.MoTa;
            add.MaNSX = user.MaNSX;
            add.MaTheLoai = user.MaTheLoai;
            add.NgayCapNhat = DateTime.Parse(user.NgayCapNhat);
            add.LuotXem = 0;

            var vd = data.VIDEOs.SingleOrDefault(n => n.MaPhim == user.MaPhim);

            vd.Video1 = user.Link;
            vd.TenPhim = user.TenPhim;
            vd.MoTa = user.MoTa;
            vd.AnhBia = user.AnhBia;
            vd.NgayCapNhat = DateTime.Parse(user.NgayCapNhat);

            data.SubmitChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Update Success");

        }
    }
}
