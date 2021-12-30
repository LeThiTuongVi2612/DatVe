namespace WebsiteDatVe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangBay")]
    public partial class HangBay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangBay()
        {
            MayBays = new HashSet<MayBay>();
        }

        [Key]
        public long MaHangBay { get; set; }

        [StringLength(50)]
        public string TenHangBay { get; set; }

        [Column(TypeName = "text")]
        public string Logo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MayBay> MayBays { get; set; }
    }
}
