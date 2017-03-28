using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.Model;
using SunShine.EF;

namespace SunShine.BLL
{
    public class SiteCategoryService
    {

        public static List<SiteCategory> GetALL()
        {
            TN db = new TN();
            var categories = (from category in db.SiteCategories orderby category.level,category.categoryname select category).ToList();
            return categories;
        }

        public static List<SiteCategoryViewModel> GetALLViewModels() {
            return ConvertToViewModel(GetALL());
        }


        private static List<SiteCategoryViewModel> ConvertToViewModel(List<SiteCategory> siteCategories) {
            List<SiteCategoryViewModel> entities = new List<SiteCategoryViewModel>();
            TN db = new TN();
            entities = siteCategories.Select(model => {
                SiteCategoryViewModel viewModel = new SiteCategoryViewModel();
                viewModel.CopyFromBase(model);
                List<SiteCategory> parentCategories = new List<SiteCategory>();
                //string parentCategoryName = string.Empty;
                //parentCategories=getParentCategories(model.parentid, parentCategories);
                //for (int i=0;i< parentCategories.Count;i++) {
                //    parentCategoryName += (i == 0 ? parentCategories[i].categoryname : parentCategories[i].categoryname + "-");
                //}
                SiteCategoryViewModel parentViewModel = new SiteCategoryViewModel();
                viewModel.ParentCategory = Get(model.parentid);

                return viewModel;
            }).ToList();
            return entities;
        }

        public static List<SelectItemViewModel<string>> SelectItems(string currentidcategory="",string rootCategoryName="根目录",string categorySplitChar="-")
        {
            List<SelectItemViewModel<string>> categoryOptions = new List<SelectItemViewModel<string>>();
            List<SiteCategory> categories = GetALL().ToList();
            if (categories != null && categories.Count > 0)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    List<SiteCategory> parentCategories = new List<SiteCategory>();
                    string categoryNamePath = string.Empty;
                    parentCategories=getParentCategories(categories[i].idcategory, parentCategories).OrderBy(en=>en.level).ToList();
                    if (!string.IsNullOrEmpty(currentidcategory)) {
                        if (parentCategories.Find(en => en.idcategory == currentidcategory) != null) {
                            continue;
                        }
                    }
                    for (int j = 0; j < parentCategories.Count; j++)
                    {
                        categoryNamePath += (j == 0 ? parentCategories[j].categoryname : "-"+parentCategories[j].categoryname );
                    }
                    categoryOptions.Add(new SelectItemViewModel<string>()
                    {
                        DisplayValue = categories[i].idcategory.ToString(),
                        DisplayText = categoryNamePath
                    });
                }
            }
            categoryOptions.Insert(0, new SelectItemViewModel<string>()
            {
                DisplayText = rootCategoryName,
                DisplayValue = ""
            });
            return categoryOptions;
        }

        public static List<SiteCategory> getParentCategories(string idcategory,List<SiteCategory> list) {
            if (!string.IsNullOrEmpty(idcategory)) {
                TN db = new TN();
                SiteCategory current = db.SiteCategories.Find(idcategory);
                list.Add(current);
                return getParentCategories(current.parentid, list);
            }
            else {
                return list;
            }
            
        }


        public static List<SiteCategory> GetNavPath(string categoryCode) {
            List<SiteCategory> navPathCategories = new List<SiteCategory>();
            navPathCategories = SiteCategoryService.getParentCategories(GetByCode(categoryCode).idcategory, navPathCategories);
            navPathCategories = navPathCategories.OrderBy(en => en.level).ToList();
            navPathCategories.Insert(0, new SiteCategory() { idcategory="",categoryname="首页" });
            return navPathCategories;
        }
        
        public static SiteCategory GetByCode(string code) {
            TN db = new TN();
            return db.SiteCategories.Where(en => en.categorycode == code).FirstOrDefault();
        }

        public static SiteCategoryViewModel GetViewModelByCode(string code)
        {
            TN db = new TN();
            return ConvertToViewModel(db.SiteCategories.Where(en => en.categorycode == code).ToList()).FirstOrDefault();
        }

        public static List<SiteCategoryViewModel> GetChildCategoriesByCode(string code) {
            TN db = new TN();
            SiteCategory parentCategory = GetByCode(code);
            List<SiteCategory> categories= db.SiteCategories.Where(en => en.parentid== parentCategory.idcategory).ToList();
            return ConvertToViewModel(categories);
        }

        public static SiteCategory Get(string idcategory)
        {
            TN db = new TN();
            return db.SiteCategories.Where(en => en.idcategory == idcategory).FirstOrDefault();
        }

        public static SiteCategory Edit(SiteCategory category)
        {
            TN db = new TN();
            SiteCategory oldCategory = db.SiteCategories.Where(en => en.idcategory == category.idcategory).FirstOrDefault();

            oldCategory.idcategory = category.idcategory;
            oldCategory.categoryname = category.categoryname;
            oldCategory.englishname = category.englishname;
            oldCategory.parentid = category.parentid;
            oldCategory.level = string.IsNullOrEmpty(category.parentid) ? 1 : db.SiteCategories.Find(category.parentid).level + 1;
            oldCategory.inuse = category.inuse;
            oldCategory.description = category.description;
            oldCategory.keyword = category.keyword;
            oldCategory.title = category.title;
            oldCategory.pagesize = category.pagesize;

            db.SaveChanges();
            return oldCategory;
        }



        public static SiteCategory Add(SiteCategory category)
        {
            TN db = new TN();
            category.level = string.IsNullOrEmpty(category.parentid) ? 1 : db.SiteCategories.Find(category.parentid).level + 1;
            db.SiteCategories.Add(category);
            db.SaveChanges();
            return category;
        }
    }
}
