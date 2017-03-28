using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunShine.Model {
    public enum GroupMethodType {
        [Display(Name = "所有分类")]
        ALL =0,

        [Display(Name ="用途分类")]
        Usage =100,

        [Display(Name = "行业分类")]
        Industry = 200,

        [Display(Name = "产品分类")]
        Product = 300
    }
}
