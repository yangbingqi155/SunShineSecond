namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WebSiteInfo")]
    public partial class WebSiteInfo
    {
        [Key]
        [StringLength(50)]
        public string idsite { get; set; }

        [StringLength(500)]
        public string sitename { get; set; }

        [StringLength(500)]
        public string copyright { get; set; }

        [StringLength(500)]
        public string support { get; set; }

        [StringLength(500)]
        public string address { get; set; }

        [StringLength(50)]
        public string hotphoneallcountry { get; set; }

        [StringLength(500)]
        public string hotphone { get; set; }

        [StringLength(500)]
        public string hotphone2 { get; set; }

        [StringLength(500)]
        public string phone { get; set; }

        [StringLength(500)]
        public string qq { get; set; }

        [StringLength(500)]
        public string qq2 { get; set; }
    }
}
