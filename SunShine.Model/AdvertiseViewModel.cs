using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SunShine.EF;

namespace SunShine.Model {
    public class AdvertiseViewModel: Advertise{
        [Display(Name ="编号")]
        [StringLength(50)]
        public new string idadvertise { get; set; }

        [Display(Name ="代码")]
        [StringLength(50)]
        public new string code { get; set; }

        [Display(Name = "标题")]
        [StringLength(100)]
        public new string title { get; set; }

        [Display(Name ="广告图片")]
        public string img { get; set; }

        public void CopyFromBase(Advertise advertise) {
            this.idadvertise = advertise.idadvertise;
            this.code = advertise.code;
            this.title = advertise.title;
        }

        public void CopyToBase(Advertise advertise) {
            advertise.idadvertise = this.idadvertise;
            advertise.code = this.code;
            advertise.title = this.title;
        }
    }
}
