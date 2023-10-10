namespace COVID19.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BenhNhan")]
    public partial class BenhNhan
    {
        [Key]
        [StringLength(6)]
        public string MaBN { get; set; }

        [Required]
        [StringLength(150)]
        public string TenBN { get; set; }

        public int MaTT { get; set; }

        public string GhiChu { get; set; }

        [StringLength(6)]
        public string BNTXG { get; set; }

        public virtual TinhTrang TinhTrang { get; set; }

    }
}
