using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.Model;
using SunShine.BLL;
using SunShine.EF;
using SunShine.Utils;

namespace SunShine.Web.Controllers
{
    public class CaseController : Controller
    {
        // GET: Case
        public ActionResult Index(string categoryCode="",string currentCategoryCode="",string idarticle="",int pageIndex = 0)
        {
            int pageCount = 0;
            int pageSize = 6;
            ArticleViewModel viewModel = null;

            List<ArticleViewModel> articles = new List<ArticleViewModel>();
            if (!string.IsNullOrEmpty(idarticle))
            {
                 viewModel = ArticleService.GetViewModel(idarticle);
                currentCategoryCode = viewModel.Category.categorycode;
                categoryCode = SiteCategoryService.GetViewModelByCode(currentCategoryCode).ParentCategory.categorycode;

                pageSize = viewModel.Category.pagesize;
                articles.Add(viewModel);
            }
            else
            {
                if (string.IsNullOrEmpty(categoryCode)) {
                    return RedirectToAction("Index", "Home");
                }
                if (string.IsNullOrEmpty(currentCategoryCode)) {
                    List<SiteCategoryViewModel> childrenCategories = SiteCategoryService.GetChildCategoriesByCode(categoryCode);
                    if (childrenCategories != null && childrenCategories.Count > 0) {
                        currentCategoryCode = childrenCategories.First().categorycode;
                        pageSize = childrenCategories.First().pagesize;
                    }
                    else {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else {
                    pageSize = SiteCategoryService.GetByCode(currentCategoryCode).pagesize;
                }
                articles = ArticleService.GetArticlesByCategoryCode(currentCategoryCode);

               
            }

            List<ArticleViewModel> pageList = articles.Pager<ArticleViewModel>(pageIndex, pageSize, out pageCount);

            List<SiteCategory> parentCategories = SiteCategoryService.GetNavPath(currentCategoryCode);

            if (!string.IsNullOrEmpty(idarticle)) {
                ViewBag.Keywords = viewModel.seokeyword;
                ViewBag.Description = viewModel.seodescription;
                ViewBag.Title = viewModel.seotitle;
            }
            else {
                SiteCategory category = SiteCategoryService.GetByCode(currentCategoryCode);
                ViewBag.Title = category != null ? category.title : "";
                ViewBag.Keywords = category != null ? category.keyword : "";
                ViewBag.Description = category != null ? category.description : "";
            }

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["navPath"] = parentCategories;
            ViewData["currentCategoryCode"] = currentCategoryCode;
            ViewData["categoryCode"] = categoryCode;
            ViewData["idarticle"] = idarticle;

            //RouteData.Values.Add("categoryCode", categoryCode);
            //RouteData.Values.Add("currentCategoryCode", currentCategoryCode);
            return View(pageList);

        }
    }
}