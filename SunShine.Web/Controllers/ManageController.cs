using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.IO;
using log4net;
using System.Reflection;
using System;
using System.Text;
using SunShine.Model;
using SunShine.BLL;
using SunShine.EF;
using SunShine.Utils;
using SunShine.Web.Authorize;
using SunShine.Web.Utils;

namespace TNet.Controllers {
    public class ManageController : Controller {
        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ManageUserViewModel model) {
            ManageUser user = ManageUserService.GetManageUserByUserName(model.username);
            if (user == null) {
                ModelState.AddModelError("", "没有找到该用户名或者帐号被禁用.");
                return View(model);
            }
            string md5Password = string.Empty;
            md5Password = Crypto.GetPwdhash(model.ClearPassword, user.md5salt);
            if (md5Password.ToUpper() != user.password.ToUpper()) {
                ModelState.AddModelError("", "密码不正确.");
                return View(model);
            }
            Session["ManageUser"] = user;
            return RedirectToAction("Index", "Manage");
        }

        [ManageLoginValidation]
        public ActionResult SignOut() {
            Session["ManageUser"] = null;
            return RedirectToAction("Login", "Manage");
        }

        [ManageLoginValidation]
        public ActionResult Index() {
            return View();
        }


        /// <summary>
        /// 产品类别列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult ProductCategoryList(int pageIndex = 0, int groupmethod = 0) {
            int pageCount = 0;
            int pageSize = 10;
            List<ProductCategory> entities = ProductCategoryService.Search(groupmethod);
            List<ProductCategory> pageList = entities.Pager<ProductCategory>(pageIndex, pageSize, out pageCount);


