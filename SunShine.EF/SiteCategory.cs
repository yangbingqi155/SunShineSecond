namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteCategory")]
    public partial class SiteCategory
    {
        [Key]
        [StringLength(50)]
        public string idcategory { get; set; }

        [StringLength(50)]
        public string categorycode { get; set; }

        [Required]
        [StringLength(100)]
        public string categoryname { get; set; }

        [StringLength(100)]
        public string englishname { get; set; }

        [StringLength(50)]
        public string parentid { get; set; }

        public int? level { get; set; }

        public DateTime? cretime { get; set; }

        public bool inuse { get; set; }

        [StringLength(500)]
        public string title { get; set; }

        [StringLength(500)]
        public string keyword { get; set; }

        [StringLength(5000)]
        public string description { get; set; }

        public int pagesize { get; set; }
    }
}
