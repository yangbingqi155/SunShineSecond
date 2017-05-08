using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.EF;
using SunShine.Model;
using SunShine.BLL;
using SunShine.Utils;

namespace SunShine.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List(string idproduct="",string idcategory="",string categoryCode="",string keyword="", int pageIndex = 0)
        {
            string contactusStr = string.Empty;
            int pageCount = 0;
            int pageSize = 8;
            bool result = true;
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            ProductCategory category = null;
            if (!string.IsNullOrEmpty(idproduct)&& result) {
                productViewModels.Add(ProductService.GetViewModel(idproduct));
                category = productViewModels.Count>0? productViewModels.First().ParentCategory:null;
                List<ArticleViewModel> contactus= ArticleService.GetArticlesByCategoryCode("contactus");
                contactusStr = contactus.Count > 0 ? contactus.First().content: " <div class=\"no_data_div\">暂无联系方式！</div>";
                result = false;
            }
            if (!string.IsNullOrEmpty(idcategory) && result) {
                productViewModels = ProductService.GetViewModelsByCategory(idcategory).Where(en=>en.inuse).ToList();
                category = ProductCategoryService.Get(idcategory);
                result = false;
            }
            if (!string.IsNullOrEmpty(categoryCode) && result) {
                productViewModels = ProductService.GetViewModelsByCategoryCode(categoryCode).Where(en => en.inuse).ToList();
                category = ProductCategoryService.GetByCategoryCode(categoryCode);
                result = false;
            }
           
            if (result) {
                if (!string.IsNullOrEmpty(keyword)) {
                    productViewModels = ProductService.SearchViewModels(keyword).Where(en => en.inuse).ToList();
                }
                else {
                    productViewModels = ProductService.GetALLViewModels().Where(en => en.inuse).ToList();
                }
                
            }
            
            if (result)
            {
                SEO seo = SEOService.GetByCode("ProductList");
                ViewBag.Title = seo != null ? seo.seotitle : "";
                ViewBag.Keywords = seo != null ? seo.seokeywords : "";
                ViewBag.Description = seo != null ? seo.seodescription : "";
            }
            else
            {
                if (!string.IsNullOrEmpty(idproduct)) {
                    ViewBag.Title = productViewModels.First().seotitle;
                    ViewBag.Keywords = productViewModels.First().seokeyword;
                    ViewBag.Description = productViewModels.First().seodescription;
                }
                else {
                    ViewBag.Title = category != null ? category.title : "";
                    ViewBag.Keywords = category != null ? category.keyword : "";
                    ViewBag.Description = category != null ? category.description : "";
                }
            }

            if (productViewModels.Count>0) {
                productViewModels = productViewModels.OrderBy(en => en.sortno).ToList();
            }

            List<ProductViewModel> pageList = productViewModels.Pager<ProductViewModel>(pageIndex, pageSize, out pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["contactus"] = string.IsNullOrEmpty(contactusStr) ? "" : contactusStr;
            ViewData["keyword"] = keyword;
            ViewData["category"] = category;

            //RouteData.Values.Add("keyword", keyword);
            //RouteData.Values.Add("idproduct", idproduct);
            //RouteData.Values.Add("idcategory", idcategory);
            //RouteData.Values.Add("categoryCode", categoryCode);
            
            return View(pageList);
        }
    }
}