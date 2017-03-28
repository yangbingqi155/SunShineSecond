using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using SunShine.Model;
using SunShine.Utils;

namespace SunShine.BLL {
    public class ProductCategoryService {

        public static List<ProductCategory> GetALL() {
            TN db = new TN();
            return db.ProductCategories.OrderBy(en => en.sortno).ToList();
        }

        public static List<ProductCategory> Search(int groupmethod = 0) {
            TN db = new TN();
            return db.ProductCategories.Where(en=>(groupmethod==0 ||en.groupmethod==groupmethod)).OrderBy(en => en.sortno).ToList();
        }

        public static List<SelectItemViewModel<string>> SelectItems(GroupMethodType methodType) {
            List<SelectItemViewModel<string>> mercTypeOptions = new List<SelectItemViewModel<string>>();
            List<ProductCategory> categories = GetALL().Where(en=> methodType== GroupMethodType.ALL || en.groupmethod==(int)methodType).ToList();
            if (categories != null && categories.Count > 0) {
                for (int i = 0; i < categories.Count; i++) {
                    mercTypeOptions.Add(new SelectItemViewModel<string>() {
                        DisplayValue = categories[i].idcategory.ToString(),
                        DisplayText = categories[i].categoryname
                    });
                }
            }
            mercTypeOptions.Insert(0,new SelectItemViewModel<string>() {
                 DisplayText="请选择",
                  DisplayValue=""
            });
            return mercTypeOptions;
        }

        public static ProductCategory Get(string idcategory) {
            TN db = new TN();
            return db.ProductCategories.Where(en => en.idcategory == idcategory).FirstOrDefault();
        }

        public static ProductCategory GetByCategoryCode(string categoryCode) {
            TN db = new TN();
            return db.ProductCategories.Where(en => en.categorycode == categoryCode).FirstOrDefault();
        }

        public static ProductCategory Edit(ProductCategory category) {
            TN db = new TN();
            ProductCategory oldCategory = db.ProductCategories.Where(en => en.idcategory == category.idcategory).FirstOrDefault();

            oldCategory.idcategory = category.idcategory;
            oldCategory.categoryname = category.categoryname;
            oldCategory.groupmethod = category.groupmethod;
            oldCategory.sortno = category.sortno;
            oldCategory.inuse = category.inuse;
            oldCategory.title = category.title;
            oldCategory.keyword = category.keyword;
            oldCategory.description = category.description;
            oldCategory.isintro = category.isintro;
            db.SaveChanges();
            return oldCategory;
        }



        public static ProductCategory Add(ProductCategory category) {
            TN db = new TN();
            db.ProductCategories.Add(category);
            db.SaveChanges();
            return category;
        }
    }
}
