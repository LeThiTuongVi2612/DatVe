using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDatVe.Models
{
    public class ThongTinDatVe
    {
        public long DiemDi { get; set; }
        public long DiemDen { get; set; }
        public int NguoiLon { get; set; }
        public int TreEm { get; set; }
        public int EmBe { get; set; }
        public DateTime NgayDi { get; set; }
        public string HangGhe { get; set; }

    }
}