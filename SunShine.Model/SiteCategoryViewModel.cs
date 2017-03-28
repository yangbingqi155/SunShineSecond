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
    public class SiteCategoryViewModel : SiteCategory {
        [Display(Name = "类别编号")]
        [StringLength(50)]
        public new string idcategory { get; set; }

        [Display(Name = "类别代码")]
        public new string categorycode { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(100)]
        public new string categoryname { get; set; }

        [Display(Name = "英文名")]
        [StringLength(100)]
        public new string englishname { get; set; }

        [Display(Name = "父级编号")]
        [StringLength(50)]
        public new string parentid { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        public new int level { get; set; }

        [Display(Name = "父级类别")]
        public SiteCategory ParentCategory { get; set; }

        [Display(Name = "父级类别路径")]
        public string ParentCategoryNamePath { get; set; }

        [Display(Name = "SEO标题")]
        public new string title { get; set; }

        [Display(Name = "SEO关键词")]
        public new string keyword { get; set; }

        [Display(Name = "SEO描述")]
        public new string description { get; set; }

        [Display(Name = "每页记录数")]
        public new int pagesize { get; set; }

        public void CopyFromBase(SiteCategory category) {
            this.idcategory = category.idcategory;
            this.categorycode = category.categorycode;
            this.categoryname = category.categoryname;
            this.englishname = category.englishname;
            this.inuse = category.inuse;
            this.cretime = category.cretime;
            this.parentid = category.parentid;
            this.description = category.description;
            this.keyword = category.keyword;
            this.title = category.title;
            this.pagesize = category.pagesize;
        }

        public void CopyToBase(SiteCategory category) {
            category.idcategory = this.idcategory;
            category.categorycode = this.categorycode;
            category.categoryname = this.categoryname;
            category.englishname = this.englishname;
            category.inuse = this.inuse;
            category.cretime = this.cretime;
            category.parentid = this.parentid;
            category.description = this.description;
            category.keyword = this.keyword;
            category.title = this.title;
            category.pagesize = this.pagesize;
        }
    }
}
