namespace WebsiteDatVe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ve")]
    public partial class Ve
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ve()
        {
            KhachHangs = new HashSet<KhachHang>();
            NguoiDatVes = new HashSet<NguoiDatVe>();
        }

        [Key]
        [StringLength(50)]
        public string MaVe { get; set; }

        public long? MaChuyenBay { get; set; }

        [StringLength(50)]
        public string HangVe { get; set; }

        public int? SoLuongGhe { get; set; }

        public long? MaTaiKhoan { get; set; }

        [StringLength(50)]
        public string TinhTrang { get; set; }

        public DateTime? NgayDat { get; set; }

        public double? TongTien { get; set; }

        public virtual ChuyenBay ChuyenBay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDatVe> NguoiDatVes { get; set; }
    }
}
