using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPhimAPI.Models
{
    public class AddFilm
    {
        public int MaPhim { get; set; }
        public String TenPhim { get; set; }
        public String MoTa { get; set; }
        public String AnhBia { get; set; }
        public String NgayCapNhat { get; set; }
        public int MaTheLoai { get; set; }
        public int MaNSX { get; set; }
        public long LuotXem { get; set; }

        public String Link { get; set; }
    }
}