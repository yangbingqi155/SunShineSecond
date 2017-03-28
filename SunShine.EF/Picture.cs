namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Picture")]
    public partial class Picture
    {
        [Key]
        [StringLength(50)]
        public string idimage { get; set; }

        [Required]
        [StringLength(50)]
        public string idmodule { get; set; }

        public int moduletype { get; set; }

        [Required]
        public string path { get; set; }

        public int? sortno { get; set; }

        public DateTime? cretime { get; set; }
    }
}
