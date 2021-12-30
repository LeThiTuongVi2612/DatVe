namespace WebsiteDatVe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanBay")]
    public partial class SanBay
    {
        [Key]
        public long MaSanBay { get; set; }

        [StringLength(50)]
        public string TenSanBay { get; set; }

        public string DiaChi { get; set; }
    }
}
