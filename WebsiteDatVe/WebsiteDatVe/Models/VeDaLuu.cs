namespace WebsiteDatVe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VeDaLuu")]
    public partial class VeDaLuu
    {
        [Key]
        public long MaVeDaLuu { get; set; }

        public long? MaChuyenBay { get; set; }

        public long? MaTaiKhoan { get; set; }

        public virtual ChuyenBay ChuyenBay { get; set; }
    }
}
