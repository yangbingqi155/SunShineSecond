namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ManageUser")]
    public partial class ManageUser
    {
        [Key]
        [StringLength(50)]
        public string iduser { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        [Required]
        [StringLength(100)]
        public string md5salt { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(500)]
        public string notes { get; set; }

        public bool inuse { get; set; }
    }
}
