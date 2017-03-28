namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [Key]
        [StringLength(50)]
        public string idcategory { get; set; }

        [StringLength(50)]
        public string categorycode { get; set; }

        [Required]
        [StringLength(50)]
        public string categoryname { get; set; }

        public int? sortno { get; set; }

        public bool inuse { get; set; }

        public int groupmethod { get; set; }

        public DateTime? cretime { get; set; }

        [StringLength(500)]
        public string title { get; set; }

        [StringLength(500)]
        public string keyword { get; set; }

        [StringLength(5000)]
        public string description { get; set; }

        public bool isintro { get; set; }
    }
}
