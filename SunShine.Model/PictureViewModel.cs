using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunShine.Model {
    [NotMapped]
    public class PictureViewModel:Picture {

        [Display(Name = "图片编号")]
        [StringLength(50)]
        public new string idimage { get; set; }

        [Display(Name = "模块编号")]
        [Required]
        [StringLength(50)]
        public new string idmodule { get; set; }

        [Display(Name = "模块类型")]
        public new int moduletype { get; set; }

        [Display(Name = "图片路径")]
        [Required]
        public new string path { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }

        public void CopyFromBase(Picture picture) {
            this.idimage = picture.idimage;
            this.idmodule = picture.idmodule;
            this.moduletype = picture.moduletype;
            this.path = picture.path;
            this.sortno = picture.sortno;
            this.cretime = picture.cretime;
        }

        public void CopyToBase(Picture picture) {
            picture.idimage = this.idimage;
            picture.idmodule = this.idmodule;
            picture.moduletype = this.moduletype;
            picture.path = this.path;
            picture.sortno = this.sortno;
            picture.cretime = this.cretime;
        }
    }
}
