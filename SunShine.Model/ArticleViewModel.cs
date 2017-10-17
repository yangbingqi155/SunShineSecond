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
    public class ArticleViewModel:Article {
        [Display(Name = "文章编号")]
        [StringLength(50)]
        public new string idarticle { get; set; }

        [Display(Name = "标题")]
        [Required]
        [StringLength(100)]
        public new string title { get; set; }

        [Display(Name = "标题图")]
        [StringLength(500)]
        public new string img { get; set; }

        [Display(Name = "内容简介")]
        [StringLength(5000)]
        public new string introduction{get;set;}

        [Display(Name = "内容")]
        public new string content { get; set; }

        [Display(Name = "网站类别")]
        [Required]
        [StringLength(50)]
        public new string idcategory { get; set; }

        [Display(Name = "关注量")]
        public new int? follow { get; set; }

        [Display(Name ="排序")]
        public new int sortno { get; set; }

        [Display(Name = "发布时间")]
        public new DateTime? cretime { get; set; }
        
        [Display(Name ="类别")]
        public SiteCategory Category { get; set; }

        [Display(Name = "SEO标题")]
        [StringLength(500)]
        public new string seotitle { get; set; }

        [Display(Name = "SEO关键词")]
        public new string seokeyword { get; set; }

        [Display(Name = "SEO描述")]
        public new string seodescription { get; set; }


        public void CopyFromBase(Article article) {
            this.idarticle = article.idarticle;
            this.title = article.title;
            this.img = article.img;
            this.content = article.content;
            this.idcategory = article.idcategory;
            this.follow = article.follow;
            this.sortno = article.sortno==null?0: article.sortno.Value;
            this.cretime = article.cretime;
            this.introduction = article.introduction;
            this.seotitle = article.seotitle;
            this.seokeyword = article.seokeyword;
            this.seodescription = article.seodescription;
        }

        public void CopyToBase(Article article) {
            article.idarticle = this.idarticle;
            article.title = this.title;
            article.img = this.img;
            article.content = this.content;
            article.idcategory = this.idcategory;
            article.follow = this.follow;
            article.sortno = this.sortno;
            article.cretime = this.cretime;
            article.introduction = this.introduction;
            article.seotitle = this.seotitle;
            article.seokeyword = this.seokeyword;
            article.seodescription = this.seodescription;
        }
    }
}
