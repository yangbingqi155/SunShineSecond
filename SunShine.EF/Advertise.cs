namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertise")]
    public partial class Advertise
    {
        [Key]
        [StringLength(50)]
        public string idadvertise { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        [StringLength(100)]
        public string title { get; set; }
    }
}
