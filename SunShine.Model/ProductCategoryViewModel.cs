using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SunShine.Utils;

namespace SunShine.Model {
    [NotMapped]
    public class ProductCategoryViewModel :ProductCategory{

        [Display(Name = "类别编号")]
        [StringLength(50)]
        public new string idcategory { get; set; }

        [Display(Name = "代码")]
        public new string categorycode { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(50)]
        public new string categoryname { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        [Display(Name = "分组方式")]
        public new int groupmethod { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }

        [Display(Name = "SEO标题")]
        public new string title { get; set; }

        [Display(Name = "SEO关键词")]
        public new string keyword { get; set; }

        [Display(Name = "SEO描述")]
        public new string description { get; set; }

        [Display(Name = "搜索推荐")]
        public new bool isintro { get; set; }

        public static List<SelectItemViewModel<int>> GroupMethodTypeSelectItems {
            get { 
            List<SelectItemViewModel<int>> list = new List<SelectItemViewModel<int>>();
            list.Add(new SelectItemViewModel<int>() {
                DisplayText = AttributeHelper.GetDisplayName<GroupMethodType>(GroupMethodType.Industry),
                DisplayValue = (int)GroupMethodType.Industry
            });
            list.Add(new SelectItemViewModel<int>() {
                DisplayText = AttributeHelper.GetDisplayName<GroupMethodType>(GroupMethodType.Usage),
                DisplayValue = (int)GroupMethodType.Usage
            });
            list.Add(new SelectItemViewModel<int>() {
                DisplayText = AttributeHelper.GetDisplayName<GroupMethodType>(GroupMethodType.Product),
                DisplayValue = (int)GroupMethodType.Product
            });
            return list;
            }
        }

        [Display(Name = "分组方式")]
        public  string groupmethodname { get {
               var methodTypes= GroupMethodTypeSelectItems.Where(en => en.DisplayValue == groupmethod).ToList();
                return (methodTypes != null && methodTypes.Count > 0) ? methodTypes.First().DisplayText : "";
            }  }

        public void CopyFromBase(ProductCategory category) {
            this.idcategory = category.idcategory;
            this.categorycode = category.categorycode;
            this.categoryname = category.categoryname;
            this.sortno = category.sortno;
            this.inuse = category.inuse;
            this.groupmethod = category.groupmethod;
            this.cretime = category.cretime;
            this.description = category.description;
            this.keyword = category.keyword;
            this.title = category.title;
            this.isintro = category.isintro;
        }

        public void CopyToBase(ProductCategory category) {
            category.idcategory = this.idcategory;
            category.categorycode = this.categorycode;
            category.categoryname = this.categoryname;
            category.sortno = this.sortno;
            category.inuse = this.inuse;
            category.groupmethod = this.groupmethod;
            category.cretime = this.cretime;
            category.description = this.description;
            category.keyword = this.keyword;
            category.title = this.title;
            category.isintro = this.isintro;
        }
    }
}
