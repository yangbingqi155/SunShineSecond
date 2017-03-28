namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FriendURL")]
    public partial class FriendURL
    {
        [Key]
        [StringLength(50)]
        public string idurl { get; set; }

        [StringLength(500)]
        public string url { get; set; }

        [StringLength(200)]
        public string title { get; set; }

        public int? sortno { get; set; }
    }
}
