using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SunShine.EF;

namespace SunShine.Model {
    public class SEOViewModel:SEO {
        [Display(Name ="编号")]
        [StringLength(50)]
        public new string idseo { get; set; }

        [Display(Name = "代码")]
        [StringLength(50)]
        public new string code { get; set; }

        [Display(Name = "页面名称")]
        [StringLength(500)]
        public new string name { get; set; }

        [Display(Name = "SEO标题")]
        [StringLength(5000)]
        public new string seotitle { get; set; }

        [Display(Name = "SEO关键词")]
        [StringLength(5000)]
        public new string seokeywords { get; set; }

        [Display(Name = "SEO描述")]
        [StringLength(5000)]
        public new string seodescription { get; set; }

        public void CopyFromBase(SEO seo) {
            this.idseo = seo.idseo;
            this.code = seo.code;
            this.name = seo.name;
            this.seotitle = seo.seotitle;
            this.seokeywords = seo.seokeywords;
            this.seodescription = seo.seodescription;
        }

        public void CopyToBase(SEO seo) {
            seo.idseo = this.idseo;
            seo.code = this.code;
            seo.name = this.name;
            seo.seotitle = this.seotitle;
            seo.seokeywords = this.seokeywords;
            seo.seodescription = this.seodescription;
        }
    }
}
