using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.Model;
using SunShine.BLL;
using SunShine.EF;
using SunShine.Web.Utils;

namespace SunShine.Web.Controllers
{
    public class PartsController : Controller
    {
        // GET: HotSale
        public ActionResult HotSale()
        {
            List<ProductViewModel> products= ProductService.GetALLViewModels().Where(en=>en.ishot&&en.inuse).ToList();
            if (products!=null&& products.Count>3) {
                products=products.Take(3).ToList();
            }
            return View(products);
        }

        /// <summary>
        /// 合作伙伴
        /// </summary>
        /// <returns></returns>
        public ActionResult Partner() {
            string categoryCode = "lastestcase";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode).ToList());
        }

        /// <summary>
        /// 合作伙伴-手机版
        /// </summary>
        /// <returns></returns>
        public ActionResult Mobile_Partner()
        {
            string categoryCode = "lastestcase";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode).ToList());
        }

        /// <summary>
        /// 导航-成功案例
        /// </summary>
        /// <returns></returns>
        public ActionResult NavSuccessCases(string categoryCode) {
            return View(SiteCategoryService.GetChildCategoriesByCode(categoryCode));
        }

        /// <summary>
        /// 手机版-导航-成功案例
        /// </summary>
        /// <returns></returns>
        public ActionResult Mobile_NavSuccessCases(string categoryCode,string currentCategoryCode)
        {
            ViewData["currentCategoryCode"] = currentCategoryCode;
            ViewData["categoryCode"] = categoryCode;
            return View(SiteCategoryService.GetChildCategoriesByCode(categoryCode));
        }

        /// <summary>
        /// 导航-荣誉资质
        /// </summary>
        /// <returns></returns>
        public ActionResult NavHonors() {
            string categoryCode = "honor";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode));
        }

        /// <summary>
        /// 手机版-底部导航-荣誉资质
        /// </summary>
        /// <returns></returns>
        public ActionResult Mobile_Bottom_Honor()
        {
            string categoryCode = "honor";
            List<ArticleViewModel> articles= ArticleService.GetArticlesByCategoryCode(categoryCode);
            articles = articles.Count > 3 ? articles.Take(3).ToList() : articles;
            return View(articles);
        }
        

        /// <summary>
        /// 导航-联系我们
        /// </summary>
        /// <returns></returns>
        public ActionResult NavContactUs() {
            WebsiteInfoViewModel model = new WebsiteInfoViewModel();
            List<WebSiteInfo> all = WebSiteInfoService.GetALL();
            if (all.Count > 0) {
                model.CopyFromBase(all.First());
            }
            else {
                model = null;
            }
            return View(model);
        }

        /// <summary>
        /// 底部产品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult BottomProductCategory() {
            List<ProductCategoryViewModel> categories = new List<ProductCategoryViewModel>();
            List<ProductCategory> tempCategories = ProductCategoryService.GetALL().Where(en => en.inuse).ToList();
            categories = tempCategories.Select(model=> {
                ProductCategoryViewModel category = new ProductCategoryViewModel();
                category.CopyFromBase(model);
                return category;
            }).ToList();
            return View(categories);
        }

        /// <summary>
        /// 手机版-底部产品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Mobile_BottomProductCategory()
        {
            List<ProductCategoryViewModel> categories = new List<ProductCategoryViewModel>();
            List<ProductCategory> tempCategories = ProductCategoryService.GetALL().Where(en => en.inuse).ToList();
            categories = tempCategories.Select(model => {
                ProductCategoryViewModel category = new ProductCategoryViewModel();
                category.CopyFromBase(model);
                return category;
            }).ToList();
            WebsiteInfoViewModel siteModel = new WebsiteInfoViewModel();
            List<WebSiteInfo> all = WebSiteInfoService.GetALL();
            if (all.Count > 0)
            {
                siteModel.CopyFromBase(all.First());
            }
            else
            {
                siteModel = null;
            }
            ViewData["websiteinfo"] = siteModel;
            return View(categories);
        }


        /// <summary>
        /// 底部产品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult HotSearch() {
            List<ProductCategoryViewModel> categories = new List<ProductCategoryViewModel>();
            List<ProductCategory> tempCategories = ProductCategoryService.GetALL().Where(en => en.inuse).ToList();
            categories = tempCategories.Select(model => {
                ProductCategoryViewModel category = new ProductCategoryViewModel();
                category.CopyFromBase(model);
                return category;
            }).Where(en=>en.isintro).ToList();
            return View(categories);
        }

        /// <summary>
        /// 左边产品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult LeftProductCategory() {
            List<ProductCategoryViewModel> categories = new List<ProductCategoryViewModel>();
            List<ProductCategory> tempCategories = ProductCategoryService.GetALL().Where(en => en.inuse).ToList();
            categories = tempCategories.Select(model => {
                ProductCategoryViewModel category = new ProductCategoryViewModel();
                category.CopyFromBase(model);
                return category;
            }).ToList();
            return View(categories);
        }

        /// <summary>
        /// 合作伙伴
        /// </summary>
        /// <returns></returns>
        public ActionResult CaseListItemsScroll() {
            string categoryCode = "partner";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode));
        }

        /// <summary>
        /// 公司介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult InstroductionCompany() {
            string showProductionCategoryCode = "showproduction";
            string showOfficeCategoryCode = "showoffice";
            string showTeamCategoryCode = "showteam";
            List<ArticleViewModel> showProductionCategories = ArticleService.GetArticlesByCategoryCode(showProductionCategoryCode);
            List<ArticleViewModel> showOfficeCategories = ArticleService.GetArticlesByCategoryCode(showOfficeCategoryCode);
            List<ArticleViewModel> showTeamnCategories = ArticleService.GetArticlesByCategoryCode(showTeamCategoryCode);

            showProductionCategories = showProductionCategories.Count > 4 ? showProductionCategories.Take(4).ToList() : showProductionCategories;
            showOfficeCategories = showOfficeCategories.Count > 4 ? showOfficeCategories.Take(4).ToList() : showOfficeCategories;
            showTeamnCategories = showTeamnCategories.Count > 4 ? showTeamnCategories.Take(4).ToList() : showTeamnCategories;

            ViewData["showProductionCategories"] = showProductionCategories;
            ViewData["showOfficeCategories"] = showOfficeCategories;
            ViewData["showTeamnCategories"] = showTeamnCategories;
            return View();
        }

        public ActionResult NavNews() {
            string categoryCode = "mediareport";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode));
        }

        public ActionResult NavQuestions()
        {
            string categoryCode = "commonquestion";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode));
        }

        public ActionResult FriendURL() {
            return View(FriendURLService.GetALL());
        }

        public ActionResult BottomWebsiteInfo() {
            WebsiteInfoViewModel model = new WebsiteInfoViewModel();
            List<WebSiteInfo> all = WebSiteInfoService.GetALL();
            if (all.Count > 0) {
                model.CopyFromBase(all.First());
            }
            else {
                model = null;
            }
            return View(model);
        }

        public ActionResult Mobile_BottomWebsiteInfo()
        {
            WebsiteInfoViewModel model = new WebsiteInfoViewModel();
            List<WebSiteInfo> all = WebSiteInfoService.GetALL();
            if (all.Count > 0)
            {
                model.CopyFromBase(all.First());
            }
            else
            {
                model = null;
            }
            return View(model);
        }

        public ActionResult CoreAdvance() {
            return View();
        }

        public ActionResult HomeAdvertise() {
            string code = "home";
            if (BrowerDetectorHelper.IsMobile())
            {
                code = "mobile_home";
            }
            Advertise advertise = AdvertiseService.GetByCode(code);
            if (advertise==null) {
                return View();
            }
           
            return View( PictureService.GetImagesByIdmodule(advertise.idadvertise, ModuleType.Advertise));

        }

        public ActionResult BigQuestions() {
            return View();
        }
        public ActionResult SetPosition()
        {
            return View();
        }
        public ActionResult DesignCompare()
        {
            return View();
        }
        public ActionResult Advantages()
        {
            return View();
        }
        public ActionResult DesignService() {
            List<ArticleViewModel> designers = ArticleService.GetArticlesByCategoryCode("designteam").OrderBy(en=>en.sortno).ToList();
            designers = designers.Count > 5 ? designers.Take(5).ToList() : designers;
            return View(designers);
        }


        public ActionResult Mobile_CoreAdvance()
        {
            return View();
        }
    }

}