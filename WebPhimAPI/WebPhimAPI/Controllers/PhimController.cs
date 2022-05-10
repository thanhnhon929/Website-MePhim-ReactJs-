using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPhimAPI.Models;

namespace WebPhimAPI.Controllers
{
    public class PhimController : ApiController
    {
        PhimDataDataContext data = new PhimDataDataContext();

        [HttpGet]
        [Route("api/Phim/getphim")]
        public HttpResponseMessage GetPhim()
        {
            var ListPhim = from phim in data.PHIMs select new {
                phim.TenPhim, phim.MoTa, phim.MaPhim, phim.AnhBia, phim.NgayCapNhat, phim.MaTheLoai, phim.MaNSX,
                phim.LuotXem
            };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

        [HttpGet]
        [Route("api/Phim/getpb")]
        public HttpResponseMessage GetPhimBo()
        {
            var ListPhim = from pb in data.PHIMBOs select new { 
                pb.AnhBia, pb.MaPhimBo, pb.TenPhimBo, pb.MaNSX, pb.MaPhim
            };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

        [HttpGet]
        [Route("api/Phim/getpl")]
        public HttpResponseMessage GetPhimLe()
        {
            var ListPhim = from pl in data.PHIMLEs
                           select new
                           {
                               pl.AnhBia, pl.MaPhimLe,pl.TenPhimLe,pl.MaNSX,pl.MaPhim
                           };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

        [HttpGet]
        [Route("api/Phim/getcr")]
        public HttpResponseMessage GetChieuRap()
        {
            var ListPhim = from cr in data.CHIEURAPs
                           select new
                           {
                               cr.AnhBia,cr.MaChieuRap,cr.TenChieuRap,cr.MaNSX,cr.MaPhim
                           };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

        [HttpGet]
        [Route("api/Phim/getxn")]
        public HttpResponseMessage GetXemNhieu()
        {
            var ListPhim = from cr in data.PHIMs orderby cr.LuotXem descending
                           select new
                           {
                               cr.AnhBia, cr.TenPhim, cr.MaPhim, cr.LuotXem,cr.MoTa
                           };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

        [HttpGet]
        [Route("api/Phim/getct")]
        public HttpResponseMessage GetChiTietPhim(int id)
        {
            var ListPhim = from phim in data.PHIMs
                           where phim.MaPhim == id select new
                           {
                               phim.TenPhim,
                               phim.MoTa,
                               phim.MaPhim,
                               phim.AnhBia,
                               phim.NgayCapNhat,
                               phim.MaTheLoai,
                               phim.MaNSX,
                               phim.LuotXem
                           };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

        [HttpGet]
        [Route("api/Phim/getvd")]
        public HttpResponseMessage Getvd(int id)
        {
            var ListPhim = from phim in data.VIDEOs
                           where phim.MaPhim == id
                           select new
                           {
                               phim.TenPhim,
                               phim.MoTa,
                               phim.MaPhim,
                               phim.AnhBia,
                               phim.NgayCapNhat,
                               phim.Video1
                           };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

        [HttpGet]
        [Route("api/Phim/getsearch")]
        public HttpResponseMessage Getsearch(String id)
        {
            if (string.IsNullOrEmpty(id)) {
                var SearchPhim = from phim in data.PHIMs
                               orderby phim.LuotXem descending
                               select new
                               {
                                   phim.TenPhim,
                                   phim.MoTa,
                                   phim.MaPhim,
                                   phim.AnhBia,
                                   phim.NgayCapNhat,
                                   phim.LuotXem

                               };
                return Request.CreateResponse(HttpStatusCode.OK, SearchPhim);
            }
            var ListPhim = from phim in data.PHIMs
                           where phim.TenPhim.Contains(id)
                           orderby phim.LuotXem descending
                           select new
                           {
                               phim.TenPhim,
                               phim.MoTa,
                               phim.MaPhim,
                               phim.AnhBia,
                               phim.NgayCapNhat,
                               phim.LuotXem

                           };
            return Request.CreateResponse(HttpStatusCode.OK, ListPhim);
        }

    }
}
