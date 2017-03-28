namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SEO")]
    public partial class SEO
    {
        [Key]
        [StringLength(50)]
        public string idseo { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        [StringLength(500)]
        public string name { get; set; }

        [StringLength(5000)]
        public string seotitle { get; set; }

        [StringLength(5000)]
        public string seokeywords { get; set; }

        [StringLength(5000)]
        public string seodescription { get; set; }
    }
}