            List<ProductCategoryViewModel> viewModels = pageList.Select(model => {
                ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["groupmethod"] = groupmethod;
            RouteData.Values.Add("groupmethod", groupmethod);

            List<SelectItemViewModel<int>> groupmethodSelectItems = ProductCategoryViewModel.GroupMethodTypeSelectItems;
            groupmethodSelectItems.Insert(0, new SelectItemViewModel<int>() {
                DisplayText = "所有分类",
                DisplayValue = 0
            });
            ViewData["groupmethodSelectItems"] = groupmethodSelectItems;
            return View(viewModels);
        }


        /// <summary>
        /// 启用或者禁用产品类别
        /// </summary>
        /// <param name="idcategory"></param>
        /// <param name="enable"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult ProductCategoryEnable(string idcategory, bool enable, bool isAjax) {
            ResultModel<ProductCategoryViewModel> resultEntity = new ResultModel<ProductCategoryViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                ProductCategory category = ProductCategoryService.Get(idcategory);
                category.inuse = enable;
                ProductCategoryService.Edit(category);
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 搜索推荐类型
        /// </summary>
        /// <param name="idcategory"></param>
        /// <param name="enable"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult ProductCategoryIntro(string idcategory, bool enable, bool isAjax) {
            ResultModel<ProductCategoryViewModel> resultEntity = new ResultModel<ProductCategoryViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                ProductCategory category = ProductCategoryService.Get(idcategory);
                category.isintro = enable;
                ProductCategoryService.Edit(category);
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 新增\编辑产品类别
        /// </summary>
        /// <param name="idcategory"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult ProductCategoryEdit(string idcategory = "") {
            ProductCategoryViewModel model = new ProductCategoryViewModel();
            if (!string.IsNullOrEmpty(idcategory)) {
                ProductCategory category = ProductCategoryService.Get(idcategory);
                if (category != null) { model.CopyFromBase(category); }
            }
            else {
                model.inuse = true;
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑产品类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult ProductCategoryEdit(ProductCategoryViewModel model,string returnurl="") {
            ProductCategory category = new ProductCategory();
            model.CopyToBase(category);
            if (string.IsNullOrEmpty(category.idcategory)) {
                category.idcategory = Pub.ID();
                category.cretime = DateTime.Now;
                //新增
                category = ProductCategoryService.Add(category);
            }
            else {
                //编辑
                category = ProductCategoryService.Edit(category);
            }

            //修改后重新加载
            model.CopyFromBase(category);

            ModelState.AddModelError("", "保存成功.");
            if (!string.IsNullOrEmpty(returnurl))
            {
                return Content("<script>alert('保存成功!');window.location.href=\"" + returnurl + "\";</script>");
            }
            return Content("<script>alert('保存成功!');window.location.href=\"" + Url.Action("ProductCategoryEdit", "Manage", new { idcategory = model.idcategory }) + "\";</script>");
        }



        /// <summary>
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult ProductList(int pageIndex = 0, string name = "", string idusagecategory = "", string idindustrycategory = "", string idproductcategory = "") {
            int pageCount = 0;
            int pageSize = 10;
            string idcategory = string.Empty;
            if (!string.IsNullOrEmpty(idusagecategory)) {
                idcategory = idusagecategory;
            }
            if (!string.IsNullOrEmpty(idindustrycategory)) {
                idcategory = idindustrycategory;
            }
            if (!string.IsNullOrEmpty(idproductcategory)) {
                idcategory = idproductcategory;
            }
            List<ProductViewModel> entities = ProductService.SearchViewModels(name, idcategory);
            entities = entities.OrderBy(en => en.sortno).ToList();
            List<ProductViewModel> pageList = entities.Pager<ProductViewModel>(pageIndex, pageSize, out pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            RouteData.Values.Add("name", name);
            RouteData.Values.Add("idusagecategory", idusagecategory);
            RouteData.Values.Add("idindustrycategory", idindustrycategory);
            RouteData.Values.Add("idproductcategory", idproductcategory);

            ViewData["name"] = name;
            ViewData["idusagecategory"] = idusagecategory;
            ViewData["idindustrycategory"] = idindustrycategory;
            ViewData["idproductcategory"] = idproductcategory;

            return View(pageList);
        }


        /// <summary>
        /// 启用或者禁用产品
        /// </summary>
        /// <param name="idproduct"></param>
        /// <param name="enable"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult ProductEnable(string idproduct, bool enable, bool isAjax) {
            ResultModel<ProductViewModel> resultEntity = new ResultModel<ProductViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                Product product = ProductService.Get(idproduct);
                product.inuse = enable;
                ProductService.Edit(product);
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 设置产品是否热销
        /// </summary>
        /// <param name="idproduct"></param>
        /// <param name="ishot"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult ProductHot(string idproduct, bool ishot, bool isAjax) {
            ResultModel<ProductViewModel> resultEntity = new ResultModel<ProductViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                Product product = ProductService.Get(idproduct);
                product.ishot = ishot;
                ProductService.Edit(product);
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 设置产品是否热销
        /// </summary>
        /// <param name="idproduct"></param>
        /// <param name="isnew"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult ProductNew(string idproduct, bool isnew, bool isAjax) {
            ResultModel<ProductViewModel> resultEntity = new ResultModel<ProductViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                Product product = ProductService.Get(idproduct);
                product.isnew = isnew;
                ProductService.Edit(product);
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 新增\编辑产品
        /// </summary>
        /// <param name="idproduct"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult ProductEdit(string idproduct = "", string idcategory = "") {
            ProductViewModel model = new ProductViewModel();
            if (!string.IsNullOrEmpty(idproduct)) {
                Product product = ProductService.Get(idproduct);
                if (product != null) { model.CopyFromBase(product); }
            }
            else {
                model.idcategory = idcategory;
                model.inuse = true;
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProductEdit(ProductViewModel model, string productImages = "",string returnurl="") {
            Product product = new Product();
            model.CopyToBase(product);
            if (string.IsNullOrEmpty(product.idproduct)) {
                product.idproduct = Pub.ID();
                product.cretime = DateTime.Now;
                //新增
                product = ProductService.Add(product);
            }
            else {
                //编辑
                product = ProductService.Edit(product);
            }

            //修改后重新加载
            model.CopyFromBase(product);

            DeleteImages(product.idproduct, ModuleType.Product);

            if (!string.IsNullOrEmpty(productImages)) {
                List<Picture> list = new List<Picture>();
                string[] imgs = productImages.Split(',');
                int i = 0;
                foreach (var item in imgs) {
                    list.Add(new Picture() {
                        idimage = Pub.ID(),
                        idmodule = product.idproduct,
                        moduletype = (int)ModuleType.Product,
                        path = item,
                        sortno = i + 1,
                        cretime = DateTime.Now
                    });
                    i++;
                }
                if (list.Count > 0) {
                    PictureService.AddMuti(list);
                }
            }

            ProductService.SetDefaultImage(product.idproduct);

            ModelState.AddModelError("", "保存成功.");
            if (!string.IsNullOrEmpty(returnurl)) {
                return Content("<script>alert('保存成功!');window.location.href=\"" + returnurl + "\";</script>");
            }
            return Content("<script>alert('保存成功!');window.location.href=\"" + Url.Action("ProductEdit", "Manage", new { idcategory = model.idcategory, idproduct=model.idproduct }) + "\";</script>");

        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="idproducts"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult ProductDelete(string[] idproducts) {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ResultModel<ProductViewModel> resultEntity = new ResultModel<ProductViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "产品删除成功";

            if (idproducts == null || idproducts.Count() == 0) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "产品删除失败，参数错误。";
                return Content(resultEntity.SerializeToJson());
            }

            if (!ProductService.Delete(idproducts.ToList())) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "产品删除失败。";
                return Content(resultEntity.SerializeToJson());
            }

            return Content(resultEntity.SerializeToJson());
        }


        /// <summary>
        /// 网站类别列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult SiteCategoryList(int pageIndex = 0) {
            int pageCount = 0;
            int pageSize = 10;
            List<SiteCategoryViewModel> entities = SiteCategoryService.GetALLViewModels();
            List<SiteCategoryViewModel> pageList = entities.Pager<SiteCategoryViewModel>(pageIndex, pageSize, out pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            return View(pageList);
        }


        /// <summary>
        /// 启用或者禁用网站类别
        /// </summary>
        /// <param name="idcategory"></param>
        /// <param name="enable"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult SiteCategoryEnable(string idcategory, bool enable, bool isAjax) {
            ResultModel<SiteCategoryViewModel> resultEntity = new ResultModel<SiteCategoryViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                SiteCategory category = SiteCategoryService.Get(idcategory);
                category.inuse = enable;
                SiteCategoryService.Edit(category);
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 新增\编辑产品类别
        /// </summary>
        /// <param name="idcategory"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult SiteCategoryEdit(string idcategory = "") {
            SiteCategoryViewModel model = new SiteCategoryViewModel();
            if (!string.IsNullOrEmpty(idcategory)) {
                SiteCategory category = SiteCategoryService.Get(idcategory);
                if (category != null) { model.CopyFromBase(category); }
            }
            else {
                model.inuse = true;
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑产品类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult SiteCategoryEdit(SiteCategoryViewModel model,string returnurl="") {
            SiteCategory category = new SiteCategory();
            model.CopyToBase(category);
            if (string.IsNullOrEmpty(category.idcategory)) {
                category.idcategory = Pub.ID();
                category.cretime = DateTime.Now;
                //新增
                category = SiteCategoryService.Add(category);
            }
            else {
                //编辑
                category = SiteCategoryService.Edit(category);
            }

            //修改后重新加载
            model.CopyFromBase(category);

            ModelState.AddModelError("", "保存成功.");
            if (!string.IsNullOrEmpty(returnurl))
            {
                return Content("<script>alert('保存成功!');window.location.href=\"" + returnurl + "\";</script>");
            }
            return Content("<script>alert('保存成功!');window.location.href=\"" + Url.Action("SiteCategoryEdit", "Manage", new { idcategory = model.idcategory }) + "\";</script>");
        }

        /// <summary>
        /// 删除网站类别
        /// </summary>
        /// <param name="idarticles"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult SiteCategoryDelete(string[] idcategories) {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ResultModel<SiteCategoryViewModel> resultEntity = new ResultModel<SiteCategoryViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "网站栏目删除成功";

            if (idcategories == null || idcategories.Count() == 0) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "网站栏目删除失败，参数错误。";
                return Content(resultEntity.SerializeToJson());
            }

            if (!SiteCategoryService.Delete(idcategories.ToList())) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "网站栏目删除失败。";
                return Content(resultEntity.SerializeToJson());
            }

            return Content(resultEntity.SerializeToJson());
        }



        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult ArticleList(int pageIndex = 0, string title = "", string idcategory = "", string idindustrycategory = "", string idproductcategory = "") {
            int pageCount = 0;
            int pageSize = 10;
            List<ArticleViewModel> entities = ArticleService.SearchViewModels(title, idcategory);
            List<ArticleViewModel> pageList = entities.Pager<ArticleViewModel>(pageIndex, pageSize, out pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            RouteData.Values.Add("title", title);
            RouteData.Values.Add("idcategory", idcategory);

            ViewData["title"] = title;
            ViewData["idcategory"] = idcategory;
            List<SelectItemViewModel<string>> categorySelectItems = SiteCategoryService.SelectItems("");
            ViewData["categorySelectItems"] = categorySelectItems;
            return View(pageList);
        }

        /// <summary>
        /// 新增\编辑文章
        /// </summary>
        /// <param name="idproduct"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult ArticleEdit(string idarticle = "", string idcategory = "") {
            ArticleViewModel model = new ArticleViewModel();
            if (!string.IsNullOrEmpty(idarticle)) {
                Article article = ArticleService.Get(idarticle);
                if (article != null) { model.CopyFromBase(article); }
            }
            else {
                model.idcategory = idcategory;
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ArticleEdit(ArticleViewModel model, string articleImages = "", string returnurl = "") {
            Article article = new Article();
            model.CopyToBase(article);
            if (string.IsNullOrEmpty(article.idarticle)) {
                article.idarticle = Pub.ID();
                article.cretime = DateTime.Now;
                //新增
                article = ArticleService.Add(article);
            }
            else {
                //编辑
                article = ArticleService.Edit(article);
            }

            //修改后重新加载
            model.CopyFromBase(article);
            DeleteImages(article.idarticle, ModuleType.Article);

            if (!string.IsNullOrEmpty(articleImages)) {
                List<Picture> list = new List<Picture>();
                string[] imgs = articleImages.Split(',');
                int i = 0;
                foreach (var item in imgs) {
                    list.Add(new Picture() {
                        idimage = Pub.ID(),
                        idmodule = article.idarticle,
                        moduletype = (int)ModuleType.Article,
                        path = item,
                        sortno = i + 1,
                        cretime = DateTime.Now
                    });
                    i++;
                }
                if (list.Count > 0) {
                    PictureService.AddMuti(list);
                }
            }

            ArticleService.SetDefaultImage(article.idarticle);
            if (!string.IsNullOrEmpty(returnurl))
            {
                return Content("<script>alert('保存成功!');window.location.href=\"" + returnurl + "\";</script>");
            }
            return Content("<script>alert('保存成功!');window.location.href=\"" + Url.Action("ArticleEdit", "Manage", new { idcategory = model.idcategory,idarticle=model.idarticle }) + "\";</script>");
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="idarticles"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult ArticleDelete(string[] idarticles) {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ResultModel<ArticleViewModel> resultEntity = new ResultModel<ArticleViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "文章删除成功";

            if (idarticles == null || idarticles.Count() == 0) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "文章删除失败，参数错误。";
                return Content(resultEntity.SerializeToJson());
            }

            if (!ArticleService.Delete(idarticles.ToList())) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "文章删除失败。";
                return Content(resultEntity.SerializeToJson());
            }

            return Content(resultEntity.SerializeToJson());
        }


        /// <summary>
        /// 编辑网站信息
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult WebsiteInfoEdit() {
            WebsiteInfoViewModel model = new WebsiteInfoViewModel();
            List<WebSiteInfo> list = WebSiteInfoService.GetALL();
            if (list.Count >= 1) {
                model.CopyFromBase(list.First());
            }

            return View(model);
        }

        /// <summary>
        /// 编辑网站信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult WebsiteInfoEdit(WebsiteInfoViewModel model) {
            WebSiteInfo info = new WebSiteInfo();
            model.CopyToBase(info);
            if (string.IsNullOrEmpty(info.idsite)) {
                info.idsite = Pub.ID();

                //新增
                info = WebSiteInfoService.Add(info);
            }
            else {
                //编辑
                info = WebSiteInfoService.Edit(info);
            }

            ViewData["Message"] = "保存成功";
            return Content("<script>alert('保存成功!');window.location.href=window.location.href;</script>");

            //return View(model);
        }


        /// <summary>
        /// 编辑SEO信息
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult SEOEdit(string code) {
            SEOViewModel model = new SEOViewModel();
            SEO seo = SEOService.GetByCode(code);
            if (seo==null) {
                return Content("<script>alert('该页面SEO信息不存在!');window.location.href=\""+Url.Action("Index","Manage")+"\";</script>");
            }
            model.CopyFromBase(seo);

            return View(model);
        }

        /// <summary>
        /// 编辑SEO信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SEOEdit(SEOViewModel model) {
            SEO seo = new SEO();
            model.CopyToBase(seo);
            string message = "保存成功";
            if (string.IsNullOrEmpty(seo.idseo)) {
                message = "该页面没有设置SEO";
            }
            else {
                //编辑
                seo = SEOService.Edit(seo);
            }

            ViewData["Message"] = message;
            return Content("<script>alert('保存成功!');window.location.href=\""+ Url.Action("SEOEdit", "Manage",new { code = seo.code }) + "\";</script>");

            //return View(model);
        }


