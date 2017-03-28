namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [StringLength(50)]
        public string idproduct { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [StringLength(5000)]
        public string basicinfo { get; set; }

        [StringLength(500)]
        public string img { get; set; }

        [StringLength(50)]
        public string idcategory { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public DateTime? cretime { get; set; }

        public int? sortno { get; set; }

        public bool inuse { get; set; }

        public bool ishot { get; set; }

        public bool isnew { get; set; }

        [StringLength(500)]
        public string seotitle { get; set; }

        public string seokeyword { get; set; }

        public string seodescription { get; set; }
    }
}
