using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.EF;
using SunShine.Model;
using SunShine.BLL;

namespace SunShine.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {

            List<ProductViewModel> hotProducts = new List<ProductViewModel>();
            List<ProductViewModel> newProducts = new List<ProductViewModel>();
            List<ProductViewModel> allProducts = new List<ProductViewModel>();
            allProducts = ProductService.GetALLViewModels();

            newProducts = allProducts.Where(en => en.isnew).ToList();
            hotProducts = allProducts.Where(en => en.ishot).ToList();

            newProducts = newProducts.Count > 8 ? newProducts.Take(8).ToList(): newProducts;
            hotProducts = hotProducts.Count > 8 ? hotProducts.Take(8).ToList() : hotProducts;

            ViewData["hotProducts"] = hotProducts;
            ViewData["newProducts"] = newProducts;
            SEO seo = SEOService.GetByCode("Home");
            ViewBag.Title = seo != null ? seo.seotitle : "";
            ViewBag.Keywords = seo != null ? seo.seokeywords : "";
            ViewBag.Description = seo != null ? seo.seodescription : "";
            
            return View();
        }

        public ActionResult SiteMap() {
            //产品分类
            List<ProductCategoryViewModel> categories = new List<ProductCategoryViewModel>();
            List<ProductCategory> tempCategories = ProductCategoryService.GetALL().Where(en => en.inuse).ToList();
            categories = tempCategories.Select(model => {
                ProductCategoryViewModel category = new ProductCategoryViewModel();
                category.CopyFromBase(model);
                return category;
            }).ToList();

            //成功案例
            List<SiteCategoryViewModel> successcase = SiteCategoryService.GetChildCategoriesByCode("successcase");

            //品牌创意阳光
            List<SiteCategoryViewModel> sunshinebrand = SiteCategoryService.GetChildCategoriesByCode("news");

            //关于创意阳光
            List<SiteCategoryViewModel> contactsunshine = SiteCategoryService.GetChildCategoriesByCode("contactsunshine");
            
            ViewData["categories"] = categories;
            ViewData["successcase"] = successcase;
            ViewData["sunshinebrand"] = sunshinebrand;
            ViewData["contactsunshine"] = contactsunshine;
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}