        /// <summary>
        /// 友情链接列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult FriendURLList(int pageIndex = 0) {
            int pageCount = 0;
            int pageSize = 10;
            List<FriendURL> entities = FriendURLService.GetALL();
            List<FriendURL> pageList = entities.Pager<FriendURL>(pageIndex, pageSize, out pageCount);


            List<FriendURLViewModel> viewModels = pageList.Select(model => {
                FriendURLViewModel viewModel = new FriendURLViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            return View(viewModels);
        }


        /// <summary>
        /// 新增\编辑友情链接
        /// </summary>
        /// <param name="idurl"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult FriendURLEdit(string idurl = "") {
            FriendURLViewModel model = new FriendURLViewModel();
            if (!string.IsNullOrEmpty(idurl)) {
                FriendURL friendURL = FriendURLService.Get(idurl);
                if (friendURL != null) { model.CopyFromBase(friendURL); }
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑友情链接
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult FriendURLEdit(FriendURLViewModel model) {
            FriendURL friendURL = new FriendURL();
            model.CopyToBase(friendURL);
            if (string.IsNullOrEmpty(friendURL.idurl)) {
                friendURL.idurl = Pub.ID();
                //新增
                friendURL = FriendURLService.Add(friendURL);
            }
            else {
                //编辑
                friendURL = FriendURLService.Edit(friendURL);
            }

            //修改后重新加载
            model.CopyFromBase(friendURL);

            return Content("<script>alert('保存成功!');window.location.href=\"" + Url.Action("FriendURLEdit", "Manage", new { idurl = model.idurl }) + "\";</script>");
        }


        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="idurls"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult FriendURLDelete(string[] idurls) {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ResultModel<FriendURLViewModel> resultEntity = new ResultModel<FriendURLViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "友情链接删除成功";

            if (idurls == null || idurls.Count() == 0) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "友情链接删除失败，参数错误。";
                return Content(resultEntity.SerializeToJson());
            }

            if (!FriendURLService.Delete(idurls.ToList())) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "友情链接删除失败。";
                return Content(resultEntity.SerializeToJson());
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 新增\编辑广告
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult AdvertiseEdit(string code) {
            AdvertiseViewModel model = new AdvertiseViewModel();
            if (!string.IsNullOrEmpty(code)) {
                Advertise advertise = AdvertiseService.GetByCode(code);
                if (advertise != null) { model.CopyFromBase(advertise); }
            }
            else {
            }

            return View(model);
        }


        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="manageUserId"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult ManageUserPasswordEdit(string iduser) {
            ManageUserViewModel model = new ManageUserViewModel();

            ManageUser manageUser = ManageUserService.Get(iduser);

            if (manageUser != null) { model.CopyFromBase(manageUser); }

            model.password = "";
            model.ConfirmPassword = "";

            return View(model);
        }

        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult ManageUserPasswordEdit(ManageUserViewModel model) {

            if (string.IsNullOrEmpty(model.currentpassword)) {
                ModelState.AddModelError("", "原密码不能为空.");
                return View(model);
            }
            ManageUser oldManage = ManageUserService.Get(model.iduser);
            string checkPassword= Crypto.GetPwdhash(model.currentpassword, oldManage.md5salt);
            if (checkPassword!= oldManage.password) {
                ModelState.AddModelError("", "原密码不正确.");
                return View(model);
            }

            if (model.newpassword != model.ConfirmPassword) {
                ModelState.AddModelError("", "密码与确认密码必须一致.");
                return View(model);
            }
            ManageUser manageUser = new ManageUser();
            model.CopyToBase(manageUser);
            string md5Password = string.Empty;
            string md5Salt = string.Empty;
            Crypto.GetPwdhashAndSalt(model.newpassword, out md5Salt, out md5Password);
            manageUser.password = md5Password;
            manageUser.md5salt = md5Salt;

            //修改密码
            manageUser = ManageUserService.PasswordEdit(manageUser);

            return RedirectToAction("ManageUserList", "Manage");
        }


        /// <summary>
        /// 新增\编辑产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdvertiseEdit(AdvertiseViewModel model, string advertiseImages = "") {
            Advertise advertise = new Advertise();
            model.CopyToBase(advertise);
            if (string.IsNullOrEmpty(advertise.idadvertise)) {
                advertise.idadvertise = Pub.ID();

                //新增
                advertise = AdvertiseService.Add(advertise);
            }
            else {
                //编辑
                advertise = AdvertiseService.Edit(advertise);
            }

            //修改后重新加载
            model.CopyFromBase(advertise);

            DeleteImages(advertise.idadvertise, ModuleType.Advertise);

            if (!string.IsNullOrEmpty(advertiseImages)) {
                List<Picture> list = new List<Picture>();
                string[] imgs = advertiseImages.Split(',');
                int i = 0;
                foreach (var item in imgs) {
                    list.Add(new Picture() {
                        idimage = Pub.ID(),
                        idmodule = advertise.idadvertise,
                        moduletype = (int)ModuleType.Advertise,
                        path = item,
                        sortno = i + 1,
                        cretime = DateTime.Now
                    });
                    i++;
                }
                if (list.Count > 0) {
                    PictureService.AddMuti(list);
                }
            }

            ModelState.AddModelError("", "保存成功.");
            return Content("<script>alert('保存成功!');window.location.href=\"" + Url.Action("AdvertiseEdit", "Manage", new { code = model.code }) + "\";</script>");

        }

        [ManageLoginValidation]
        public ActionResult UploadImage(ModuleType moduleType, string idmodule = "") {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ResultModel<PictureViewModel> resultEntity = new ResultModel<PictureViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "文件上传成功";
            string GUID = System.Guid.NewGuid().ToString();
            string path = "~/Resource/Images/";
            if (moduleType == ModuleType.Product) {
                path += "Product/";
            }
            else if (moduleType == ModuleType.Article) {
                path += "Article/";
            }
            else if (moduleType == ModuleType.Advertise) {
                path += "Advertise/";
            }
            string filename = string.Empty;
            string message = string.Empty;
            try {
                if (Request.Files != null && Request.Files.Count > 0) {
                    //if (Request.Files.Count > 1)
                    //{
                    //    resultEntity.Code = ResponseCodeType.Fail;
                    //    resultEntity.Message = "请选择文件.";
                    //    return Content(resultEntity.SerializeToJson());
                    //}
                    resultEntity.Content = new List<PictureViewModel>();
                    int i = 0;
                    foreach (string upload in Request.Files) {
                        GUID = System.Guid.NewGuid().ToString();
                        if (!Request.Files[i].HasFile()) {
                            resultEntity.Code = ResponseCodeType.Fail;
                            resultEntity.Message = "文件大小不能0.";
                            return Content(resultEntity.SerializeToJson());
                        }


                        if (!CheckFileType((HttpPostedFileWrapper)((HttpFileCollectionWrapper)Request.Files)[i])) {
                            resultEntity.Code = ResponseCodeType.Fail;
                            resultEntity.Message = "文件类型只能是jpg,bmp,gif,PNG..";
                            return Content(resultEntity.SerializeToJson());
                        }

                        //获取文件后缀名
                        string originFileName = Request.Files[i].FileName;
                        string originFileNameSuffix = string.Empty;
                        int lastIndex = originFileName.LastIndexOf(".");
                        if (lastIndex < 0) {
                            resultEntity.Code = ResponseCodeType.Fail;
                            resultEntity.Message = "文件类型只能是jpg,bmp,gif,PNG..";
                            return Content(resultEntity.SerializeToJson());
                        }
                        originFileNameSuffix = originFileName.Substring(lastIndex, originFileName.Length - lastIndex);

                        filename = GUID + originFileNameSuffix;
                        if (!Directory.Exists(Server.MapPath(path))) {
                            Directory.CreateDirectory(Server.MapPath(path));
                        }

                        Request.Files[i].SaveAs(Path.Combine(Server.MapPath(path), filename));
                        PictureViewModel model = new PictureViewModel() {
                            idmodule = idmodule,
                            path = path + filename,
                            sortno = 0
                        };

                        resultEntity.Content.Add(model);
                        i++;
                        StringBuilder initialPreview = new StringBuilder();
                        StringBuilder initialPreviewConfig = new StringBuilder();
                        initialPreviewConfig.Append(",\"initialPreviewConfig\":[");
                        initialPreview.Append("{\"initialPreview\":[");
                        for (int k = 0; k < resultEntity.Content.Count; k++) {
                            if (k == 0) {
                                initialPreview.AppendFormat("\"{0}\"", Url.Content(resultEntity.Content[k].path));
                                initialPreviewConfig.Append("{\"url\":\"" + Url.Action("DeleteImages", "Manage") + "\"}");
                            }
                            else {
                                initialPreview.AppendFormat("\",{0}\"", Url.Content(resultEntity.Content[k].path));
                                initialPreviewConfig.Append(",{\"url\":\"" + Url.Action("DeleteImages", "Manage") + "\"}");
                            }
                        }
                        initialPreview.Append("]");
                        initialPreviewConfig.Append("]");
                        initialPreview.Append(initialPreviewConfig.ToString());
                        initialPreview.Append("}");
                        return Content(initialPreview.ToString());
                    }

                }
                else {
                    resultEntity.Code = ResponseCodeType.Fail;
                    resultEntity.Message = "文件上传失败.";
                    return Content(resultEntity.SerializeToJson());
                }
            }
            catch (Exception ex) {
                log.Error(ex.ToString());
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "没有选择要上传的文件.";
                return Content(resultEntity.SerializeToJson());
            }
            return Content(resultEntity.SerializeToJson());


        }

        [ManageLoginValidation]
        public ActionResult DeleteImages(bool isAjax) {
            ResultModel<PictureViewModel> resultEntity = new ResultModel<PictureViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "文件删除成功";
            return Content(resultEntity.SerializeToJson());
        }

        public static bool DeleteImages(string idmodule = "", ModuleType moduleType = ModuleType.ALL) {
            bool result = false;
            try {
                TN db = new TN();
                db.Pictures.RemoveRange(db.Pictures.Where(en => en.idmodule == idmodule && en.moduletype == (int)moduleType));
                db.SaveChanges();
                result = true;
            }
            catch (Exception) {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="idmodule"></param>
        /// <param name="moduleType"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult AjaxImageList(string idmodule, ModuleType moduleType, bool isAjax) {
            ResultModel<PictureViewModel> resultEntity = new ResultModel<PictureViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                List<Picture> entities = PictureService.GetImagesByIdmodule(idmodule, moduleType);
                List<PictureViewModel> viewModels = entities.Select(model => {
                    PictureViewModel viewModel = new PictureViewModel();
                    viewModel.CopyFromBase(model);
                    viewModel.path = Url.Content(viewModel.path);
                    return viewModel;
                }).ToList();
                resultEntity.Content = viewModels;
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 判断上传文件类型
        /// </summary>
        private bool CheckFileType(HttpPostedFileWrapper postedFile) {

            bool result = true;
            /*  文件扩展名说明  
            jpg：255216  
            bmp：6677  
            gif：7173  
            PNG：13780
            pdf：3780  
            */
            int[] fileTypes = new int[] { 255216, 6677, 7173, 13780, 3780 };
            string fileTypeStr = "255216, 6677, 7173, 13780, 3780";
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            int fileLength = postedFile.ContentLength;
            if (fileLength <= 0) {
                return false;
            }
            byte[] imgArray = new byte[fileLength];

            postedFile.InputStream.Read(imgArray, 0, fileLength);

            System.IO.MemoryStream fs = new System.IO.MemoryStream();
            fs = new System.IO.MemoryStream(imgArray);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = string.Empty;
            byte buffer;
            try {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch (Exception) {
                result = false;
                //Log.Error(ex.ToString());
            }
            finally {
                r.Close();
                fs.Close();
                r.Dispose();
                fs.Dispose();
            }
            if (fileTypeStr.IndexOf(fileclass) < 0) {
                result = false;
            }

            return result;

        }
    }
}
