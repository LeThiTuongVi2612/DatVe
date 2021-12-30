namespace WebsiteDatVe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MayBay")]
    public partial class MayBay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MayBay()
        {
            ChuyenBays = new HashSet<ChuyenBay>();
        }

        [Key]
        [StringLength(10)]
        public string MaMayBay { get; set; }

        public int? SoGhePhoThong { get; set; }

        public int? SoGheThuongGia { get; set; }

        public int? SoGhePhoThongDacBiet { get; set; }

        public int? SoGheHangNhat { get; set; }

        public long? MahangBay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuyenBay> ChuyenBays { get; set; }

        public virtual HangBay HangBay { get; set; }
    }
}
